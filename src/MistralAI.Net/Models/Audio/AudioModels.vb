Imports System
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Audio

    ''' <summary>
    ''' Request for audio transcription.
    ''' </summary>
    Public Class TranscriptionRequest
        ''' <summary>
        ''' Path to the audio file to transcribe.
        ''' </summary>
        Public Property FilePath As String

        ''' <summary>
        ''' ID of the model to use for transcription.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The language of the input audio (optional).
        ''' </summary>
        <JsonProperty("language")>
        Public Property Language As String

        ''' <summary>
        ''' An optional text to guide the model's style or continue a previous audio segment.
        ''' </summary>
        <JsonProperty("prompt")>
        Public Property Prompt As String

        ''' <summary>
        ''' The sampling temperature, between 0 and 1 (optional).
        ''' </summary>
        <JsonProperty("temperature")>
        Public Property Temperature As Double?

        Public Sub New()
            ' Default constructor
        End Sub

        Public Sub New(filePath As String, model As String)
            Me.FilePath = filePath
            Me.Model = model
        End Sub
    End Class

    ''' <summary>
    ''' Response from audio transcription.
    ''' </summary>
    Public Class TranscriptionResponse
        ''' <summary>
        ''' The transcribed text.
        ''' </summary>
        <JsonProperty("text")>
        Public Property Text As String

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Request for audio translation.
    ''' </summary>
    Public Class TranslationRequest
        ''' <summary>
        ''' Path to the audio file to translate.
        ''' </summary>
        Public Property FilePath As String

        ''' <summary>
        ''' ID of the model to use for translation.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' An optional text to guide the model's style or continue a previous audio segment.
        ''' </summary>
        <JsonProperty("prompt")>
        Public Property Prompt As String

        ''' <summary>
        ''' The sampling temperature, between 0 and 1 (optional).
        ''' </summary>
        <JsonProperty("temperature")>
        Public Property Temperature As Double?

        Public Sub New()
            ' Default constructor
        End Sub

        Public Sub New(filePath As String, model As String)
            Me.FilePath = filePath
            Me.Model = model
        End Sub
    End Class

    ''' <summary>
    ''' Response from audio translation.
    ''' </summary>
    Public Class TranslationResponse
        ''' <summary>
        ''' The translated text.
        ''' </summary>
        <JsonProperty("text")>
        Public Property Text As String

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Request for speech generation.
    ''' </summary>
    Public Class SpeechRequest
        ''' <summary>
        ''' ID of the model to use for speech generation.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The text to generate audio for.
        ''' </summary>
        <JsonProperty("input")>
        Public Property Input As String

        ''' <summary>
        ''' The voice to use when generating the audio.
        ''' </summary>
        <JsonProperty("voice")>
        Public Property Voice As String

        ''' <summary>
        ''' The format to audio in (optional).
        ''' </summary>
        <JsonProperty("response_format")>
        Public Property ResponseFormat As String

        ''' <summary>
        ''' The speed of the generated audio (optional).
        ''' </summary>
        <JsonProperty("speed")>
        Public Property Speed As Double?

        Public Sub New()
            ResponseFormat = "mp3"
            Speed = 1.0
        End Sub

        Public Sub New(model As String, input As String, voice As String)
            Me.Model = model
            Me.Input = input
            Me.Voice = voice
            ResponseFormat = "mp3"
            Speed = 1.0
        End Sub
    End Class

End Namespace
