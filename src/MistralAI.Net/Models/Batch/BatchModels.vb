Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Batch

    ''' <summary>
    ''' Request for creating a batch processing job.
    ''' </summary>
    Public Class BatchRequest
        ''' <summary>
        ''' The ID of an uploaded file that contains requests for the new batch.
        ''' </summary>
        <JsonProperty("input_file_id")>
        Public Property InputFileId As String

        ''' <summary>
        ''' The endpoint to be used for all requests in the batch.
        ''' </summary>
        <JsonProperty("endpoint")>
        Public Property Endpoint As String

        ''' <summary>
        ''' The time frame within which the batch should be processed.
        ''' </summary>
        <JsonProperty("completion_window")>
        Public Property CompletionWindow As String

        ''' <summary>
        ''' Optional custom metadata for the batch.
        ''' </summary>
        <JsonProperty("metadata")>
        Public Property Metadata As Dictionary(Of String, String)

        Public Sub New()
            Metadata = New Dictionary(Of String, String)()
        End Sub
    End Class

    ''' <summary>
    ''' Represents a batch processing job.
    ''' </summary>
    Public Class Batch
        ''' <summary>
        ''' The batch identifier.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, which is always "batch".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The endpoint to be used for all requests in the batch.
        ''' </summary>
        <JsonProperty("endpoint")>
        Public Property Endpoint As String

        ''' <summary>
        ''' Set of 16 key-value pairs that can be attached to an object.
        ''' </summary>
        <JsonProperty("errors")>
        Public Property Errors As BatchErrors

        ''' <summary>
        ''' The ID of the input file for the batch.
        ''' </summary>
        <JsonProperty("input_file_id")>
        Public Property InputFileId As String

        ''' <summary>
        ''' The time frame within which the batch should be processed.
        ''' </summary>
        <JsonProperty("completion_window")>
        Public Property CompletionWindow As String

        ''' <summary>
        ''' The current status of the batch.
        ''' </summary>
        <JsonProperty("status")>
        Public Property Status As String

        ''' <summary>
        ''' The ID of the file containing the outputs of successfully executed requests.
        ''' </summary>
        <JsonProperty("output_file_id")>
        Public Property OutputFileId As String

        ''' <summary>
        ''' The ID of the file containing the outputs of requests with errors.
        ''' </summary>
        <JsonProperty("error_file_id")>
        Public Property ErrorFileId As String

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the batch was created.
        ''' </summary>
        <JsonProperty("created_at")>
        Public Property CreatedAt As Long

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the batch started processing.
        ''' </summary>
        <JsonProperty("in_progress_at")>
        Public Property InProgressAt As Long?

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the batch will expire.
        ''' </summary>
        <JsonProperty("expires_at")>
        Public Property ExpiresAt As Long?

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the batch started finalizing.
        ''' </summary>
        <JsonProperty("finalizing_at")>
        Public Property FinalizingAt As Long?

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the batch was completed.
        ''' </summary>
        <JsonProperty("completed_at")>
        Public Property CompletedAt As Long?

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the batch failed.
        ''' </summary>
        <JsonProperty("failed_at")>
        Public Property FailedAt As Long?

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the batch was cancelled.
        ''' </summary>
        <JsonProperty("cancelled_at")>
        Public Property CancelledAt As Long?

        ''' <summary>
        ''' The request counts for different statuses within the batch.
        ''' </summary>
        <JsonProperty("request_counts")>
        Public Property RequestCounts As BatchRequestCounts

        ''' <summary>
        ''' Set of 16 key-value pairs that can be attached to an object.
        ''' </summary>
        <JsonProperty("metadata")>
        Public Property Metadata As Dictionary(Of String, String)

        Public Sub New()
            [Object] = "batch"
            Metadata = New Dictionary(Of String, String)()
        End Sub
    End Class

    ''' <summary>
    ''' Error information for batch processing.
    ''' </summary>
    Public Class BatchErrors
        ''' <summary>
        ''' The object type.
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of errors that occurred during batch processing.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of BatchError)

        Public Sub New()
            [Object] = "list"
            Data = New List(Of BatchError)()
        End Sub
    End Class

    ''' <summary>
    ''' Represents an error that occurred during batch processing.
    ''' </summary>
    Public Class BatchError
        ''' <summary>
        ''' An error code identifying the error type.
        ''' </summary>
        <JsonProperty("code")>
        Public Property Code As String

        ''' <summary>
        ''' A human-readable message providing more details about the error.
        ''' </summary>
        <JsonProperty("message")>
        Public Property Message As String

        ''' <summary>
        ''' The name of the parameter that caused the error, if applicable.
        ''' </summary>
        <JsonProperty("param")>
        Public Property Param As String

        ''' <summary>
        ''' The line number of the input file where the error occurred, if applicable.
        ''' </summary>
        <JsonProperty("line")>
        Public Property Line As Integer?

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Request counts for different statuses within a batch.
    ''' </summary>
    Public Class BatchRequestCounts
        ''' <summary>
        ''' Total number of requests in the batch.
        ''' </summary>
        <JsonProperty("total")>
        Public Property Total As Integer

        ''' <summary>
        ''' Number of requests that have been completed successfully.
        ''' </summary>
        <JsonProperty("completed")>
        Public Property Completed As Integer

        ''' <summary>
        ''' Number of requests that have failed.
        ''' </summary>
        <JsonProperty("failed")>
        Public Property Failed As Integer

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Response containing a list of batch jobs.
    ''' </summary>
    Public Class BatchList
        ''' <summary>
        ''' The object type, always "list".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of batch jobs.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of Batch)

        ''' <summary>
        ''' The first ID in the current page of results.
        ''' </summary>
        <JsonProperty("first_id")>
        Public Property FirstId As String

        ''' <summary>
        ''' The last ID in the current page of results.
        ''' </summary>
        <JsonProperty("last_id")>
        Public Property LastId As String

        ''' <summary>
        ''' Whether there are more results available.
        ''' </summary>
        <JsonProperty("has_more")>
        Public Property HasMore As Boolean

        Public Sub New()
            Data = New List(Of Batch)()
            [Object] = "list"
        End Sub
    End Class

End Namespace
