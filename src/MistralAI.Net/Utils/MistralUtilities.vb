Imports System

Namespace Utils

    ''' <summary>
    ''' Contains constant values used throughout the library.
    ''' </summary>
    Public Class MistralConstants

        ''' <summary>
        ''' The default base URL for the Mistral AI API.
        ''' </summary>
        Public Const DefaultBaseUrl As String = "https://api.mistral.ai/"

        ''' <summary>
        ''' Available Mistral AI models.
        ''' </summary>
        Public Class Models

            ''' <summary>
            ''' The latest large Mistral model.
            ''' </summary>
            Public Const MistralLargeLatest As String = "mistral-large-latest"

            ''' <summary>
            ''' Mistral Large 2 model.
            ''' </summary>
            Public Const MistralLarge2 As String = "mistral-large-2"

            ''' <summary>
            ''' Mistral Small model.
            ''' </summary>
            Public Const MistralSmall As String = "mistral-small-latest"

            ''' <summary>
            ''' Codestral model for code generation.
            ''' </summary>
            Public Const Codestral As String = "codestral-latest"

            ''' <summary>
            ''' Mistral embedding model.
            ''' </summary>
            Public Const MistralEmbed As String = "mistral-embed"

            ''' <summary>
            ''' Pixtral model for vision tasks.
            ''' </summary>
            Public Const Pixtral As String = "pixtral-12b-latest"

        End Class

        ''' <summary>
        ''' Message roles for chat conversations.
        ''' </summary>
        Public Class Roles

            ''' <summary>
            ''' System message role for instructions.
            ''' </summary>
            Public Const System As String = "system"

            ''' <summary>
            ''' User message role for user input.
            ''' </summary>
            Public Const User As String = "user"

            ''' <summary>
            ''' Assistant message role for AI responses.
            ''' </summary>
            Public Const Assistant As String = "assistant"

            ''' <summary>
            ''' Tool message role for tool call responses.
            ''' </summary>
            Public Const Tool As String = "tool"

        End Class

        ''' <summary>
        ''' Finish reasons for chat completions.
        ''' </summary>
        Public Class FinishReasons

            ''' <summary>
            ''' The model finished generating the response naturally.
            ''' </summary>
            Public Const [Stop] As String = "stop"

            ''' <summary>
            ''' The response was truncated due to length limits.
            ''' </summary>
            Public Const Length As String = "length"

            ''' <summary>
            ''' The model made a tool call.
            ''' </summary>
            Public Const ToolCalls As String = "tool_calls"

            ''' <summary>
            ''' The response was filtered by content policy.
            ''' </summary>
            Public Const ContentFilter As String = "content_filter"

        End Class

        ''' <summary>
        ''' Encoding formats for embeddings.
        ''' </summary>
        Public Class EncodingFormats

            ''' <summary>
            ''' Float encoding format.
            ''' </summary>
            Public Const Float As String = "float"

            ''' <summary>
            ''' Base64 encoding format.
            ''' </summary>
            Public Const Base64 As String = "base64"

        End Class

    End Class

    ''' <summary>
    ''' Helper class for common operations.
    ''' </summary>
    Public Class MistralHelpers

        ''' <summary>
        ''' Validates an API key format.
        ''' </summary>
        ''' <param name="apiKey">The API key to validate.</param>
        ''' <returns>True if the API key appears to be valid.</returns>
        Public Shared Function IsValidApiKey(apiKey As String) As Boolean
            If String.IsNullOrWhiteSpace(apiKey) Then
                Return False
            End If

            ' Basic validation - API keys typically start with certain prefixes
            Return apiKey.Length > 10 AndAlso Not apiKey.Contains(" ")
        End Function

        ''' <summary>
        ''' Masks an API key for secure display.
        ''' </summary>
        ''' <param name="apiKey">The API key to mask.</param>
        ''' <returns>The masked API key.</returns>
        Public Shared Function MaskApiKey(apiKey As String) As String
            If String.IsNullOrEmpty(apiKey) Then
                Return "****"
            End If

            If apiKey.Length <= 8 Then
                Return "****"
            End If

            Return apiKey.Substring(0, 4) & "..." & apiKey.Substring(apiKey.Length - 4)
        End Function

        ''' <summary>
        ''' Validates temperature parameter.
        ''' </summary>
        ''' <param name="temperature">The temperature value to validate.</param>
        ''' <returns>True if the temperature is valid.</returns>
        Public Shared Function IsValidTemperature(temperature As Double?) As Boolean
            If Not temperature.HasValue Then
                Return True
            End If

            Return temperature.Value >= 0 AndAlso temperature.Value <= 2
        End Function

        ''' <summary>
        ''' Validates top_p parameter.
        ''' </summary>
        ''' <param name="topP">The top_p value to validate.</param>
        ''' <returns>True if the top_p is valid.</returns>
        Public Shared Function IsValidTopP(topP As Double?) As Boolean
            If Not topP.HasValue Then
                Return True
            End If

            Return topP.Value >= 0 AndAlso topP.Value <= 1
        End Function

        ''' <summary>
        ''' Estimates token count for text (rough approximation).
        ''' </summary>
        ''' <param name="text">The text to estimate tokens for.</param>
        ''' <returns>Estimated token count.</returns>
        Public Shared Function EstimateTokenCount(text As String) As Integer
            If String.IsNullOrEmpty(text) Then
                Return 0
            End If

            ' Rough approximation: 1 token ≈ 4 characters
            Return Math.Max(1, Math.Ceiling(text.Length / 4.0))
        End Function

    End Class

End Namespace
