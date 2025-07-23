'use client'

import * as React from 'react'
import mapboxgl from 'mapbox-gl'

// You'll need to set your Mapbox access token
const MAPBOX_ACCESS_TOKEN = process.env.NEXT_PUBLIC_MAPBOX_ACCESS_TOKEN || 'pk.your_token_here'

export default function MapboxMap() {
  const mapContainer = React.useRef<HTMLDivElement>(null)
  const map = React.useRef<mapboxgl.Map | null>(null)

  React.useEffect(() => {
    if (map.current) return // Initialize map only once

    if (!mapContainer.current) return

    mapboxgl.accessToken = MAPBOX_ACCESS_TOKEN

    map.current = new mapboxgl.Map({
      container: mapContainer.current,
      style: 'mapbox://styles/mapbox/dark-v11',
      center: [-74.006, 40.7128], // NYC coordinates
      zoom: 2.5,
      pitch: 45,
      bearing: -17.6,
      antialias: true,
      interactive: false, // Disable interaction for background map
      attributionControl: false,
    })

    // Add subtle animation
    map.current.on('load', () => {
      if (!map.current) return

      // Add a subtle animated layer
      map.current.addSource('points', {
        type: 'geojson',
        data: {
          type: 'FeatureCollection',
          features: [
            {
              type: 'Feature',
              geometry: { type: 'Point', coordinates: [-74.006, 40.7128] },
              properties: { title: 'New York' }
            },
            {
              type: 'Feature', 
              geometry: { type: 'Point', coordinates: [-122.4194, 37.7749] },
              properties: { title: 'San Francisco' }
            },
            {
              type: 'Feature',
              geometry: { type: 'Point', coordinates: [2.3522, 48.8566] },
              properties: { title: 'Paris' }
            },
            {
              type: 'Feature',
              geometry: { type: 'Point', coordinates: [139.6917, 35.6895] },
              properties: { title: 'Tokyo' }
            }
          ]
        }
      })

      map.current.addLayer({
        id: 'points',
        type: 'circle',
        source: 'points',
        paint: {
          'circle-radius': {
            stops: [
              [0, 0],
              [20, 30]
            ],
            base: 2
          },
          'circle-color': '#38bdf8',
          'circle-opacity': 0.6,
          'circle-stroke-color': '#0ea5e9',
          'circle-stroke-width': 2
        }
      })

      // Subtle rotation animation
      const rotateCamera = (timestamp: number) => {
        if (!map.current) return
        map.current.rotateTo((timestamp / 100) % 360, { duration: 0 })
        requestAnimationFrame(rotateCamera)
      }
      rotateCamera(0)
    })

    return () => {
      if (map.current) {
        map.current.remove()
      }
    }
  }, [])

  return (
    <div 
      ref={mapContainer} 
      className="absolute inset-0 w-full h-full opacity-60"
      style={{ filter: 'blur(1px)' }}
    />
  )
} 