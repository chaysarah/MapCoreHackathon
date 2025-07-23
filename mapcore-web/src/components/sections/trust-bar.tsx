'use client'

import * as React from 'react'
import Image from 'next/image'
import { motion } from 'framer-motion'

const clients = [
  { name: 'Esri', logo: '/logos/esri.svg', width: 120, height: 40 },
  { name: 'HERE Technologies', logo: '/logos/here.svg', width: 100, height: 40 },
  { name: 'Mapbox', logo: '/logos/mapbox.svg', width: 140, height: 40 },
  { name: 'Google Cloud', logo: '/logos/google-cloud.svg', width: 160, height: 40 },
  { name: 'AWS', logo: '/logos/aws.svg', width: 80, height: 40 },
  { name: 'Microsoft Azure', logo: '/logos/azure.svg', width: 120, height: 40 },
]

const container = {
  initial: { opacity: 0 },
  animate: {
    opacity: 1,
    transition: {
      staggerChildren: 0.1,
      delayChildren: 0.3
    }
  }
}

const item = {
  initial: { opacity: 0, y: 20 },
  animate: { 
    opacity: 1, 
    y: 0,
    transition: { duration: 0.5 }
  }
}

export function TrustBar() {
  return (
    <section className="py-16 bg-slate-900/50 border-y border-slate-800">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <motion.div
          variants={container}
          initial="initial"
          whileInView="animate"
          viewport={{ once: true }}
          className="text-center"
        >
          <motion.p 
            variants={item}
            className="text-sm font-medium text-slate-400 mb-8 uppercase tracking-wider"
          >
            Trusted by industry leaders
          </motion.p>
          
          <motion.div 
            variants={item}
            className="grid grid-cols-2 md:grid-cols-3 lg:grid-cols-6 gap-8 items-center justify-items-center"
          >
            {clients.map((client) => (
              <div
                key={client.name}
                className="flex items-center justify-center h-16 grayscale hover:grayscale-0 transition-all duration-300 opacity-60 hover:opacity-100"
              >
                <Image
                  src={client.logo}
                  alt={`${client.name} logo`}
                  width={client.width}
                  height={client.height}
                  className="max-w-full h-auto"
                  priority={false}
                />
              </div>
            ))}
          </motion.div>
        </motion.div>
      </div>
    </section>
  )
} 