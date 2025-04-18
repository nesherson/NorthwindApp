import { fileURLToPath, URL } from "node:url";

import { defineConfig, loadEnv } from "vite";
import plugin from "@vitejs/plugin-react";
import fs from "node:fs";
import path from "node:path";
import child_process from "node:child_process";

export default ({ mode }) => {
	process.env = { ...process.env, ...loadEnv(mode, process.cwd()) };

	const baseFolder =
		process.env.APPDATA !== undefined && process.env.APPDATA !== ""
			? `${process.env.APPDATA}/ASP.NET/https`
			: `${process.env.HOME}/.aspnet/https`;

	const certificateName = "northwindapp.client";
	const certFilePath = path.join(baseFolder, `${certificateName}.pem`);
	const keyFilePath = path.join(baseFolder, `${certificateName}.key`);

	if (!fs.existsSync(baseFolder)) {
		fs.mkdirSync(baseFolder, { recursive: true });
	}

	if (!fs.existsSync(certFilePath) || !fs.existsSync(keyFilePath)) {
		if (
			0 !==
			child_process.spawnSync(
				"dotnet",
				[
					"dev-certs",
					"https",
					"--export-path",
					certFilePath,
					"--format",
					"Pem",
					"--no-password",
				],
				{ stdio: "inherit" },
			).status
		) {
			throw new Error("Could not create certificate.");
		}
	}

	return defineConfig({
		plugins: [plugin()],
		resolve: {
			alias: {
				"@": fileURLToPath(new URL("./src", import.meta.url)),
			},
		},
		server: {
			port: 58543,
			https: {
				key: fs.readFileSync(keyFilePath),
				cert: fs.readFileSync(certFilePath),
			},
		},
	});
};
