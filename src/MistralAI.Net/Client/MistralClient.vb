Imports System
Imports System.Net.Http
Imports System.Threading.Tasks

Namespace MistralAI.Net.Client

    ''' <summary>
    ''' Main client for interacting with the Mistral AI API.
    ''' This class provides access to all Mistral AI endpoints and functionality.
    ''' </summary>
    Public Class MistralClient
        Implements IDisposable

        Private ReadOnly _httpClient As HttpClient
        Private ReadOnly _apiKey As String
        Private ReadOnly _baseUrl As String
        Private _disposed As Boolean = False

        ' Endpoint backing fields
        Private ReadOnly _chat As MistralAI.Net.Endpoints.ChatEndpoint
        Private ReadOnly _models As MistralAI.Net.Endpoints.ModelsEndpoint
        Private ReadOnly _embeddings As MistralAI.Net.Endpoints.EmbeddingsEndpoint
        Private ReadOnly _files As MistralAI.Net.Endpoints.FilesEndpoint
        'Private ReadOnly _fineTuning As MistralAI.Net.Endpoints.FineTuningEndpoint
        Private ReadOnly _batch As MistralAI.Net.Endpoints.BatchEndpoint
        Private ReadOnly _agents As MistralAI.Net.Endpoints.AgentsEndpoint
        Private ReadOnly _audio As MistralAI.Net.Endpoints.AudioEndpoint
        Private ReadOnly _ocr As MistralAI.Net.Endpoints.OcrEndpoint
        Private ReadOnly _classifiers As MistralAI.Net.Endpoints.ClassifiersEndpoint
        Private ReadOnly _fim As MistralAI.Net.Endpoints.FimEndpoint
        Private ReadOnly _beta As MistralAI.Net.Endpoints.BetaEndpoint

        ''' <summary>
        ''' Gets the Chat endpoint for chat completion operations.
        ''' </summary>
        Public ReadOnly Property Chat As MistralAI.Net.Endpoints.ChatEndpoint
            Get
                Return _chat
            End Get
        End Property

        ''' <summary>
        ''' Gets the Models endpoint for model management operations.
        ''' </summary>
        Public ReadOnly Property Models As MistralAI.Net.Endpoints.ModelsEndpoint
            Get
                Return _models
            End Get
        End Property

        ''' <summary>
        ''' Gets the Embeddings endpoint for text embedding operations.
        ''' </summary>
        Public ReadOnly Property Embeddings As MistralAI.Net.Endpoints.EmbeddingsEndpoint
            Get
                Return _embeddings
            End Get
        End Property

        ''' <summary>
        ''' Gets the Files endpoint for file management operations.
        ''' </summary>
        Public ReadOnly Property Files As MistralAI.Net.Endpoints.FilesEndpoint
            Get
                Return _files
            End Get
        End Property

        ''' <summary>
        ''' Gets the FineTuning endpoint for fine-tuning operations.
        ''' </summary>
        'Public ReadOnly Property FineTuning As MistralAI.Net.Endpoints.FineTuningEndpoint
        '    Get
        '        Return _fineTuning
        '    End Get
        'End Property

        ''' <summary>
        ''' Gets the Batch endpoint for batch processing operations.
        ''' </summary>
        Public ReadOnly Property Batch As MistralAI.Net.Endpoints.BatchEndpoint
            Get
                Return _batch
            End Get
        End Property

        ''' <summary>
        ''' Gets the Agents endpoint for AI agent operations.
        ''' </summary>
        Public ReadOnly Property Agents As MistralAI.Net.Endpoints.AgentsEndpoint
            Get
                Return _agents
            End Get
        End Property

        ''' <summary>
        ''' Gets the Audio endpoint for audio processing operations.
        ''' </summary>
        Public ReadOnly Property Audio As MistralAI.Net.Endpoints.AudioEndpoint
            Get
                Return _audio
            End Get
        End Property

        ''' <summary>
        ''' Gets the OCR endpoint for optical character recognition operations.
        ''' </summary>
        Public ReadOnly Property Ocr As MistralAI.Net.Endpoints.OcrEndpoint
            Get
                Return _ocr
            End Get
        End Property

        ''' <summary>
        ''' Gets the Classifiers endpoint for text classification operations.
        ''' </summary>
        Public ReadOnly Property Classifiers As MistralAI.Net.Endpoints.ClassifiersEndpoint
            Get
                Return _classifiers
            End Get
        End Property

        ''' <summary>
        ''' Gets the FIM (Fill-in-the-middle) endpoint for code completion operations.
        ''' </summary>
        Public ReadOnly Property Fim As MistralAI.Net.Endpoints.FimEndpoint
            Get
                Return _fim
            End Get
        End Property

        ''' <summary>
        ''' Gets the Beta endpoint for experimental features.
        ''' </summary>
        Public ReadOnly Property Beta As MistralAI.Net.Endpoints.BetaEndpoint
            Get
                Return _beta
            End Get
        End Property

        ''' <summary>
        ''' Initializes a new instance of the MistralClient class.
        ''' </summary>
        ''' <param name="apiKey">The API key for authentication with Mistral AI.</param>
        ''' <param name="baseUrl">Optional custom base URL. Defaults to official Mistral AI API URL.</param>
        ''' <param name="httpClient">Optional custom HttpClient. If not provided, a default one will be created.</param>
        Public Sub New(apiKey As String, Optional baseUrl As String = "https://api.mistral.ai/", Optional httpClient As HttpClient = Nothing)
            If String.IsNullOrWhiteSpace(apiKey) Then
                Throw New ArgumentException("API key cannot be null or empty.", NameOf(apiKey))
            End If

            _apiKey = apiKey
            _baseUrl = If(baseUrl?.EndsWith("/"), baseUrl, baseUrl & "/")
            
            If httpClient Is Nothing Then
                _httpClient = New HttpClient()
            Else
                _httpClient = httpClient
            End If

            ConfigureHttpClient()
            
            ' Initialize endpoints
            _chat = New MistralAI.Net.Endpoints.ChatEndpoint(_httpClient, _apiKey, _baseUrl)
            _models = New MistralAI.Net.Endpoints.ModelsEndpoint(_httpClient, _apiKey, _baseUrl)
            _embeddings = New MistralAI.Net.Endpoints.EmbeddingsEndpoint(_httpClient, _apiKey, _baseUrl)
            _files = New MistralAI.Net.Endpoints.FilesEndpoint(_httpClient, _apiKey, _baseUrl)
            '_fineTuning = New MistralAI.Net.Endpoints.FineTuningEndpoint(_httpClient, _apiKey, _baseUrl)
            _batch = New MistralAI.Net.Endpoints.BatchEndpoint(_httpClient, _apiKey, _baseUrl)
            _agents = New MistralAI.Net.Endpoints.AgentsEndpoint(_httpClient, _apiKey, _baseUrl)
            _audio = New MistralAI.Net.Endpoints.AudioEndpoint(_httpClient, _apiKey, _baseUrl)
            _ocr = New MistralAI.Net.Endpoints.OcrEndpoint(_httpClient, _apiKey, _baseUrl)
            _classifiers = New MistralAI.Net.Endpoints.ClassifiersEndpoint(_httpClient, _apiKey, _baseUrl)
            _fim = New MistralAI.Net.Endpoints.FimEndpoint(_httpClient, _apiKey, _baseUrl)
            _beta = New MistralAI.Net.Endpoints.BetaEndpoint(_httpClient, _apiKey, _baseUrl)
        End Sub

        ''' <summary>
        ''' Configures the HTTP client with necessary headers and settings.
        ''' </summary>
        Private Sub ConfigureHttpClient()
            _httpClient.BaseAddress = New Uri(_baseUrl)
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}")
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json")
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MistralAI.Net/1.0.0")
            _httpClient.Timeout = TimeSpan.FromMinutes(10) ' Default timeout
        End Sub

        ''' <summary>
        ''' Sets a custom timeout for HTTP requests.
        ''' </summary>
        ''' <param name="timeout">The timeout duration.</param>
        Public Sub SetTimeout(timeout As TimeSpan)
            _httpClient.Timeout = timeout
        End Sub

        ''' <summary>
        ''' Gets the current API key being used.
        ''' </summary>
        ''' <returns>The masked API key for security.</returns>
        Public Function GetMaskedApiKey() As String
            If String.IsNullOrEmpty(_apiKey) OrElse _apiKey.Length < 8 Then
                Return "****"
            End If
            Return _apiKey.Substring(0, 4) & "..." & _apiKey.Substring(_apiKey.Length - 4)
        End Function

        ''' <summary>
        ''' Gets the base URL being used for API calls.
        ''' </summary>
        ''' <returns>The base URL.</returns>
        Public Function GetBaseUrl() As String
            Return _baseUrl
        End Function

        ''' <summary>
        ''' Disposes the HTTP client and releases resources.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Protected dispose method.
        ''' </summary>
        ''' <param name="disposing">True if disposing managed resources.</param>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    _httpClient?.Dispose()
                End If
                _disposed = True
            End If
        End Sub

    End Class

End Namespace
