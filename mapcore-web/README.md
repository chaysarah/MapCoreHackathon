# MapCore Website

A production-ready, professional website for **MapCore** - a senior software development team specializing in geospatial solutions.

## 🌍 Overview

MapCore's website showcases our expertise in designing and building complex, map-centric digital products. Built with modern web technologies and optimized for performance, accessibility, and user experience.

**Live Demo:** [https://mapcore.dev](https://mapcore.dev)

## ✨ Features

- **🚀 Modern Stack**: Next.js 14, TypeScript, Tailwind CSS
- **🗺️ Interactive Maps**: Mapbox GL JS integration with dynamic backgrounds
- **🎨 Beautiful UI**: Dark-mode first design with subtle animations
- **📱 Responsive**: Fully responsive design (min-width 320px)
- **♿ Accessible**: 100% accessible with keyboard navigation
- **⚡ Performance**: Lighthouse score ≥ 95
- **🧪 Well Tested**: Unit tests with Vitest + E2E tests with Playwright
- **🐳 Docker Ready**: Containerized for easy deployment
- **📝 Content Management**: MDX for blog posts and case studies

## 🛠 Tech Stack

### Core Framework
- **Next.js 14** - React framework with App Router
- **TypeScript** - Type-safe development
- **Tailwind CSS** - Utility-first CSS framework

### UI & Styling
- **shadcn/ui** - Reusable component library
- **Framer Motion** - Smooth animations and transitions
- **Lucide React** - Beautiful icon set
- **Inter Font** - Modern typography

### Maps & Visualization
- **Mapbox GL JS** - Interactive maps and geospatial visualization
- **Dynamic imports** - Optimized map loading

### Forms & Validation
- **React Hook Form** - Performant form handling
- **Zod** - Type-safe validation schemas
- **SendGrid** - Email delivery service

### Content & Data
- **MDX** - Markdown with React components
- **Gray Matter** - Front matter parser
- **GitHub API** - Dynamic repository data

### Testing & Quality
- **Vitest** - Unit testing framework
- **React Testing Library** - Component testing utilities
- **Playwright** - End-to-end testing
- **ESLint** - Code linting
- **axe-core** - Accessibility testing

### Deployment & DevOps
- **Vercel** - Hosting and deployment
- **Docker** - Containerization
- **GitHub Actions** - CI/CD pipeline
- **Husky** - Git hooks

## 🚀 Getting Started

### Prerequisites

- **Node.js** 18+ 
- **npm** or **yarn**
- **Git**

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/mapcore/website.git
   cd mapcore-website
   ```

2. **Install dependencies**
   ```bash
   npm install
   ```

3. **Set up environment variables**
   ```bash
   cp .env.example .env.local
   ```
   
   Edit `.env.local` with your configuration:
   ```env
   NEXT_PUBLIC_MAPBOX_ACCESS_TOKEN=your_mapbox_token
   SENDGRID_API_KEY=your_sendgrid_key
   SENDGRID_FROM_EMAIL=hello@mapcore.dev
   GITHUB_TOKEN=your_github_token
   ```

4. **Run the development server**
   ```bash
   npm run dev
   ```

5. **Open in browser**
   Visit [http://localhost:3000](http://localhost:3000)

### Docker Development

```bash
# Build and run with Docker Compose
docker-compose up --build

# Or run individual commands
docker build -t mapcore-website .
docker run -p 3000:3000 mapcore-website
```

## 📁 Project Structure

```
mapcore-website/
├── src/
│   ├── app/                    # Next.js 14 App Router
│   │   ├── layout.tsx         # Root layout
│   │   ├── page.tsx           # Homepage
│   │   ├── case-studies/      # Case studies pages
│   │   ├── blog/              # Blog pages
│   │   ├── team/              # Team page
│   │   ├── contact/           # Contact page
│   │   └── api/               # API routes
│   ├── components/            # React components
│   │   ├── ui/                # shadcn/ui components
│   │   ├── layout/            # Layout components
│   │   ├── sections/          # Page sections
│   │   └── map/               # Map components
│   ├── lib/                   # Utilities and helpers
│   ├── hooks/                 # Custom React hooks
│   ├── styles/                # Global styles
│   └── types/                 # TypeScript type definitions
├── content/                   # MDX content files
│   ├── blog/                  # Blog posts
│   └── case-studies/          # Case study content
├── public/                    # Static assets
├── tests/                     # Test files
│   ├── e2e/                   # Playwright E2E tests
│   └── __tests__/             # Unit tests
└── docs/                      # Documentation
```

## 🧪 Testing

### Unit Tests
```bash
# Run unit tests
npm run test

# Run with coverage
npm run test:coverage

# Run with UI
npm run test:ui
```

### End-to-End Tests
```bash
# Run E2E tests
npm run test:e2e

# Run with UI
npm run test:e2e:ui
```

### Accessibility Testing
```bash
# Run accessibility tests
npm run test:a11y
```

## 🚀 Deployment

### Vercel (Recommended)
1. Connect your GitHub repository to Vercel
2. Configure environment variables
3. Deploy automatically on push to main

### Manual Deployment
```bash
# Build for production
npm run build

# Start production server
npm start
```

### Docker Deployment
```bash
# Build production image
docker build -t mapcore-website .

# Run in production
docker run -p 3000:3000 -e NODE_ENV=production mapcore-website
```

## 🎨 Design System

### Colors
- **Primary**: Indigo 500 (`#6366f1`)
- **Secondary**: Sky 400 (`#38bdf8`)
- **Background**: Slate 950 (`#020617`)
- **Text**: Neutral variants

### Typography
- **Font**: Inter (Google Fonts)
- **Headings**: 3xl–6xl with bold weight
- **Body**: Base size with comfortable line height

### Spacing
- **Border Radius**: 2xl for cards and components
- **Shadows**: Subtle lg/20 for depth
- **Padding**: Consistent 4, 6, 8 scale

## 📝 Content Management

### Adding Blog Posts
1. Create a new MDX file in `content/blog/`
2. Add front matter with metadata
3. Write content using MDX syntax

### Adding Case Studies
1. Create a new MDX file in `content/case-studies/`
2. Include project details and metrics
3. Add screenshots and technical details

### Example Front Matter
```yaml
---
title: "Building Real-time Geospatial Analytics"
date: "2024-01-15"
author: "MapCore Team"
excerpt: "How we built a scalable real-time analytics platform"
tags: ["analytics", "real-time", "mapbox"]
image: "/images/case-studies/analytics-dashboard.jpg"
---
```

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guide](CONTRIBUTING.md) for details.

