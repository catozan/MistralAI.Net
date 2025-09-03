Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports MistralAI.Net.Models
Imports MistralAI.Net.Exceptions

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Handles Fill-in-the-Middle (FIM) code completion operations for the Mistral AI API.
    ''' </summary>
    Public Class FimEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the FimEndpoint class.
        ''' </summary>
        ''' <param name="client">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(client As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(client, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Performs fill-in-the-middle code completion.
        ''' </summary>
        ''' <param name="request">The FIM completion request.</param>
        ''' <returns>The completion result.</returns>
        Public Function Complete(request As Models.Fim.FimRequest) As Models.Fim.FimResponse
            Return CompleteAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Performs fill-in-the-middle code completion asynchronously.
        ''' </summary>
        ''' <param name="request">The FIM completion request.</param>
        ''' <returns>The completion result.</returns>
        Public Async Function CompleteAsync(request As Models.Fim.FimRequest) As Task(Of Models.Fim.FimResponse)
            ValidateRequest(request)
            Return Await PostAsync(Of Models.Fim.FimRequest, Models.Fim.FimResponse)("v1/fim/completions", request)
        End Function

        ''' <summary>
        ''' Convenience method for simple code completion.
        ''' </summary>
        ''' <param name="model">The model to use.</param>
        ''' <param name="prefix">The code before the cursor.</param>
        ''' <param name="suffix">The code after the cursor.</param>
        ''' <param name="maxTokens">Maximum tokens to generate.</param>
        ''' <returns>The completion result.</returns>
        Public Function CompleteCode(model As String, prefix As String, suffix As String, Optional maxTokens As Integer? = Nothing) As Models.Fim.FimResponse
            Return CompleteCodeAsync(model, prefix, suffix, maxTokens).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Convenience method for simple code completion asynchronously.
        ''' </summary>
        ''' <param name="model">The model to use.</param>
        ''' <param name="prefix">The code before the cursor.</param>
        ''' <param name="suffix">The code after the cursor.</param>
        ''' <param name="maxTokens">Maximum tokens to generate.</param>
        ''' <returns>The completion result.</returns>
        Public Async Function CompleteCodeAsync(model As String, prefix As String, suffix As String, Optional maxTokens As Integer? = Nothing) As Task(Of Models.Fim.FimResponse)
            Dim request As New Models.Fim.FimRequest With {
                .Model = model,
                .Prefix = prefix,
                .Suffix = suffix,
                .MaxTokens = maxTokens
            }

            Return Await CompleteAsync(request)
        End Function

        Private Sub ValidateRequest(request As Models.Fim.FimRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "FIM request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If

            If String.IsNullOrWhiteSpace(request.Prefix) AndAlso String.IsNullOrWhiteSpace(request.Suffix) Then
                Throw New ArgumentException("Either prefix or suffix must be provided.", NameOf(request))
            End If
        End Sub

    End Class

End Namespace
