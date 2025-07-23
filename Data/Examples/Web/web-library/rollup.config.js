import external from 'rollup-plugin-peer-deps-external';
import typescript from 'rollup-plugin-typescript2';
import del from 'rollup-plugin-delete';
import pkg from './package.json';

export default {
  input: pkg.source, // e.g., "src/index.ts"
  output: {
    dir: 'dist',
    format: 'esm',
    preserveModules: true,
    preserveModulesRoot: 'src',
    sourcemap: true,
  },
  plugins: [
    external(),
    del({ targets: 'dist/*' }),
    typescript({
      tsconfigOverride: {
        compilerOptions: {
          declaration: true,
          declarationDir: 'dist',
        },
      },
      useTsconfigDeclarationDir: true,
    }),
  ],
  external: Object.keys(pkg.peerDependencies || {}),
};
