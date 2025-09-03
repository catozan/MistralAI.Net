Imports System
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Models API endpoint for managing and retrieving model information.
    ''' </summary>
    Public Class ModelsEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the ModelsEndpoint class.
        ''' </summary>
        ''' <param name="httpClient">The HTTP client to use for requests.</param>
        Public Sub New(httpClient As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(httpClient, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Lists all available models synchronously.
        ''' </summary>
        ''' <returns>A list of available models.</returns>
        Public Function List() As Models.Models.ModelList
            Return ListAsync().GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Lists all available models asynchronously.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation with a list of available models.</returns>
        Public Async Function ListAsync() As Task(Of Models.Models.ModelList)
            Return Await GetAsync(Of Models.Models.ModelList)("v1/models")
        End Function

        ''' <summary>
        ''' Retrieves information about a specific model synchronously.
        ''' </summary>
        ''' <param name="modelId">The ID of the model to retrieve.</param>
        ''' <returns>Information about the specified model.</returns>
        Public Function Retrieve(modelId As String) As Models.Models.ModelInfo
            Return RetrieveAsync(modelId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Retrieves information about a specific model asynchronously.
        ''' </summary>
        ''' <param name="modelId">The ID of the model to retrieve.</param>
        ''' <returns>A task that represents the asynchronous operation with information about the specified model.</returns>
        Public Async Function RetrieveAsync(modelId As String) As Task(Of Models.Models.ModelInfo)
            If String.IsNullOrWhiteSpace(modelId) Then
                Throw New ArgumentException("Model ID is required.", NameOf(modelId))
            End If

            Return Await GetAsync(Of Models.Models.ModelInfo)($"v1/models/{modelId}")
        End Function

        ''' <summary>
        ''' Deletes a fine-tuned model synchronously.
        ''' </summary>
        ''' <param name="modelId">The ID of the model to delete.</param>
        ''' <returns>Deletion confirmation response.</returns>
        Public Function Delete(modelId As String) As ModelDeletionResponse
            Return DeleteAsync(modelId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Deletes a fine-tuned model asynchronously.
        ''' </summary>
        ''' <param name="modelId">The ID of the model to delete.</param>
        ''' <returns>A task that represents the asynchronous operation with deletion confirmation response.</returns>
        Public Overloads Async Function DeleteAsync(modelId As String) As Task(Of ModelDeletionResponse)
            If String.IsNullOrWhiteSpace(modelId) Then
                Throw New ArgumentException("Model ID is required.", NameOf(modelId))
            End If

            Return Await DeleteAsync(Of ModelDeletionResponse)($"v1/models/{modelId}")
        End Function

    End Class

    ''' <summary>
    ''' Response object for model deletion operations.
    ''' </summary>
    Public Class ModelDeletionResponse

        ''' <summary>
        ''' The ID of the deleted model.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type (always "model").
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' Whether the model was successfully deleted.
        ''' </summary>
        <JsonProperty("deleted")>
        Public Property Deleted As Boolean

    End Class

End Namespace
