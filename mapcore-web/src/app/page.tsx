import { Metadata } from 'next'
import { HeroSection } from '@/components/sections/hero-section'
import { ServicesSection } from '@/components/sections/services-section'
import { TeamSection } from '@/components/sections/team-section'
import { Header } from '@/components/layout/header'
import { Footer } from '@/components/layout/footer'

export const metadata: Metadata = {
  title: 'MapCore - Advanced Geospatial Technologies to Mission Excellence',
  description: 'We develop cutting-edge mapping infrastructure, autonomous robotics, and AI-powered solutions for defense and intelligence applications.',
}

export default function HomePage() {
  return (
    <div className="min-h-screen bg-slate-950">
      <Header />
      <main>
        <HeroSection />
        <ServicesSection />
        <TeamSection />
      </main>
      <Footer />
    </div>
  )
} 