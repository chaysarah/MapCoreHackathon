'use client'

import * as React from 'react'
import dynamic from 'next/dynamic'

// Dynamically import the map component to avoid SSR issues
const MapboxMap = dynamic(() => import('./mapbox-map'), {
  ssr: false,
  loading: () => (
    <div className="w-full h-full bg-slate-900 animate-pulse flex items-center justify-center">
      <div className="text-slate-400">Loading map...</div>
    </div>
  ),
})

export function MapBackground() {
  return (
    <div className="absolute inset-0 w-full h-full">
      <MapboxMap />
    </div>
  )
} 