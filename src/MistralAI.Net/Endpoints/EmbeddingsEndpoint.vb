Imports System
Imports System.Net.Http
Imports System.Threading.Tasks
Imports System.Collections.Generic

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Embeddings API endpoint for creating text embeddings.
    ''' </summary>
    Public Class EmbeddingsEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the EmbeddingsEndpoint class.
        ''' </summary>
        ''' <param name="httpClient">The HttpClient to use for API requests.</param>
        Public Sub New(httpClient As HttpClient)
            MyBase.New(httpClient)
        End Sub

        ''' <summary>
        ''' Creates embeddings for the specified input synchronously.
        ''' </summary>
        ''' <param name="request">The embedding request.</param>
        ''' <returns>The embedding response.</returns>
        Public Function Create(request As Models.Embeddings.EmbeddingRequest) As Models.Embeddings.EmbeddingResponse
            Return CreateAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Creates embeddings for the specified input asynchronously.
        ''' </summary>
        ''' <param name="request">The embedding request.</param>
        ''' <returns>A task that represents the asynchronous operation with the embedding response.</returns>
        Public Async Function CreateAsync(request As Models.Embeddings.EmbeddingRequest) As Task(Of Models.Embeddings.EmbeddingResponse)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request))
            End If

            ValidateRequest(request)
            Return Await PostAsync(Of Models.Embeddings.EmbeddingRequest, Models.Embeddings.EmbeddingResponse)("v1/embeddings", request)
        End Function

        ''' <summary>
        ''' Convenience method to create embeddings for a single text input.
        ''' </summary>
        ''' <param name="model">The model to use for embedding.</param>
        ''' <param name="input">The input text to embed.</param>
        ''' <param name="encodingFormat">The encoding format (optional).</param>
        ''' <returns>A task that represents the asynchronous operation with the embedding response.</returns>
        Public Async Function CreateAsync(model As String, input As String, Optional encodingFormat As String = Nothing) As Task(Of Models.Embeddings.EmbeddingResponse)
            Dim request As New Models.Embeddings.EmbeddingRequest With {
                .Model = model,
                .Input = input
            }

            If Not String.IsNullOrEmpty(encodingFormat) Then
                request.EncodingFormat = encodingFormat
            End If

            Return Await CreateAsync(request)
        End Function

        ''' <summary>
        ''' Convenience method to create embeddings for multiple text inputs.
        ''' </summary>
        ''' <param name="model">The model to use for embedding.</param>
        ''' <param name="inputs">The input texts to embed.</param>
        ''' <param name="encodingFormat">The encoding format (optional).</param>
        ''' <returns>A task that represents the asynchronous operation with the embedding response.</returns>
        Public Async Function CreateAsync(model As String, inputs As List(Of String), Optional encodingFormat As String = Nothing) As Task(Of Models.Embeddings.EmbeddingResponse)
            Dim request As New Models.Embeddings.EmbeddingRequest With {
                .Model = model,
                .Input = inputs
            }

            If Not String.IsNullOrEmpty(encodingFormat) Then
                request.EncodingFormat = encodingFormat
            End If

            Return Await CreateAsync(request)
        End Function

        ''' <summary>
        ''' Validates the embedding request.
        ''' </summary>
        ''' <param name="request">The request to validate.</param>
        Private Sub ValidateRequest(request As Models.Embeddings.EmbeddingRequest)
            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model is required.", NameOf(request.Model))
            End If

            If request.Input Is Nothing Then
                Throw New ArgumentNullException(NameOf(request.Input), "Input is required.")
            End If
        End Sub

    End Class

End Namespace
