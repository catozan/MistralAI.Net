Imports Newtonsoft.Json
Imports System.Collections.Generic

Namespace MistralAI.Net.Models.Models

    ''' <summary>
    ''' Represents a list of available models.
    ''' </summary>
    Public Class ModelList

        ''' <summary>
        ''' The object type, always "list".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of model information objects.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of ModelInfo)

        Public Sub New()
            Data = New List(Of ModelInfo)()
        End Sub

    End Class

    ''' <summary>
    ''' Represents information about a specific model.
    ''' </summary>
    Public Class ModelInfo

        ''' <summary>
        ''' The unique identifier of the model.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, always "model".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The Unix timestamp when the model was created.
        ''' </summary>
        <JsonProperty("created")>
        Public Property Created As Long

        ''' <summary>
        ''' The organization that owns the model.
        ''' </summary>
        <JsonProperty("owned_by")>
        Public Property OwnedBy As String

        ''' <summary>
        ''' The capabilities of the model.
        ''' </summary>
        <JsonProperty("capabilities")>
        Public Property Capabilities As ModelCapabilities

        ''' <summary>
        ''' The name of the model.
        ''' </summary>
        <JsonProperty("name", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Name As String

        ''' <summary>
        ''' The description of the model.
        ''' </summary>
        <JsonProperty("description", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Description As String

        ''' <summary>
        ''' The maximum number of tokens the model can process.
        ''' </summary>
        <JsonProperty("max_context_length", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property MaxContextLength As Integer?

        ''' <summary>
        ''' The aliases for the model.
        ''' </summary>
        <JsonProperty("aliases", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Aliases As List(Of String)

        ''' <summary>
        ''' The default model parameters.
        ''' </summary>
        <JsonProperty("default_model_parameters", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property DefaultModelParameters As ModelParameters

        ''' <summary>
        ''' Whether the model is deprecated.
        ''' </summary>
        <JsonProperty("deprecation", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Deprecation As String

        Public Sub New()
            Aliases = New List(Of String)()
        End Sub

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
        ''' Whether the model supports function calling.
        ''' </summary>
        <JsonProperty("function_calling")>
        Public Property FunctionCalling As Boolean

        ''' <summary>
        ''' Whether the model supports fine-tuning.
        ''' </summary>
        <JsonProperty("fine_tuning")>
        Public Property FineTuning As Boolean

        ''' <summary>
        ''' Whether the model supports vision/image understanding.
        ''' </summary>
        <JsonProperty("vision")>
        Public Property Vision As Boolean

    End Class

    ''' <summary>
    ''' Represents default parameters for a model.
    ''' </summary>
    Public Class ModelParameters

        ''' <summary>
        ''' The default temperature for the model.
        ''' </summary>
        <JsonProperty("temperature", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Temperature As Double?

        ''' <summary>
        ''' The default top_p for the model.
        ''' </summary>
        <JsonProperty("top_p", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property TopP As Double?

        ''' <summary>
        ''' The default max_tokens for the model.
        ''' </summary>
        <JsonProperty("max_tokens", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property MaxTokens As Integer?

    End Class

End Namespace
