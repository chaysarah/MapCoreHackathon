const fs = require('fs');
const crypto = require('crypto');
const { execSync } = require("child_process");
const path = require("path");

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
// This resolves to the parent of the scripts folder = project root

fs.writeFileSync(stampFile, currentHash);
