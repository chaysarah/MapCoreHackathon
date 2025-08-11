# RAG Priority Configuration
# Adjust these settings to control file importance in search results

# File category priorities (higher = more important)
CATEGORY_PRIORITIES = {
    'API': 10,           # API files - highest priority
    'Documentation': 7,   # Documentation files - high priority
    'Core': 5,           # Regular code files - medium priority
    'Example': 3         # Example files - lower priority
}

# File path keywords for classification
API_KEYWORDS = [
    'api', 'interface', 'service', 'controller', 'endpoint',
    'rest', 'graphql', 'webapi', 'client', 'sdk', 'contract'
]

EXAMPLE_KEYWORDS = [
    'example', 'sample', 'demo', 'test', 'tutorial', 
    'playground', 'sandbox', 'hello', 'getting-started'
]

DOCUMENTATION_KEYWORDS = [
    'readme', 'doc', 'guide', 'manual', 'specification',
    'spec', 'reference', 'help', 'documentation'
]

# Search behavior settings
DEFAULT_PRIORITIZE_API = True      # Whether to prioritize API files by default
API_SEARCH_RATIO = 0.5            # Portion of results to dedicate to API files
MIN_API_RESULTS = 2               # Minimum API results to include when available

# Advanced settings
PRIORITY_BOOST_FACTOR = 1.5       # How much to boost high-priority results
EXAMPLE_PENALTY_FACTOR = 0.7      # How much to reduce example file scores

# File type specific priorities
FILE_TYPE_PRIORITIES = {
    '.h': 8,      # Header files often contain APIs
    '.hpp': 8,    # C++ header files
    '.cs': 6,     # C# files
    '.ts': 6,     # TypeScript files
    '.js': 5,     # JavaScript files
    '.json': 4,   # Configuration files
    '.md': 7,     # Markdown documentation
    '.txt': 6,    # Text documentation
    '.pdf': 8,    # PDF documentation
    '.docx': 7    # Word documentation
}
