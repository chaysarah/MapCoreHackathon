'use client'

import * as React from 'react'
import Link from 'next/link'
import { motion } from 'framer-motion'
import { Button } from '@/components/ui/button'
import { Users, Monitor, Heart, Award, Coffee, Zap, Mail } from 'lucide-react'

const keyTeamMembers = [
  { name: 'Sarah Chen', role: 'Lead GIS Engineer', image: '/images/team/sarah.jpg' },
  { name: 'Michael Rodriguez', role: 'Robotics Specialist', image: '/images/team/michael.jpg' },
  { name: 'Emily Johnson', role: 'AI Research Director', image: '/images/team/emily.jpg' },
  { name: 'David Kim', role: 'Senior Frontend Developer', image: '/images/team/david.jpg' },
  { name: 'Anna Petrov', role: 'Geospatial Analyst', image: '/images/team/anna.jpg' },
  { name: 'James Wilson', role: 'DevOps Engineer', image: '/images/team/james.jpg' },
  { name: 'Maria Garcia', role: 'UX Design Lead', image: '/images/team/maria.jpg' },
  { name: 'Alex Thompson', role: 'Backend Architect', image: '/images/team/alex.jpg' },
]

const allTeamMembers = [
  ...keyTeamMembers,
  { name: 'Lisa Chang', role: 'Senior Mapping Engineer', image: '/images/team/lisa.jpg' },
  { name: 'Robert Miller', role: 'Autonomous Systems Engineer', image: '/images/team/robert.jpg' },
  { name: 'Jennifer Park', role: 'Computer Vision Specialist', image: '/images/team/jennifer.jpg' },
  { name: 'Thomas Brown', role: 'Geospatial Data Scientist', image: '/images/team/thomas.jpg' },
  { name: 'Amanda Davis', role: 'Mobile GIS Developer', image: '/images/team/amanda.jpg' },
  { name: 'Kevin Lee', role: 'Navigation Systems Engineer', image: '/images/team/kevin.jpg' },
  { name: 'Rachel Green', role: 'Sensor Fusion Engineer', image: '/images/team/rachel.jpg' },
  { name: 'Daniel White', role: 'Cloud Infrastructure Engineer', image: '/images/team/daniel.jpg' },
  { name: 'Sophie Turner', role: 'Machine Learning Engineer', image: '/images/team/sophie.jpg' },
  { name: 'Carlos Rodriguez', role: 'Spatial Analytics Engineer', image: '/images/team/carlos.jpg' },
  { name: 'Nicole Adams', role: 'Quality Assurance Lead', image: '/images/team/nicole.jpg' },
  { name: 'Mark Johnson', role: 'Systems Integration Engineer', image: '/images/team/mark.jpg' },
  { name: 'Laura Wilson', role: 'Technical Writer', image: '/images/team/laura.jpg' },
  { name: 'Steve Chen', role: 'Database Engineer', image: '/images/team/steve.jpg' },
  { name: 'Maya Patel', role: 'Research Scientist', image: '/images/team/maya.jpg' },
  { name: 'Ryan Taylor', role: 'Performance Engineer', image: '/images/team/ryan.jpg' },
  { name: 'Elena Vasquez', role: 'Security Engineer', image: '/images/team/elena.jpg' },
  { name: 'Jonathan Kim', role: 'Platform Engineer', image: '/images/team/jonathan.jpg' },
  { name: 'Samantha Lee', role: 'Product Manager', image: '/images/team/samantha.jpg' },
  { name: 'Andrew Clark', role: 'Solutions Architect', image: '/images/team/andrew.jpg' },
  { name: 'Christina Wang', role: 'UI/UX Designer', image: '/images/team/christina.jpg' },
  { name: 'Brian Murphy', role: 'Field Applications Engineer', image: '/images/team/brian.jpg' },
  { name: 'Priya Sharma', role: 'Data Analytics Engineer', image: '/images/team/priya.jpg' },
  { name: 'Marcus Johnson', role: 'Embedded Systems Engineer', image: '/images/team/marcus.jpg' },
  { name: 'Olivia Brown', role: 'Documentation Specialist', image: '/images/team/olivia.jpg' },
  { name: 'Jason Liu', role: 'Integration Engineer', image: '/images/team/jason.jpg' },
  { name: 'Rebecca Martinez', role: 'Support Engineer', image: '/images/team/rebecca.jpg' },
  { name: 'Paul Anderson', role: 'Senior Researcher', image: '/images/team/paul.jpg' },
  { name: 'Michelle Zhang', role: 'Test Automation Engineer', image: '/images/team/michelle.jpg' },
  { name: 'Gary Wilson', role: 'Release Manager', image: '/images/team/gary.jpg' },
  { name: 'Hannah Davis', role: 'Business Analyst', image: '/images/team/hannah.jpg' },
  { name: 'Victor Rodriguez', role: 'Compliance Engineer', image: '/images/team/victor.jpg' },
]

const values = [
  {
    icon: Award,
    title: 'Craftsmanship',
    description: 'We take pride in writing clean, maintainable code and building robust systems that stand the test of time.'
  },
  {
    icon: Heart,
    title: 'Reliability',
    description: 'Our team delivers consistent, dependable solutions that our clients can trust for their most critical applications.'
  },
  {
    icon: Coffee,
    title: 'Knowledge Sharing',
    description: 'We believe in continuous learning and actively share our expertise through mentoring and open collaboration.'
  }
]

