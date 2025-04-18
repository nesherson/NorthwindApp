import { configureAuth } from "react-query-auth";
import { Navigate, useLocation } from "react-router";
import { z } from "zod";

import { paths } from "@/config/paths";
import { api } from "./api-client";
import type { AuthResponse, User } from "@/types/api";

export type LoginInput = z.infer<typeof loginInputSchema>;

export const loginInputSchema = z.object({
	email: z.string().min(1, "Required").email("Invalid email"),
	password: z.string().min(5, "Required"),
});

export type RegisterInput = z.infer<typeof registerInputSchema>;

const getUser = async (): Promise<User> => {
	const response = await api.get("/auth/me");
	
	return response.data;
};

const logout = (): Promise<void> => {
	return api.post("/auth/logout");
};

const loginWithEmailAndPassword = (data: LoginInput): Promise<AuthResponse> => {
	return api.post("/auth/login", data);
};

const registerWithEmailAndPassword = (
	data: RegisterInput,
): Promise<AuthResponse> => {
	return api.post("/auth/register", data);
};

export const registerInputSchema = z
	.object({
		email: z.string().min(1, "Required"),
		firstName: z.string().min(1, "Required"),
		lastName: z.string().min(1, "Required"),
		password: z.string().min(5, "Required"),
	})
	.and(
		z
			.object({
				teamId: z.string().min(1, "Required"),
				teamName: z.null().default(null),
			})
			.or(
				z.object({
					teamName: z.string().min(1, "Required"),
					teamId: z.null().default(null),
				}),
			),
	);

const authConfig = {
	userFn: getUser,
	loginFn: async (data: LoginInput) => {
		const response = await loginWithEmailAndPassword(data);
		return response.user;
	},
	registerFn: async (data: RegisterInput) => {
		const response = await registerWithEmailAndPassword(data);
		return response.user;
	},
	logoutFn: logout,
};

export const { useUser, useLogin, useLogout, useRegister, AuthLoader } =
	configureAuth(authConfig);

export function ProtectedRoute({ children }: { children: React.ReactNode }) {
	console.log("Protected route");
	const user = useUser();
	const location = useLocation();

	if (!user.data) {
		return (
			<Navigate to={paths.auth.login.getHref(location.pathname)} replace />
		);
	}

	return children;
}
