Imports System.Text
Imports System.Net.Http
Imports System.IO
Imports Newtonsoft.Json
Imports MistralAI.Net.Models
Imports MistralAI.Net.Exceptions

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Handles audio processing operations for the Mistral AI API.
    ''' </summary>
    Public Class AudioEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the AudioEndpoint class.
        ''' </summary>
        ''' <param name="client">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(client As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(client, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Transcribes audio to text.
        ''' </summary>
        ''' <param name="request">The transcription request.</param>
        ''' <returns>The transcription result.</returns>
        Public Function Transcribe(request As Models.Audio.TranscriptionRequest) As Models.Audio.TranscriptionResponse
            Return TranscribeAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Transcribes audio to text asynchronously.
        ''' </summary>
        ''' <param name="request">The transcription request.</param>
        ''' <returns>The transcription result.</returns>
        Public Async Function TranscribeAsync(request As Models.Audio.TranscriptionRequest) As Task(Of Models.Audio.TranscriptionResponse)
            ValidateTranscriptionRequest(request)

            Using form As New MultipartFormDataContent()
                ' Add file content
                Dim fileContent As New ByteArrayContent(File.ReadAllBytes(request.FilePath))
                fileContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("audio/wav")
                form.Add(fileContent, "file", Path.GetFileName(request.FilePath))

                ' Add model
                form.Add(New StringContent(request.Model), "model")

                ' Add optional parameters
                If Not String.IsNullOrEmpty(request.Language) Then
                    form.Add(New StringContent(request.Language), "language")
                End If

                If Not String.IsNullOrEmpty(request.Prompt) Then
                    form.Add(New StringContent(request.Prompt), "prompt")
                End If

                If request.Temperature.HasValue Then
                    form.Add(New StringContent(request.Temperature.Value.ToString()), "temperature")
                End If

                Dim responseJson = Await PostMultipartAsync("v1/audio/transcriptions", form)
                Return JsonConvert.DeserializeObject(Of Models.Audio.TranscriptionResponse)(responseJson)
            End Using
        End Function

        ''' <summary>
        ''' Translates audio to English text.
        ''' </summary>
        ''' <param name="request">The translation request.</param>
        ''' <returns>The translation result.</returns>
        Public Function Translate(request As Models.Audio.TranslationRequest) As Models.Audio.TranslationResponse
            Return TranslateAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Translates audio to English text asynchronously.
        ''' </summary>
        ''' <param name="request">The translation request.</param>
        ''' <returns>The translation result.</returns>
        Public Async Function TranslateAsync(request As Models.Audio.TranslationRequest) As Task(Of Models.Audio.TranslationResponse)
            ValidateTranslationRequest(request)

            Using form As New MultipartFormDataContent()
                ' Add file content
                Dim fileContent As New ByteArrayContent(File.ReadAllBytes(request.FilePath))
                fileContent.Headers.ContentType = New Headers.MediaTypeHeaderValue("audio/wav")
                form.Add(fileContent, "file", Path.GetFileName(request.FilePath))

                ' Add model
                form.Add(New StringContent(request.Model), "model")

                ' Add optional parameters
                If Not String.IsNullOrEmpty(request.Prompt) Then
                    form.Add(New StringContent(request.Prompt), "prompt")
                End If

                If request.Temperature.HasValue Then
                    form.Add(New StringContent(request.Temperature.Value.ToString()), "temperature")
                End If

                Dim responseJson = Await PostMultipartAsync("v1/audio/translations", form)
                Return JsonConvert.DeserializeObject(Of Models.Audio.TranslationResponse)(responseJson)
            End Using
        End Function

        ''' <summary>
        ''' Generates speech from text.
        ''' </summary>
        ''' <param name="request">The speech generation request.</param>
        ''' <returns>The generated audio data.</returns>
        Public Function Speech(request As Models.Audio.SpeechRequest) As Byte()
            Return SpeechAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Generates speech from text asynchronously.
        ''' </summary>
        ''' <param name="request">The speech generation request.</param>
        ''' <returns>The generated audio data.</returns>
        Public Async Function SpeechAsync(request As Models.Audio.SpeechRequest) As Task(Of Byte())
            ValidateSpeechRequest(request)

            Dim json = JsonConvert.SerializeObject(request)
            Dim responseJson = Await PostAsync("v1/audio/speech", json)
            ' For audio endpoints, we typically get binary data back
            Return System.Text.Encoding.UTF8.GetBytes(responseJson)
        End Function

        Private Sub ValidateTranscriptionRequest(request As Models.Audio.TranscriptionRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "Transcription request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.FilePath) Then
                Throw New ArgumentException("File path cannot be null or empty.", NameOf(request.FilePath))
            End If

            If Not File.Exists(request.FilePath) Then
                Throw New FileNotFoundException($"Audio file not found: {request.FilePath}")
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If
        End Sub

        Private Sub ValidateTranslationRequest(request As Models.Audio.TranslationRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "Translation request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.FilePath) Then
                Throw New ArgumentException("File path cannot be null or empty.", NameOf(request.FilePath))
            End If

            If Not File.Exists(request.FilePath) Then
                Throw New FileNotFoundException($"Audio file not found: {request.FilePath}")
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If
        End Sub

        Private Sub ValidateSpeechRequest(request As Models.Audio.SpeechRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "Speech request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If

            If String.IsNullOrWhiteSpace(request.Input) Then
                Throw New ArgumentException("Input text cannot be null or empty.", NameOf(request.Input))
            End If

            If String.IsNullOrWhiteSpace(request.Voice) Then
                Throw New ArgumentException("Voice cannot be null or empty.", NameOf(request.Voice))
            End If
        End Sub

    End Class

End Namespace
