'use client'

import * as React from 'react'
import { motion } from 'framer-motion'
import { MapBackground } from '@/components/map/map-background'
import { Code, Globe, Zap } from 'lucide-react'

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

export function HeroSection() {
  return (
    <section className="relative min-h-screen flex items-center justify-center overflow-hidden">
      {/* Background Map */}
      <div className="absolute inset-0 z-0">
        <MapBackground />
        <div className="absolute inset-0 bg-gradient-to-br from-slate-950/70 via-slate-950/50 to-slate-950/70" />
      </div>

      {/* Content */}
      <div className="relative z-10 max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 pt-20">
        <motion.div
          variants={staggerContainer}
          initial="initial"
          animate="animate"
          className="text-center"
        >


          {/* Main Heading */}
          <motion.h1 
            variants={fadeInUp}
            className="text-4xl sm:text-5xl lg:text-6xl font-bold text-white mb-6 leading-tight"
          >
            Advanced Geospatial Technologies to{' '}
            <span className="bg-gradient-to-r from-indigo-500 to-sky-400 bg-clip-text text-transparent">
              Mission Excellence
            </span>
          </motion.h1>

          {/* Subtitle */}
          <motion.p 
            variants={fadeInUp}
            className="text-xl text-neutral-300 mb-8 max-w-3xl mx-auto leading-relaxed"
          >
            We develop cutting-edge mapping, autonomous robotics, and AI solutions for defense and intelligence. 
            From real-time terrain analysis to autonomous navigation, we transform geospatial data into operational superiority.
          </motion.p>

          {/* Feature highlights */}
          <motion.div 
            variants={fadeInUp}
            className="flex flex-wrap justify-center gap-6 mb-10 text-sm text-neutral-400"
          >
            <div className="flex items-center space-x-2">
              <Globe className="h-4 w-4 text-sky-400" />
              <span>Mapping Infrastructure</span>
            </div>
            <div className="flex items-center space-x-2">
              <Code className="h-4 w-4 text-sky-400" />
              <span>Autonomous Robotics</span>
            </div>
            <div className="flex items-center space-x-2">
              <Zap className="h-4 w-4 text-sky-400" />
              <span>AI-Powered Analysis</span>
            </div>
          </motion.div>


        </motion.div>
      </div>


    </section>
  )
} 