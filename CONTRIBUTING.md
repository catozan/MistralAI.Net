# Contributing to MistralAI.Net

We welcome contributions to MistralAI.Net! This document provides guidelines for contributing to the project.

## Table of Contents

- [Code of Conduct](#code-of-conduct)
- [Getting Started](#getting-started)
- [Development Environment Setup](#development-environment-setup)
- [Making Changes](#making-changes)
- [Testing](#testing)
- [Submitting Changes](#submitting-changes)
- [Code Style Guidelines](#code-style-guidelines)
- [Documentation](#documentation)

## Code of Conduct

This project adheres to a code of conduct. By participating, you are expected to uphold this code. Please report unacceptable behavior to the project maintainers.

## Getting Started

1. Fork the repository on GitHub
2. Clone your fork locally:
   ```bash
   git clone https://github.com/yourusername/MistralAI.Net.git
   cd MistralAI.Net
   ```
3. Set up the development environment (see below)

## Development Environment Setup

### Requirements

- **Visual Studio 2019+** or **VS Code** with VB.NET support
- **.NET Framework 4.8+** and **.NET 6.0+**
- **Git**
- **Mistral AI API Key** (for testing)

### Setup Steps

1. **Clone and restore packages**:
   ```bash
   git clone https://github.com/yourusername/MistralAI.Net.git
   cd MistralAI.Net
   dotnet restore
   ```

2. **Set up API key** (for testing):
   ```bash
   # Windows
   setx MISTRAL_API_KEY "your-api-key-here"
   
   # Or create a .env file in the project root
   echo MISTRAL_API_KEY=your-api-key-here > .env
   ```

3. **Build the solution**:
   ```bash
   dotnet build
   ```

4. **Run tests**:
   ```bash
   dotnet test
   ```

## Making Changes

### Branch Naming

- **Feature branches**: `feature/your-feature-name`
- **Bug fixes**: `fix/bug-description`
- **Documentation**: `docs/what-you-changed`

### Commit Messages

Use clear, descriptive commit messages:

- **feat**: A new feature
- **fix**: A bug fix
- **docs**: Documentation changes
- **style**: Code style changes (formatting, etc.)
- **refactor**: Code refactoring
- **test**: Adding or updating tests
- **chore**: Maintenance tasks

Examples:
```
feat: add support for streaming chat completions
fix: handle rate limiting in embeddings endpoint
docs: update README with new examples
test: add unit tests for file upload validation
```

## Testing

### Running Tests

```bash
# Run all tests
dotnet test

# Run specific test project
dotnet test src/MistralAI.Net.Tests

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Writing Tests

- Write unit tests for all new functionality
- Use descriptive test names: `Should_ReturnError_When_ApiKeyIsInvalid`
- Follow AAA pattern: Arrange, Act, Assert
- Mock external dependencies

Example test structure:
```vb
<Test>
Public Sub Should_CreateChatCompletion_When_ValidRequestProvided()
    ' Arrange
    Dim client As New MistralClient("test-key")
    Dim request As New ChatCompletionRequest()
    
    ' Act
    Dim result = client.Chat.CreateAsync(request).Result
    
    ' Assert
    Assert.IsNotNull(result)
    Assert.IsTrue(result.Choices.Count > 0)
End Sub
```

### Integration Tests

- Test against real API endpoints (with valid API key)
- Use separate test configuration
- Clean up resources after tests

## Submitting Changes

### Pull Request Process

1. **Update documentation** if needed
2. **Add or update tests** for your changes
3. **Ensure all tests pass**: `dotnet test`
4. **Build successfully**: `dotnet build`
5. **Update CHANGELOG.md** with your changes
6. **Submit pull request** with clear description

### Pull Request Template

```markdown
## Description
Brief description of changes made.

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Documentation update
- [ ] Refactoring
- [ ] Other (please describe)

## Testing
- [ ] Unit tests added/updated
- [ ] Integration tests added/updated
- [ ] All tests pass
- [ ] Manual testing completed

## Documentation
- [ ] README updated
- [ ] API documentation updated
- [ ] Code comments added/updated
- [ ] Examples added/updated

## Checklist
- [ ] Code follows project style guidelines
- [ ] Self-review completed
- [ ] Peer review requested if needed
```

## Code Style Guidelines

### VB.NET Conventions

1. **Naming**:
   - **Classes**: PascalCase (`MistralClient`)
   - **Methods**: PascalCase (`CreateAsync`)
   - **Properties**: PascalCase (`ApiKey`)
   - **Variables**: camelCase (`apiResponse`)
   - **Constants**: PascalCase (`DefaultTimeout`)

2. **Structure**:
   ```vb
   Public Class ExampleClass
       ' Constants first
       Private Const DefaultRetries As Integer = 3
       
       ' Fields
       Private ReadOnly _httpClient As HttpClient
       
       ' Constructors
       Public Sub New(apiKey As String)
           ' Constructor implementation
       End Sub
       
       ' Properties
       Public Property ApiKey As String
       
       ' Methods
       Public Async Function CreateAsync() As Task(Of Response)
           ' Method implementation
       End Function
   End Class
   ```

3. **Documentation**:
   ```vb
   ''' <summary>
   ''' Creates a new chat completion using the specified model.
   ''' </summary>
   ''' <param name="model">The model to use for completion.</param>
   ''' <param name="message">The user message.</param>
   ''' <returns>The chat completion response.</returns>
   Public Async Function CreateAsync(model As String, message As String) As Task(Of ChatCompletionResponse)
   ```

4. **Error Handling**:
   ```vb
   Try
       Dim result = Await ApiCallAsync()
       Return result
   Catch ex As HttpRequestException
       Throw New MistralApiException("API request failed", ex)
   End Try
   ```

### File Organization

```
src/MistralAI.Net/
├── Client/                 # Core client classes
├── Endpoints/              # API endpoint implementations
├── Models/                 # Request/response models
│   ├── Chat/              # Chat-related models
│   ├── Embeddings/        # Embedding models
│   └── Common/            # Shared models
├── Utils/                 # Utility classes
└── Exceptions/            # Custom exceptions
```

### Dependencies

- **Minimize dependencies**: Only add necessary NuGet packages
- **Target multiple frameworks**: Support .NET Framework 4.8, .NET 6.0, .NET 8.0
- **Use standard libraries**: Prefer System.* over third-party when possible

## Documentation

### Code Documentation

- **XML comments** for all public APIs
- **Clear parameter descriptions**
- **Usage examples** for complex methods
- **Exception documentation**

### README Updates

When adding new features:
1. Update the features list
2. Add usage examples
3. Update the API coverage section
4. Add to the table of contents if needed

### API Documentation

- Keep `docs/README.md` updated with new endpoints
- Add examples for new functionality
- Update the API reference section
- Include error handling examples

## Release Process

### Versioning

We follow [Semantic Versioning](https://semver.org/):
- **MAJOR**: Breaking changes
- **MINOR**: New features (backward compatible)
- **PATCH**: Bug fixes (backward compatible)

### Changelog

Update `CHANGELOG.md` with:
- **Added**: New features
- **Changed**: Changes in existing functionality
- **Deprecated**: Soon-to-be removed features
- **Removed**: Now removed features
- **Fixed**: Bug fixes
- **Security**: Security fixes

## Getting Help

- **GitHub Issues**: Report bugs or request features
- **GitHub Discussions**: Ask questions or discuss ideas
- **Documentation**: Check the docs/ directory
- **Examples**: See examples/ directory for usage patterns

## Recognition

Contributors will be recognized in:
- README.md contributors section
- Release notes
- GitHub contributors page

Thank you for contributing to MistralAI.Net! 🎉
