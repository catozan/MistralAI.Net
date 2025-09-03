Imports System
Imports System.Collections.Generic
Imports Newtonsoft.Json

Namespace Models.FineTuning

    ''' <summary>
    ''' Request for creating a fine-tuning job.
    ''' </summary>
    Public Class FineTuningJobRequest
        ''' <summary>
        ''' The name of the model to fine-tune.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The ID of an uploaded file that contains training data.
        ''' </summary>
        <JsonProperty("training_file")>
        Public Property TrainingFile As String

        ''' <summary>
        ''' The ID of an uploaded file that contains validation data (optional).
        ''' </summary>
        <JsonProperty("validation_file")>
        Public Property ValidationFile As String

        ''' <summary>
        ''' A string of up to 18 characters that will be added to your fine-tuned model name.
        ''' </summary>
        <JsonProperty("suffix")>
        Public Property Suffix As String

        ''' <summary>
        ''' The hyperparameters used for the fine-tuning job.
        ''' </summary>
        <JsonProperty("hyperparameters")>
        Public Property Hyperparameters As FineTuningHyperparameters

        ''' <summary>
        ''' Integrations to enable for your fine-tuning job.
        ''' </summary>
        <JsonProperty("integrations")>
        Public Property Integrations As List(Of FineTuningIntegration)

        Public Sub New()
            Integrations = New List(Of FineTuningIntegration)()
        End Sub
    End Class

    ''' <summary>
    ''' Represents a fine-tuning job.
    ''' </summary>
    Public Class FineTuningJob
        ''' <summary>
        ''' The object identifier, which can be referenced in the API endpoints.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, which is always "fine_tuning.job".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the fine-tuning job was created.
        ''' </summary>
        <JsonProperty("created_at")>
        Public Property CreatedAt As Long

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the fine-tuning job was finished.
        ''' </summary>
        <JsonProperty("finished_at")>
        Public Property FinishedAt As Long?

        ''' <summary>
        ''' The base model that is being fine-tuned.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The name of the fine-tuned model that is being created.
        ''' </summary>
        <JsonProperty("fine_tuned_model")>
        Public Property FineTunedModel As String

        ''' <summary>
        ''' The organization that owns the fine-tuning job.
        ''' </summary>
        <JsonProperty("organization_id")>
        Public Property OrganizationId As String

        ''' <summary>
        ''' The current status of the fine-tuning job.
        ''' </summary>
        <JsonProperty("status")>
        Public Property Status As String

        ''' <summary>
        ''' The hyperparameters used for the fine-tuning job.
        ''' </summary>
        <JsonProperty("hyperparameters")>
        Public Property Hyperparameters As FineTuningHyperparameters

        ''' <summary>
        ''' The file ID used for training.
        ''' </summary>
        <JsonProperty("training_file")>
        Public Property TrainingFile As String

        ''' <summary>
        ''' The file ID used for validation.
        ''' </summary>
        <JsonProperty("validation_file")>
        Public Property ValidationFile As String

        ''' <summary>
        ''' The compiled results file ID(s) for the fine-tuning job.
        ''' </summary>
        <JsonProperty("result_files")>
        Public Property ResultFiles As List(Of String)

        ''' <summary>
        ''' The total number of billable tokens processed by this fine-tuning job.
        ''' </summary>
        <JsonProperty("trained_tokens")>
        Public Property TrainedTokens As Integer?

        ''' <summary>
        ''' For fine-tuning jobs that have failed, this will contain more information on the cause of the failure.
        ''' </summary>
        <JsonProperty("error")>
        Public Property [Error] As FineTuningError

        Public Sub New()
            [Object] = "fine_tuning.job"
            ResultFiles = New List(Of String)()
        End Sub
    End Class

    ''' <summary>
    ''' Hyperparameters for fine-tuning.
    ''' </summary>
    Public Class FineTuningHyperparameters
        ''' <summary>
        ''' Number of examples in each batch.
        ''' </summary>
        <JsonProperty("batch_size")>
        Public Property BatchSize As Integer?

        ''' <summary>
        ''' Scaling factor for the learning rate.
        ''' </summary>
        <JsonProperty("learning_rate_multiplier")>
        Public Property LearningRateMultiplier As Double?

        ''' <summary>
        ''' The number of epochs to train the model for.
        ''' </summary>
        <JsonProperty("n_epochs")>
        Public Property NEpochs As Integer?

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Integration configuration for fine-tuning jobs.
    ''' </summary>
    Public Class FineTuningIntegration
        ''' <summary>
        ''' The type of integration to enable.
        ''' </summary>
        <JsonProperty("type")>
        Public Property Type As String

        ''' <summary>
        ''' The settings for a Weights and Biases integration.
        ''' </summary>
        <JsonProperty("wandb")>
        Public Property Wandb As WandbIntegration

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Weights and Biases integration settings.
    ''' </summary>
    Public Class WandbIntegration
        ''' <summary>
        ''' The name of the project that the new run will be created under.
        ''' </summary>
        <JsonProperty("project")>
        Public Property Project As String

        ''' <summary>
        ''' A display name to set for the run.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' A list of tags to be attached to the newly created run.
        ''' </summary>
        <JsonProperty("tags")>
        Public Property Tags As List(Of String)

        Public Sub New()
            Tags = New List(Of String)()
        End Sub
    End Class

    ''' <summary>
    ''' Error information for failed fine-tuning jobs.
    ''' </summary>
    Public Class FineTuningError
        ''' <summary>
        ''' A machine-readable error code.
        ''' </summary>
        <JsonProperty("code")>
        Public Property Code As String

        ''' <summary>
        ''' A human-readable error message.
        ''' </summary>
        <JsonProperty("message")>
        Public Property Message As String

        ''' <summary>
        ''' The parameter that was invalid, usually training_file or validation_file.
        ''' </summary>
        <JsonProperty("param")>
        Public Property Param As String

        Public Sub New()
            ' Default constructor
        End Sub
    End Class

    ''' <summary>
    ''' Response containing a list of fine-tuning jobs.
    ''' </summary>
    Public Class FineTuningJobList
        ''' <summary>
        ''' The object type, always "list".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of fine-tuning jobs.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of FineTuningJob)

        ''' <summary>
        ''' Whether there are more fine-tuning jobs available.
        ''' </summary>
        <JsonProperty("has_more")>
        Public Property HasMore As Boolean

        Public Sub New()
            Data = New List(Of FineTuningJob)()
            [Object] = "list"
        End Sub
    End Class

    ''' <summary>
    ''' Represents an event in a fine-tuning job.
    ''' </summary>
    Public Class FineTuningJobEvent
        ''' <summary>
        ''' The event identifier.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, which is always "fine_tuning.job.event".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The Unix timestamp (in seconds) for when the event was created.
        ''' </summary>
        <JsonProperty("created_at")>
        Public Property CreatedAt As Long

        ''' <summary>
        ''' The log level of the event.
        ''' </summary>
        <JsonProperty("level")>
        Public Property Level As String

        ''' <summary>
        ''' The message describing the event.
        ''' </summary>
        <JsonProperty("message")>
        Public Property Message As String

        Public Sub New()
            [Object] = "fine_tuning.job.event"
        End Sub
    End Class

    ''' <summary>
    ''' Response containing a list of fine-tuning job events.
    ''' </summary>
    Public Class FineTuningJobEventList
        ''' <summary>
        ''' The object type, always "list".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' List of fine-tuning job events.
        ''' </summary>
        <JsonProperty("data")>
        Public Property Data As List(Of FineTuningJobEvent)

        Public Sub New()
            Data = New List(Of FineTuningJobEvent)()
            [Object] = "list"
        End Sub
    End Class

End Namespace