### Development Workflow
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Add tests if needed
5. Ensure all tests pass
6. Submit a pull request

### Code Standards
- **TypeScript**: Strict mode enabled
- **ESLint**: Configured for Next.js and accessibility
- **Prettier**: Code formatting
- **Conventional Commits**: Commit message format

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🙋‍♀️ Support

- **Documentation**: Check our [docs](./docs/)
- **Issues**: [GitHub Issues](https://github.com/mapcore/website/issues)
- **Email**: [hello@mapcore.dev](mailto:hello@mapcore.dev)

## 🏆 Performance

- **Lighthouse Score**: 95+ (Performance, Accessibility, Best Practices, SEO)
- **Core Web Vitals**: Optimized for LCP, FID, and CLS
- **Bundle Size**: Optimized with code splitting and dynamic imports
- **Image Optimization**: Next.js Image component with WebP/AVIF support

---

Built with ❤️ by the MapCore team 

## 🔧 Solution: Add Azure Configuration Files

You need to create several configuration files for Azure App Service to properly deploy your Next.js app.

### 1. Create `web.config` (Required for Azure)

Create this file in your project root:

```xml:web.config
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <system.webServer>
    <handlers>
      <add name="iisnode" path="server.js" verb="*" modules="iisnode"/>
    </handlers>
    <rewrite>
      <rules>
        <rule name="NodeInspector" patternSyntax="ECMAScript" stopProcessing="true">
          <match url="^server.js\/debug[\/]?" />
        </rule>
        <rule name="StaticContent">
          <action type="Rewrite" url="public{REQUEST_URI}"/>
        </rule>
        <rule name="DynamicContent">
          <conditions>
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="True"/>
          </conditions>
          <action type="Rewrite" url="server.js"/>
        </rule>
      </rules>
    </rewrite>
    <security>
      <requestFiltering>
        <hiddenSegments>
          <remove segment="bin"/>
        </hiddenSegments>
      </requestFiltering>
    </security>
    <httpErrors existingResponse="PassThrough" />
    <iisnode watchedFiles="web.config;*.js"/>
  </system.webServer>
</configuration>
```

### 2. Update `next.config.js`

Modify your Next.js config to be Azure-compatible:

```javascript:next.config.js
/** @type {import('next').NextConfig} */
const nextConfig = {
  // Add output configuration for Azure
  output: 'standalone',
  
  experimental: {
    mdxRs: true,
  },
  pageExtensions: ['js', 'jsx', 'ts', 'tsx', 'md', 'mdx'],
  images: {
    formats: ['image/avif', 'image/webp'],
    remotePatterns: [
      {
        protocol: 'https',
        hostname: 'avatars.githubusercontent.com',
      },
      {
        protocol: 'https',
        hostname: 'github.com',
      },
    ],
  },
  async headers() {
    return [
      {
        source: '/(.*)',
        headers: [
          {
            key: 'X-Frame-Options',
            value: 'DENY',
          },
          {
            key: 'X-Content-Type-Options',
            value: 'nosniff',
          },
          {
            key: 'Referrer-Policy',
            value: 'strict-origin-when-cross-origin',
          },
        ],
      },
    ];
  },
  // Enable compression for better performance
  compress: true,
  // Optimize bundles
  swcMinify: true,
  // Enable experimental features for better performance
  experimental: {
    optimizeCss: true,
    scrollRestoration: true,
  },
};

module.exports = nextConfig;
```

### 3. Create `server.js` (Azure Entry Point)

Create this file in your project root:

```javascript:server.js
const { createServer } = require('http')
const { parse } = require('url')
const next = require('next')

const dev = process.env.NODE_ENV !== 'production'
const hostname = 'localhost'
const port = process.env.PORT || 3000

const app = next({ dev, hostname, port })
const handle = app.getRequestHandler()

app.prepare().then(() => {
  createServer(async (req, res) => {
    try {
      const parsedUrl = parse(req.url, true)
      await handle(req, res, parsedUrl)
    } catch (err) {
      console.error('Error occurred handling', req.url, err)
      res.statusCode = 500
      res.end('internal server error')
    }
  })
  .once('error', (err) => {
    console.error(err)
    process.exit(1)
  })
  .listen(port, () => {
    console.log(`> Ready on http://${hostname}:${port}`)
  })
})
```

### 4. Update `package.json` Scripts

Add Azure-specific scripts to your `package.json`:

```json:package.json
{
  "scripts": {
    "dev": "next dev",
    "build": "next build",
    "start": "next start",
    "start:azure": "node server.js",
    "lint": "next lint",
    "lint:fix": "next lint --fix",
    "type-check": "tsc --noEmit",
    "test": "vitest",
    "test:ui": "vitest --ui",
    "test:coverage": "vitest --coverage",
    "test:e2e": "playwright test",
    "test:e2e:ui": "playwright test --ui",
    "prepare": "husky install"
  }
}
```

## 🚀 Azure App Service Configuration

### In Azure Portal:

1. **Set Node.js Version**:
   - Go to Configuration → General Settings
   - Set Node.js version to `18-lts` or `20-lts`

2. **Set Startup Command**:
   - Go to Configuration → General Settings
   - Set Startup Command to: `npm run start:azure`

3. **Add Application Settings**:
   ```
   NODE_ENV = production
   WEBSITE_NODE_DEFAULT_VERSION = 18-lts
   SCM_DO_BUILD_DURING_DEPLOYMENT = true
   ```

4. **Environment Variables** (if needed):
   ```
   NEXT_PUBLIC_MAPBOX_ACCESS_TOKEN = your_mapbox_token
   SENDGRID_API_KEY = your_sendgrid_key
   SENDGRID_FROM_EMAIL = hello@mapcore.dev
   GITHUB_TOKEN = your_github_token
   ```

## 📦 Deployment Steps

1. **Build locally first** (to test):
   ```bash
   npm run build
   ```

2. **Create deployment package**:
   - Zip your entire project folder (including node_modules if you want faster deployment)
   - Or let Azure handle npm install during deployment

3. **Deploy to Azure**:
   - Use Azure CLI, VS Code extension, or Azure Portal zip deploy
   - Make sure the zip includes all the new config files

## 🔍 Alternative: App Service on Linux

If you continue having issues, consider using **App Service on Linux** instead of Windows, which has better Next.js support:

1. Create Linux App Service
2. Set runtime stack to `Node 18 LTS`
3. Use these settings:
   ```
   Startup Command: npm start
   Node.js version: 18-lts
   ```

This should resolve your deployment issues! The key changes are adding the `output: 'standalone'` configuration and proper Azure startup files. 

## 🐳 Build Docker Image Locally

```bash
# From your project directory (c:\git\mapcore-web)
docker build -t mapcore-web:local .
```

## 🚀 Run Docker Container Locally

```bash
# Run the container on port 3000
docker run -p 3000:3000 mapcore-web:local
```

## 🌐 Test in Browser

Open your browser and go to:
- **http://localhost:3000**

## 🔧 Run with Environment Variables

If your app needs environment variables:

```bash
# Run with environment variables
docker run -p 3000:3000 \
  -e NODE_ENV=production \
  -e NEXT_PUBLIC_MAPBOX_ACCESS_TOKEN="your_token_here" \
  -e SENDGRID_API_KEY="your_key_here" \
  mapcore-web:local
