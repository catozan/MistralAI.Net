Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Agents

    ''' <summary>
    ''' Request for creating an agent.
    ''' </summary>
    Public Class AgentRequest
        ''' <summary>
        ''' ID of the model to use.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The name of the agent.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' The description of the agent.
        ''' </summary>
        <JsonProperty("description")>
        Public Property Description As String

        ''' <summary>
        ''' The system instruction that the agent uses.
        ''' </summary>
        <JsonProperty("instructions")>
        Public Property Instructions As String

        ''' <summary>
        ''' A list of tool enabled on the agent.
        ''' </summary>
        <JsonProperty("tools")>
        Public Property Tools As List(Of AgentTool)

        ''' <summary>
        ''' A list of file IDs attached to this agent.
        ''' </summary>
        <JsonProperty("file_ids")>
        Public Property FileIds As List(Of String)

        ''' <summary>
        ''' Set of 16 key-value pairs that can be attached to an object.
        ''' </summary>
        <JsonProperty("metadata")>
        Public Property Metadata As Dictionary(Of String, String)

        Public Sub New()
            Tools = New List(Of AgentTool)()
            FileIds = New List(Of String)()
            Metadata = New Dictionary(Of String, String)()
        End Sub
    End Class

    ''' <summary>
    ''' Request for updating an agent.
    ''' </summary>
    Public Class AgentUpdateRequest
        ''' <summary>
        ''' ID of the model to use.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The name of the agent.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' The description of the agent.
        ''' </summary>
        <JsonProperty("description")>
        Public Property Description As String

        ''' <summary>
        ''' The system instruction that the agent uses.
        ''' </summary>
        <JsonProperty("instructions")>
        Public Property Instructions As String

        ''' <summary>
        ''' A list of tool enabled on the agent.
        ''' </summary>
        <JsonProperty("tools")>
        Public Property Tools As List(Of AgentTool)

        ''' <summary>
        ''' A list of file IDs attached to this agent.
        ''' </summary>
        <JsonProperty("file_ids")>
        Public Property FileIds As List(Of String)

        ''' <summary>
        ''' Set of 16 key-value pairs that can be attached to an object.
        ''' </summary>
        <JsonProperty("metadata")>
        Public Property Metadata As Dictionary(Of String, String)

        Public Sub New()
            Tools = New List(Of AgentTool)()
            FileIds = New List(Of String)()
            Metadata = New Dictionary(Of String, String)()
        End Sub
    End Class

    ''' <summary>
    ''' Represents an agent.
    ''' </summary>
    Public Class Agent
        ''' <summary>
        ''' The identifier, which can be referenced in API endpoints.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, which is always "agent".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the agent was created.
        ''' </summary>
        <JsonProperty("created_at")>
        Public Property CreatedAt As Long

        ''' <summary>
        ''' The name of the agent.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' The description of the agent.
        ''' </summary>
        <JsonProperty("description")>
        Public Property Description As String

        ''' <summary>
        ''' ID of the model to use.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The system instruction that the agent uses.
        ''' </summary>
        <JsonProperty("instructions")>
        Public Property Instructions As String

        ''' <summary>
        ''' A list of tool enabled on the agent.
        ''' </summary>
        <JsonProperty("tools")>
        Public Property Tools As List(Of AgentTool)

        ''' <summary>
        ''' A list of file IDs attached to this agent.
        ''' </summary>
        <JsonProperty("file_ids")>
        Public Property FileIds As List(Of String)

        ''' <summary>
        ''' Set of 16 key-value pairs that can be attached to an object.
        ''' </summary>
        <JsonProperty("metadata")>
        Public Property Metadata As Dictionary(Of String, String)

        Public Sub New()
            [Object] = "agent"
            Tools = New List(Of AgentTool)()
            FileIds = New List(Of String)()
            Metadata = New Dictionary(Of String, String)()
        End Sub
    End Class

    ''' <summary>
    ''' Represents a tool that can be used by an agent.
    ''' </summary>
    Public Class AgentTool
        ''' <summary>
        ''' The type of tool being defined.
        ''' </summary>
        <JsonProperty("type")>
        Public Property Type As String

        ''' <summary>
        ''' The function definition for function tools.
        ''' </summary>
        <JsonProperty("function")>
        Public Property [Function] As AgentFunction

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Represents a function that can be called by an agent.
    ''' </summary>
    Public Class AgentFunction
        ''' <summary>
        ''' A description of what the function does.
        ''' </summary>
        <JsonProperty("description")>
        Public Property Description As String

        ''' <summary>
        ''' The name of the function to be called.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' The parameters the functions accepts, described as a JSON Schema object.
        ''' </summary>
        <JsonProperty("parameters")>
        Public Property Parameters As Object

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Response containing a list of agents.
    ''' </summary>
    Public Class AgentList
        ''' <summary>
        ''' The object type, always "list".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of agents.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of Agent)

        ''' <summary>
        ''' The first ID in the current page of results.
        ''' </summary>
        <JsonProperty("first_id")>
        Public Property FirstId As String

        ''' <summary>
        ''' The last ID in the current page of results.
        ''' </summary>
        <JsonProperty("last_id")>
        Public Property LastId As String

        ''' <summary>
        ''' Whether there are more results available.
        ''' </summary>
        <JsonProperty("has_more")>
        Public Property HasMore As Boolean

        Public Sub New()
            Data = New List(Of Agent)()
            [Object] = "list"
        End Sub
    End Class

    ''' <summary>
    ''' Response from an agent deletion operation.
    ''' </summary>
    Public Class AgentDeleteResponse
        ''' <summary>
        ''' The ID of the deleted agent.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, always "agent.deleted".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' Whether the agent was successfully deleted.
        ''' </summary>
        <JsonProperty("deleted")>
        Public Property Deleted As Boolean

        Public Sub New()
            [Object] = "agent.deleted"
        End Sub
    End Class

End Namespace
