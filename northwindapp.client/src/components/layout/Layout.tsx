import Sidebar from "../sidebar/Sidebar";
import style from "./layout.module.css";

function Layout() {
    return (
        <div className={style.layout}>
            <div>Header</div>
            <Sidebar />
            <main>Content</main>
        </div>
    );

}

export default Layout;