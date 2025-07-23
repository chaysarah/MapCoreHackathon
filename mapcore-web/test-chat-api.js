// Test script to verify Flask server communication
// Run this in browser console: node test-chat-api.js

const testChatAPI = async () => {
  const testMessages = [
    "Hello!",
    "What services do you offer?",
    "Tell me about your mapping technology",
    "How can I contact your team?"
  ];

  console.log("🧪 Testing MapCore Chat API...\n");

  for (const message of testMessages) {
    try {
      console.log(`📤 Sending: "${message}"`);
      
      const response = await fetch('http://localhost:5000/api/chat', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          message: message,
          timestamp: new Date().toISOString()
        })
      });

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      console.log(`📥 Response: "${data.response.substring(0, 100)}..."`);
      console.log(`⏰ Timestamp: ${data.timestamp}`);
      console.log(`✅ Status: ${data.status}\n`);
      
      // Wait a bit between requests
      await new Promise(resolve => setTimeout(resolve, 500));
      
    } catch (error) {
      console.error(`❌ Error testing message "${message}":`, error.message);
    }
  }

  // Test health endpoint
  try {
    console.log("🔍 Testing health endpoint...");
    const healthResponse = await fetch('http://localhost:5000/api/health');
    const healthData = await healthResponse.json();
    console.log("✅ Health check:", healthData);
  } catch (error) {
    console.error("❌ Health check failed:", error.message);
  }
};

// For Node.js environment
if (typeof module !== 'undefined' && module.exports) {
  module.exports = { testChatAPI };
}

// For browser environment
if (typeof window !== 'undefined') {
  window.testChatAPI = testChatAPI;
}

// Auto-run if in Node.js
if (typeof require !== 'undefined') {
  // Check if this is the main module
  if (require.main === module) {
    testChatAPI();
  }
}
