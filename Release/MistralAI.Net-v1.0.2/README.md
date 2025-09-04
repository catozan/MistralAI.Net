# MistralAI.Net v1.0.2 - API Endpoint URL Fix Release

A comprehensive VB.NET implementation of the Mistral AI API client library, compatible with .NET Framework 4.8, .NET 6.0, and .NET 8.0.

## What's Fixed in v1.0.2

This release addresses a critical API routing issue where API calls would fail with "no Route matched with those values" error due to incorrect URL construction.

### Key Fixes
- **API Endpoint URLs**: Fixed base URL construction to prevent double `v1/` in API paths
- **URL Routing**: Corrected base URL from `https://api.mistral.ai/v1/` to `https://api.mistral.ai/`
- **Route Matching**: All endpoints now construct proper URLs like `https://api.mistral.ai/v1/chat/completions`
- **Error Resolution**: Eliminates "no Route matched with those values" routing errors

### Technical Details
The issue was caused by the base URL including `/v1/` while endpoint paths also included the `v1/` prefix, creating malformed URLs like `https://api.mistral.ai/v1/v1/chat/completions`. The fix ensures proper URL construction by using `https://api.mistral.ai/` as the base URL, allowing endpoints to correctly append their `v1/[endpoint]` paths.

## Installation

### Option 1: Direct DLL Reference
1. Copy `MistralAI.Net.dll` to your project
2. Add reference to the DLL in your VB.NET or C# project
3. Start using the library

### Option 2: NuGet Package (if available)
```
Install-Package MistralAI.Net
```

## Quick Start

```vb
Imports MistralAI.Net

' Initialize the client
Dim client As New MistralClient("your-api-key-here")

' Use the chat endpoint
Dim response = Await client.Chat.CreateAsync(New ChatCompletionRequest() With {
    .Model = "mistral-7b-instruct",
    .Messages = {
        New ChatMessage() With {
            .Role = "user",
            .Content = "Hello, how are you?"
        }
    }
})

Console.WriteLine(response.Choices(0).Message.Content)
```

## Features

### Available Endpoints
- **Chat**: Chat completions and conversations
- **Completions**: Text completions
- **Embeddings**: Text embeddings generation
- **Models**: Model information and management
- **Files**: File operations and management
- **Batch**: Batch processing operations
- **Agents**: AI agents functionality
- **Audio**: Audio processing capabilities
- **OCR**: Optical Character Recognition
- **Classifiers**: Text classification
- **FIM**: Fill-in-the-middle completions
- **Beta**: Beta features and experimental functionality

### Multi-Framework Support
- **.NET Framework 4.8**: Full compatibility with legacy applications
- **.NET 6.0**: Modern cross-platform support
- **.NET 8.0**: Latest performance optimizations

## Breaking Changes
None - this is a backward compatible bug fix release.

## Technical Requirements
- .NET Framework 4.8+ OR .NET 6.0+ OR .NET 8.0+
- System.Net.Http (included in modern frameworks)
- System.Text.Json (for .NET Core/.NET 5+ versions)
- Newtonsoft.Json (for .NET Framework versions)

## Version History

### v1.0.2 (Current)
- Fixed API endpoint URL construction preventing routing errors
- Resolved "no Route matched with those values" error
- Corrected base URL structure for proper API communication

### v1.0.1
- Fixed HttpClient BaseAddress configuration
- Added proper authorization headers setup
- Resolved "invalid request URI" runtime errors

### v1.0.0
- Initial release with complete Mistral AI API coverage

## Support
For issues, feature requests, or contributions, please visit the GitHub repository.

## License
This library is released under the MIT License.

---

**Version**: 1.0.2  
**Release Date**: September 2025  
**Compatibility**: .NET Framework 4.8, .NET 6.0, .NET 8.0
