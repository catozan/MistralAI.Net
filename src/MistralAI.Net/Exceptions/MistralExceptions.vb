Imports System
Imports System.Collections.Generic

Namespace Exceptions

    ''' <summary>
    ''' Base exception class for all Mistral AI API related exceptions.
    ''' </summary>
    Public Class MistralApiException
        Inherits Exception

        ''' <summary>
        ''' Initializes a new instance of the MistralApiException class.
        ''' </summary>
        Public Sub New()
            MyBase.New()
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralApiException class with a specified error message.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralApiException class with a specified error message and inner exception.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

    End Class

    ''' <summary>
    ''' Exception thrown when authentication fails.
    ''' </summary>
    Public Class MistralAuthenticationException
        Inherits MistralApiException

        ''' <summary>
        ''' Initializes a new instance of the MistralAuthenticationException class.
        ''' </summary>
        Public Sub New()
            MyBase.New("Authentication failed. Please check your API key.")
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralAuthenticationException class with a specified error message.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralAuthenticationException class with a specified error message and inner exception.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

    End Class

    ''' <summary>
    ''' Exception thrown when rate limits are exceeded.
    ''' </summary>
    Public Class MistralRateLimitException
        Inherits MistralApiException

        ''' <summary>
        ''' Gets the retry after seconds if provided by the API.
        ''' </summary>
        Public ReadOnly Property RetryAfterSeconds As Integer?

        ''' <summary>
        ''' Initializes a new instance of the MistralRateLimitException class.
        ''' </summary>
        Public Sub New()
            MyBase.New("Rate limit exceeded. Please try again later.")
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralRateLimitException class with a specified error message.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralRateLimitException class with retry information.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="retryAfterSeconds">The number of seconds to wait before retrying.</param>
        Public Sub New(message As String, retryAfterSeconds As Integer?)
            MyBase.New(message)
            Me.RetryAfterSeconds = retryAfterSeconds
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralRateLimitException class with a specified error message and inner exception.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

    End Class

    ''' <summary>
    ''' Exception thrown when validation fails for request parameters.
    ''' </summary>
    Public Class MistralValidationException
        Inherits MistralApiException

        ''' <summary>
        ''' Gets the validation errors if available.
        ''' </summary>
        Public ReadOnly Property ValidationErrors As Dictionary(Of String, String())

        ''' <summary>
        ''' Initializes a new instance of the MistralValidationException class.
        ''' </summary>
        Public Sub New()
            MyBase.New("Validation failed.")
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralValidationException class with a specified error message.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralValidationException class with validation errors.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="validationErrors">The validation errors.</param>
        Public Sub New(message As String, validationErrors As Dictionary(Of String, String()))
            MyBase.New(message)
            Me.ValidationErrors = validationErrors
        End Sub

        ''' <summary>
        ''' Initializes a new instance of the MistralValidationException class with a specified error message and inner exception.
        ''' </summary>
        ''' <param name="message">The message that describes the error.</param>
        ''' <param name="innerException">The exception that is the cause of the current exception.</param>
        Public Sub New(message As String, innerException As Exception)
            MyBase.New(message, innerException)
        End Sub

    End Class

End Namespace
