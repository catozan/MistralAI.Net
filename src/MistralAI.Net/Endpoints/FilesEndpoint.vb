Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Newtonsoft.Json

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Files API endpoint for file upload, management, and retrieval operations.
    ''' </summary>
    Public Class FilesEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the FilesEndpoint class.
        ''' </summary>
        ''' <param name="httpClient">The HTTP client to use for requests.</param>
        Public Sub New(httpClient As HttpClient)
            MyBase.New(httpClient)
        End Sub

        ''' <summary>
        ''' Uploads a file to be used for fine-tuning or other operations.
        ''' </summary>
        ''' <param name="filePath">Path to the file to upload.</param>
        ''' <param name="purpose">The purpose of the file (e.g., "fine-tune").</param>
        ''' <returns>Information about the uploaded file.</returns>
        Public Function Upload(filePath As String, purpose As String) As Models.Files.FileObject
            Return UploadAsync(filePath, purpose).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Uploads a file asynchronously to be used for fine-tuning or other operations.
        ''' </summary>
        ''' <param name="filePath">Path to the file to upload.</param>
        ''' <param name="purpose">The purpose of the file (e.g., "fine-tune").</param>
        ''' <returns>Information about the uploaded file.</returns>
        Public Async Function UploadAsync(filePath As String, purpose As String) As Task(Of Models.Files.FileObject)
            ValidateUploadRequest(filePath, purpose)

            Using form As New MultipartFormDataContent()
                ' Add file content
                Dim fileContent As New ByteArrayContent(File.ReadAllBytes(filePath))
                fileContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("application/octet-stream")
                form.Add(fileContent, "file", Path.GetFileName(filePath))

                ' Add purpose
                form.Add(New StringContent(purpose), "purpose")

                Return Await PostMultipartAsync(Of Models.Files.FileObject)("v1/files", form)
            End Using
        End Function

        ''' <summary>
        ''' Lists all uploaded files.
        ''' </summary>
        ''' <returns>List of uploaded files.</returns>
        Public Function List() As Models.Files.FileList
            Return ListAsync().GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Lists all uploaded files asynchronously.
        ''' </summary>
        ''' <returns>List of uploaded files.</returns>
        Public Async Function ListAsync() As Task(Of Models.Files.FileList)
            Return Await GetAsync(Of Models.Files.FileList)("v1/files")
        End Function

        ''' <summary>
        ''' Retrieves information about a specific file.
        ''' </summary>
        ''' <param name="fileId">The ID of the file to retrieve.</param>
        ''' <returns>Information about the file.</returns>
        Public Function Retrieve(fileId As String) As Models.Files.FileObject
            Return RetrieveAsync(fileId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Retrieves information about a specific file asynchronously.
        ''' </summary>
        ''' <param name="fileId">The ID of the file to retrieve.</param>
        ''' <returns>Information about the file.</returns>
        Public Async Function RetrieveAsync(fileId As String) As Task(Of Models.Files.FileObject)
            ValidateFileId(fileId)
            Return Await GetAsync(Of Models.Files.FileObject)($"v1/files/{fileId}")
        End Function

        ''' <summary>
        ''' Deletes a file.
        ''' </summary>
        ''' <param name="fileId">The ID of the file to delete.</param>
        ''' <returns>Deletion status.</returns>
        Public Function Delete(fileId As String) As Models.Files.FileDeleteResponse
            Return DeleteAsync(fileId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Deletes a file asynchronously.
        ''' </summary>
        ''' <param name="fileId">The ID of the file to delete.</param>
        ''' <returns>Deletion status.</returns>
        Public Overloads Async Function DeleteAsync(fileId As String) As Task(Of Models.Files.FileDeleteResponse)
            ValidateFileId(fileId)
            Try
                Using response As HttpResponseMessage = Await HttpClient.DeleteAsync($"v1/files/{fileId}")
                    Dim content As String = Await response.Content.ReadAsStringAsync()
                    
                    If response.IsSuccessStatusCode Then
                        Return JsonConvert.DeserializeObject(Of Models.Files.FileDeleteResponse)(content, JsonSettings)
                    Else
                        Throw New Exceptions.MistralApiException($"API request failed with status {response.StatusCode}: {content}")
                    End If
                End Using
            Catch ex As HttpRequestException
                Throw New Exceptions.MistralApiException($"HTTP request failed: {ex.Message}", ex)
            Catch ex As TaskCanceledException
                Throw New Exceptions.MistralApiException("Request timed out.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Downloads the content of a file.
        ''' </summary>
        ''' <param name="fileId">The ID of the file to download.</param>
        ''' <returns>File content as byte array.</returns>
        Public Function Download(fileId As String) As Byte()
            Return DownloadAsync(fileId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Downloads the content of a file asynchronously.
        ''' </summary>
        ''' <param name="fileId">The ID of the file to download.</param>
        ''' <returns>File content as byte array.</returns>
        Public Async Function DownloadAsync(fileId As String) As Task(Of Byte())
            ValidateFileId(fileId)
            
            Dim response = Await HttpClient.GetAsync($"v1/files/{fileId}/content")
            response.EnsureSuccessStatusCode()
            
            Return Await response.Content.ReadAsByteArrayAsync()
        End Function

        Private Sub ValidateUploadRequest(filePath As String, purpose As String)
            If String.IsNullOrWhiteSpace(filePath) Then
                Throw New ArgumentException("File path cannot be null or empty.", NameOf(filePath))
            End If

            If Not File.Exists(filePath) Then
                Throw New FileNotFoundException($"File not found: {filePath}")
            End If

            If String.IsNullOrWhiteSpace(purpose) Then
                Throw New ArgumentException("Purpose cannot be null or empty.", NameOf(purpose))
            End If
        End Sub

        Private Sub ValidateFileId(fileId As String)
            If String.IsNullOrWhiteSpace(fileId) Then
                Throw New ArgumentException("File ID cannot be null or empty.", NameOf(fileId))
            End If
        End Sub

    End Class

End Namespace
