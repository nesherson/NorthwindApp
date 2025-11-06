import { ContentLayout } from '@/components/layouts';
import { useUser } from '@/lib/auth';

const DashboardRoute = () => {
  const { data: userData } = useUser();

  return (
    <ContentLayout title="Dashboard">
      <h1 className="text-xl">
        Welcome <b>{`${userData?.firstName} ${userData?.lastName}`}</b>
      </h1>
    </ContentLayout>
  );
};

export default DashboardRoute;
