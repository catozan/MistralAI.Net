Imports Newtonsoft.Json
Imports System.Collections.Generic

Namespace MistralAI.Net.Models.Chat

    ''' <summary>
    ''' Represents a chat message in a conversation.
    ''' </summary>
    Public Class ChatMessage

        ''' <summary>
        ''' The role of the message author (system, user, assistant, or tool).
        ''' </summary>
        <JsonProperty("role")>
        Public Property Role As String

        ''' <summary>
        ''' The content of the message.
        ''' </summary>
        <JsonProperty("content")>
        Public Property Content As String

        ''' <summary>
        ''' Optional name of the message author.
        ''' </summary>
        <JsonProperty("name", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Name As String

        ''' <summary>
        ''' Tool calls made by the assistant (only for assistant messages).
        ''' </summary>
        <JsonProperty("tool_calls", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ToolCalls As List(Of ToolCall)

        ''' <summary>
        ''' Tool call ID (only for tool messages).
        ''' </summary>
        <JsonProperty("tool_call_id", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ToolCallId As String

        Public Sub New()
        End Sub

        Public Sub New(role As String, content As String)
            Me.Role = role
            Me.Content = content
        End Sub

    End Class

    ''' <summary>
    ''' Represents a tool call made by the assistant.
    ''' </summary>
    Public Class ToolCall

        ''' <summary>
        ''' The ID of the tool call.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The type of the tool call (always "function").
        ''' </summary>
        <JsonProperty("type")>
        Public Property Type As String

        ''' <summary>
        ''' The function details of the tool call.
        ''' </summary>
        <JsonProperty("function")>
        Public Property [Function] As FunctionCall

    End Class

    ''' <summary>
    ''' Represents a function call within a tool call.
    ''' </summary>
    Public Class FunctionCall

        ''' <summary>
        ''' The name of the function to call.
        ''' </summary>
        <JsonProperty("name")>
        Public Property Name As String

        ''' <summary>
        ''' The arguments to pass to the function, as a JSON string.
        ''' </summary>
        <JsonProperty("arguments")>
        Public Property Arguments As String

    End Class

    ''' <summary>
    ''' Request object for chat completions.
    ''' </summary>
    Public Class ChatCompletionRequest

        ''' <summary>
        ''' ID of the model to use.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' The messages to generate completions for.
        ''' </summary>
        <JsonProperty("messages")>
        Public Property Messages As List(Of ChatMessage)

        ''' <summary>
        ''' What sampling temperature to use (0.0 to 2.0).
        ''' </summary>
        <JsonProperty("temperature", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Temperature As Double?

        ''' <summary>
        ''' Nucleus sampling probability (0.0 to 1.0).
        ''' </summary>
        <JsonProperty("top_p", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property TopP As Double?

        ''' <summary>
        ''' The maximum number of tokens to generate.
        ''' </summary>
        <JsonProperty("max_tokens", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property MaxTokens As Integer?

        ''' <summary>
        ''' Whether to stream back partial progress.
        ''' </summary>
        <JsonProperty("stream", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Stream As Boolean?

        ''' <summary>
        ''' Stop generation if these tokens are detected.
        ''' </summary>
        <JsonProperty("stop", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property [Stop] As Object ' Can be String or List(Of String)

        ''' <summary>
        ''' The seed to use for random sampling.
        ''' </summary>
        <JsonProperty("random_seed", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property RandomSeed As Integer?

        ''' <summary>
        ''' Response format specification.
        ''' </summary>
        <JsonProperty("response_format", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ResponseFormat As ResponseFormat

        ''' <summary>
        ''' A list of tools available for the model to call.
        ''' </summary>
        <JsonProperty("tools", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Tools As List(Of Tool)

        ''' <summary>
        ''' Controls which tools are called.
        ''' </summary>
        <JsonProperty("tool_choice", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ToolChoice As Object ' Can be String or ToolChoice object

        ''' <summary>
        ''' Presence penalty (-2.0 to 2.0).
        ''' </summary>
        <JsonProperty("presence_penalty", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property PresencePenalty As Double?

        ''' <summary>
        ''' Frequency penalty (-2.0 to 2.0).
        ''' </summary>
        <JsonProperty("frequency_penalty", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property FrequencyPenalty As Double?

        ''' <summary>
        ''' Number of completions to return for each request.
        ''' </summary>
        <JsonProperty("n", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property N As Integer?

        ''' <summary>
        ''' Prediction settings for speculative generation.
        ''' </summary>
        <JsonProperty("prediction", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Prediction As Prediction

        ''' <summary>
        ''' Whether to enable parallel tool calls.
        ''' </summary>
        <JsonProperty("parallel_tool_calls", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ParallelToolCalls As Boolean?

        ''' <summary>
        ''' Prompt mode setting.
        ''' </summary>
        <JsonProperty("prompt_mode", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property PromptMode As String

        ''' <summary>
        ''' Whether to inject a safety prompt.
        ''' </summary>
        <JsonProperty("safe_prompt", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property SafePrompt As Boolean?

        Public Sub New()
            Messages = New List(Of ChatMessage)()
        End Sub

    End Class

    ''' <summary>
    ''' Response object for chat completions.
    ''' </summary>
    Public Class ChatCompletionResponse

        ''' <summary>
        ''' A unique identifier for the chat completion.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, always "chat.completion".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The Unix timestamp of when the completion was created.
        ''' </summary>
        <JsonProperty("created")>
        Public Property Created As Long

        ''' <summary>
        ''' The model used for the completion.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' A list of completion choices.
        ''' </summary>
        <JsonProperty("choices")>
        Public Property Choices As List(Of ChatCompletionChoice)

        ''' <summary>
        ''' Usage statistics for the completion request.
        ''' </summary>
        <JsonProperty("usage")>
        Public Property Usage As UsageInfo

        Public Sub New()
            Choices = New List(Of ChatCompletionChoice)()
        End Sub

    End Class

    ''' <summary>
    ''' Represents a completion choice in the response.
    ''' </summary>
    Public Class ChatCompletionChoice

        ''' <summary>
        ''' The index of this choice in the list.
        ''' </summary>
        <JsonProperty("index")>
        Public Property Index As Integer

        ''' <summary>
        ''' The message generated by the model.
        ''' </summary>
        <JsonProperty("message")>
        Public Property Message As ChatMessage

        ''' <summary>
        ''' The reason the model stopped generating tokens.
        ''' </summary>
        <JsonProperty("finish_reason")>
        Public Property FinishReason As String

    End Class

    ''' <summary>
    ''' Usage statistics for API requests.
    ''' </summary>
    Public Class UsageInfo

        ''' <summary>
        ''' Number of tokens in the prompt.
        ''' </summary>
        <JsonProperty("prompt_tokens")>
        Public Property PromptTokens As Integer

        ''' <summary>
        ''' Number of tokens in the completion.
        ''' </summary>
        <JsonProperty("completion_tokens")>
        Public Property CompletionTokens As Integer

        ''' <summary>
        ''' Total number of tokens used.
        ''' </summary>
        <JsonProperty("total_tokens")>
        Public Property TotalTokens As Integer

    End Class

    ''' <summary>
    ''' Represents a streaming chat completion chunk.
    ''' </summary>
    Public Class ChatCompletionChunk

        ''' <summary>
        ''' A unique identifier for the chat completion.
        ''' </summary>
        <JsonProperty("id")>
        Public Property Id As String

        ''' <summary>
        ''' The object type, always "chat.completion.chunk".
        ''' </summary>
        <JsonProperty("object")>
        Public Property [Object] As String

        ''' <summary>
        ''' The Unix timestamp of when the completion was created.
        ''' </summary>
        <JsonProperty("created")>
        Public Property Created As Long

        ''' <summary>
        ''' The model used for the completion.
        ''' </summary>
        <JsonProperty("model")>
        Public Property Model As String

        ''' <summary>
        ''' A list of completion choices for this chunk.
        ''' </summary>
        <JsonProperty("choices")>
        Public Property Choices As List(Of ChatCompletionStreamChoice)

        Public Sub New()
            Choices = New List(Of ChatCompletionStreamChoice)()
        End Sub

    End Class

    ''' <summary>
    ''' Represents a choice in a streaming chat completion.
    ''' </summary>
    Public Class ChatCompletionStreamChoice

        ''' <summary>
        ''' The index of this choice in the list.
        ''' </summary>
        <JsonProperty("index")>
        Public Property Index As Integer

        ''' <summary>
        ''' The delta content for this chunk.
        ''' </summary>
        <JsonProperty("delta")>
        Public Property Delta As ChatMessageDelta

        ''' <summary>
        ''' The reason the model stopped generating tokens.
        ''' </summary>
        <JsonProperty("finish_reason")>
        Public Property FinishReason As String

    End Class

    ''' <summary>
    ''' Represents a delta message in streaming responses.
    ''' </summary>
    Public Class ChatMessageDelta

        ''' <summary>
        ''' The role of the message author (if this is the first chunk).
        ''' </summary>
        <JsonProperty("role", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Role As String

        ''' <summary>
        ''' The content delta for this chunk.
        ''' </summary>
        <JsonProperty("content", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Content As String

        ''' <summary>
        ''' Tool call deltas (if any).
        ''' </summary>
        <JsonProperty("tool_calls", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property ToolCalls As List(Of ToolCallDelta)

    End Class

    ''' <summary>
    ''' Represents a tool call delta in streaming responses.
    ''' </summary>
    Public Class ToolCallDelta

        ''' <summary>
        ''' The index of this tool call.
        ''' </summary>
        <JsonProperty("index")>
        Public Property Index As Integer

        ''' <summary>
        ''' The ID of the tool call.
        ''' </summary>
        <JsonProperty("id", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Id As String

        ''' <summary>
        ''' The type of the tool call.
        ''' </summary>
        <JsonProperty("type", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Type As String

        ''' <summary>
        ''' The function call delta.
        ''' </summary>
        <JsonProperty("function", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property [Function] As FunctionCallDelta

    End Class

    ''' <summary>
    ''' Represents a function call delta in streaming responses.
    ''' </summary>
    Public Class FunctionCallDelta

        ''' <summary>
        ''' The name of the function (if this is the first chunk).
        ''' </summary>
        <JsonProperty("name", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Name As String

        ''' <summary>
        ''' The arguments delta for this chunk.
        ''' </summary>
        <JsonProperty("arguments", NullValueHandling:=NullValueHandling.Ignore)>
        Public Property Arguments As String

    End Class

End Namespace
