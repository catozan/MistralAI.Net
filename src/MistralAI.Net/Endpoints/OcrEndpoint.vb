Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports MistralAI.Net.Models
Imports MistralAI.Net.Exceptions

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Handles optical character recognition (OCR) operations for the Mistral AI API.
    ''' </summary>
    Public Class OcrEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the OcrEndpoint class.
        ''' </summary>
        ''' <param name="client">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(client As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(client, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Extracts text from an image.
        ''' </summary>
        ''' <param name="request">The OCR request.</param>
        ''' <returns>The extracted text.</returns>
        Public Function ExtractText(request As Models.Ocr.OcrRequest) As Models.Ocr.OcrResponse
            Return ExtractTextAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Extracts text from an image asynchronously.
        ''' </summary>
        ''' <param name="request">The OCR request.</param>
        ''' <returns>The extracted text.</returns>
        Public Async Function ExtractTextAsync(request As Models.Ocr.OcrRequest) As Task(Of Models.Ocr.OcrResponse)
            ValidateRequest(request)

            Using form As New MultipartFormDataContent()
                ' Add file content
                Dim fileContent As New ByteArrayContent(File.ReadAllBytes(request.ImagePath))
                fileContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("image/jpeg")
                form.Add(fileContent, "file", Path.GetFileName(request.ImagePath))

                ' Add model
                form.Add(New StringContent(request.Model), "model")

                ' Add optional parameters
                If Not String.IsNullOrEmpty(request.Language) Then
                    form.Add(New StringContent(request.Language), "language")
                End If

                Return Await PostMultipartAsync(Of Models.Ocr.OcrResponse)("v1/ocr", form)
            End Using
        End Function

        ''' <summary>
        ''' Extracts text from an image using a base64-encoded image.
        ''' </summary>
        ''' <param name="model">The model to use for OCR.</param>
        ''' <param name="base64Image">The base64-encoded image data.</param>
        ''' <param name="language">Optional language hint.</param>
        ''' <returns>The extracted text.</returns>
        Public Function ExtractTextFromBase64(model As String, base64Image As String, Optional language As String = Nothing) As Models.Ocr.OcrResponse
            Return ExtractTextFromBase64Async(model, base64Image, language).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Extracts text from an image using a base64-encoded image asynchronously.
        ''' </summary>
        ''' <param name="model">The model to use for OCR.</param>
        ''' <param name="base64Image">The base64-encoded image data.</param>
        ''' <param name="language">Optional language hint.</param>
        ''' <returns>The extracted text.</returns>
        Public Async Function ExtractTextFromBase64Async(model As String, base64Image As String, Optional language As String = Nothing) As Task(Of Models.Ocr.OcrResponse)
            Dim request As New Models.Ocr.OcrBase64Request With {
                .Model = model,
                .Image = base64Image,
                .Language = language
            }

            ValidateBase64Request(request)
            Return Await PostAsync(Of Models.Ocr.OcrBase64Request, Models.Ocr.OcrResponse)("v1/ocr", request)
        End Function

        Private Sub ValidateRequest(request As Models.Ocr.OcrRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "OCR request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.ImagePath) Then
                Throw New ArgumentException("Image path cannot be null or empty.", NameOf(request.ImagePath))
            End If

            If Not File.Exists(request.ImagePath) Then
                Throw New FileNotFoundException($"Image file not found: {request.ImagePath}")
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If
        End Sub

        Private Sub ValidateBase64Request(request As Models.Ocr.OcrBase64Request)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "OCR request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.Image) Then
                Throw New ArgumentException("Base64 image data cannot be null or empty.", NameOf(request.Image))
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If
        End Sub

    End Class

End Namespace
