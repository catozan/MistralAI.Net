# Changelog

All notable changes to this project will be documented in this file.

## [1.0.1] - 2024-12-xx

### Fixed
- Fixed HttpClient BaseAddress configuration in BaseEndpoint constructor
- Added proper authorization headers setup during endpoint initialization
- Updated MistralClient to use correct Mistral API v1 endpoint URL
- Resolved "invalid request URI" runtime errors that occurred when HttpClient lacked proper BaseAddress configuration
- Fixed all endpoint initialization parameter references for consistent behavior

### Technical Details
- BaseEndpoint now properly configures `HttpClient.BaseAddress` to the provided base URL
- Authorization headers are correctly set with `Bearer {apiKey}` format during endpoint initialization
- Mistral API base URL corrected from `https://api.mistral.ai` to `https://api.mistral.ai/v1/`
- All endpoint constructors updated with consistent parameter handling (`_baseUrl` vs `baseUrl`)

### No Breaking Changes
This is a backward compatible bug fix release that maintains the same public API surface.

## [1.0.0] - 2024-12-xx

### Added
- Initial release of MistralAI.Net library
- Complete implementation of Mistral AI API client
- Support for .NET Framework 4.8, .NET 6.0, and .NET 8.0
- Comprehensive endpoint coverage:
  - Chat completions
  - Text completions  
  - Embeddings
  - Models management
  - Files operations
  - Batch processing
  - Agents functionality
  - Audio processing
  - OCR capabilities
  - Text classification
  - Fill-in-the-middle completions
  - Beta features

### Features
- Async/await support for all operations
- Strongly typed request/response models
- Comprehensive error handling
- Multi-framework compatibility
- Professional documentation
- Unit test coverage
