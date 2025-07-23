import { Metadata } from 'next'
import { Header } from '@/components/layout/header'
import { Footer } from '@/components/layout/footer'

export const metadata: Metadata = {
  title: 'Case Studies | MapCore',
  description: 'Explore real-world projects where MapCore has delivered cutting-edge geospatial solutions across our four specialized domains.',
}

export default function CaseStudiesPage() {
  return (
    <div className="min-h-screen bg-slate-950">
      <Header />
      <main className="pt-20">
        <section className="py-24">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="text-center">
              <h1 className="text-4xl sm:text-5xl font-bold text-white mb-6">
                Case Studies
              </h1>
              <p className="text-xl text-neutral-300 max-w-3xl mx-auto">
                Real-world applications of our mapping infrastructure, robotics, smart terrain, and AI solutions.
              </p>
            </div>
          </div>
        </section>
      </main>
      <Footer />
    </div>
  )
} 