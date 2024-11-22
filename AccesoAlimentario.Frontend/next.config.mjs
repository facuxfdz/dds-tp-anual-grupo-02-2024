/** @type {import('next').NextConfig} */
const nextConfig = {
    webpack: (config) => {
        config.resolve.alias.canvas = false;
        return config;
    },
    output: "standalone",
    images: {
        remotePatterns: [
        ],
    },
    async redirects() {
        return [
            {
                source: '/',
                destination: '/login',
                permanent: true,
            },
        ]
    },
};

export default nextConfig;
