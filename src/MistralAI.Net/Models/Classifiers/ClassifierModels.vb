Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Classifiers

    ''' <summary>
    ''' Request for text classification.
    ''' </summary>
    Public Class ClassificationRequest
        ''' <summary>
        ''' ID of the model to use for classification.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The text to classify.
        ''' </summary>
        <JsonProperty("input")>
        Public Property Input As String

        ''' <summary>
        ''' List of possible categories to classify into.
        ''' </summary>
        <JsonProperty("categories")>
        Public Property Categories As List(Of String)

        ''' <summary>
        ''' The maximum number of categories to return (optional).
        ''' </summary>
        <JsonProperty("max_categories")>
        Public Property MaxCategories As Integer?

        ''' <summary>
        ''' The minimum confidence threshold (optional).
        ''' </summary>
        <JsonProperty("min_confidence")>
        Public Property MinConfidence As Double?

        Public Sub New()
            Categories = New List(Of String)()
        End Sub

        Public Sub New(model As String, input As String, categories As List(Of String))
            Me.Model = model
            Me.Input = input
            Me.Categories = categories
        End Sub
    End Class

    ''' <summary>
    ''' Response from text classification.
    ''' </summary>
    Public Class ClassificationResponse
        ''' <summary>
        ''' List of classification results.
        ''' </summary>
        <JsonProperty("classifications")>
        Public Property Classifications As List(Of Classification)

        ''' <summary>
        ''' The model used for classification.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The original input text.
        ''' </summary>
        <JsonProperty("input")>
        Public Property Input As String

        Public Sub New()
            Classifications = New List(Of Classification)()
        End Sub
    End Class

    ''' <summary>
    ''' Represents a single classification result.
    ''' </summary>
    Public Class Classification
        ''' <summary>
        ''' The predicted category.
        ''' </summary>
        <JsonProperty("category")>
        Public Property Category As String

        ''' <summary>
        ''' The confidence score for this classification (0-1).
        ''' </summary>
        <JsonProperty("confidence")>
        Public Property Confidence As Double

        ''' <summary>
        ''' The ranking of this classification among all results.
        ''' </summary>
        <JsonProperty("rank")>
        Public Property Rank As Integer

        Public Sub New()
            ' Default constructor
        End Sub

        Public Sub New(category As String, confidence As Double, rank As Integer)
            Me.Category = category
            Me.Confidence = confidence
            Me.Rank = rank
        End Sub
    End Class

End Namespace
