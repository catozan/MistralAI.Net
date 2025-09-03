Imports System.Text
Imports System.Net.Http
Imports System.Collections.Generic
Imports Newtonsoft.Json
Imports MistralAI.Net.Models
Imports MistralAI.Net.Exceptions

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Handles text classification operations for the Mistral AI API.
    ''' </summary>
    Public Class ClassifiersEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the ClassifiersEndpoint class.
        ''' </summary>
        ''' <param name="client">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(client As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(client, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Classifies text into categories.
        ''' </summary>
        ''' <param name="request">The classification request.</param>
        ''' <returns>The classification result.</returns>
        Public Function Classify(request As MistralAI.Net.Models.Classifiers.ClassificationRequest) As MistralAI.Net.Models.Classifiers.ClassificationResponse
            Return ClassifyAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Classifies text into categories asynchronously.
        ''' </summary>
        ''' <param name="request">The classification request.</param>
        ''' <returns>The classification result.</returns>
        Public Async Function ClassifyAsync(request As MistralAI.Net.Models.Classifiers.ClassificationRequest) As Task(Of MistralAI.Net.Models.Classifiers.ClassificationResponse)
            ValidateRequest(request)
            Return Await PostAsync(Of MistralAI.Net.Models.Classifiers.ClassificationRequest, MistralAI.Net.Models.Classifiers.ClassificationResponse)("v1/classifiers", request)
        End Function

        ''' <summary>
        ''' Convenience method to classify a single text.
        ''' </summary>
        ''' <param name="model">The model to use for classification.</param>
        ''' <param name="text">The text to classify.</param>
        ''' <param name="categories">List of possible categories.</param>
        ''' <returns>The classification result.</returns>
        Public Function ClassifyText(model As String, text As String, categories As List(Of String)) As MistralAI.Net.Models.Classifiers.ClassificationResponse
            Return ClassifyTextAsync(model, text, categories).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Convenience method to classify a single text asynchronously.
        ''' </summary>
        ''' <param name="model">The model to use for classification.</param>
        ''' <param name="text">The text to classify.</param>
        ''' <param name="categories">List of possible categories.</param>
        ''' <returns>The classification result.</returns>
        Public Async Function ClassifyTextAsync(model As String, text As String, categories As List(Of String)) As Task(Of MistralAI.Net.Models.Classifiers.ClassificationResponse)
            Dim request As New MistralAI.Net.Models.Classifiers.ClassificationRequest With {
                .Model = model,
                .Input = text,
                .Categories = categories
            }

            Return Await ClassifyAsync(request)
        End Function

        Private Sub ValidateRequest(request As MistralAI.Net.Models.Classifiers.ClassificationRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "Classification request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If

            If String.IsNullOrWhiteSpace(request.Input) Then
                Throw New ArgumentException("Input text cannot be null or empty.", NameOf(request.Input))
            End If

            If request.Categories Is Nothing OrElse request.Categories.Count = 0 Then
                Throw New ArgumentException("Categories cannot be null or empty.", NameOf(request.Categories))
            End If
        End Sub

    End Class

End Namespace