```

## 📋 Useful Docker Commands

### Stop the Container
```bash
# Find running containers
docker ps

# Stop by container ID or name
docker stop <container_id>

# Or stop all running containers
docker stop $(docker ps -q)
```

### Run in Background (Detached Mode)
```bash
# Run in background
docker run -d -p 3000:3000 --name mapcore-app mapcore-web:local

# Check logs
docker logs mapcore-app

# Stop background container
docker stop mapcore-app
docker rm mapcore-app
```

### Interactive Shell (for debugging)
```bash
# Run with shell access
docker run -it -p 3000:3000 mapcore-web:local sh

# Or connect to running container
docker exec -it <container_name> sh
```

## 🔍 Debug and Test Commands

### Check if the app is responding
```bash
# Test the endpoint
curl http://localhost:3000

# Or use PowerShell on Windows
Invoke-WebRequest http://localhost:3000
```

### View container logs in real-time
```bash
# Follow logs
docker logs -f <container_name>
```

### Check container resource usage
```bash
# Monitor container stats
docker stats
```

## 🔄 Complete Test Workflow

Here's a complete workflow to test your Docker image:

```bash
# 1. Build the image
docker build -t mapcore-web:test .

# 2. Run the container
docker run -d -p 3000:3000 --name mapcore-test mapcore-web:test