const container = {
  initial: { opacity: 0 },
  animate: {
    opacity: 1,
    transition: {
      staggerChildren: 0.1,
      delayChildren: 0.2
    }
  }
}

const item = {
  initial: { opacity: 0, y: 30 },
  animate: { 
    opacity: 1, 
    y: 0,
    transition: { duration: 0.6 }
  }
}

export function TeamSection() {
  const [showAllMembers, setShowAllMembers] = React.useState(false)
  const displayedMembers = showAllMembers ? allTeamMembers : keyTeamMembers

  return (
    <div>
      {/* Hero Section */}
      <section className="py-24 bg-slate-950">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <motion.div
            variants={container}
            initial="initial"
            animate="animate"
            className="text-center"
          >


            <motion.h1 
              variants={item}
              className="text-4xl sm:text-5xl lg:text-6xl font-bold text-white mb-6"
            >
              The minds behind MapCore
            </motion.h1>

                         <motion.p 
               variants={item}
               className="text-xl text-neutral-300 max-w-3xl mx-auto mb-8"
             >
               Our team of 40+ specialized engineers and researchers work together in a 
               Windows development environment to create cutting-edge geospatial solutions that 
               push the boundaries of what's possible.
             </motion.p>

            <motion.div 
              variants={item}
              className="flex flex-wrap justify-center gap-6 text-sm text-neutral-400"
            >
                             <div className="flex items-center space-x-2">
                 <Users className="h-4 w-4 text-sky-400" />
                 <span>40+ specialized engineers and researchers</span>
               </div>
              <div className="flex items-center space-x-2">
                <Monitor className="h-4 w-4 text-sky-400" />
                <span>Windows development environment</span>
              </div>
              <div className="flex items-center space-x-2">
                <Zap className="h-4 w-4 text-sky-400" />
                <span>4 specialized domains</span>
              </div>
            </motion.div>
          </motion.div>
        </div>
      </section>



      {/* Team Grid */}
      <section className="py-24 bg-slate-900/50">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
          <motion.div
            initial={{ opacity: 0, y: 60 }}
            whileInView={{ opacity: 1, y: 0 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            <h2 className="text-3xl sm:text-4xl font-bold text-white mb-6">
              Meet the team
            </h2>
            <p className="text-lg text-neutral-300 max-w-2xl mx-auto">
              Our diverse team brings together expertise from computer science, geography, 
              robotics, and artificial intelligence to solve complex challenges.
            </p>
          </motion.div>

          {/* Team Member Cards */}
          <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
            {displayedMembers.map((member, index) => (
              <motion.div
                key={member.name}
                initial={{ opacity: 0, y: 30 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ duration: 0.6, delay: index * 0.1 }}
                className="bg-slate-800/50 rounded-2xl p-6 text-center border border-slate-700 hover:border-slate-600 transition-colors"
              >
                <div className="w-20 h-20 bg-gradient-to-br from-sky-400 to-indigo-500 rounded-full mx-auto mb-4 flex items-center justify-center">
                  <span className="text-white font-bold text-xl">
                    {member.name.split(' ').map((n: string) => n[0]).join('')}
                  </span>
                </div>
                <h3 className="text-lg font-semibold text-white mb-2">
                  {member.name}
                </h3>
                <p className="text-sm text-neutral-400">
                  {member.role}
                </p>
              </motion.div>
            ))}
          </div>

          {/* Toggle Button */}
          <motion.div
            initial={{ opacity: 0 }}
            whileInView={{ opacity: 1 }}
            viewport={{ once: true }}
            transition={{ duration: 0.8 }}
            className="text-center mb-16"
          >
            {!showAllMembers ? (
              <Button
                onClick={() => setShowAllMembers(true)}
                variant="outline"
                size="lg"
                className="border-slate-600 text-white hover:bg-slate-800 px-8"
              >
                See All {allTeamMembers.length} Team Members
              </Button>
            ) : (
              <div className="space-y-4">
                <p className="text-neutral-400 text-sm">
                  Showing all {allTeamMembers.length} team members
                </p>
                <Button
                  onClick={() => setShowAllMembers(false)}
                  variant="ghost"
                  size="lg"
                  className="text-slate-400 hover:text-white hover:bg-slate-800 px-8"
                >
                  Show Key Members Only
                </Button>
              </div>
            )}
          </motion.div>
        </div>
      </section>

      {/* Values Section */}
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
              Our values
            </h2>
            <p className="text-lg text-neutral-300 max-w-2xl mx-auto">
              These principles guide everything we do, from the code we write to 
              the relationships we build with our clients and each other.
            </p>
          </motion.div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            {values.map((value, index) => (
              <motion.div
                key={value.title}
                initial={{ opacity: 0, y: 30 }}
                whileInView={{ opacity: 1, y: 0 }}
                viewport={{ once: true }}
                transition={{ duration: 0.6, delay: index * 0.2 }}
                className="text-center"
              >
                <div className="inline-flex items-center justify-center w-16 h-16 bg-gradient-to-br from-indigo-500 to-sky-400 rounded-xl mb-6">
                  <value.icon className="h-8 w-8 text-white" />
                </div>
                <h3 className="text-xl font-bold text-white mb-4">
                  {value.title}
                </h3>
                <p className="text-neutral-300 leading-relaxed">
                  {value.description}
                </p>
              </motion.div>
            ))}
          </div>
        </div>
      </section>
    </div>
  )
} 