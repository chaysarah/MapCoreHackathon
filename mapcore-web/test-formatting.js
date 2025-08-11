// Test the chatbot formatting by running this in the browser console
// when your Next.js app is running

const testFormattedResponses = () => {
  console.log("🧪 Testing MapCore Chatbot Formatting")
  console.log("=====================================")
  
  const examples = [
    {
      name: "Code Block",
      text: `Here's a JavaScript example:

\`\`\`javascript
const map = new MapCore.Map({
  center: [lat, lng],
  zoom: 10
})
\`\`\`

And here's inline code: \`map.getCenter()\``
    },
    {
      name: "Emoji Bullets with Bold",
      text: `MapCore features:

🗺️ **Real-time Mapping** - Live data visualization
🤖 **AI Analysis** - Machine learning powered insights
🔒 **Secure Platform** - Enterprise-grade security`
    },
    {
      name: "Mixed Formatting",
      text: `**Quick Start Guide:**

1. Install the SDK: \`npm install @mapcore/sdk\`
2. Get your API key from the dashboard
3. Initialize your first map

• Documentation: https://docs.mapcore.com
• Support: 24/7 assistance available

Ready to get started?`
    }
  ]
  
  examples.forEach((example, index) => {
    console.log(`\n${index + 1}. ${example.name}:`)
    console.log("-".repeat(30))
    console.log(example.text)
    console.log("\n" + "=".repeat(50))
  })
  
  console.log("\n✅ All formatting examples displayed above!")
  console.log("💡 Your Flask backend can return any of these formats")
  console.log("🎨 The chatbot will automatically format them nicely")
}

// Auto-run if in browser
if (typeof window !== 'undefined') {
  window.testFormattedResponses = testFormattedResponses
  console.log("Run testFormattedResponses() to see formatting examples")
}

// Export for Node.js
if (typeof module !== 'undefined') {
  module.exports = { testFormattedResponses }
}
