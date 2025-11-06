import { useNavigate, useSearchParams } from 'react-router';

import { AuthLayout } from '@/components/layouts/AuthLayout';
import { paths } from '@/config/paths';
import { RegisterForm } from '@/features/auth/components/RegisterForm';

const RegisterRoute = () => {
    const navigate = useNavigate();
    const [searchParams] = useSearchParams();
    const redirectTo = searchParams.get('redirectTo');

    return (
        <AuthLayout title="Register">
            <RegisterForm
                onSuccess={() => {
                    navigate(
                        `${redirectTo ? `${redirectTo}` : paths.auth.login.getHref()}`,
                        {
                            replace: true,
                        },
                    );
                }}
            />
        </AuthLayout>
    );
};

export default RegisterRoute;
