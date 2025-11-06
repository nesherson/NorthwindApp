import * as React from 'react';
import { useNavigate, useSearchParams } from 'react-router';

import { Link } from '@/components/ui/link';
import { useUser } from '@/lib/auth';
import { paths } from '@/config/paths';

type LayoutProps = {
    children: React.ReactNode;
    title: string;
    isLogin?: boolean;
};

export const AuthLayout = ({ children, title, isLogin = false }: LayoutProps) => {
    const navigate = useNavigate();

    const user = useUser();
    const [searchParams] = useSearchParams();
    const redirectTo = searchParams.get('redirectTo');


    React.useEffect(() => {
        if (user.data) {
            navigate(redirectTo ? redirectTo : paths.app.dashboard.getHref(), {
                replace: true,
            });
        }
    }, [user.data, navigate, redirectTo]);

    return (
        <div className='min-h-screen'>
            <div className="absolute -z-1 blur-custom">
                <div className='w-75 h-75 bg-orange-200 rounded-full absolute top-0 left-5'>
                </div>
                <div className='w-50 h-50 bg-green-200 rounded-full absolute bottom-50 left-25'>
                </div>
                <div className='w-50 h-50 bg-red-400 rounded-full absolute bottom-0 -left-25'>
                </div>
            </div>
            <div className='min-h-screen flex justify-center items-center'>
                <div className='w-120'>
                    <div className="sm:max-w-md">
                        <div className="bg-white border-primary test px-4 py-8 sm:rounded-lg sm:px-10">
                            <div className='flex flex-row justify-between items-center mb-6'>
                                <h2 className="mt-3 text-center font-semibold text-2xl text-gray-900">
                                    {title}
                                </h2>
                                <Link className='text-sm mt-3 hover:none' to={isLogin ? '/register' : '/login'}>
                                    {isLogin ? 'Don\'t have an account?' : 'Already have an account?'}
                                </Link>
                            </div>
                            {children}
                        </div>
                    </div>
                </div>
            </div>
        </div>
    );
};
