Imports System
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Collections.Generic
Imports System.Linq
Imports Newtonsoft.Json

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Chat Completion API endpoint providing text generation capabilities.
    ''' </summary>
    Public Class ChatEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the ChatEndpoint class.
        ''' </summary>
        ''' <param name="httpClient">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(httpClient As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(httpClient, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Creates a chat completion synchronously.
        ''' </summary>
        ''' <param name="request">The chat completion request.</param>
        ''' <returns>The chat completion response.</returns>
        Public Function Create(request As Models.Chat.ChatCompletionRequest) As Models.Chat.ChatCompletionResponse
            Return CreateAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Creates a chat completion asynchronously.
        ''' </summary>
        ''' <param name="request">The chat completion request.</param>
        ''' <returns>A task that represents the asynchronous operation with the chat completion response.</returns>
        Public Async Function CreateAsync(request As Models.Chat.ChatCompletionRequest) As Task(Of Models.Chat.ChatCompletionResponse)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request))
            End If

            ValidateRequest(request)
            Dim json = JsonConvert.SerializeObject(request)
            Dim responseJson = Await PostAsync("v1/chat/completions", json)
            Return JsonConvert.DeserializeObject(Of Models.Chat.ChatCompletionResponse)(responseJson)
        End Function

        ''' <summary>
        ''' Creates a streaming chat completion asynchronously.
        ''' </summary>
        ''' <param name="request">The chat completion request with streaming enabled.</param>
        ''' <returns>An async enumerable of chat completion chunks.</returns>
        Public Function CreateStreamAsync(request As Models.Chat.ChatCompletionRequest) As Task(Of IAsyncEnumerable(Of Models.Chat.ChatCompletionChunk))
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request))
            End If

            ValidateRequest(request)
            request.Stream = True

            ' This would require SSE (Server-Sent Events) implementation
            ' For now, throwing not implemented exception
            Throw New NotImplementedException("Streaming support will be implemented in a future version.")
        End Function

        ''' <summary>
        ''' Creates a chat completion with a simplified interface.
        ''' </summary>
        ''' <param name="model">The model to use.</param>
        ''' <param name="messages">The conversation messages.</param>
        ''' <param name="temperature">Optional temperature setting.</param>
        ''' <param name="maxTokens">Optional maximum tokens.</param>
        ''' <returns>The chat completion response.</returns>
        Public Async Function CreateAsync(model As String, messages As List(Of Models.Chat.ChatMessage), Optional temperature As Double? = Nothing, Optional maxTokens As Integer? = Nothing) As Task(Of Models.Chat.ChatCompletionResponse)
            Dim request As New Models.Chat.ChatCompletionRequest With {
                .Model = model,
                .Messages = messages,
                .Temperature = temperature,
                .MaxTokens = maxTokens
            }

            Return Await CreateAsync(request)
        End Function

        ''' <summary>
        ''' Creates a simple chat completion with a single user message.
        ''' </summary>
        ''' <param name="model">The model to use.</param>
        ''' <param name="userMessage">The user message.</param>
        ''' <param name="temperature">Optional temperature setting.</param>
        ''' <param name="maxTokens">Optional maximum tokens.</param>
        ''' <returns>The chat completion response.</returns>
        Public Async Function CreateAsync(model As String, userMessage As String, Optional temperature As Double? = Nothing, Optional maxTokens As Integer? = Nothing) As Task(Of Models.Chat.ChatCompletionResponse)
            Dim messages As New List(Of Models.Chat.ChatMessage) From {
                New Models.Chat.ChatMessage("user", userMessage)
            }

            Return Await CreateAsync(model, messages, temperature, maxTokens)
        End Function

        ''' <summary>
        ''' Creates a chat completion with system and user messages.
        ''' </summary>
        ''' <param name="model">The model to use.</param>
        ''' <param name="systemMessage">The system message.</param>
        ''' <param name="userMessage">The user message.</param>
        ''' <param name="temperature">Optional temperature setting.</param>
        ''' <param name="maxTokens">Optional maximum tokens.</param>
        ''' <returns>The chat completion response.</returns>
        Public Async Function CreateAsync(model As String, systemMessage As String, userMessage As String, Optional temperature As Double? = Nothing, Optional maxTokens As Integer? = Nothing) As Task(Of Models.Chat.ChatCompletionResponse)
            Dim messages As New List(Of Models.Chat.ChatMessage) From {
                New Models.Chat.ChatMessage("system", systemMessage),
                New Models.Chat.ChatMessage("user", userMessage)
            }

            Return Await CreateAsync(model, messages, temperature, maxTokens)
        End Function

        ''' <summary>
        ''' Validates the chat completion request.
        ''' </summary>
        ''' <param name="request">The request to validate.</param>
        Private Sub ValidateRequest(request As Models.Chat.ChatCompletionRequest)
            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model is required.", NameOf(request.Model))
            End If

            If request.Messages Is Nothing OrElse request.Messages.Count = 0 Then
                Throw New ArgumentException("At least one message is required.", NameOf(request.Messages))
            End If

            For Each message In request.Messages
                If String.IsNullOrWhiteSpace(message.Role) Then
                    Throw New ArgumentException("Message role is required.")
                End If

                If String.IsNullOrWhiteSpace(message.Content) AndAlso 
                   (message.ToolCalls Is Nothing OrElse message.ToolCalls.Count = 0) Then
                    Throw New ArgumentException("Message content or tool calls are required.")
                End If
            Next

            If request.Temperature.HasValue AndAlso (request.Temperature < 0 OrElse request.Temperature > 2) Then
                Throw New ArgumentOutOfRangeException(NameOf(request.Temperature), "Temperature must be between 0 and 2.")
            End If

            If request.TopP.HasValue AndAlso (request.TopP < 0 OrElse request.TopP > 1) Then
                Throw New ArgumentOutOfRangeException(NameOf(request.TopP), "TopP must be between 0 and 1.")
            End If

            If request.MaxTokens.HasValue AndAlso request.MaxTokens <= 0 Then
                Throw New ArgumentOutOfRangeException(NameOf(request.MaxTokens), "MaxTokens must be positive.")
            End If
        End Sub

    End Class

End Namespace
