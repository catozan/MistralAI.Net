Imports System
Imports System.IO
Imports System.Net.Http
Imports System.Collections.Generic
Imports System.Text
Imports System.Threading.Tasks
Imports Newtonsoft.Json
Imports MistralAI.Net.Exceptions

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Base class for all API endpoints providing common functionality.
    ''' </summary>
    Public MustInherit Class BaseEndpoint

        Protected ReadOnly _httpClient As HttpClient
        Protected ReadOnly _apiKey As String
        Protected ReadOnly _baseUrl As String

        ''' <summary>
        ''' Initializes a new instance of the BaseEndpoint class.
        ''' </summary>
        ''' <param name="client">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Protected Sub New(client As HttpClient, apiKey As String, baseUrl As String)
            If client Is Nothing Then
                Throw New ArgumentNullException(NameOf(client))
            End If

            If String.IsNullOrWhiteSpace(apiKey) Then
                Throw New ArgumentException("API key cannot be null or empty.", NameOf(apiKey))
            End If

            If String.IsNullOrWhiteSpace(baseUrl) Then
                Throw New ArgumentException("Base URL cannot be null or empty.", NameOf(baseUrl))
            End If

            _httpClient = client
            _apiKey = apiKey
            _baseUrl = If(baseUrl.EndsWith("/"), baseUrl, baseUrl & "/")
        End Sub

        ''' <summary>
        ''' Sends a GET request to the specified endpoint and returns the JSON response.
        ''' </summary>
        ''' <param name="endpoint">The API endpoint to call.</param>
        ''' <returns>The JSON response as a string.</returns>
        Protected Async Function GetAsync(endpoint As String) As Task(Of String)
            Try
                Using response = Await _httpClient.GetAsync(endpoint.TrimStart("/"c))
                    Dim content = Await response.Content.ReadAsStringAsync()
                    
                    If response.IsSuccessStatusCode Then
                        Return content
                    Else
                        Throw New MistralApiException($"API request failed with status {response.StatusCode}: {content}")
                    End If
                End Using
            Catch ex As HttpRequestException
                Throw New MistralApiException($"HTTP request failed: {ex.Message}", ex)
            Catch ex As TaskCanceledException
                Throw New MistralApiException("Request timed out.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Sends a POST request to the specified endpoint with JSON content.
        ''' </summary>
        ''' <param name="endpoint">The API endpoint to call.</param>
        ''' <param name="jsonContent">The JSON content to send.</param>
        ''' <returns>The JSON response as a string.</returns>
        Protected Async Function PostAsync(endpoint As String, jsonContent As String) As Task(Of String)
            Try
                Using content As New StringContent(jsonContent, Encoding.UTF8, "application/json")
                Using response = Await _httpClient.PostAsync(endpoint.TrimStart("/"c), content)
                    Dim responseContent = Await response.Content.ReadAsStringAsync()
                    
                    If response.IsSuccessStatusCode Then
                        Return responseContent
                    Else
                        Throw New MistralApiException($"API request failed with status {response.StatusCode}: {responseContent}")
                    End If
                End Using
                End Using
            Catch ex As HttpRequestException
                Throw New MistralApiException($"HTTP request failed: {ex.Message}", ex)
            Catch ex As TaskCanceledException
                Throw New MistralApiException("Request timed out.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Sends a DELETE request to the specified endpoint.
        ''' </summary>
        ''' <param name="endpoint">The API endpoint to call.</param>
        ''' <returns>The JSON response as a string.</returns>
        Protected Async Function DeleteAsync(endpoint As String) As Task(Of String)
            Try
                Using response = Await _httpClient.DeleteAsync(endpoint.TrimStart("/"c))
                    Dim content = Await response.Content.ReadAsStringAsync()
                    
                    If response.IsSuccessStatusCode Then
                        Return content
                    Else
                        Throw New MistralApiException($"API request failed with status {response.StatusCode}: {content}")
                    End If
                End Using
            Catch ex As HttpRequestException
                Throw New MistralApiException($"HTTP request failed: {ex.Message}", ex)
            Catch ex As TaskCanceledException
                Throw New MistralApiException("Request timed out.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Sends a POST request with multipart form data.
        ''' </summary>
        ''' <param name="endpoint">The API endpoint to call.</param>
        ''' <param name="content">The multipart form data content.</param>
        ''' <returns>The JSON response as a string.</returns>
        Protected Async Function PostMultipartAsync(endpoint As String, content As MultipartFormDataContent) As Task(Of String)
            Try
                Using response = Await _httpClient.PostAsync(endpoint.TrimStart("/"c), content)
                    Dim responseContent = Await response.Content.ReadAsStringAsync()
                    
                    If response.IsSuccessStatusCode Then
                        Return responseContent
                    Else
                        Throw New MistralApiException($"API request failed with status {response.StatusCode}: {responseContent}")
                    End If
                End Using
            Catch ex As HttpRequestException
                Throw New MistralApiException($"HTTP request failed: {ex.Message}", ex)
            Catch ex As TaskCanceledException
                Throw New MistralApiException("Request timed out.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Sends a POST request for streaming responses.
        ''' </summary>
        ''' <typeparam name="T">The type to deserialize streaming chunks to.</typeparam>
        ''' <param name="endpoint">The API endpoint to call.</param>
        ''' <param name="jsonContent">The JSON content to send.</param>
        ''' <returns>An async enumerable of streaming response chunks.</returns>
        Protected Async Function PostStreamAsync(Of T)(endpoint As String, jsonContent As String) As Task(Of List(Of T))
            Try
                Using content As New StringContent(jsonContent, Encoding.UTF8, "application/json")
                Using response = Await _httpClient.PostAsync(endpoint.TrimStart("/"c), content)
                    If Not response.IsSuccessStatusCode Then
                        Dim errorContent = Await response.Content.ReadAsStringAsync()
                        Throw New MistralApiException($"API request failed with status {response.StatusCode}: {errorContent}")
                    End If
                    
                    ' Return list for streaming
                    Return Await ParseServerSentEvents(Of T)(response)
                End Using
                End Using
            Catch ex As HttpRequestException
                Throw New MistralApiException($"HTTP request failed: {ex.Message}", ex)
            Catch ex As TaskCanceledException
                Throw New MistralApiException("Request timed out.", ex)
            End Try
        End Function

        ''' <summary>
        ''' Parses Server-Sent Events from the HTTP response stream.
        ''' </summary>
        ''' <typeparam name="T">The type to deserialize each event to.</typeparam>
        ''' <param name="response">The HTTP response message.</param>
        ''' <returns>A list of parsed events.</returns>
        Private Async Function ParseServerSentEvents(Of T)(response As HttpResponseMessage) As Task(Of List(Of T))
            Dim results As New List(Of T)()
            Using stream = Await response.Content.ReadAsStreamAsync()
            Using reader As New StreamReader(stream)
                Dim line As String
                While (line = Await reader.ReadLineAsync()) IsNot Nothing
                    If Not String.IsNullOrWhiteSpace(line) AndAlso line.StartsWith("data: ") Then
                        Dim jsonData = line.Substring(6) ' Remove "data: " prefix
                        If jsonData.Trim() <> "[DONE]" Then
                            Try
                                results.Add(JsonConvert.DeserializeObject(Of T)(jsonData, GetSerializerSettings()))
                            Catch ex As JsonException
                                ' Skip malformed JSON chunks
                                Continue While
                            End Try
                        End If
                    End If
                End While
            End Using
            End Using
            Return results
        End Function

        ''' <summary>
        ''' Gets the default JSON serializer settings.
        ''' </summary>
        ''' <returns>JsonSerializerSettings configured for the API.</returns>
        Protected Function GetSerializerSettings() As JsonSerializerSettings
            Return New JsonSerializerSettings With {
                .NullValueHandling = NullValueHandling.Ignore,
                .DateFormatHandling = DateFormatHandling.IsoDateFormat,
                .DateTimeZoneHandling = DateTimeZoneHandling.Utc
            }
        End Function

    End Class

End Namespace
