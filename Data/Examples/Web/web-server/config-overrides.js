const path = require('path');

module.exports = {
    devServer: function (configFunction) {
        return function (proxy, allowedHost) {
            const config = configFunction(proxy, allowedHost);
            config.headers = {
                "Cross-Origin-Embedder-Policy": "require-corp",
                "Cross-Origin-Opener-Policy": "same-origin"
            };
            return config;
        };
    },

    webpack: function (config) {
        // Ensure symlinked libraries are resolved
        config.resolve = {
            ...config.resolve,
            symlinks: false,
        };

        // Add babel-loader for the linked library
        config.module.rules.push({
            test: /\.(js|jsx|ts|tsx)$/,
            include: path.resolve(__dirname, '../web-library/dist'),
            use: {
                loader: 'babel-loader',
                options: {
                    presets: ['@babel/preset-env', '@babel/preset-react'],
                    plugins: ['@babel/plugin-transform-runtime'],
                },
            },
        });

        return config;
    }
};
