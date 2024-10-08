﻿// Add Azure OpenAI package
using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace azure_openai_api
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            IConfiguration config=new ConfigurationBuilder()
                .AddJsonFile("appsettings.json",true,true)
                .Build();

            string? oaiEndpoint = config["AzureOAIEndpoint"];
            string? oaiKey = config["AzureOAIKey"];
            string? oaiDeploymentName = config["AzureOAIDeploymentName"];

            if (string.IsNullOrEmpty(oaiEndpoint) || string.IsNullOrEmpty(oaiKey) || string.IsNullOrEmpty(oaiDeploymentName))
            {
                Console.WriteLine("Please check your appsettings.json file for missing or incorrect values.");
                return;
            }
            // Initialize the Azure OpenAI client
            OpenAIClient client = new OpenAIClient(new Uri(oaiEndpoint), new AzureKeyCredential(oaiKey));
            // System message to provide context to the model
            string systemMessage = @"你是个AI助手";

            var messagesList = new List<ChatRequestMessage>()
            {
                new ChatRequestSystemMessage(systemMessage),
            };// List to store messages

            do
            {
                Console.WriteLine("Enter your prompt text (or type 'quit' to exit): ");
                //我应该在雷尼尔附近徒步旅行吗？
                string? inputText = Console.ReadLine();
                if (inputText == "quite") break;

                if(inputText==null)
                {
                    Console.WriteLine("Please enter a prompt.");
                    continue;
                }

                Console.WriteLine("\nSending request for summary to Azure OpenAI endpoint...\n\n");
                // Add code to send request...
                // Build completion options object
                #region
                //ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions()
                //{
                //    Messages =
                //    {
                //        new ChatRequestSystemMessage(systemMessage),
                //        new ChatRequestUserMessage(inputText),
                //    },
                //    MaxTokens = 400,
                //    Temperature = 1f,
                //    DeploymentName = oaiDeploymentName
                //};
                //// Send request to Azure OpenAI model
                //ChatCompletions response = client.GetChatCompletions(chatCompletionsOptions);

                //// Print the response
                //string completion = response.Choices[0].Message.Content;
                //Console.WriteLine("Response: " + completion + "\n");
                #endregion
                // Add code to send request...
                // Build completion options object

                #region

                messagesList.Add(new ChatRequestUserMessage(inputText));

                ChatCompletionsOptions chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    MaxTokens = 1200,
                    Temperature = 0.7f,
                    DeploymentName = oaiDeploymentName
                };

                // Add messages to the completion options
                foreach (ChatRequestMessage chatMessage in messagesList)
                {
                    chatCompletionsOptions.Messages.Add(chatMessage);
                }

                // Send request to Azure OpenAI model
                ChatCompletions response = client.GetChatCompletions(chatCompletionsOptions);

                // Return the response
                string completion = response.Choices[0].Message.Content;

                // Add generated text to messages list
                messagesList.Add(new ChatRequestAssistantMessage(completion));

                Console.WriteLine("Response: " + completion + "\n");

                #endregion
            } while (true);

        }
    }
}