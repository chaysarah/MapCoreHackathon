import { Metadata } from 'next'
import { Header } from '@/components/layout/header'
import { Footer } from '@/components/layout/footer'
import { TeamSection } from '@/components/sections/team-section'

export const metadata: Metadata = {
  title: 'Our Team | MapCore',
  description: 'Meet the 40+ engineers and specialists behind MapCore\'s cutting-edge geospatial solutions. Expert developers working on Windows development environment.',
  keywords: [
    'mapcore team',
    'geospatial engineers',
    'software developers',
    'windows development',
    'gis experts',
    'robotics engineers'
  ],
}

export default function TeamPage() {
  return (
    <div className="min-h-screen bg-slate-950">
      <Header />
      <main className="pt-20">
        <TeamSection />
      </main>
      <Footer />
    </div>
  )
} 