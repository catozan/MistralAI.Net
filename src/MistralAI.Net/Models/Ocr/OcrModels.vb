Imports System
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Ocr

    ''' <summary>
    ''' Request for OCR text extraction from image file.
    ''' </summary>
    Public Class OcrRequest
        ''' <summary>
        ''' Path to the image file to process.
        ''' </summary>
        Public Property ImagePath As String

        ''' <summary>
        ''' ID of the model to use for OCR.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' Language hint for better OCR accuracy (optional).
        ''' </summary>
        <JsonProperty("language")>
        Public Property Language As String

        Public Sub New()
            ' Default constructor
        End Sub

        Public Sub New(imagePath As String, model As String)
            Me.ImagePath = imagePath
            Me.Model = model
        End Sub
    End Class

    ''' <summary>
    ''' Request for OCR text extraction from base64-encoded image.
    ''' </summary>
    Public Class OcrBase64Request
        ''' <summary>
        ''' Base64-encoded image data.
        ''' </summary>
        <JsonProperty("image")>
        Public Property Image As String

        ''' <summary>
        ''' ID of the model to use for OCR.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' Language hint for better OCR accuracy (optional).
        ''' </summary>
        <JsonProperty("language")>
        Public Property Language As String

        Public Sub New()
            ' Default constructor
        End Sub

        Public Sub New(image As String, model As String)
            Me.Image = image
            Me.Model = model
        End Sub
    End Class

    ''' <summary>
    ''' Response from OCR text extraction.
    ''' </summary>
    Public Class OcrResponse
        ''' <summary>
        ''' The extracted text from the image.
        ''' </summary>
        <JsonProperty("text")>
        Public Property Text As String

        ''' <summary>
        ''' Confidence score of the OCR result (0-1).
        ''' </summary>
        <JsonProperty("confidence")>
        Public Property Confidence As Double?

        ''' <summary>
        ''' Language detected in the image.
        ''' </summary>
        <JsonProperty("detected_language")>
        Public Property DetectedLanguage As String

        ''' <summary>
        ''' Processing time in milliseconds.
        ''' </summary>
        <JsonProperty("processing_time")>
        Public Property ProcessingTime As Integer?

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

End Namespace
