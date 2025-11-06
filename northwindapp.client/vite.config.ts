import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import tailwindcss from '@tailwindcss/vite'
import plugin from '@vitejs/plugin-react';
import mkcert from 'vite-plugin-mkcert'

export default defineConfig({
    plugins: [plugin(), tailwindcss(), mkcert()],
     server: {
        https: true,
        port: 3000
    },
     resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
})