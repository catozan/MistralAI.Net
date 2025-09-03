Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Files

    ''' <summary>
    ''' Represents a file object in the Mistral AI system.
    ''' </summary>
    Public Class FileObject
        ''' <summary>
        ''' The unique identifier of the file.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The name of the file.
        ''' </summary>
        <JsonProperty("filename")>
        Public Property Filename As String

        ''' <summary>
        ''' The purpose of the file (e.g., "fine-tune").
        ''' </summary>
        <JsonProperty("purpose")>
        Public Property Purpose As String

        ''' <summary>
        ''' The size of the file in bytes.
        ''' </summary>
        <JsonProperty("bytes")>
        Public Property Bytes As Long

        ''' <summary>
        ''' The Unix timestamp when the file was created.
        ''' </summary>
        <JsonProperty("created_at")>
        Public Property CreatedAt As Long

        ''' <summary>
        ''' The object type, always "file".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The status of the file processing.
        ''' </summary>
        <JsonProperty("status")>
        Public Property Status As String

        ''' <summary>
        ''' Additional details about the file status.
        ''' </summary>
        <JsonProperty("status_details")>
        Public Property StatusDetails As String

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Response containing a list of files.
    ''' </summary>
    Public Class FileList
        ''' <summary>
        ''' The object type, always "list".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of file objects.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of FileObject)

        Public Sub New()
            Data = New List(Of FileObject)()
            [Object] = "list"
        End Sub
    End Class

    ''' <summary>
    ''' Response from a file deletion operation.
    ''' </summary>
    Public Class FileDeleteResponse
        ''' <summary>
        ''' The ID of the deleted file.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, always "file".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' Whether the file was successfully deleted.
        ''' </summary>
        <JsonProperty("deleted")>
        Public Property Deleted As Boolean

        Public Sub New()
            [Object] = "file"
        End Sub
    End Class

End Namespace
