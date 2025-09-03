Imports System
Imports System.Collections.Generic
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Batch API endpoint for creating and managing batch processing jobs.
    ''' </summary>
    Public Class BatchEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the BatchEndpoint class.
        ''' </summary>
        ''' <param name="httpClient">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(httpClient As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(httpClient, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Creates a batch processing job.
        ''' </summary>
        ''' <param name="request">The batch job request.</param>
        ''' <returns>Information about the created batch job.</returns>
        Public Function Create(request As Models.Batch.BatchRequest) As Models.Batch.Batch
            Return CreateAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Creates a batch processing job asynchronously.
        ''' </summary>
        ''' <param name="request">The batch job request.</param>
        ''' <returns>Information about the created batch job.</returns>
        Public Async Function CreateAsync(request As Models.Batch.BatchRequest) As Task(Of Models.Batch.Batch)
            ValidateRequest(request)
            Return Await PostAsync(Of Models.Batch.BatchRequest, Models.Batch.Batch)("v1/batches", request)
        End Function

        ''' <summary>
        ''' Lists batch processing jobs.
        ''' </summary>
        ''' <param name="after">Optional cursor for pagination.</param>
        ''' <param name="limit">Optional limit for the number of jobs to return.</param>
        ''' <returns>List of batch jobs.</returns>
        Public Function List(Optional after As String = Nothing, Optional limit As Integer? = Nothing) As Models.Batch.BatchList
            Return ListAsync(after, limit).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Lists batch processing jobs asynchronously.
        ''' </summary>
        ''' <param name="after">Optional cursor for pagination.</param>
        ''' <param name="limit">Optional limit for the number of jobs to return.</param>
        ''' <returns>List of batch jobs.</returns>
        Public Async Function ListAsync(Optional after As String = Nothing, Optional limit As Integer? = Nothing) As Task(Of Models.Batch.BatchList)
            Dim queryParams As New List(Of String)()
            
            If Not String.IsNullOrEmpty(after) Then
                queryParams.Add($"after={after}")
            End If
            
            If limit.HasValue Then
                queryParams.Add($"limit={limit.Value}")
            End If

            Dim query As String = If(queryParams.Count > 0, "?" & String.Join("&", queryParams), "")
            Return Await GetAsync(Of Models.Batch.BatchList)($"v1/batches{query}")
        End Function

        ''' <summary>
        ''' Retrieves information about a specific batch job.
        ''' </summary>
        ''' <param name="batchId">The ID of the batch job.</param>
        ''' <returns>Information about the batch job.</returns>
        Public Function Retrieve(batchId As String) As Models.Batch.Batch
            Return RetrieveAsync(batchId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Retrieves information about a specific batch job asynchronously.
        ''' </summary>
        ''' <param name="batchId">The ID of the batch job.</param>
        ''' <returns>Information about the batch job.</returns>
        Public Async Function RetrieveAsync(batchId As String) As Task(Of Models.Batch.Batch)
            ValidateBatchId(batchId)
            Return Await GetAsync(Of Models.Batch.Batch)($"v1/batches/{batchId}")
        End Function

        ''' <summary>
        ''' Cancels a batch processing job.
        ''' </summary>
        ''' <param name="batchId">The ID of the batch job to cancel.</param>
        ''' <returns>Information about the cancelled batch job.</returns>
        Public Function Cancel(batchId As String) As Models.Batch.Batch
            Return CancelAsync(batchId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Cancels a batch processing job asynchronously.
        ''' </summary>
        ''' <param name="batchId">The ID of the batch job to cancel.</param>
        ''' <returns>Information about the cancelled batch job.</returns>
        Public Async Function CancelAsync(batchId As String) As Task(Of Models.Batch.Batch)
            ValidateBatchId(batchId)
            Return Await PostAsync(Of Object, Models.Batch.Batch)($"v1/batches/{batchId}/cancel", Nothing)
        End Function

        Private Sub ValidateRequest(request As Models.Batch.BatchRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "Batch request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.InputFileId) Then
                Throw New ArgumentException("Input file ID cannot be null or empty.", NameOf(request.InputFileId))
            End If

            If String.IsNullOrWhiteSpace(request.Endpoint) Then
                Throw New ArgumentException("Endpoint cannot be null or empty.", NameOf(request.Endpoint))
            End If

            If String.IsNullOrWhiteSpace(request.CompletionWindow) Then
                Throw New ArgumentException("Completion window cannot be null or empty.", NameOf(request.CompletionWindow))
            End If
        End Sub

        Private Sub ValidateBatchId(batchId As String)
            If String.IsNullOrWhiteSpace(batchId) Then
                Throw New ArgumentException("Batch ID cannot be null or empty.", NameOf(batchId))
            End If
        End Sub

    End Class

End Namespace
