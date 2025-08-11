# DOC File Support Guide

## Issue: Legacy DOC Files
Legacy `.doc` files require special handling compared to modern `.docx` files.

## Solutions (in order of preference):

### 1. Convert DOC to DOCX (Recommended)
- Open the `.doc` file in Microsoft Word
- Save As → Word Document (.docx)
- The RAG system will process DOCX files perfectly

### 2. Install LibreOffice (Full Support)
```bash
# Download and install LibreOffice from:
# https://www.libreoffice.org/download/download/

# After installation, the RAG system will automatically use it
```

### 3. Use Alternative Libraries (Limited Support)
Install additional dependencies:
```bash
pip install docx2txt
```

### 4. Skip DOC Files (Temporary)
If neither option works, the system will skip `.doc` files and continue processing other supported formats.

## Supported Document Formats:
✅ **PDF** - Full support  
✅ **DOCX** - Full support  
⚠️ **DOC** - Requires LibreOffice or conversion  

## Error Messages:
- `soffice command was not found` → Install LibreOffice
- `DOC loading error` → Try converting to DOCX
- `No text content found` → File may be corrupted or image-only

## Quick Fix for Your Current Error:
The file "MapCore Capabilities.doc" failed to load. You can:
1. Convert it to DOCX format, or
2. Install LibreOffice, or  
3. The system will continue and process other files
