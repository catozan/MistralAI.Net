# VB.NET Mistral AI API Client Library

A comprehensive VB.NET implementation of the Mistral AI API client library, designed to provide full compatibility with the Python mistralai library.

## Features

- **Chat Completion API** - Generate text using Mistral's language models
- **Embeddings API** - Create text embeddings
- **Files API** - Upload and manage files
- **Fine-tuning API** - Fine-tune models on custom data
- **Batch API** - Process batch requests
- **Agents API** - Create and manage AI agents
- **Audio API** - Audio transcription and processing
- **OCR API** - Optical character recognition
- **Classifiers API** - Text classification
- **Fill-in-the-middle API** - Code completion

## Installation

```bash
# Via NuGet (when published)
Install-Package MistralAI.Net

# Or via Package Manager Console
PM> Install-Package MistralAI.Net
```

## Quick Start

```vb
Imports MistralAI.Net

' Initialize client
Dim client As New MistralClient("your-api-key")

' Chat completion
Dim response = Await client.Chat.CreateAsync(New ChatCompletionRequest With {
    .Model = "mistral-large-latest",
    .Messages = {
        New ChatMessage With {.Role = "user", .Content = "Hello, world!"}
    }
})

Console.WriteLine(response.Choices(0).Message.Content)
```

## API Coverage

This library provides 100% feature parity with the Python mistralai library:

- ✅ Chat Completions (sync/async/streaming)
- ✅ Embeddings
- ✅ File Management
- ✅ Fine-tuning Jobs
- ✅ Batch Processing
- ✅ Agent Management
- ✅ Conversations
- ✅ Audio Transcription
- ✅ OCR Processing
- ✅ Text Classification
- ✅ Fill-in-the-middle

## Requirements

- .NET Framework 4.8+ or .NET 6+
- Visual Studio 2019+ or VS Code
- Internet connection for API calls

## Project Structure

```
MistralAI.Net/
├── src/
│   ├── MistralAI.Net/              # Main library
│   │   ├── Client/                 # Core client implementation
│   │   ├── Models/                 # Request/response models
│   │   ├── Endpoints/              # API endpoint implementations
│   │   ├── Utils/                  # Utility classes
│   │   └── Exceptions/             # Exception handling
│   └── MistralAI.Net.Tests/        # Unit tests
├── examples/                       # Usage examples
├── docs/                          # Documentation
└── nuget/                         # NuGet packaging files
```

## License

MIT License - see LICENSE file for details.

## Contributing

Contributions are welcome! Please see CONTRIBUTING.md for guidelines.

## Documentation

Full documentation is available at [docs/](docs/).
