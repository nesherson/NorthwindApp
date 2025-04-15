import { Link } from '../../components/ui/link';
import { paths } from '../../config/paths';

const NotFoundRoute = () => {
    return (
        <div className="">
            <h1>404 - Not Found</h1>
            <p>Sorry, the page you are looking for does not exist.</p>
            <Link to={paths.home.getHref()} replace>
                Go to Home
            </Link>
        </div>
    );
};

export default NotFoundRoute;
