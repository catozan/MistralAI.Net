Imports Newtonsoft.Json
Imports System.Collections.Generic

Namespace MistralAI.Net.Models.Embeddings

    ''' <summary>
    ''' Request object for creating embeddings.
    ''' </summary>
    Public Class EmbeddingRequest

        ''' <summary>
        ''' ID of the model to use for embeddings.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' Input text or list of texts to embed.
        ''' </summary>
        <JsonProperty("input")>
        Public Property Input As Object ' Can be String or List(Of String)

        ''' <summary>
        ''' The format to return embeddings in.
        ''' </summary>
        <JsonProperty("encoding_format", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property EncodingFormat As String

        Public Sub New()
        End Sub

        Public Sub New(model As String, input As String)
            Me.Model = model
            Me.Input = input
        End Sub

        Public Sub New(model As String, input As List(Of String))
            Me.Model = model
            Me.Input = input
        End Sub

    End Class

    ''' <summary>
    ''' Response object for embedding requests.
    ''' </summary>
    Public Class EmbeddingResponse

        ''' <summary>
        ''' The object type (always "list").
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of embedding data.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of EmbeddingData)

        ''' <summary>
        ''' The model used for generating embeddings.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' Usage statistics for the embedding request.
        ''' </summary>
        <JsonProperty("usage")>
        Public Property Usage As EmbeddingUsage

        Public Sub New()
            Data = New List(Of EmbeddingData)()
        End Sub

    End Class

    ''' <summary>
    ''' Represents individual embedding data.
    ''' </summary>
    Public Class EmbeddingData

        ''' <summary>
        ''' The object type (always "embedding").
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The embedding vector.
        ''' </summary>
        <JsonProperty("embedding")>
        Public Property Embedding As List(Of Double)

        ''' <summary>
        ''' The index of this embedding in the request.
        ''' </summary>
        <JsonProperty("index")>
        Public Property Index As Integer

        Public Sub New()
            Embedding = New List(Of Double)()
        End Sub

    End Class

    ''' <summary>
    ''' Usage statistics for embedding requests.
    ''' </summary>
    Public Class EmbeddingUsage

        ''' <summary>
        ''' Number of tokens in the prompt.
        ''' </summary>
        <JsonProperty("prompt_tokens")>
        Public Property PromptTokens As Integer

        ''' <summary>
        ''' Total number of tokens used.
        ''' </summary>
        <JsonProperty("total_tokens")>
        Public Property TotalTokens As Integer

    End Class

End Namespace
