Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Fim

    ''' <summary>
    ''' Request for fill-in-the-middle code completion.
    ''' </summary>
    Public Class FimRequest
        ''' <summary>
        ''' ID of the model to use.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The code before the cursor position.
        ''' </summary>
        <JsonProperty("prefix")>
        Public Property Prefix As String

        ''' <summary>
        ''' The code after the cursor position.
        ''' </summary>
        <JsonProperty("suffix")>
        Public Property Suffix As String

        ''' <summary>
        ''' The maximum number of tokens to generate (optional).
        ''' </summary>
        <JsonProperty("max_tokens")>
        Public Property MaxTokens As Integer?

        ''' <summary>
        ''' Sampling temperature between 0 and 2 (optional).
        ''' </summary>
        <JsonProperty("temperature")>
        Public Property Temperature As Double?

        ''' <summary>
        ''' Nucleus sampling parameter (optional).
        ''' </summary>
        <JsonProperty("top_p")>
        Public Property TopP As Double?

        ''' <summary>
        ''' Number of completions to generate (optional).
        ''' </summary>
        <JsonProperty("n")>
        Public Property N As Integer?

        ''' <summary>
        ''' Whether to stream the response (optional).
        ''' </summary>
        <JsonProperty("stream")>
        Public Property Stream As Boolean?

        ''' <summary>
        ''' Stop sequences where the API will stop generating further tokens (optional).
        ''' </summary>
        <JsonProperty("stop")>
        Public Property StopSequences As List(Of String)

        Public Sub New()
            StopSequences = New List(Of String)()
        End Sub

        Public Sub New(model As String, prefix As String, suffix As String)
            Me.Model = model
            Me.Prefix = prefix
            Me.Suffix = suffix
            StopSequences = New List(Of String)()
        End Sub
    End Class

    ''' <summary>
    ''' Response from fill-in-the-middle completion.
    ''' </summary>
    Public Class FimResponse
        ''' <summary>
        ''' A unique identifier for the completion.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, which is always "text_completion".
        ''' </summary>
        <JsonProperty("object")>
        Public Property ObjectType As String

        ''' <summary>
        ''' The Unix timestamp (in seconds) of when the completion was created.
        ''' </summary>
        <JsonProperty("created")>
        Public Property Created As Long

        ''' <summary>
        ''' The model used for completion.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' A list of completion choices.
        ''' </summary>
        <JsonProperty("choices")>
        Public Property Choices As List(Of FimChoice)

        ''' <summary>
        ''' Usage statistics for the completion request.
        ''' </summary>
        <JsonProperty("usage")>
        Public Property Usage As FimUsage

        Public Sub New()
            ObjectType = "text_completion"
            Choices = New List(Of FimChoice)()
        End Sub
    End Class

    ''' <summary>
    ''' Represents a single completion choice in FIM response.
    ''' </summary>
    Public Class FimChoice
        ''' <summary>
        ''' The generated text completion.
        ''' </summary>
        <JsonProperty("text")>
        Public Property Text As String

        ''' <summary>
        ''' The index of this choice in the list of choices.
        ''' </summary>
        <JsonProperty("index")>
        Public Property Index As Integer

        ''' <summary>
        ''' The reason the model stopped generating tokens.
        ''' </summary>
        <JsonProperty("finish_reason")>
        Public Property FinishReason As String

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Usage statistics for FIM completion.
    ''' </summary>
    Public Class FimUsage
        ''' <summary>
        ''' Number of tokens in the prompt.
        ''' </summary>
        <JsonProperty("prompt_tokens")>
        Public Property PromptTokens As Integer

        ''' <summary>
        ''' Number of tokens in the generated completion.
        ''' </summary>
        <JsonProperty("completion_tokens")>
        Public Property CompletionTokens As Integer

        ''' <summary>
        ''' Total number of tokens used in the request.
        ''' </summary>
        <JsonProperty("total_tokens")>
        Public Property TotalTokens As Integer

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

End Namespace
