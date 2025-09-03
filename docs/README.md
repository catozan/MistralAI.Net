# MistralAI.Net Documentation

## Table of Contents

1. [Quick Start](#quick-start)
2. [Installation](#installation)
3. [Authentication](#authentication)
4. [Core Concepts](#core-concepts)
5. [API Reference](#api-reference)
6. [Examples](#examples)
7. [Error Handling](#error-handling)
8. [Best Practices](#best-practices)
9. [Contributing](#contributing)

## Quick Start

```vb
Imports MistralAI.Net.Client
Imports MistralAI.Net.Utils

' Initialize client
Using client As New MistralClient("your-api-key-here")
    
    ' Simple chat completion
    Dim response = Await client.Chat.CreateAsync(
        MistralConstants.Models.MistralLargeLatest,
        "Hello, how are you?"
    )
    
    Console.WriteLine(response.Choices(0).Message.Content)
    
End Using
```

## Installation

### Via NuGet Package Manager

```bash
Install-Package MistralAI.Net
```

### Via .NET CLI

```bash
dotnet add package MistralAI.Net
```

### Via Package Manager Console

```powershell
PM> Install-Package MistralAI.Net
```

## Authentication

### API Key

Get your API key from [La Plateforme](https://console.mistral.ai/):

```vb
' Option 1: Direct instantiation
Dim client As New MistralClient("your-api-key-here")

' Option 2: Environment variable
Environment.SetEnvironmentVariable("MISTRAL_API_KEY", "your-api-key-here")
Dim client As New MistralClient(Environment.GetEnvironmentVariable("MISTRAL_API_KEY"))

' Option 3: Custom base URL (for enterprise)
Dim client As New MistralClient("your-api-key-here", "https://your-custom-endpoint.com/")
```

## Core Concepts

### Messages

Messages are the building blocks of conversations:

```vb
Imports MistralAI.Net.Models.Chat
Imports MistralAI.Net.Utils

' Create messages
Dim messages As New List(Of ChatMessage) From {
    New ChatMessage(MistralConstants.Roles.System, "You are a helpful assistant."),
    New ChatMessage(MistralConstants.Roles.User, "What is VB.NET?"),
    New ChatMessage(MistralConstants.Roles.Assistant, "VB.NET is a programming language..."),
    New ChatMessage(MistralConstants.Roles.User, "Tell me more about its syntax.")
}
```

### Models

Available model constants:

```vb
' Text generation models
MistralConstants.Models.MistralLargeLatest   ' Latest large model
MistralConstants.Models.MistralLarge2        ' Mistral Large 2
MistralConstants.Models.MistralSmall         ' Small model for simple tasks
MistralConstants.Models.Codestral           ' Code generation

' Specialized models
MistralConstants.Models.MistralEmbed        ' Text embeddings
MistralConstants.Models.Pixtral            ' Vision tasks
```

## API Reference

### Chat Completions

#### Basic Chat Completion

```vb
' Simple message
Dim response = Await client.Chat.CreateAsync(
    MistralConstants.Models.MistralLargeLatest,
    "Hello, world!"
)

' With system message
Dim response = Await client.Chat.CreateAsync(
    MistralConstants.Models.MistralLargeLatest,
    "You are a helpful assistant.",  ' System message
    "What is the weather like?"      ' User message
)

' With full control
Dim request As New ChatCompletionRequest With {
    .Model = MistralConstants.Models.MistralLargeLatest,
    .Messages = messages,
    .Temperature = 0.7,
    .MaxTokens = 150,
    .TopP = 0.9
}

Dim response = Await client.Chat.CreateAsync(request)
```

#### Streaming (Coming Soon)

```vb
' Note: Streaming will be implemented in a future version
Dim request As New ChatCompletionRequest With {
    .Model = MistralConstants.Models.MistralLargeLatest,
    .Messages = messages,
    .Stream = True
}

Await For Each chunk In client.Chat.CreateStreamAsync(request)
    Console.Write(chunk.Choices(0).Delta.Content)
Next
```

#### Tool/Function Calling

```vb
' Define tools
Dim tools As New List(Of Tool) From {
    New Tool With {
        .Type = "function",
        .[Function] = New ToolFunction With {
            .Name = "get_weather",
            .Description = "Get the current weather in a location",
            .Parameters = New With {
                .type = "object",
                .properties = New With {
                    .location = New With {
                        .type = "string",
                        .description = "The city and state, e.g. San Francisco, CA"
                    }
                },
                .required = {"location"}
            }
        }
    }
}

Dim request As New ChatCompletionRequest With {
    .Model = MistralConstants.Models.MistralLargeLatest,
    .Messages = messages,
    .Tools = tools,
    .ToolChoice = "auto"
}

Dim response = Await client.Chat.CreateAsync(request)
```

### Embeddings

```vb
' Single text
Dim response = Await client.Embeddings.CreateAsync(
    MistralConstants.Models.MistralEmbed,
    "This is a sample text for embedding."
)

' Multiple texts
Dim texts As New List(Of String) From {
    "First text to embed",
    "Second text to embed",
    "Third text to embed"
}

Dim response = Await client.Embeddings.CreateAsync(
    MistralConstants.Models.MistralEmbed,
    texts
)

' Access embeddings
For Each data In response.Data
    Console.WriteLine($"Embedding {data.Index}: {data.Embedding.Count} dimensions")
Next
```

### Models

```vb
' List all models
Dim models = Await client.Models.ListAsync()
For Each model In models.Data
    Console.WriteLine($"{model.Id} - {model.OwnedBy}")
Next

' Get specific model info
Dim modelInfo = Await client.Models.RetrieveAsync("mistral-large-latest")
Console.WriteLine($"Model: {modelInfo.Id}")
Console.WriteLine($"Created: {DateTimeOffset.FromUnixTimeSeconds(modelInfo.Created)}")

' Delete a fine-tuned model
Dim deleteResponse = Await client.Models.DeleteAsync("your-fine-tuned-model-id")
Console.WriteLine($"Deleted: {deleteResponse.Deleted}")
```

## Examples

### Example 1: Simple Q&A Bot

```vb
Sub Main()
    RunBotAsync().GetAwaiter().GetResult()
End Sub

Async Function RunBotAsync() As Task
    Using client As New MistralClient(Environment.GetEnvironmentVariable("MISTRAL_API_KEY"))
        
        Console.WriteLine("VB.NET Q&A Bot (type 'quit' to exit)")
        
        While True
            Console.Write("You: ")
            Dim userInput = Console.ReadLine()
            
            If userInput?.ToLower() = "quit" Then
                Exit While
            End If
            
            Try
                Dim response = Await client.Chat.CreateAsync(
                    MistralConstants.Models.MistralLargeLatest,
                    "You are a helpful assistant that answers questions about VB.NET programming.",
                    userInput
                )
                
                Console.WriteLine($"Bot: {response.Choices(0).Message.Content}")
                Console.WriteLine()
                
            Catch ex As Exception
                Console.WriteLine($"Error: {ex.Message}")
            End Try
        End While
        
    End Using
End Function
```

### Example 2: Document Summarization

```vb
Async Function SummarizeDocumentAsync(document As String) As Task(Of String)
    Using client As New MistralClient(Environment.GetEnvironmentVariable("MISTRAL_API_KEY"))
        
        Dim systemPrompt = "You are an expert at summarizing documents. Create a concise, informative summary of the following text."
        
        Dim response = Await client.Chat.CreateAsync(
            MistralConstants.Models.MistralLargeLatest,
            systemPrompt,
            document,
            temperature:=0.3,  ' Lower temperature for more focused summaries
            maxTokens:=200     ' Limit summary length
        )
        
        Return response.Choices(0).Message.Content
    End Using
End Function
```

### Example 3: Code Generation

```vb
Async Function GenerateCodeAsync(description As String) As Task(Of String)
    Using client As New MistralClient(Environment.GetEnvironmentVariable("MISTRAL_API_KEY"))
        
        Dim systemPrompt = "You are a VB.NET programming expert. Generate clean, well-documented VB.NET code based on the user's requirements."
        
        Dim response = Await client.Chat.CreateAsync(
            MistralConstants.Models.Codestral,  ' Use Codestral for code generation
            systemPrompt,
            description,
            temperature:=0.2   ' Lower temperature for more precise code
        )
        
        Return response.Choices(0).Message.Content
    End Using
End Function

' Usage
Dim code = Await GenerateCodeAsync("Create a VB.NET function that sorts a list of integers in descending order")
Console.WriteLine(code)
```

### Example 4: Semantic Search with Embeddings

```vb
Class SemanticSearch
    Private ReadOnly client As MistralClient
    Private ReadOnly documents As List(Of String)
    Private ReadOnly documentEmbeddings As List(Of List(Of Double))
    
    Sub New(apiKey As String)
        client = New MistralClient(apiKey)
        documents = New List(Of String)
        documentEmbeddings = New List(Of List(Of Double))
    End Sub
    
    Public Async Function AddDocumentAsync(document As String) As Task
        documents.Add(document)
        
        Dim response = Await client.Embeddings.CreateAsync(
            MistralConstants.Models.MistralEmbed,
            document
        )
        
        documentEmbeddings.Add(response.Data(0).Embedding)
    End Function
    
    Public Async Function SearchAsync(query As String, topK As Integer) As Task(Of List(Of String))
        Dim queryResponse = Await client.Embeddings.CreateAsync(
            MistralConstants.Models.MistralEmbed,
            query
        )
        
        Dim queryEmbedding = queryResponse.Data(0).Embedding
        
        ' Calculate cosine similarity
        Dim similarities As New List(Of (Index As Integer, Similarity As Double))
        
        For i = 0 To documentEmbeddings.Count - 1
            Dim similarity = CalculateCosineSimilarity(queryEmbedding, documentEmbeddings(i))
            similarities.Add((i, similarity))
        Next
        
        ' Return top K most similar documents
        Return similarities.
            OrderByDescending(Function(x) x.Similarity).
            Take(topK).
            Select(Function(x) documents(x.Index)).
            ToList()
    End Function
    
    Private Function CalculateCosineSimilarity(a As List(Of Double), b As List(Of Double)) As Double
        Dim dotProduct = 0.0
        Dim magnitudeA = 0.0
        Dim magnitudeB = 0.0
        
        For i = 0 To a.Count - 1
            dotProduct += a(i) * b(i)
            magnitudeA += a(i) * a(i)
            magnitudeB += b(i) * b(i)
        Next
        
        Return dotProduct / (Math.Sqrt(magnitudeA) * Math.Sqrt(magnitudeB))
    End Function
    
End Class
```

## Error Handling

The library provides specific exception types for different error scenarios:

```vb
Try
    Dim response = Await client.Chat.CreateAsync(request)
    
Catch ex As MistralAuthenticationException
    Console.WriteLine("Authentication failed. Please check your API key.")
    
Catch ex As MistralRateLimitException
    Console.WriteLine($"Rate limit exceeded. Retry after {ex.RetryAfterSeconds} seconds.")
    
Catch ex As MistralValidationException
    Console.WriteLine("Request validation failed:")
    If ex.ValidationErrors IsNot Nothing Then
        For Each kvp In ex.ValidationErrors
            Console.WriteLine($"  {kvp.Key}: {String.Join(", ", kvp.Value)}")
        Next
    End If
    
Catch ex As MistralApiException
    Console.WriteLine($"API error: {ex.Message}")
    
Catch ex As Exception
    Console.WriteLine($"Unexpected error: {ex.Message}")
    
End Try
```

## Best Practices

### 1. Resource Management

Always use `Using` statements to properly dispose of the client:

```vb
Using client As New MistralClient(apiKey)
    ' Your API calls here
End Using
```

### 2. API Key Security

- Never hard-code API keys in your source code
- Use environment variables or secure configuration
- Mask API keys in logs

```vb
' Good
Dim apiKey = Environment.GetEnvironmentVariable("MISTRAL_API_KEY")
Dim client As New MistralClient(apiKey)

' Bad - never do this
Dim client As New MistralClient("sk-your-actual-key-here")
```

### 3. Rate Limiting

Implement proper retry logic for rate limits:

```vb
Async Function CallWithRetryAsync(Of T)(apiCall As Func(Of Task(Of T)), maxRetries As Integer) As Task(Of T)
    For attempt = 1 To maxRetries
        Try
            Return Await apiCall()
        Catch ex As MistralRateLimitException
            If attempt = maxRetries Then
                Throw
            End If
            
            Dim delay = If(ex.RetryAfterSeconds, 2 ^ attempt) * 1000
            Await Task.Delay(delay)
        End Try
    Next
    
    Throw New InvalidOperationException("Should not reach here")
End Function
```

### 4. Token Management

Monitor token usage to avoid unexpected costs:

```vb
Dim response = Await client.Chat.CreateAsync(request)
Console.WriteLine($"Tokens used: {response.Usage.TotalTokens}")
Console.WriteLine($"Cost estimate: ${response.Usage.TotalTokens * 0.0001:F4}") ' Example pricing
```

### 5. Model Selection

Choose the right model for your use case:

```vb
' For complex reasoning
Dim model = MistralConstants.Models.MistralLargeLatest

' For simple tasks
Dim model = MistralConstants.Models.MistralSmall

' For code generation
Dim model = MistralConstants.Models.Codestral

' For embeddings
Dim model = MistralConstants.Models.MistralEmbed
```

## Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details on:

- Setting up the development environment
- Running tests
- Submitting pull requests
- Code style guidelines

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Support

- **Documentation**: [docs/](docs/)
- **Issues**: [GitHub Issues](https://github.com/mistralai-net/mistralai-net/issues)
- **Discussions**: [GitHub Discussions](https://github.com/mistralai-net/mistralai-net/discussions)
- **API Documentation**: [Mistral AI API Docs](https://docs.mistral.ai/)

## Changelog

See [CHANGELOG.md](CHANGELOG.md) for a detailed history of changes.
