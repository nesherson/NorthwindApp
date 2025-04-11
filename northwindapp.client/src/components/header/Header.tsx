import classes from "./header.module.css";

interface Props {
	showSidebar: boolean;
	handleShowSidebar: () => void;
}

function Header({ showSidebar, handleShowSidebar }: Props) {
	console.log(showSidebar);
	return (
		<header
			className={`${classes.header} ${!showSidebar ? classes["hide-sidebar"] : ""}`}
		>
			<div className={classes["hamburger-btn"]} onClick={handleShowSidebar}>
				<img src="./assets/header/menu.svg" alt="Hamburger menu icon" />
			</div>
		</header>
	);
}

export default Header;
