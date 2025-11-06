import {
	type MutationFunction,
	type QueryFunction,
	type QueryKey,
	type UseMutationOptions,
	type UseQueryOptions,
	useMutation,
	useQuery,
	useQueryClient,
} from '@tanstack/react-query'
import React, { JSX } from 'react'

export interface ReactQueryAuthConfig<LoginUserResponse, RegisterUserResponse, AuthUserResponse, LoginCredentials, RegisterCredentials> {
	userFn: QueryFunction<AuthUserResponse, QueryKey>
	loginFn: MutationFunction<LoginUserResponse, LoginCredentials>
	registerFn: MutationFunction<RegisterUserResponse, RegisterCredentials>
	logoutFn: MutationFunction<unknown, unknown>
	userKey?: QueryKey
}

export interface AuthProviderProps {
	children: React.ReactNode
}

export function configureAuth<LoginUserResponse, RegisterUserResponse, AuthUserResponse, Error, LoginCredentials, RegisterCredentials>(
	config: ReactQueryAuthConfig<LoginUserResponse, RegisterUserResponse, AuthUserResponse, LoginCredentials, RegisterCredentials>
) {
	const { userFn, userKey = ['authenticated-user'], loginFn, registerFn, logoutFn } = config

	const useUser = (options?: Omit<UseQueryOptions<AuthUserResponse, Error, AuthUserResponse, QueryKey>, 'queryKey' | 'queryFn'>) =>
		useQuery({
			queryKey: userKey,
			queryFn: userFn,
			...options,
		})

	const useLogin = (options?: Omit<UseMutationOptions<LoginUserResponse, Error, LoginCredentials>, 'mutationFn'>) => {
		const queryClient = useQueryClient()

		const setUser = React.useCallback((data: LoginUserResponse) => queryClient.setQueryData(userKey, data), [queryClient])

		return useMutation({
			mutationFn: loginFn,
			...options,
			onSuccess: (user, ...rest) => {
				setUser(user)
				options?.onSuccess?.(user, ...rest)
			},
		})
	}

	const useRegister = (options?: Omit<UseMutationOptions<RegisterUserResponse, Error, RegisterCredentials>, 'mutationFn'>) => {
		const queryClient = useQueryClient()

		const setUser = React.useCallback((data: RegisterUserResponse) => queryClient.setQueryData(userKey, data), [queryClient])

		return useMutation({
			mutationFn: registerFn,
			...options,
			onSuccess: (user, ...rest) => {
				setUser(user)
				options?.onSuccess?.(user, ...rest)
			},
		})
	}

	const useLogout = (options?: UseMutationOptions<unknown, Error, unknown>) => {
		const queryClient = useQueryClient()

		const setUser = React.useCallback((data: AuthUserResponse | null) => queryClient.setQueryData(userKey, data), [queryClient])

		return useMutation({
			mutationFn: logoutFn,
			...options,
			onSuccess: (...args) => {
				setUser(null)
				options?.onSuccess?.(...args)
			},
		})
	}

	function AuthLoader({
		children,
		renderLoading,
		renderUnauthenticated,
		renderError = (error: Error) => <>{JSON.stringify(error)}</>,
	}: {
		children: React.ReactNode
		renderLoading: () => JSX.Element
		renderUnauthenticated?: () => JSX.Element
		renderError?: (error: Error) => JSX.Element
	}) {
		const { isSuccess, isFetched, status, data, error } = useUser()

		if (isSuccess) {
			if (renderUnauthenticated && !data) {
				return renderUnauthenticated()
			}
			return <>{children}</>
		}

		if (!isFetched) {
			return renderLoading()
		}

		if (status === 'error') {
			return renderError(error)
		}

		return null
	}

	return {
		useUser,
		useLogin,
		useRegister,
		useLogout,
		AuthLoader,
	}
}
