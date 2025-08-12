# MapCore Chatbot

## Overview
The MapCore chatbot is an intelligent assistant that helps visitors learn about MapCore's geospatial technologies and services. It integrates with a Flask backend server to provide dynamic, server-processed responses.

## Features

### � **Server Integration**
- Sends chat messages to Flask backend on localhost:5000
- Real-time API communication with error handling
- Automatic fallback for server connectivity issues

### 🎨 **Modern UI/UX**
- Floating chat button with smooth animations
- Responsive design that works on all devices
- Gradient color scheme matching MapCore branding
- Typing indicators and smooth message animations

### 🔗 **API Communication**
The chatbot sends POST requests to:
- **Endpoint**: `http://localhost:5000/api/chat`
- **Payload**: `{ message: string, timestamp: string }`
- **Response**: `{ response: string, timestamp: string, status: string }`

## Implementation

### Components Used
- **Framer Motion**: Smooth animations and transitions
- **Lucide React**: Professional icon set
- **Tailwind CSS**: Responsive styling
- **Custom UI Components**: Button and utility components

### Key Files
- `src/components/chatbot/chatbot.tsx` - Main chatbot component
- `src/app/layout.tsx` - Integration into the app layout

### How It Works

1. **Message Analysis**: The `analyzeMessage()` function uses regex patterns to identify user intent
2. **Response Generation**: Based on the identified intent, it selects appropriate responses from the knowledge base
3. **Conversation Flow**: Maintains message history and provides contextual follow-up suggestions

## Customization

### Adding New Response Categories
To add new conversation topics:

1. Add new response arrays to `MAPCORE_RESPONSES`
2. Update the `analyzeMessage()` function with new pattern matching
3. Include appropriate keywords and response variations

### Styling Changes
The chatbot uses Tailwind CSS classes that can be easily modified:
- Colors: Update gradient classes for brand consistency
- Size: Modify width/height in the chat window
- Position: Adjust fixed positioning classes

## Future Enhancements

### Backend Integration
The current implementation uses client-side pattern matching. For more advanced capabilities, consider:

- **Flask Backend**: Natural language processing with spaCy or NLTK
- **OpenAI Integration**: GPT-powered responses
- **Knowledge Base**: Database-driven content management
- **Analytics**: Conversation tracking and insights

### Advanced Features
- **Multi-language support**: Internationalization
- **Voice input**: Speech-to-text integration
- **File sharing**: Document and image exchange
- **Live chat**: Human agent handoff
- **Persistence**: Save conversation history

## Technical Details

### Performance
- Lightweight component with minimal bundle impact
- Smooth animations that don't block the UI
- Efficient regex-based pattern matching

### Accessibility
- Keyboard navigation support
- Screen reader friendly
- High contrast color schemes
- Focus management

### Mobile Responsiveness
- Touch-friendly button sizes
- Responsive chat window sizing
- Proper viewport handling

## Usage Examples

### Common User Interactions

**User**: "Hi there!"
**Bot**: Welcome message with overview of available help

**User**: "What services do you offer?"
**Bot**: Detailed breakdown of geospatial services with follow-up questions

**User**: "Tell me about your mapping technology"
**Bot**: Technical details about mapping solutions and capabilities

**User**: "How can I contact your team?"
**Bot**: Contact information and project inquiry guidance

## Integration Notes

The chatbot is automatically included on all pages through the root layout component. It appears as a floating button in the bottom-right corner and can be toggled open/closed by users.

The component is fully self-contained and doesn't require any external API calls in its current implementation, making it fast and reliable for all visitors.
