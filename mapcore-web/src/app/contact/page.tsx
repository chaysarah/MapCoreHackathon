import { Metadata } from 'next'
import { Header } from '@/components/layout/header'
import { Footer } from '@/components/layout/footer'

export const metadata: Metadata = {
  title: 'Contact | MapCore',
  description: 'Get in touch with MapCore to discuss your geospatial software development needs.',
}

export default function ContactPage() {
  return (
    <div className="min-h-screen bg-slate-950">
      <Header />
      <main className="pt-20">
        <section className="py-24">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="text-center">
              <h1 className="text-4xl sm:text-5xl font-bold text-white mb-6">
                Contact Us
              </h1>
              <p className="text-xl text-neutral-300 max-w-3xl mx-auto">
                Ready to start your next geospatial project? Let's discuss how we can help.
              </p>
            </div>
          </div>
        </section>
      </main>
      <Footer />
    </div>
  )
} 