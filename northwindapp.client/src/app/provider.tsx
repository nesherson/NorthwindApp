import { QueryClient, QueryClientProvider } from "@tanstack/react-query";
import { Suspense, useState } from "react";
import { ErrorBoundary } from "react-error-boundary";
import { ReactQueryDevtools } from "@tanstack/react-query-devtools";

import { queryConfig } from "@/lib/react-query";
import { AuthLoader } from "@/lib/auth";
import { MainErrorFallback } from "@/components/errors/main";

type AppProviderProps = {
	children: React.ReactNode;
};

export const AppProvider = ({ children }: AppProviderProps) => {
	const [queryClient] = useState(
		() =>
			new QueryClient({
				defaultOptions: queryConfig,
			}),
	);

	return (
		<Suspense
			fallback={
				<div className="">
					<p>Loading...</p>
				</div>
			}
		>
			<ErrorBoundary FallbackComponent={MainErrorFallback}>
				<QueryClientProvider client={queryClient}>
					{import.meta.env.DEV && <ReactQueryDevtools />}
					{/* <Notifications /> */}
					<AuthLoader
						renderLoading={() => (
							<div className="flex h-screen w-screen items-center justify-center">
								<p>Loading...</p>
							</div>
						)}
					>
						{children}
					</AuthLoader>
				</QueryClientProvider>
			</ErrorBoundary>
		</Suspense>
	);
};
