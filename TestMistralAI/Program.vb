Imports System
Imports System.Threading.Tasks

Module Program
    Private Const API_KEY As String = "97ZQlsV45YrDusgZRwjArWGbh3nerFPb"

    Sub Main()
        MainAsync().GetAwaiter().GetResult()
    End Sub

    Async Function MainAsync() As Task
        Console.WriteLine("Testing MistralAI.Net VB.NET Library")
        Console.WriteLine("===================================")
        
        Try
            ' Initialize the client
            Using client As New MistralAI.Net.Client.MistralClient(API_KEY)
                Console.WriteLine("Client initialized successfully!")
                Console.WriteLine()
                
                ' Test 1: List available models
                Console.WriteLine("1. Testing Models endpoint...")
                Try
                    Dim models = Await client.Models.ListAsync()
                    Console.WriteLine($"Found {models.Data.Count} models:")
                    For Each model In models.Data
                        Console.WriteLine($"   - {model.Id} (owned by {model.OwnedBy})")
                    Next
                    Console.WriteLine("✅ Models test passed")
                Catch ex As Exception
                    Console.WriteLine($"❌ Models test failed: {ex.Message}")
                End Try
                
                Console.WriteLine()
                
                ' Test 2: Simple chat completion
                Console.WriteLine("2. Testing Chat endpoint...")
                Try
                    Dim response = Await client.Chat.CreateAsync("mistral-small-latest", "Hello, how are you today?")
                    Console.WriteLine($"Chat response: {response.Choices(0).Message.Content}")
                    Console.WriteLine("✅ Chat test passed")
                Catch ex As Exception
                    Console.WriteLine($"❌ Chat test failed: {ex.Message}")
                End Try
                
                Console.WriteLine()
                
                ' Test 3: Chat with system message
                Console.WriteLine("3. Testing Chat with system message...")
                Try
                    Dim response = Await client.Chat.CreateAsync("mistral-small-latest", 
                                                                "You are a helpful assistant. Be concise.", 
                                                                "What is the capital of France?")
                    Console.WriteLine($"Chat response: {response.Choices(0).Message.Content}")
                    Console.WriteLine("✅ Chat with system message test passed")
                Catch ex As Exception
                    Console.WriteLine($"❌ Chat with system message test failed: {ex.Message}")
                End Try
                
                Console.WriteLine()
                
                ' Test 4: Embeddings
                Console.WriteLine("4. Testing Embeddings endpoint...")
                Try
                    Dim response = Await client.Embeddings.CreateAsync("mistral-embed", "Hello, world!")
                    Console.WriteLine($"Generated embedding with {response.Data(0).Embedding.Count} dimensions")
                    Console.WriteLine("✅ Embeddings test passed")
                Catch ex As Exception
                    Console.WriteLine($"❌ Embeddings test failed: {ex.Message}")
                End Try
            End Using
            
            Console.WriteLine()
            Console.WriteLine("🎉 All tests completed!")
            
        Catch ex As Exception
            Console.WriteLine($"❌ Fatal error: {ex.Message}")
        End Try
        
        Console.WriteLine()
        Console.WriteLine("Press any key to exit...")
        Console.ReadKey()
    End Function
End Module
