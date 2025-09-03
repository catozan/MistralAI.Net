# MistralAI.Net v1.0.0 - VB.NET Library Release

## 🚀 Official VB.NET Client Library for Mistral AI API

This is the complete standalone release of **MistralAI.Net**, a comprehensive VB.NET library for integrating with the Mistral AI API. This library provides full access to all Mistral AI endpoints and functionality.

## 📦 What's Included

### 🔧 **Library Files** (`/lib` folder):
- **`MistralAI.Net.dll`** - Main library (multi-framework support: .NET Framework 4.8, .NET 6.0, .NET 8.0)
- **`Newtonsoft.Json.dll`** - Required JSON dependency

### 📚 **Documentation** (`/docs` folder):
- **`MistralAI.Net.xml`** - Complete IntelliSense documentation

### 📦 **NuGet Package**:
- **`MistralAI.Net.1.0.0.nupkg`** - Ready for NuGet installation

## ✨ Features

### 🎯 **Complete API Coverage**
- ✅ **Chat Completions** - Text generation and conversations
- ✅ **Models Management** - List and retrieve model information
- ✅ **Embeddings** - Text-to-vector conversions
- ✅ **File Operations** - Upload, download, and manage files
- ✅ **Batch Processing** - Handle multiple requests efficiently
- ✅ **AI Agents** - Create and manage intelligent agents
- ✅ **Audio Processing** - Speech-to-text and text-to-speech
- ✅ **OCR (Optical Character Recognition)** - Extract text from images
- ✅ **Text Classification** - Categorize and analyze text
- ✅ **FIM (Fill-In-the-Middle)** - Code completion and generation
- ✅ **Beta Features** - Access to experimental functionality

### 🏗️ **Framework Support**
- ✅ **.NET Framework 4.8** - Legacy Windows applications
- ✅ **.NET 6.0** - Cross-platform modern applications
- ✅ **.NET 8.0** - Latest .NET with performance optimizations

## 🔧 Quick Start

### Installation Options

#### Option 1: Direct DLL Reference
1. Copy `MistralAI.Net.dll` and `Newtonsoft.Json.dll` to your project
2. Add references to both DLLs in your VB.NET project
3. Start using the library!

#### Option 2: NuGet Package
```powershell
Install-Package MistralAI.Net -Source <path-to-nupkg-file>
```

### Basic Usage Example

```vb
Imports MistralAI.Net.Client
Imports MistralAI.Net.Models.Chat

' Initialize the client
Dim client As New MistralClient("your-api-key-here")

' Create a chat completion request
Dim request As New ChatCompletionRequest With {
    .Model = "mistral-small",
    .Messages = New List(Of ChatMessage) From {
        New ChatMessage With {
            .Role = "user",
            .Content = "Hello, how are you today?"
        }
    }
}

' Get response
Dim response = Await client.Chat.CreateAsync(request)
Console.WriteLine(response.Choices(0).Message.Content)

' Don't forget to dispose
client.Dispose()
```

## 🌟 Key Advantages

- **🔥 Zero Configuration** - Works out of the box
- **📝 Full IntelliSense Support** - Complete documentation and code completion
- **⚡ High Performance** - Optimized for speed and efficiency
- **🛡️ Robust Error Handling** - Comprehensive exception management
- **🔧 Multi-Framework** - Works with legacy and modern .NET applications
- **🎯 Type-Safe** - Strongly-typed models and responses
- **📦 Self-Contained** - Minimal dependencies

## 📋 System Requirements

- **Windows**: Windows 10/11, Windows Server 2016+
- **Framework**: .NET Framework 4.8 or .NET 6.0/8.0 Runtime
- **Dependencies**: Newtonsoft.Json (included)

## 🔐 Authentication

Get your API key from [Mistral AI Console](https://console.mistral.ai/) and use it to initialize the client:

```vb
Dim client As New MistralClient("your-api-key-here")
```

## 🆘 Support & Documentation

- **GitHub Repository**: https://github.com/catozan/MistralAI.Net
- **Issues & Bug Reports**: https://github.com/catozan/MistralAI.Net/issues
- **API Documentation**: Available in the included XML documentation

## 📜 License

This library is released under the MIT License. See the repository for full license details.

## 🏆 Build Information

- **Version**: 1.0.0
- **Build Date**: September 3, 2025
- **Compiler**: VB.NET with .NET SDK
- **Status**: Production Ready ✅

---

**Made with ❤️ for the VB.NET community**

Ready to integrate Mistral AI into your VB.NET applications? Just reference the DLL and start coding! 🚀
