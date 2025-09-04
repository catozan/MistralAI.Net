# Changelog

All notable changes to this project will be documented in this file.

## [1.0.2] - 2024-09-04

### Fixed
- Fixed API endpoint URL construction causing "no Route matched with those values" errors
- Corrected base URL from `https://api.mistral.ai/v1/` to `https://api.mistral.ai/`
- Resolved double `v1/` prefix in final API URLs that caused 404 routing errors
- All endpoints now properly construct URLs like `https://api.mistral.ai/v1/chat/completions`

### Technical Details
- Base URL now properly combines with endpoint paths (`v1/chat/completions`, `v1/models`, etc.)
- Eliminates malformed URLs that were constructed as `https://api.mistral.ai/v1/v1/[endpoint]`
- Final URLs correctly format as `https://api.mistral.ai/v1/[endpoint]`
- Resolves Mistral API server rejecting requests due to invalid route patterns

### No Breaking Changes
This is a backward compatible bug fix release that maintains the same public API surface.

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
