Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace MistralAI.Net.Models.Beta

    ''' <summary>
    ''' Represents a beta feature.
    ''' </summary>
    Public Class BetaFeature
        ''' <summary>
        ''' The unique identifier of the beta feature.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The name of the beta feature.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' Description of the beta feature.
        ''' </summary>
        <JsonProperty("description")>
        Public Property Description As String

        ''' <summary>
        ''' The current status of the beta feature.
        ''' </summary>
        <JsonProperty("status")>
        Public Property Status As String

        ''' <summary>
        ''' Whether the feature is currently enabled for the user.
        ''' </summary>
        <JsonProperty("enabled")>
        Public Property Enabled As Boolean

        ''' <summary>
        ''' The version of the beta feature.
        ''' </summary>
        <JsonProperty("version")>
        Public Property Version As String

        ''' <summary>
        ''' Documentation URL for the beta feature.
        ''' </summary>
        <JsonProperty("documentation_url")>
        Public Property DocumentationUrl As String

        ''' <summary>
        ''' Expected graduation date from beta to stable.
        ''' </summary>
        <JsonProperty("expected_graduation_date")>
        Public Property ExpectedGraduationDate As String

        ''' <summary>
        ''' Feedback URL for reporting issues or suggestions.
        ''' </summary>
        <JsonProperty("feedback_url")>
        Public Property FeedbackUrl As String

        ''' <summary>
        ''' List of capabilities provided by this beta feature.
        ''' </summary>
        <JsonProperty("capabilities")>
        Public Property Capabilities As List(Of String)

        ''' <summary>
        ''' Known limitations of the beta feature.
        ''' </summary>
        <JsonProperty("limitations")>
        Public Property Limitations As List(Of String)

        Public Sub New()
            Capabilities = New List(Of String)()
            Limitations = New List(Of String)()
        End Sub
    End Class

    ''' <summary>
    ''' Response containing a list of beta features.
    ''' </summary>
    Public Class BetaFeatureList
        ''' <summary>
        ''' The object type, always "list".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of beta features.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of BetaFeature)

        ''' <summary>
        ''' Total number of beta features available.
        ''' </summary>
        <JsonProperty("total_count")>
        Public Property TotalCount As Integer

        Public Sub New()
            Data = New List(Of BetaFeature)()
            [Object] = "list"
        End Sub
    End Class

    ''' <summary>
    ''' Response from enabling or disabling a beta feature.
    ''' </summary>
    Public Class BetaFeatureToggleResponse
        ''' <summary>
        ''' The ID of the beta feature that was toggled.
        ''' </summary>
        <JsonProperty("feature_id")>
        Public Property FeatureId As String

        ''' <summary>
        ''' Whether the feature is now enabled.
        ''' </summary>
        <JsonProperty("enabled")>
        Public Property Enabled As Boolean

        ''' <summary>
        ''' Timestamp when the change was made.
        ''' </summary>
        <JsonProperty("changed_at")>
        Public Property ChangedAt As String

        ''' <summary>
        ''' Success message.
        ''' </summary>
        <JsonProperty("message")>
        Public Property Message As String

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

End Namespace
