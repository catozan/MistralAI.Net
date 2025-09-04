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
        ' Private ReadOnly _fineTuning As MistralAI.Net.Endpoints.FineTuningEndpoint
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
        ''' Gets the Embeddings endpoint for embedding operations.
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

        ' ''' <summary>
        ' ''' Gets the FineTuning endpoint for fine-tuning operations.
        ' ''' </summary>
        ' Public ReadOnly Property FineTuning As MistralAI.Net.Endpoints.FineTuningEndpoint
        '     Get
        '         Return _fineTuning
        '     End Get
        ' End Property

        ''' <summary>
        ''' Gets the Batch endpoint for batch processing operations.
        ''' </summary>
        Public ReadOnly Property Batch As MistralAI.Net.Endpoints.BatchEndpoint
            Get
                Return _batch
            End Get
        End Property

        ''' <summary>
        ''' Gets the Agents endpoint for agent management operations.
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
        ''' Gets the Classifiers endpoint for classification operations.
        ''' </summary>
        Public ReadOnly Property Classifiers As MistralAI.Net.Endpoints.ClassifiersEndpoint
            Get
                Return _classifiers
            End Get
        End Property

        ''' <summary>
        ''' Gets the FIM (Fill-In-the-Middle) endpoint for code completion operations.
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
        ''' <param name="apiKey">The Mistral AI API key.</param>
        ''' <param name="baseUrl">The base URL for the API. Defaults to https://api.mistral.ai.</param>
        ''' <param name="httpClient">Optional HttpClient instance. If not provided, a new instance will be created.</param>
        Public Sub New(apiKey As String, Optional baseUrl As String = "https://api.mistral.ai/", Optional httpClient As HttpClient = Nothing)
            If String.IsNullOrWhiteSpace(apiKey) Then
                Throw New ArgumentException("API key cannot be null or empty.", NameOf(apiKey))
            End If

            If String.IsNullOrWhiteSpace(baseUrl) Then
                Throw New ArgumentException("Base URL cannot be null or empty.", NameOf(baseUrl))
            End If

            _apiKey = apiKey
            _baseUrl = If(baseUrl.EndsWith("/"), baseUrl, baseUrl + "/")
            _httpClient = If(httpClient, New HttpClient())

            ' Initialize all endpoints
            _chat = New MistralAI.Net.Endpoints.ChatEndpoint(_httpClient, apiKey, _baseUrl)
            _models = New MistralAI.Net.Endpoints.ModelsEndpoint(_httpClient, apiKey, _baseUrl)
            _embeddings = New MistralAI.Net.Endpoints.EmbeddingsEndpoint(_httpClient, apiKey, _baseUrl)
            _files = New MistralAI.Net.Endpoints.FilesEndpoint(_httpClient, apiKey, _baseUrl)
            ' _fineTuning = New MistralAI.Net.Endpoints.FineTuningEndpoint(_httpClient, apiKey, _baseUrl)
            _batch = New MistralAI.Net.Endpoints.BatchEndpoint(_httpClient, apiKey, _baseUrl)
            _agents = New MistralAI.Net.Endpoints.AgentsEndpoint(_httpClient, apiKey, _baseUrl)
            _audio = New MistralAI.Net.Endpoints.AudioEndpoint(_httpClient, apiKey, _baseUrl)
            _ocr = New MistralAI.Net.Endpoints.OcrEndpoint(_httpClient, apiKey, _baseUrl)
            _classifiers = New MistralAI.Net.Endpoints.ClassifiersEndpoint(_httpClient, apiKey, _baseUrl)
            _fim = New MistralAI.Net.Endpoints.FimEndpoint(_httpClient, apiKey, _baseUrl)
            _beta = New MistralAI.Net.Endpoints.BetaEndpoint(_httpClient, apiKey, _baseUrl)
        End Sub

        ''' <summary>
        ''' Releases all resources used by the MistralClient.
        ''' </summary>
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ''' <summary>
        ''' Releases the unmanaged resources used by the MistralClient and optionally releases the managed resources.
        ''' </summary>
        ''' <param name="disposing">True to release both managed and unmanaged resources; False to release only unmanaged resources.</param>
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not _disposed Then
                If disposing Then
                    _httpClient?.Dispose()
                End If
                _disposed = True
            End If
        End Sub

        ''' <summary>
        ''' Gets the API key being used by this client.
        ''' </summary>
        Public ReadOnly Property ApiKey As String
            Get
                Return _apiKey
            End Get
        End Property

        ''' <summary>
        ''' Gets the base URL being used by this client.
        ''' </summary>
        Public ReadOnly Property BaseUrl As String
            Get
                Return _baseUrl
            End Get
        End Property

        ''' <summary>
        ''' Gets a value indicating whether this instance has been disposed.
        ''' </summary>
        Public ReadOnly Property IsDisposed As Boolean
            Get
                Return _disposed
            End Get
        End Property

    End Class

End Namespace
