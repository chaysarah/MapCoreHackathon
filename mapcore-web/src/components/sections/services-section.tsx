'use client'

import * as React from 'react'
import { motion } from 'framer-motion'
import { 
  Map, 
  Bot, 
  Cloud, 
  Brain, 
  Zap,
  ArrowRight 
} from 'lucide-react'
import { Button } from '@/components/ui/button'
import Link from 'next/link'

const domains = [
  {
    icon: Map,
    title: 'Mapping Infrastructure',
    slug: 'mapping-infrastructure',
    description: 'Advanced engine for management, distribution, and display of mapping materials with comprehensive SDK and mapping server.',
    features: ['Comprehensive SDK', 'Mapping Server Platform', 'Format Conversion Tools', 'Customer Demo Utilities'],
    color: 'from-blue-500 to-cyan-500'
  },
  {
    icon: Bot,
    title: 'Robotics',
    slug: 'robotics',
    description: 'Advanced technologies for autonomous movement of ground vehicles in open terrain, including navigation, sensor fusion, and control.',
    features: ['Autonomous Navigation', 'Sensor Fusion', 'Ground Vehicle Control', 'Open Terrain Mapping'],
    color: 'from-green-500 to-emerald-500'
  },
  {
    icon: Cloud,
    title: 'Smart Terrain',
    slug: 'smart-terrain',
    description: 'Cloud application for advanced geospatial information analysis with orthophoto processing and intuitive UI.',
    features: ['Orthophoto Processing', 'Cloud-based Compute', 'Intuitive UI/UX', 'Geospatial Analysis'],
    color: 'from-purple-500 to-pink-500'
  },
  {
    icon: Brain,
    title: 'Artificial Intelligence',
    slug: 'artificial-intelligence',
    description: 'Deep-learning R&D in computer vision and 3D processing for advanced aerial intelligence analysis.',
    features: ['Semantic Classification', 'Object Detection', 'Stereo & Depth Estimation', 'GPS-free Localization'],
    color: 'from-orange-500 to-red-500'
  }
]

const container = {
  initial: { opacity: 0 },
  animate: {
    opacity: 1,
    transition: {
      staggerChildren: 0.2,
      delayChildren: 0.1
    }
  }
}

const item = {
  initial: { opacity: 0, y: 60 },
  animate: { 
    opacity: 1, 
    y: 0,
    transition: { duration: 0.6, ease: 'easeOut' }
  }
}

export function ServicesSection() {
  return (
    <section className="py-24 bg-slate-950">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <motion.div
          variants={container}
          initial="initial"
          whileInView="animate"
          viewport={{ once: true }}
        >
          {/* Section Header */}
          <motion.div variants={item} className="text-center mb-16">
            <h2 className="text-3xl sm:text-4xl lg:text-5xl font-bold text-white mb-6">
              What we build
            </h2>
            <p className="text-xl text-neutral-300 max-w-3xl mx-auto">
              We specialize in four core domains that push the boundaries of geospatial technology, 
              robotics, and artificial intelligence.
            </p>
          </motion.div>

          {/* Domains Grid */}
          <motion.div 
            variants={item}
            className="grid grid-cols-1 md:grid-cols-2 gap-8"
          >
            {domains.map((domain, index) => (
              <motion.div
                key={domain.title}
                variants={item}
                className="group relative bg-slate-900/50 backdrop-blur-sm border border-slate-800 rounded-2xl p-8 hover:border-slate-700 transition-all duration-300"
              >
                {/* Gradient Background */}
                <div className={`absolute inset-0 bg-gradient-to-br ${domain.color} opacity-0 group-hover:opacity-5 rounded-2xl transition-opacity duration-300`} />
                
                {/* Icon */}
                <div className={`inline-flex items-center justify-center w-12 h-12 bg-gradient-to-br ${domain.color} rounded-xl mb-6`}>
                  <domain.icon className="h-6 w-6 text-white" />
                </div>

                {/* Content */}
                <h3 className="text-xl font-bold text-white mb-4 group-hover:text-sky-400 transition-colors">
                  {domain.title}
                </h3>
                <p className="text-neutral-300 mb-6 leading-relaxed">
                  {domain.description}
                </p>

                {/* Features */}
                <ul className="space-y-2 mb-6">
                  {domain.features.map((feature, featureIndex) => (
                    <li key={featureIndex} className="flex items-center text-sm text-neutral-400">
                      <Zap className="h-3 w-3 text-sky-400 mr-2 flex-shrink-0" />
                      {feature}
                    </li>
                  ))}
                </ul>

                {/* Learn More Link */}
                <Link 
                  href={`/domains/${domain.slug}`}
                  className="flex items-center text-sky-400 text-sm font-medium group-hover:text-sky-300 transition-colors"
                >
                  <span>Learn more</span>
                  <ArrowRight className="h-4 w-4 ml-1 group-hover:translate-x-1 transition-transform" />
                </Link>
              </motion.div>
            ))}
          </motion.div>


        </motion.div>
      </div>
    </section>
  )
} 