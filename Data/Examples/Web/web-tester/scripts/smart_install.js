const fs = require('fs');
const crypto = require('crypto');
const { execSync } = require("child_process");

const pkgPath = 'package.json';
const stampFile = 'node_modules/.packagehash';

const currentHash = crypto
  .createHash('sha256')
  .update(fs.readFileSync(pkgPath))
  .digest('hex');

if (fs.existsSync(stampFile)) {
  const previousHash = fs.readFileSync(stampFile, 'utf-8');
  if (currentHash === previousHash) {
    console.log("package.json unchanged — skipping npm install.");
    process.exit(0);
  }
}

console.log("package.json changed — running npm install...");
execSync('npm install', { stdio: 'inherit' });

// Link the library into this project
execSync("npm link mapcore-lib", { stdio: "inherit"});

fs.writeFileSync(stampFile, currentHash);
