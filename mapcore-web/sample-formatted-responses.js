// Sample responses to test the formatting in your Flask backend

const sampleFormattedResponses = {
  services: `MapCore specializes in three main areas:

🗺️ **Geospatial Intelligence** - Advanced mapping and spatial analysis
🤖 **Autonomous Systems** - Robotics and UGV navigation  
🛡️ **Defense Solutions** - Mission-critical applications for defense and intelligence

Which area interests you most?`,

  technology: `MapCore leverages cutting-edge technologies:

🔧 **Tech Stack** - React, Next.js, TypeScript, Mapbox
🤖 **AI/ML** - Machine learning for spatial analysis
🗺️ **Mapping** - Advanced cartography and real-time data visualization
📡 **Robotics** - Autonomous navigation and control systems

Our core capabilities include:
• Real-time data processing
• High-precision geospatial analysis
• Custom application development
• Mission-critical system deployment

What specific technology interests you?`,

  codeExample: `Here's how you can integrate MapCore's mapping API:

\`\`\`javascript
import { MapCoreAPI } from '@mapcore/sdk'

// Initialize the MapCore client
const mapcore = new MapCoreAPI({
  apiKey: 'your-api-key',
  environment: 'production'
})

// Create a new map instance
const map = await mapcore.createMap({
  container: 'map-container',
  style: 'mapcore://styles/satellite',
  center: [-74.5, 40],
  zoom: 9
})

// Add real-time data layer
map.addLayer({
  id: 'realtime-vehicles',
  type: 'symbol',
  source: {
    type: 'geojson',
    data: await mapcore.getLiveVehicleData()
  }
})
\`\`\`

This example shows:
• API initialization with authentication
• Map creation with custom styling
• Real-time data integration

You can also use inline code like \`mapcore.getLocation()\` for simple functions.

Would you like to see more advanced examples?`,

  pythonExample: `Here's a Python example for geospatial analysis:

\`\`\`python
import mapcore
import numpy as np
from shapely.geometry import Point, Polygon

# Initialize MapCore Python SDK
client = mapcore.Client(api_key="your-key")

# Define area of interest
aoi = Polygon([
    (-74.5, 40.0),
    (-74.0, 40.0), 
    (-74.0, 40.5),
    (-74.5, 40.5)
])

# Get satellite imagery
imagery = client.get_imagery(
    area=aoi,
    date_range=("2024-01-01", "2024-12-31"),
    resolution=1.0  # 1 meter resolution
)

# Perform analysis
results = mapcore.analyze.detect_changes(
    imagery.before,
    imagery.after,
    threshold=0.8
)

print(f"Detected {len(results.changes)} changes")
\`\`\`

Key features:
🔍 **Change Detection** - Automated analysis of temporal imagery
📊 **High Resolution** - Sub-meter precision analysis
⚡ **Fast Processing** - Optimized algorithms for large datasets

Want to see how this integrates with your existing workflows?`,

  capabilities: `MapCore's capabilities span multiple domains:

🎯 **Precision Mapping** - Centimeter-level accuracy for critical applications
⚡ **Real-time Processing** - Live data streams and instantaneous analysis
🔒 **Secure Systems** - Defense-grade security protocols and compliance
🌍 **Global Scale** - Worldwide deployment capabilities and support

We also provide:
1. Custom GIS application development
2. Autonomous vehicle navigation systems
3. Spatial data analytics and visualization
4. Mission planning and execution tools

What specific capability would you like to learn more about?`,

  contact: `Ready to discuss your geospatial needs?

📧 **Email** - Contact through our website form
🌐 **Website** - Browse our case studies and technical blog
👥 **Team** - Meet our experts in geospatial technology
📱 **Demo** - Schedule a live demonstration

**Project Types We Handle:**
• Defense and intelligence applications
• Commercial mapping solutions  
• Autonomous systems integration
• Custom geospatial platforms

What type of project are you considering?`,

  fullExample: `# Complete MapCore Integration Guide

## **Getting Started**

Welcome! Here's everything you need to know about integrating MapCore's geospatial technologies:

### **1. API Setup**

First, initialize the MapCore SDK:

\`\`\`javascript
import { MapCore } from '@mapcore/sdk'

const client = new MapCore({
  apiKey: process.env.MAPCORE_API_KEY,
  region: 'us-east-1'
})
\`\`\`

### **2. Basic Map Creation**

\`\`\`html
<div id="mapcore-container" style="width: 100%; height: 400px;"></div>

<script>
const map = client.createMap({
  container: 'mapcore-container',
  style: 'satellite',
  center: [-74.006, 40.7128], // NYC coordinates
  zoom: 12
})
</script>
\`\`\`

### **3. Advanced Features**

🗺️ **Real-time Data** - Live vehicle tracking and sensor data
🎯 **Precision Routing** - Centimeter-level navigation accuracy  
🔒 **Secure APIs** - Defense-grade encryption and authentication
📊 **Analytics** - Advanced spatial analysis and reporting

### **Common Use Cases:**

1. **Military Operations** - Mission planning and real-time coordination
2. **Autonomous Vehicles** - Path planning and obstacle detection
3. **Smart Cities** - Infrastructure monitoring and traffic optimization
4. **Emergency Response** - Rapid deployment and resource allocation

### **Quick Commands:**

• \`client.getLocation()\` - Get current GPS coordinates
• \`map.addLayer(config)\` - Add custom data layers
• \`client.analyze(area)\` - Perform spatial analysis
• \`map.navigate(start, end)\` - Calculate optimal routes

### **Support Resources:**

📚 **Documentation**: https://docs.mapcore.com
💬 **Community**: Join our Discord server
🎓 **Training**: Free online certification courses
🔧 **Support**: 24/7 technical assistance

Need help with a specific integration challenge?`
}

// You can use these in your Flask backend for testing
console.log("Sample formatted responses for MapCore chatbot:")
console.log("=============================================")
Object.entries(sampleFormattedResponses).forEach(([key, value]) => {
  console.log(`\n${key.toUpperCase()}:`)
  console.log(value)
  console.log("\n" + "-".repeat(50))
})

module.exports = sampleFormattedResponses
