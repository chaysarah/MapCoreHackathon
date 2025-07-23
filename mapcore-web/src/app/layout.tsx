import type { Metadata } from 'next'
import { Inter } from 'next/font/google'
import { ThemeProvider } from '@/components/theme-provider'
import { Toaster } from '@/components/ui/toaster'
import { Chatbot } from '@/components/chatbot/chatbot'
import '@/styles/globals.css'

const inter = Inter({
  subsets: ['latin'],
  variable: '--font-inter',
  display: 'swap',
})

export const metadata: Metadata = {
  title: {
    default: 'MapCore - Expert Geospatial Software Development',
    template: '%s | MapCore',
  },
  description: 'MapCore develops cutting-edge mapping infrastructure, autonomous robotics, and AI-powered solutions for defense and intelligence applications. Expert geospatial software development team.',
  keywords: [
    'geospatial software',
    'mapping solutions',
    'GIS development',
    'web mapping',
    'mobile mapping',
    'spatial analytics',
    'location intelligence',
    'map development',
  ],
  authors: [{ name: 'MapCore Team' }],
  creator: 'MapCore',
  openGraph: {
    type: 'website',
    locale: 'en_US',
    url: 'https://mapcore.dev',
    title: 'MapCore - Expert Geospatial Software Development',
    description: 'Mapping ideas to software excellence. We build complex, map-centric digital products.',
    siteName: 'MapCore',
    images: [
      {
        url: '/og-image.jpg',
        width: 1200,
        height: 630,
        alt: 'MapCore - Expert Geospatial Software Development',
      },
    ],
  },
  twitter: {
    card: 'summary_large_image',
    title: 'MapCore - Expert Geospatial Software Development',
    description: 'Mapping ideas to software excellence. We build complex, map-centric digital products.',
    images: ['/og-image.jpg'],
  },
  robots: {
    index: true,
    follow: true,
    googleBot: {
      index: true,
      follow: true,
      'max-video-preview': -1,
      'max-image-preview': 'large',
      'max-snippet': -1,
    },
  },
  verification: {
    google: 'your-google-verification-code',
  },
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en" className={inter.variable} suppressHydrationWarning>
      <head>
        <link rel="icon" href="/favicon.ico" />
        <link rel="apple-touch-icon" sizes="180x180" href="/apple-touch-icon.png" />
        <link rel="icon" type="image/png" sizes="32x32" href="/favicon-32x32.png" />
        <link rel="icon" type="image/png" sizes="16x16" href="/favicon-16x16.png" />
        <link rel="manifest" href="/site.webmanifest" />
      </head>
      <body className="min-h-screen bg-background font-sans antialiased">
        <ThemeProvider
          attribute="class"
          defaultTheme="dark"
          enableSystem
          disableTransitionOnChange
        >
          {children}
          <Toaster />
          <Chatbot />
        </ThemeProvider>
      </body>
    </html>
  )
} 