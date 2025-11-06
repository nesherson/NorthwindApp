import { useSearchParams } from 'react-router';

import { Button } from '@/components/ui/button';
import { Form, Input } from '@/components/ui/form';
import { registerInputSchema, useRegister } from '@/lib/auth';

type RegisterFormProps = {
    onSuccess: () => void;
};

export const RegisterForm = ({
    onSuccess,
}: RegisterFormProps) => {
    const registering = useRegister({ onSuccess });
    const [searchParams] = useSearchParams();
    const redirectTo = searchParams.get('redirectTo');

    return (
        <div>
            <Form
                onSubmit={(values) => {
                    registering.mutate(values);
                }}
                schema={registerInputSchema}
                options={{
                    shouldUnregister: true,
                }}
            >
                {({ register, formState }) => (
                    <>
                        <Input
                            type="text"
                            label="First Name"
                            error={formState.errors['firstName']}
                            registration={register('firstName')}
                        />
                        <Input
                            type="text"
                            label="Last Name"
                            error={formState.errors['lastName']}
                            registration={register('lastName')}
                        />
                        <Input
                            type="email"
                            label="Email Address"
                            error={formState.errors['email']}
                            registration={register('email')}
                        />
                        <Input
                            type="password"
                            label="Password"
                            error={formState.errors['password']}
                            registration={register('password')}
                        />
                        <div>
                            <Button
                                isLoading={registering.isPending}
                                type="submit"
                                className="w-full"
                            >
                                Register
                            </Button>
                        </div>
                    </>
                )}
            </Form>
        </div>
    );
};