# 3. Check if it's running
docker ps

# 4. View logs
docker logs mapcore-test

# 5. Test in browser: http://localhost:3000

# 6. When done, clean up
docker stop mapcore-test
docker rm mapcore-test
```

## 🛠️ Troubleshooting

### If port 3000 is busy:
```bash
# Use a different port
docker run -p 8080:3000 mapcore-web:local
# Then visit: http://localhost:8080
```

### If build fails:
```bash
# Build with detailed output
docker build --progress=plain -t mapcore-web:debug .

# Build without cache
docker build --no-cache -t mapcore-web:fresh .
```

### Check what's inside the container:
```bash
# Run with shell to inspect
docker run -it mapcore-web:local sh

# Inside container, check:
ls -la
cat package.json
node --version
```

## 📊 Quick Health Check

Create a simple script to test your container:

```bash
# test-docker.sh
#!/bin/bash
echo "🐳 Building Docker image..."
docker build -t mapcore-web:test .

echo "🚀 Starting container..."
docker run -d -p 3000:3000 --name mapcore-test mapcore-web:test

echo "⏳ Waiting for container to start..."
sleep 10

echo "🔍 Testing endpoint..."
curl -f http://localhost:3000 || echo "❌ Failed to connect"

echo "📋 Container logs:"
docker logs mapcore-test

echo "🧹 Cleaning up..."
docker stop mapcore-test
docker rm mapcore-test
```

## 🎯 Expected Output

When everything works correctly, you should see:
1. **Docker build** completes without errors
2. **Container starts** successfully
3. **Browser shows** your Next.js app at http://localhost:3000
4. **No errors** in docker logs

Once you confirm it works locally, you can confidently deploy to Azure! 🚀 