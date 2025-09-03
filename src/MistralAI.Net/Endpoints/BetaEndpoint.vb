Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports MistralAI.Net.Models
Imports MistralAI.Net.Exceptions

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Handles beta/experimental features for the Mistral AI API.
    ''' </summary>
    Public Class BetaEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the BetaEndpoint class.
        ''' </summary>
        ''' <param name="client">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(client As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(client, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Lists available beta features.
        ''' </summary>
        ''' <returns>List of beta features.</returns>
        Public Function ListFeatures() As Models.Beta.BetaFeatureList
            Return ListFeaturesAsync().GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Lists available beta features asynchronously.
        ''' </summary>
        ''' <returns>List of beta features.</returns>
        Public Async Function ListFeaturesAsync() As Task(Of Models.Beta.BetaFeatureList)
            Dim responseJson = Await GetAsync("v1/beta/features")
            Return JsonConvert.DeserializeObject(Of Models.Beta.BetaFeatureList)(responseJson)
        End Function

        ''' <summary>
        ''' Gets information about a specific beta feature.
        ''' </summary>
        ''' <param name="featureId">The ID of the beta feature.</param>
        ''' <returns>Information about the beta feature.</returns>
        Public Function GetFeature(featureId As String) As Models.Beta.BetaFeature
            Return GetFeatureAsync(featureId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Gets information about a specific beta feature asynchronously.
        ''' </summary>
        ''' <param name="featureId">The ID of the beta feature.</param>
        ''' <returns>Information about the beta feature.</returns>
        Public Async Function GetFeatureAsync(featureId As String) As Task(Of Models.Beta.BetaFeature)
            ValidateFeatureId(featureId)
            Dim responseJson = Await GetAsync($"v1/beta/features/{featureId}")
            Return JsonConvert.DeserializeObject(Of Models.Beta.BetaFeature)(responseJson)
        End Function

        ''' <summary>
        ''' Enables a beta feature for the current user.
        ''' </summary>
        ''' <param name="featureId">The ID of the beta feature to enable.</param>
        ''' <returns>Result of the enable operation.</returns>
        Public Function EnableFeature(featureId As String) As Models.Beta.BetaFeatureToggleResponse
            Return EnableFeatureAsync(featureId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Enables a beta feature for the current user asynchronously.
        ''' </summary>
        ''' <param name="featureId">The ID of the beta feature to enable.</param>
        ''' <returns>Result of the enable operation.</returns>
        Public Async Function EnableFeatureAsync(featureId As String) As Task(Of Models.Beta.BetaFeatureToggleResponse)
            ValidateFeatureId(featureId)
            Dim json = JsonConvert.SerializeObject(Nothing)
            Dim responseJson = Await PostAsync($"v1/beta/features/{featureId}/enable", json)
            Return JsonConvert.DeserializeObject(Of Models.Beta.BetaFeatureToggleResponse)(responseJson)
        End Function

        ''' <summary>
        ''' Disables a beta feature for the current user.
        ''' </summary>
        ''' <param name="featureId">The ID of the beta feature to disable.</param>
        ''' <returns>Result of the disable operation.</returns>
        Public Function DisableFeature(featureId As String) As Models.Beta.BetaFeatureToggleResponse
            Return DisableFeatureAsync(featureId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Disables a beta feature for the current user asynchronously.
        ''' </summary>
        ''' <param name="featureId">The ID of the beta feature to disable.</param>
        ''' <returns>Result of the disable operation.</returns>
        Public Async Function DisableFeatureAsync(featureId As String) As Task(Of Models.Beta.BetaFeatureToggleResponse)
            ValidateFeatureId(featureId)
            Dim json = JsonConvert.SerializeObject(Nothing)
            Dim responseJson = Await PostAsync($"v1/beta/features/{featureId}/disable", json)
            Return JsonConvert.DeserializeObject(Of Models.Beta.BetaFeatureToggleResponse)(responseJson)
        End Function

        ''' <summary>
        ''' Executes a beta API call with experimental parameters.
        ''' </summary>
        ''' <typeparam name="TRequest">The request type.</typeparam>
        ''' <typeparam name="TResponse">The response type.</typeparam>
        ''' <param name="endpoint">The beta endpoint to call.</param>
        ''' <param name="request">The request object.</param>
        ''' <returns>The response from the beta API.</returns>
        Public Function CallBetaApi(Of TRequest, TResponse)(endpoint As String, request As TRequest) As TResponse
            Return CallBetaApiAsync(Of TRequest, TResponse)(endpoint, request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Executes a beta API call with experimental parameters asynchronously.
        ''' </summary>
        ''' <typeparam name="TRequest">The request type.</typeparam>
        ''' <typeparam name="TResponse">The response type.</typeparam>
        ''' <param name="endpoint">The beta endpoint to call.</param>
        ''' <param name="request">The request object.</param>
        ''' <returns>The response from the beta API.</returns>
        Public Async Function CallBetaApiAsync(Of TRequest, TResponse)(endpoint As String, request As TRequest) As Task(Of TResponse)
            ValidateBetaEndpoint(endpoint)
            Dim json = JsonConvert.SerializeObject(request)
            Dim responseJson = Await PostAsync($"v1/beta/{endpoint}", json)
            Return JsonConvert.DeserializeObject(Of TResponse)(responseJson)
        End Function

        Private Sub ValidateFeatureId(featureId As String)
            If String.IsNullOrWhiteSpace(featureId) Then
                Throw New ArgumentException("Feature ID cannot be null or empty.", NameOf(featureId))
            End If
        End Sub

        Private Sub ValidateBetaEndpoint(endpoint As String)
            If String.IsNullOrWhiteSpace(endpoint) Then
                Throw New ArgumentException("Beta endpoint cannot be null or empty.", NameOf(endpoint))
            End If
        End Sub

    End Class

End Namespace
