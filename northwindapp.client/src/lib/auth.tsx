import { configureAuth } from './react-query-auth';
import { Navigate, useLocation } from 'react-router';
import { z } from 'zod';

import { paths } from '@/config/paths';
import { AuthUserResponse } from '@/types/api';

import { api } from './api-client';

const getUser = async (): Promise<AuthUserResponse | null> => {
    try {
        const response = await api.get<AuthUserResponse>('/auth/me');

        return response;
    } catch (error) {
        return null;
    }
};

const logout = (): Promise<void> => {
    return api.post('/auth/logout');
};

export const loginInputSchema = z.object({
    email: z.string().min(1, 'Required').email('Invalid email'),
    password: z.string().min(5, 'Required'),
});

export type LoginInput = z.infer<typeof loginInputSchema>;
const loginWithEmailAndPassword = (data: LoginInput): Promise<AuthUserResponse> => {
    return api.post('/auth/login', data);
};

export const registerInputSchema = z
    .object({
        email: z.string().min(1, 'Required'),
        firstName: z.string().min(1, 'Required'),
        lastName: z.string().min(1, 'Required'),
        password: z.string().min(5, 'Required'),
    });

export type RegisterInput = z.infer<typeof registerInputSchema>;

const registerWithEmailAndPassword = (
    data: RegisterInput,
): Promise<AuthUserResponse> => {
    return api.post('/auth/register', data);
};

const authConfig = {
    userFn: getUser,
    loginFn: async (data: LoginInput) => {
        const response = await loginWithEmailAndPassword(data);

        return response;
    },
    registerFn: async (data: RegisterInput) => {
        const response = await registerWithEmailAndPassword(data);

        return response;
    },
    logoutFn: logout,
};

export const { useUser, useLogin, useLogout, useRegister, AuthLoader } =
    configureAuth(authConfig);

export const ProtectedRoute = ({ children }: { children: React.ReactNode }) => {
    const user = useUser();
    const location = useLocation();

    if (!user.data) {
        return (
            <Navigate to={paths.auth.login.getHref(location.pathname)} replace />
        );
    }

    return children;
};
