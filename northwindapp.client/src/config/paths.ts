export const paths = {
    home: {
        path: '/',
        getHref: () => '/',
    },

    auth: {
        register: {
            path: '/register',
            getHref: (redirectTo?: string | null | undefined) =>
                `/register${redirectTo ? `?redirectTo=${encodeURIComponent(redirectTo)}` : ''}`,
        },
        login: {
            path: '/login',
            getHref: (redirectTo?: string | null | undefined) =>
                `/login${redirectTo ? `?redirectTo=${encodeURIComponent(redirectTo)}` : ''}`,
        },
    },

    app: {
        root: {
            path: '/app',
            getHref: () => '/app',
        },
        dashboard: {
            path: '',
            getHref: () => '/app',
        },
        profile: {
            path: 'profile',
            getHref: () => '/app/profile',
        },
    },
} as const;
