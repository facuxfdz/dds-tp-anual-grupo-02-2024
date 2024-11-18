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
};

export default nextConfig;
