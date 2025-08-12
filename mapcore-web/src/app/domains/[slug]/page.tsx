import { Metadata } from 'next'
import { notFound } from 'next/navigation'
import { DomainPage } from '@/components/domains/domain-page'

// Domain data structure
const domains = {
  'mapping-infrastructure': {
    title: 'Mapping Infrastructure',
    description: 'Advanced engine for management, distribution, and display of mapping materials.',
    longDescription: 'Development of an advanced engine for management, distribution, and display of mapping materials. Our comprehensive solution provides the foundation for dozens of projects through direct integration or derivative intelligence stacks.',
    capabilities: [
      'SDK – Comprehensive dev tool for creating advanced GIS apps',
      'Mapping Server – Central platform for efficient management/distribution of mapping assets',
      'Aux Tools – Utilities for format conversions & customer demos',
      'Usage Scope – Dozens of projects use the product directly or via derivative intelligence stacks'
    ],
    icon: 'Map',
    color: 'from-blue-500 to-cyan-500'
  },
  'robotics': {
    title: 'Robotics',
    description: 'Advanced technologies for autonomous movement of ground vehicles in open terrain.',
    longDescription: 'Development of advanced technologies for autonomous movement of ground vehicles in open terrain. Our expertise covers navigation systems, sensor fusion, and control algorithms that enable safe and efficient autonomous operation in challenging environments.',
    capabilities: [
      'Navigation – GPS and GPS-free navigation systems for challenging terrain',
      'Sensor Fusion – Integration of multiple sensor types for comprehensive environmental awareness',
      'Control Systems – Advanced algorithms for vehicle control and path planning',
      'Open Terrain Expertise – Specialized solutions for unstructured outdoor environments'
    ],
    icon: 'Bot',
    color: 'from-green-500 to-emerald-500'
  },
  'smart-terrain': {
    title: 'Smart Terrain',
    description: 'Cloud application for advanced geospatial information analysis.',
    longDescription: 'Cloud application for advanced geospatial information analysis with focus on orthophoto processing, cloud-based compute, and intuitive user interfaces. Our platform enables organizations to extract actionable insights from complex geospatial datasets.',
    capabilities: [
      'Orthophoto Processing – Advanced algorithms for high-resolution aerial imagery analysis',
      'Cloud-based Compute – Scalable processing infrastructure for large datasets',
      'Intuitive UI – User-friendly interfaces for complex geospatial workflows',
      'Advanced Analysis – Machine learning-powered insights from geospatial data'
    ],
    icon: 'Cloud',
    color: 'from-purple-500 to-pink-500'
  },
  'artificial-intelligence': {
    title: 'Artificial Intelligence',
    description: 'Deep-learning R&D in computer vision and 3D processing.',
    longDescription: 'Deep-learning research and development in computer vision and 3D processing. Our AI solutions enable automated analysis of visual and spatial data, from semantic classification to advanced aerial intelligence analysis.',
    capabilities: [
      'Semantic Classification – Automated categorization of objects and terrain features',
      'Object Detection – Real-time identification and tracking of objects in imagery',
      'Aerial Intel Analysis – Advanced processing of aerial and satellite imagery',
      'Stereo & Depth Estimation – 3D reconstruction from multi-view imagery',
      'GPS-free Self-localisation – Computer vision-based positioning systems'
    ],
    icon: 'Brain',
    color: 'from-orange-500 to-red-500'
  }
}

type Props = {
  params: { slug: string }
}

export async function generateMetadata({ params }: Props): Promise<Metadata> {
  const domain = domains[params.slug as keyof typeof domains]
  
  if (!domain) {
    return {
      title: 'Domain Not Found | MapCore'
    }
  }

  return {
    title: `${domain.title} | MapCore`,
    description: domain.description,
    keywords: [
      domain.title.toLowerCase(),
      'geospatial technology',
      'software development',
      'mapcore',
      'gis',
      'mapping'
    ],
    openGraph: {
      title: `${domain.title} | MapCore`,
      description: domain.description,
      type: 'website',
    }
  }
}

export async function generateStaticParams() {
  return Object.keys(domains).map((slug) => ({
    slug: slug,
  }))
}

export default function DomainDetailPage({ params }: Props) {
  const domain = domains[params.slug as keyof typeof domains]
  
  if (!domain) {
    notFound()
  }

  return <DomainPage domain={domain} />
} 