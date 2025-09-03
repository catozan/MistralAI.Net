Imports Newtonsoft.Json
Imports System.Collections.Generic

Namespace MistralAI.Net.Models

    ''' <summary>
    ''' Represents response format specification for structured outputs.
    ''' </summary>
    Public Class ResponseFormat

        ''' <summary>
        ''' The type of response format (e.g., "json_object").
        ''' </summary>
        <JsonProperty("type")>
        Public Property Type As String

        ''' <summary>
        ''' JSON schema for structured output.
        ''' </summary>
        <JsonProperty("schema", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Schema As Object

    End Class

    ''' <summary>
    ''' Represents a tool that can be called by the model.
    ''' </summary>
    Public Class Tool

        ''' <summary>
        ''' The type of the tool (always "function").
        ''' </summary>
        <JsonProperty("type")>
        Public Property Type As String

        ''' <summary>
        ''' Function definition for the tool.
        ''' </summary>
        <JsonProperty("function")>
        Public Property [Function] As ToolFunction

    End Class

    ''' <summary>
    ''' Represents a function definition for a tool.
    ''' </summary>
    Public Class ToolFunction

        ''' <summary>
        ''' The name of the function.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' A description of what the function does.
        ''' </summary>
        <JsonProperty("description")>
        Public Property Description As String

        ''' <summary>
        ''' The parameters the function accepts.
        ''' </summary>
        <JsonProperty("parameters", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Parameters As Object

    End Class

    ''' <summary>
    ''' Represents prediction settings for speculative generation.
    ''' </summary>
    Public Class Prediction

        ''' <summary>
        ''' The type of prediction.
        ''' </summary>
        <JsonProperty("type")>
        Public Property Type As String

        ''' <summary>
        ''' Content for the prediction.
        ''' </summary>
        <JsonProperty("content", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Content As String

    End Class

    ''' <summary>
    ''' Represents a model available in the API.
    ''' </summary>
    Public Class ModelInfo

        ''' <summary>
        ''' The model identifier.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type (always "model").
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The Unix timestamp of when the model was created.
        ''' </summary>
        <JsonProperty("created")>
        Public Property Created As Long

        ''' <summary>
        ''' The organization that owns the model.
        ''' </summary>
        <JsonProperty("owned_by")>
        Public Property OwnedBy As String

        ''' <summary>
        ''' Model capabilities.
        ''' </summary>
        <JsonProperty("capabilities", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Capabilities As ModelCapabilities

    End Class

    ''' <summary>
    ''' Represents the capabilities of a model.
    ''' </summary>
    Public Class ModelCapabilities

        ''' <summary>
        ''' Whether the model supports completion.
        ''' </summary>
        <JsonProperty("completion_chat")>
        Public Property CompletionChat As Boolean

        ''' <summary>
        ''' Whether the model supports embeddings.
        ''' </summary>
        <JsonProperty("embeddings")>
        Public Property Embeddings As Boolean

        ''' <summary>
        ''' Whether the model supports function calling.
        ''' </summary>
        <JsonProperty("function_calling")>
        Public Property FunctionCalling As Boolean

        ''' <summary>
        ''' Whether the model supports fine-tuning.
        ''' </summary>
        <JsonProperty("fine_tuning")>
        Public Property FineTuning As Boolean

    End Class

    ''' <summary>
    ''' List response for models.
    ''' </summary>
    Public Class ModelList

        ''' <summary>
        ''' The object type (always "list").
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of available models.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of ModelInfo)

        Public Sub New()
            Data = New List(Of ModelInfo)()
        End Sub

    End Class

End Namespace
