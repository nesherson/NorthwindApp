import Sidebar from "../sidebar/Sidebar";
import Header from "../header/Header";

import style from "./layout.module.css";
import { useState } from "react";

function Layout() {
    const [showSidebar, setShowSidebar] = useState(true);

    const handleShowSideBar = () => {
        setShowSidebar(prev => !prev);
    }

    return (
        <div className={style.layout}>
            <Header showSidebar={showSidebar} handleShowSidebar={handleShowSideBar} />
            <Sidebar showSidebar={showSidebar} />
            {/* <main>Content</main> */}
        </div>
    );

}

export default Layout;