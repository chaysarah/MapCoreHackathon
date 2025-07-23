const path = require('path');
const TerserPlugin = require('terser-webpack-plugin');
const fs = require('fs');
const webpack = require('webpack');

module.exports = {
    devServer: function (configFunction) {
        return function (proxy, allowedHost) {
            const config = configFunction(proxy, allowedHost);
            config.headers = {
                "Cross-Origin-Embedder-Policy": "require-corp",
                "Cross-Origin-Opener-Policy": "same-origin"
            };
            config.hot = true;
            // Replace deprecated middleware options
            config.setupMiddlewares = (middlewares, devServer) => {
                if (!devServer) {
                    throw new Error('webpack-dev-server is not defined');
                }

                // Add your custom middlewares here
                // Example: middlewares.push(yourMiddleware);

                return middlewares;
            };

            // Configure watch options
            config.watchFiles = {
                paths: [path.resolve(__dirname, '../web-library/dist/**/*')],
                options: {
                    usePolling: true,
                    interval: 1000,
                    // ignored: /node_modules/, // Optional: ignore unrelated folders
                },
            };
            return config;
        };
    },

    webpack: function (config, env) {
        // Add symlink resolution handling
        config.resolve = {
            ...config.resolve,
            symlinks: false, // required for npm/yarn link to work properly with hot reload
        };

        // Add babel-loader rule for the linked library
        config.module.rules.push({
            test: /\.(js|jsx|ts|tsx)$/,
            include: fs.realpathSync(path.resolve(__dirname, '../web-library/dist')),
            use: {
                loader: 'babel-loader',
                options: {
                    presets: ['@babel/preset-env', '@babel/preset-react'],
                    plugins: ['@babel/plugin-transform-runtime'],
                },
            },
        });

        config.snapshot = {
            managedPaths: [],
            immutablePaths: [],
        };

        config.plugins = [
            ...config.plugins,
            new webpack.HotModuleReplacementPlugin(),
        ];

        // Production-specific optimization
        if (env === 'production') {
            config.optimization = {
                ...config.optimization,
                minimize: true,
                minimizer: [
                    new TerserPlugin({
                        terserOptions: {
                            keep_classnames: true,
                            keep_fnames: true,
                        },
                    }),
                ],
            };
        }

        return config;
    },
};
