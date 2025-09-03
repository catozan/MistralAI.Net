Imports System
Imports System.Threading.Tasks
Imports MistralAI.Net.Client
Imports MistralAI.Net.Models.Chat
Imports MistralAI.Net.Utils

''' &lt;summary&gt;
''' Simple example demonstrating basic chat completion using the Mistral AI VB.NET library.
''' &lt;/summary&gt;
Module BasicChatExample

    Sub Main()
        MainAsync().GetAwaiter().GetResult()
    End Sub

    Async Function MainAsync() As Task
        ' Replace with your actual API key
        Dim apiKey As String = Environment.GetEnvironmentVariable("MISTRAL_API_KEY")
        
        If String.IsNullOrEmpty(apiKey) Then
            Console.WriteLine("Please set the MISTRAL_API_KEY environment variable.")
            Console.WriteLine("You can get an API key from: https://console.mistral.ai/")
            Return
        End If

        Try
            ' Initialize the Mistral client
            Using client As New MistralClient(apiKey)
                Console.WriteLine("Mistral AI VB.NET Client Example")
                Console.WriteLine("================================")
                Console.WriteLine()

                ' Example 1: Simple chat completion
                Console.WriteLine("Example 1: Simple Chat Completion")
                Dim response1 = Await client.Chat.CreateAsync(
                    MistralConstants.Models.MistralLargeLatest,
                    "Hello! Can you explain what VB.NET is in one sentence?"
                )

                Console.WriteLine($"Response: {response1.Choices(0).Message.Content}")
                Console.WriteLine($"Tokens used: {response1.Usage.TotalTokens}")
                Console.WriteLine()

                ' Example 2: Chat with system message
                Console.WriteLine("Example 2: Chat with System Message")
                Dim response2 = Await client.Chat.CreateAsync(
                    MistralConstants.Models.MistralLargeLatest,
                    "You are a helpful programming assistant specialized in VB.NET.",
                    "What are the main advantages of using VB.NET for enterprise applications?"
                )

                Console.WriteLine($"Response: {response2.Choices(0).Message.Content}")
                Console.WriteLine()

                ' Example 3: Conversation with multiple messages
                Console.WriteLine("Example 3: Multi-turn Conversation")
                Dim messages As New List(Of ChatMessage) From {
                    New ChatMessage(MistralConstants.Roles.System, "You are a code reviewer."),
                    New ChatMessage(MistralConstants.Roles.User, "Here's some VB.NET code: Dim x As Integer = 5"),
                    New ChatMessage(MistralConstants.Roles.Assistant, "This code declares an integer variable and initializes it to 5."),
                    New ChatMessage(MistralConstants.Roles.User, "How can I make it more descriptive?")
                }

                Dim response3 = Await client.Chat.CreateAsync(
                    MistralConstants.Models.MistralLargeLatest,
                    messages,
                    temperature:=0.7
                )

                Console.WriteLine($"Response: {response3.Choices(0).Message.Content}")
                Console.WriteLine()

                ' Example 4: List available models
                Console.WriteLine("Example 4: Available Models")
                Dim modelList = Await client.Models.ListAsync()
                Console.WriteLine($"Found {modelList.Data.Count} models:")
                For Each model In modelList.Data.Take(5) ' Show first 5 models
                    Console.WriteLine($"- {model.Id} (owned by: {model.OwnedBy})")
                Next
                Console.WriteLine()

                ' Example 5: Get embeddings
                Console.WriteLine("Example 5: Text Embeddings")
                Try
                    Dim embeddingResponse = Await client.Embeddings.CreateAsync(
                        MistralConstants.Models.MistralEmbed,
                        "VB.NET is a multi-paradigm programming language."
                    )

                    Console.WriteLine($"Generated embedding with {embeddingResponse.Data(0).Embedding.Count} dimensions")
                    Console.WriteLine($"First 5 dimensions: {String.Join(", ", embeddingResponse.Data(0).Embedding.Take(5).Select(Function(x) x.ToString("F4")))}")
                Catch ex As Exception
                    Console.WriteLine($"Embeddings example failed: {ex.Message}")
                End Try

                Console.WriteLine()
                Console.WriteLine("All examples completed successfully!")

            End Using

        Catch ex As Exception
            Console.WriteLine($"Error: {ex.Message}")
            If ex.InnerException IsNot Nothing Then
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}")
            End If
        End Try

        Console.WriteLine()
        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()
    End Function

End Module
