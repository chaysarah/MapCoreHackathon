'use client'

import * as React from 'react'
import { motion } from 'framer-motion'
import { Button } from '@/components/ui/button'
import Link from 'next/link'
import { ArrowRight, ExternalLink } from 'lucide-react'

export function CaseStudiesSection() {
  return (
    <section className="py-24 bg-slate-900/50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="text-center mb-16">
          <h2 className="text-3xl sm:text-4xl lg:text-5xl font-bold text-white mb-6">
            Featured Projects
          </h2>
          <p className="text-xl text-neutral-300 max-w-3xl mx-auto">
            See how we've helped organizations transform their location data into actionable insights.
          </p>
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-8 mb-12">
          {/* Placeholder case study cards */}
          <div className="bg-slate-800/50 rounded-2xl p-6 border border-slate-700 hover:border-slate-600 transition-colors">
            <div className="aspect-video bg-slate-700 rounded-xl mb-4" />
            <h3 className="text-xl font-bold text-white mb-2">Urban Planning Dashboard</h3>
            <p className="text-neutral-300 text-sm mb-4">Real-time city analytics platform</p>
            <Button variant="ghost" size="sm" className="text-sky-400 hover:text-sky-300">
              Learn more <ArrowRight className="h-4 w-4 ml-1" />
            </Button>
          </div>
        </div>

        <div className="text-center">
          <Button asChild size="lg" variant="outline" className="border-slate-600 text-white">
            <Link href="/case-studies">View All Projects</Link>
          </Button>
        </div>
      </div>
    </section>
  )
} 