'use client'

import * as React from 'react'
import Link from 'next/link'
import { motion } from 'framer-motion'
import { Button } from '@/components/ui/button'
import { Header } from '@/components/layout/header'
import { Footer } from '@/components/layout/footer'
import { Map, Bot, Cloud, Brain, ArrowRight, Check } from 'lucide-react'

const iconMap = {
  Map,
  Bot,
  Cloud,
  Brain,
}

type DomainData = {
  title: string
  description: string
  longDescription: string
  capabilities: string[]
  icon: keyof typeof iconMap
  color: string
}

interface DomainPageProps {
  domain: DomainData
}

const fadeInUp = {
  initial: { opacity: 0, y: 60 },
  animate: { opacity: 1, y: 0 },
  transition: { duration: 0.6, ease: 'easeOut' }
}

const staggerContainer = {
  initial: {},
  animate: {
    transition: {
      staggerChildren: 0.1
    }
  }
}

export function DomainPage({ domain }: DomainPageProps) {
  const IconComponent = iconMap[domain.icon]

  return (
    <div className="min-h-screen bg-slate-950">
      <Header />
      
      <main className="pt-20">
        {/* Hero Section */}
        <section className="py-24 bg-slate-950">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <motion.div
              variants={staggerContainer}
              initial="initial"
              animate="animate"
              className="text-center"
            >
              {/* Icon */}
              <motion.div
                initial={{ opacity: 0, scale: 0.8 }}
                animate={{ opacity: 1, scale: 1 }}
                transition={{ duration: 0.6 }}
                className="mb-8 flex justify-center"
              >
                <div className={`inline-flex items-center justify-center w-20 h-20 bg-gradient-to-br ${domain.color} rounded-2xl`}>
                  <IconComponent className="h-10 w-10 text-white" />
                </div>
              </motion.div>

              {/* Title */}
              <motion.h1
                initial={{ opacity: 0, y: 30 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, delay: 0.1 }}
                className="text-4xl sm:text-5xl lg:text-6xl font-bold text-white mb-6"
              >
                {domain.title}
              </motion.h1>

              {/* Description */}
              <motion.p
                initial={{ opacity: 0, y: 30 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, delay: 0.2 }}
                className="text-xl text-neutral-300 max-w-3xl mx-auto mb-8"
              >
                {domain.description}
              </motion.p>

              {/* CTA Button */}
              <motion.div
                initial={{ opacity: 0, y: 30 }}
                animate={{ opacity: 1, y: 0 }}
                transition={{ duration: 0.6, delay: 0.3 }}
              >
                <Button 
                  asChild 
                  size="lg" 
                  className="bg-gradient-to-r from-indigo-500 to-sky-400 hover:from-indigo-600 hover:to-sky-500 text-white px-8"
                >
                  <Link href="/contact" className="flex items-center space-x-2">
                    <span>Discuss a Project</span>
                    <ArrowRight className="h-5 w-5" />
                  </Link>
                </Button>
              </motion.div>
            </motion.div>
          </div>
        </section>

        {/* What We Do Section */}
        <section className="py-24 bg-slate-900/50">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <div className="grid grid-cols-1 lg:grid-cols-2 gap-16 items-center">
              {/* Content */}
              <motion.div
                initial={{ opacity: 0, x: -60 }}
                whileInView={{ opacity: 1, x: 0 }}
                viewport={{ once: true }}
                transition={{ duration: 0.8 }}
              >
                <h2 className="text-3xl sm:text-4xl font-bold text-white mb-6">
                  What we do
                </h2>
                <p className="text-lg text-neutral-300 leading-relaxed">
                  {domain.longDescription}
                </p>
              </motion.div>

              {/* Visual Element */}
              <motion.div
                initial={{ opacity: 0, x: 60 }}
                whileInView={{ opacity: 1, x: 0 }}
                viewport={{ once: true }}
                transition={{ duration: 0.8 }}
                className="relative"
              >
                <div className={`w-full h-64 bg-gradient-to-br ${domain.color} rounded-2xl opacity-10`} />
                <div className="absolute inset-0 flex items-center justify-center">
                  <IconComponent className="h-24 w-24 text-sky-400 opacity-50" />
                </div>
              </motion.div>
            </div>
          </div>
        </section>

        {/* Capabilities Section */}
        <section className="py-24 bg-slate-950">
          <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
            <motion.div
              initial={{ opacity: 0, y: 60 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.8 }}
              className="text-center mb-16"
            >
              <h2 className="text-3xl sm:text-4xl font-bold text-white mb-6">
                Key Capabilities
              </h2>
              <p className="text-lg text-neutral-300 max-w-2xl mx-auto">
                Our expertise in {domain.title.toLowerCase()} encompasses these core areas:
              </p>
            </motion.div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              {domain.capabilities.map((capability, index) => (
                <motion.div
                  key={index}
                  initial={{ opacity: 0, y: 30 }}
                  whileInView={{ opacity: 1, y: 0 }}
                  viewport={{ once: true }}
                  transition={{ duration: 0.6, delay: index * 0.1 }}
                  className="flex items-start space-x-4 p-6 bg-slate-900/50 rounded-xl border border-slate-800"
                >
                  <div className="flex-shrink-0">
                    <Check className="h-6 w-6 text-sky-400" />
                  </div>
                  <div>
                    <p className="text-neutral-300 leading-relaxed">
                      {capability}
                    </p>
                  </div>
                </motion.div>
              ))}
            </div>
          </div>
        </section>

        {/* CTA Section */}
        <section className="py-24 bg-slate-900/30">
          <div className="max-w-4xl mx-auto px-4 sm:px-6 lg:px-8 text-center">
            <motion.div
              initial={{ opacity: 0, y: 60 }}
              whileInView={{ opacity: 1, y: 0 }}
              viewport={{ once: true }}
              transition={{ duration: 0.8 }}
            >
              <h2 className="text-3xl sm:text-4xl font-bold text-white mb-6">
                Ready to leverage {domain.title.toLowerCase()}?
              </h2>
              <p className="text-lg text-neutral-300 mb-8 max-w-2xl mx-auto">
                Let's discuss how our {domain.title.toLowerCase()} expertise can help solve your specific challenges and drive your project forward.
              </p>
              <div className="flex flex-col sm:flex-row gap-4 justify-center">
                <Button 
                  asChild 
                  size="lg" 
                  className="bg-gradient-to-r from-indigo-500 to-sky-400 hover:from-indigo-600 hover:to-sky-500 text-white px-8"
                >
                  <Link href="/contact" className="flex items-center space-x-2">
                    <span>Start a Project</span>
                    <ArrowRight className="h-5 w-5" />
                  </Link>
                </Button>
                <Button 
                  asChild 
                  variant="outline" 
                  size="lg" 
                  className="border-slate-600 text-white hover:bg-slate-800 px-8"
                >
                  <Link href="/case-studies">View Case Studies</Link>
                </Button>
              </div>
            </motion.div>
          </div>
        </section>
      </main>

      <Footer />
    </div>
  )
} 