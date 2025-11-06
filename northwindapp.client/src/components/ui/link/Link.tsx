import { Link as RouterLink, LinkProps } from 'react-router';

import { cn } from '@/utils/cn';

export const Link = ({ className, children, ...props }: LinkProps) => {
    return (
        <RouterLink
            className={cn('text-blue-500', className)}
            {...props}
        >
            {children}
        </RouterLink>
    );
};
