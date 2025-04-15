import { Link as RouterLink, LinkProps } from 'react-router';

export const Link = ({ className, children, ...props }: LinkProps) => {
    return (
        <RouterLink
            className=""
            {...props}
        >
            {children}
        </RouterLink>
    );
};
