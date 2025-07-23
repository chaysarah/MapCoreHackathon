'use client'

import * as React from 'react'
import Link from 'next/link'
import { MapPin, Mail, Github, Twitter, Linkedin } from 'lucide-react'

const navigation = {
  main: [
    { name: 'Home', href: '/' },
    { name: 'Case Studies', href: '/case-studies' },
    { name: 'Open Source', href: '/open-source' },
    { name: 'Team', href: '/team' },
    { name: 'Blog', href: '/blog' },
    { name: 'Contact', href: '/contact' },
  ],
  services: [
    { name: 'Web GIS', href: '/services/web-gis' },
    { name: 'Mobile SDKs', href: '/services/mobile' },
    { name: 'Consulting', href: '/services/consulting' },
    { name: 'Support', href: '/services/support' },
  ],
  social: [
    {
      name: 'GitHub',
      href: '#',
      icon: Github,
    },
    {
      name: 'Twitter',
      href: '#',
      icon: Twitter,
    },
    {
      name: 'LinkedIn',
      href: '#',
      icon: Linkedin,
    },
  ],
}

export function Footer() {
  return (
    <footer className="bg-slate-950 border-t border-slate-800">
      <div className="mx-auto max-w-7xl overflow-hidden px-6 py-20 sm:py-24 lg:px-8">
        <div className="grid grid-cols-1 md:grid-cols-4 gap-8 mb-16">
          {/* Company Info */}
          <div className="md:col-span-2">
            <Link href="/" className="flex items-center space-x-2 text-white mb-6">
              <MapPin className="h-8 w-8 text-sky-400" />
              <span className="text-xl font-bold">MapCore</span>
            </Link>
            <p className="text-neutral-300 mb-6 max-w-md">
              Mapping ideas to software excellence. We design and build complex, 
              map-centric digital products for web and mobile.
            </p>
            <div className="flex items-center space-x-2 text-neutral-400 mb-2">
              <Mail className="h-4 w-4" />
              <span>hello@mapcore.dev</span>
            </div>
          </div>

          {/* Navigation */}
          <div>
            <h3 className="text-sm font-semibold text-white tracking-wider uppercase mb-4">
              Navigation
            </h3>
            <ul className="space-y-3">
              {navigation.main.map((item) => (
                <li key={item.name}>
                  <Link 
                    href={item.href} 
                    className="text-neutral-300 hover:text-sky-400 transition-colors"
                  >
                    {item.name}
                  </Link>
                </li>
              ))}
            </ul>
          </div>

          {/* Services */}
          <div>
            <h3 className="text-sm font-semibold text-white tracking-wider uppercase mb-4">
              Services
            </h3>
            <ul className="space-y-3">
              {navigation.services.map((item) => (
                <li key={item.name}>
                  <Link 
                    href={item.href} 
                    className="text-neutral-300 hover:text-sky-400 transition-colors"
                  >
                    {item.name}
                  </Link>
                </li>
              ))}
            </ul>
          </div>
        </div>

        {/* Bottom section */}
        <div className="flex flex-col md:flex-row md:items-center md:justify-between pt-8 border-t border-slate-800">
          <p className="text-neutral-400 text-sm">
            &copy; 2024 MapCore. All rights reserved.
          </p>
          
          <div className="flex space-x-6 mt-4 md:mt-0">
            {navigation.social.map((item) => (
              <a
                key={item.name}
                href={item.href}
                className="text-neutral-400 hover:text-sky-400 transition-colors"
              >
                <span className="sr-only">{item.name}</span>
                <item.icon className="h-5 w-5" aria-hidden="true" />
              </a>
            ))}
          </div>
        </div>
      </div>
    </footer>
  )
} 