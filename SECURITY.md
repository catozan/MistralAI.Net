# Security Policy

## Supported Versions

We actively support the following versions of MistralAI.Net with security updates:

| Version | Supported          |
| ------- | ------------------ |
| 1.0.x   | :white_check_mark: |

## Reporting a Vulnerability

We take security seriously. If you discover a security vulnerability in MistralAI.Net, please follow these steps:

### 1. **DO NOT** create a public GitHub issue

Security vulnerabilities should not be disclosed publicly until they have been addressed.

### 2. Report privately

Send an email to: **security@mistralai-net.dev** (when repository is live)

Or use GitHub's private vulnerability reporting feature:
1. Go to the repository's Security tab
2. Click "Report a vulnerability"
3. Fill out the security advisory form

### 3. Include details

Please include:
- **Description** of the vulnerability
- **Steps to reproduce** the issue
- **Potential impact** assessment
- **Suggested fix** (if you have one)
- **Your contact information**

### 4. Response timeline

- **24 hours**: Initial response acknowledging receipt
- **72 hours**: Initial assessment and severity classification
- **7 days**: Detailed investigation and fix development
- **14 days**: Security patch release (for critical issues)

## Security Best Practices

When using MistralAI.Net, follow these security guidelines:

### 1. API Key Management

**✅ DO:**
```vb
' Use environment variables
Dim apiKey = Environment.GetEnvironmentVariable("MISTRAL_API_KEY")
Dim client As New MistralClient(apiKey)

' Use secure configuration files
Dim config = ConfigurationManager.AppSettings("MistralApiKey")
Dim client As New MistralClient(config)
```

**❌ DON'T:**
```vb
' Never hardcode API keys
Dim client As New MistralClient("sk-your-actual-key-here") ' BAD!

' Don't log API keys
Console.WriteLine($"Using key: {apiKey}") ' BAD!
```

### 2. Input Validation

**✅ DO:**
```vb
' Validate and sanitize user inputs
If String.IsNullOrWhiteSpace(userInput) Then
    Throw New ArgumentException("Input cannot be empty")
End If

' Limit input length
If userInput.Length > 10000 Then
    Throw New ArgumentException("Input too long")
End If
```

### 3. Error Handling

**✅ DO:**
```vb
Try
    Dim response = Await client.Chat.CreateAsync(request)
Catch ex As MistralApiException
    ' Log error details but not sensitive data
    logger.LogError("API call failed: {Message}", ex.Message)
    ' Don't expose internal details to users
    Throw New ApplicationException("Service temporarily unavailable")
End Try
```

### 4. Network Security

- **Use HTTPS**: The library enforces HTTPS for all API calls
- **Validate certificates**: Don't disable SSL certificate validation
- **Use proper timeouts**: Configure reasonable timeout values
- **Monitor API usage**: Watch for unusual patterns that might indicate abuse

### 5. Data Handling

- **Minimize data retention**: Don't store API responses longer than necessary
- **Encrypt sensitive data**: Use encryption for any stored sensitive information
- **Audit logging**: Log API usage for security monitoring
- **Data sanitization**: Clean sensitive data from logs and error messages

## Common Security Issues

### 1. API Key Exposure

**Risk**: API keys in source code, logs, or configuration files
**Mitigation**: Use environment variables, secure key management systems

### 2. Input Injection

**Risk**: Malicious input being passed to the API
**Mitigation**: Validate and sanitize all user inputs

### 3. Information Disclosure

**Risk**: Exposing sensitive information in error messages
**Mitigation**: Generic error messages for users, detailed logs for administrators

### 4. Rate Limit Bypass

**Risk**: Excessive API usage leading to service disruption
**Mitigation**: Implement client-side rate limiting and monitoring

## Secure Configuration Examples

### 1. Development Environment

```xml
<!-- app.config - Use user secrets in development -->
<configuration>
  <appSettings>
    <add key="MistralApiKey" value="development-key-here" />
    <add key="MistralBaseUrl" value="https://api.mistral.ai/" />
  </appSettings>
</configuration>
```

### 2. Production Environment

```vb
' Use Azure Key Vault or similar secure storage
Dim keyVaultClient As New KeyVaultClient()
Dim apiKey = Await keyVaultClient.GetSecretAsync("mistral-api-key")
Dim client As New MistralClient(apiKey.Value)
```

### 3. Environment Variables

```bash
# Set environment variables securely
export MISTRAL_API_KEY="your-secure-api-key"
export MISTRAL_TIMEOUT="30"
export MISTRAL_MAX_RETRIES="3"
```

## Security Monitoring

### Recommended Logging

```vb
' Log security-relevant events
logger.LogInformation("API client initialized for user {UserId}", userId)
logger.LogWarning("Rate limit approached for user {UserId}", userId)
logger.LogError("Authentication failed for user {UserId}", userId)

' Don't log sensitive data
' BAD: logger.LogDebug("API key: {ApiKey}", apiKey)
' BAD: logger.LogInfo("User input: {Input}", sensitiveUserData)
```

### Metrics to Monitor

- **API call frequency** per user/IP
- **Error rates** by endpoint
- **Authentication failures**
- **Unusual usage patterns**
- **Response times** (potential DoS indicators)

## Vulnerability Disclosure

We follow responsible disclosure practices:

1. **Private reporting** to security team
2. **Investigation and fix** development
3. **Security advisory** publication
4. **Public disclosure** after fix is available
5. **Credit** to reporter (if desired)

## Contact

For security-related questions or reports:
- **Email**: security@mistralai-net.dev (when live)
- **GitHub**: Use private vulnerability reporting
- **Emergency**: For critical vulnerabilities requiring immediate attention

## Updates

This security policy is reviewed and updated regularly. Check back for the latest guidelines and best practices.

---

**Last Updated**: September 3, 2024  
**Version**: 1.0
