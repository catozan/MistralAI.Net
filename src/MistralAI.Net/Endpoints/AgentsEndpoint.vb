Imports System.Text
Imports System.Net.Http
Imports Newtonsoft.Json
Imports MistralAI.Net.Models
Imports MistralAI.Net.Exceptions

Namespace MistralAI.Net.Endpoints

    ''' <summary>
    ''' Handles agent operations for the Mistral AI API.
    ''' </summary>
    Public Class AgentsEndpoint
        Inherits MistralAI.Net.Endpoints.BaseEndpoint

        ''' <summary>
        ''' Initializes a new instance of the AgentsEndpoint class.
        ''' </summary>
        ''' <param name="client">The HTTP client to use for requests.</param>
        ''' <param name="apiKey">The API key for authentication.</param>
        ''' <param name="baseUrl">The base URL for the API.</param>
        Public Sub New(client As HttpClient, apiKey As String, baseUrl As String)
            MyBase.New(client, apiKey, baseUrl)
        End Sub

        ''' <summary>
        ''' Creates a new agent.
        ''' </summary>
        ''' <param name="request">The agent creation request.</param>
        ''' <returns>Information about the created agent.</returns>
        Public Function Create(request As Models.Agents.AgentRequest) As Models.Agents.Agent
            Return CreateAsync(request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Creates a new agent asynchronously.
        ''' </summary>
        ''' <param name="request">The agent creation request.</param>
        ''' <returns>Information about the created agent.</returns>
        Public Async Function CreateAsync(request As Models.Agents.AgentRequest) As Task(Of Models.Agents.Agent)
            ValidateRequest(request)
            Dim json As String = JsonConvert.SerializeObject(request)
            Dim response As String = Await PostAsync("v1/agents", json)
            Return JsonConvert.DeserializeObject(Of Models.Agents.Agent)(response)
        End Function

        ''' <summary>
        ''' Lists available agents.
        ''' </summary>
        ''' <param name="limit">Optional limit for the number of agents to return.</param>
        ''' <param name="order">Optional order for the results (asc or desc).</param>
        ''' <param name="after">Optional cursor for pagination.</param>
        ''' <param name="before">Optional cursor for pagination.</param>
        ''' <returns>List of agents.</returns>
        Public Function List(Optional limit As Integer? = Nothing, Optional order As String = "desc", Optional after As String = Nothing, Optional before As String = Nothing) As Models.Agents.AgentList
            Return ListAsync(limit, order, after, before).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Lists available agents asynchronously.
        ''' </summary>
        ''' <param name="limit">Optional limit for the number of agents to return.</param>
        ''' <param name="order">Optional order for the results (asc or desc).</param>
        ''' <param name="after">Optional cursor for pagination.</param>
        ''' <param name="before">Optional cursor for pagination.</param>
        ''' <returns>List of agents.</returns>
        Public Async Function ListAsync(Optional limit As Integer? = Nothing, Optional order As String = "desc", Optional after As String = Nothing, Optional before As String = Nothing) As Task(Of Models.Agents.AgentList)
            Dim queryParams As New List(Of String)()
            
            If limit.HasValue Then
                queryParams.Add($"limit={limit.Value}")
            End If
            
            If Not String.IsNullOrEmpty(order) Then
                queryParams.Add($"order={order}")
            End If
            
            If Not String.IsNullOrEmpty(after) Then
                queryParams.Add($"after={after}")
            End If
            
            If Not String.IsNullOrEmpty(before) Then
                queryParams.Add($"before={before}")
            End If

            Dim query As String = If(queryParams.Count > 0, "?" & String.Join("&", queryParams), "")
            Dim response As String = Await GetAsync($"v1/agents{query}")
            Return JsonConvert.DeserializeObject(Of Models.Agents.AgentList)(response)
        End Function

        ''' <summary>
        ''' Retrieves information about a specific agent.
        ''' </summary>
        ''' <param name="agentId">The ID of the agent.</param>
        ''' <returns>Information about the agent.</returns>
        Public Function Retrieve(agentId As String) As Models.Agents.Agent
            Return RetrieveAsync(agentId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Retrieves information about a specific agent asynchronously.
        ''' </summary>
        ''' <param name="agentId">The ID of the agent.</param>
        ''' <returns>Information about the agent.</returns>
        Public Async Function RetrieveAsync(agentId As String) As Task(Of Models.Agents.Agent)
            ValidateAgentId(agentId)
            Dim response As String = Await GetAsync($"v1/agents/{agentId}")
            Return JsonConvert.DeserializeObject(Of Models.Agents.Agent)(response)
        End Function

        ''' <summary>
        ''' Updates an existing agent.
        ''' </summary>
        ''' <param name="agentId">The ID of the agent to update.</param>
        ''' <param name="request">The agent update request.</param>
        ''' <returns>Information about the updated agent.</returns>
        Public Function Update(agentId As String, request As Models.Agents.AgentUpdateRequest) As Models.Agents.Agent
            Return UpdateAsync(agentId, request).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Updates an existing agent asynchronously.
        ''' </summary>
        ''' <param name="agentId">The ID of the agent to update.</param>
        ''' <param name="request">The agent update request.</param>
        ''' <returns>Information about the updated agent.</returns>
        Public Async Function UpdateAsync(agentId As String, request As Models.Agents.AgentUpdateRequest) As Task(Of Models.Agents.Agent)
            ValidateAgentId(agentId)
            ValidateUpdateRequest(request)
            Dim json As String = JsonConvert.SerializeObject(request)
            Dim response As String = Await PostAsync($"v1/agents/{agentId}", json)
            Return JsonConvert.DeserializeObject(Of Models.Agents.Agent)(response)
        End Function

        ''' <summary>
        ''' Deletes an agent.
        ''' </summary>
        ''' <param name="agentId">The ID of the agent to delete.</param>
        ''' <returns>Deletion status.</returns>
        Public Function Delete(agentId As String) As Models.Agents.AgentDeleteResponse
            Return DeleteAsync(agentId).GetAwaiter().GetResult()
        End Function

        ''' <summary>
        ''' Deletes an agent asynchronously.
        ''' </summary>
        ''' <param name="agentId">The ID of the agent to delete.</param>
        ''' <returns>Deletion status.</returns>
        Public Overloads Async Function DeleteAsync(agentId As String) As Task(Of Models.Agents.AgentDeleteResponse)
            ValidateAgentId(agentId)
            Try
                Using response As HttpResponseMessage = Await HttpClient.DeleteAsync($"v1/agents/{agentId}")
                    Dim content As String = Await response.Content.ReadAsStringAsync()
                    
                    If response.IsSuccessStatusCode Then
                        Return JsonConvert.DeserializeObject(Of Models.Agents.AgentDeleteResponse)(content, JsonSettings)
                    Else
                        Throw New Exceptions.MistralApiException($"API request failed with status {response.StatusCode}: {content}")
                    End If
                End Using
            Catch ex As HttpRequestException
                Throw New Exceptions.MistralApiException($"HTTP request failed: {ex.Message}", ex)
            Catch ex As TaskCanceledException
                Throw New Exceptions.MistralApiException("Request timed out.", ex)
            End Try
        End Function

        Private Sub ValidateRequest(request As Models.Agents.AgentRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "Agent request cannot be null.")
            End If

            If String.IsNullOrWhiteSpace(request.Model) Then
                Throw New ArgumentException("Model cannot be null or empty.", NameOf(request.Model))
            End If
        End Sub

        Private Sub ValidateUpdateRequest(request As Models.Agents.AgentUpdateRequest)
            If request Is Nothing Then
                Throw New ArgumentNullException(NameOf(request), "Agent update request cannot be null.")
            End If
        End Sub

        Private Sub ValidateAgentId(agentId As String)
            If String.IsNullOrWhiteSpace(agentId) Then
                Throw New ArgumentException("Agent ID cannot be null or empty.", NameOf(agentId))
            End If
        End Sub

    End Class

End Namespace
