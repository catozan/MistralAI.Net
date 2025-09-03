# MistralAI.Net v1.0.1 - HttpClient Configuration Fix Release

A comprehensive VB.NET implementation of the Mistral AI API client library, compatible with .NET Framework 4.8, .NET 6.0, and .NET 8.0.

## What's Fixed in v1.0.1

This release addresses a critical runtime issue where API calls would fail with "An invalid request URI was provided. The request URI must either be an absolute URI or BaseAddress must be set" error.

### Key Fixes
- **HttpClient Configuration**: Fixed BaseEndpoint constructor to properly set `HttpClient.BaseAddress`
- **Authorization Headers**: Added proper authorization headers configuration during endpoint initialization
- **API Endpoint**: Updated MistralClient to use the correct Mistral API v1 endpoint URL (`https://api.mistral.ai/v1/`)
- **Parameter Consistency**: Fixed all endpoint constructor parameter references for reliable initialization

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

## Support
For issues, feature requests, or contributions, please visit the GitHub repository.

## License
This library is released under the MIT License.

---

**Version**: 1.0.1  
**Release Date**: December 2024  
**Compatibility**: .NET Framework 4.8, .NET 6.0, .NET 8.0
