import classes from "./sidebar.module.css";

interface Props {
	showSidebar: boolean;
}

const menuItems = [
	{
		menuCategoryName: "Dashboard",
		items: [
			{
				name: "Default",
				icon: null,
			},
			{
				name: "Analytics",
				icon: null,
			},
			{
				name: "Finance",
				icon: null,
			},
		],
	},
];

function Sidebar({ showSidebar }: Props) {
	return (
		<div className={`${classes.sidebar} ${!showSidebar ? classes["hide-sidebar"] : ""}`}>
			<div className={classes["logo-wrapper"]}>
				<img src="./logo.svg" alt="Northwind logo" />
			</div>
			<nav className={classes["sidebar-nav"]}>
				{menuItems.map((mi) => {
					return (
						<div key={mi.toString()}>
							<span className={classes["nav-category"]}>
								{mi.menuCategoryName}
							</span>
							<ul>
								{mi.items.map((i) => (
									<li key={i.toString()}>{i.name}</li>
								))}
							</ul>
						</div>
					);
				})}
			</nav>
		</div>
	);
}

export default Sidebar;
