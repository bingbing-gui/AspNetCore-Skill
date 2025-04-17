using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
// Add references
using Azure.Identity;
using Azure.AI.Projects;
using Azure.AI.Inference;
using Azure;
using Azure.AI.OpenAI;  
using System.Text.Json;
using ChatApp.Models;

namespace ChatApp.Controllers;
public class ChatController : Controller
{
    private readonly ILogger<ChatController> _logger;

    public ChatController(ILogger<ChatController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    [Route("Chat/SendMessageAsync")]  // API 路由
    public async Task SendMessageAsync([FromBody] string message)
    {
        // 设置响应流的内容类型为 SSE
        HttpResponse response = HttpContext.Response;
        response.ContentType = "text/event-stream";  // 设置为 SSE
        response.Headers.Add("Cache-Control", "no-cache");  // 防止缓存
        response.Headers.Add("Connection", "keep-alive");  // 保持连接

        await response.StartAsync();  // 启动响应流

        // 模拟一些生成数据的过程
        var aiReply = $"你说的是： {message}。AI 正在处理...";

        // 初始化配置
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        IConfigurationRoot configuration = builder.Build();
        string project_connection = configuration["PROJECT_CONNECTION"];
        string model_deployment = configuration["MODEL_DEPLOYMENT"];

        // 初始化项目客户端
        var projectClient = new AIProjectClient(project_connection, new DefaultAzureCredential());

        // 获取聊天客户端
        ChatCompletionsClient chat = projectClient.GetChatCompletionsClient();

        // 初始化聊天提示
        var prompt = new List<ChatRequestMessage>
        {
            new ChatRequestSystemMessage("你是个AI助手帮助回答问题.")
        };

        string input_text = message;  // 使用传入的消息作为输入

        try
        {
            // 模拟逐步生成响应并流式写入
            while (input_text.ToLower() != "quit")
            {
                // 更新提示消息
                prompt.Add(new ChatRequestUserMessage(input_text));
                var requestOptions = new ChatCompletionsOptions
                {
                    Model = model_deployment,
                    Messages = prompt
                };

                // 获取聊天完成响应
                Response<ChatCompletions> chatResponse = chat.Complete(requestOptions);
                var completion = chatResponse.Value.Content;

                // 创建并发送 SSE 消息
                string messageResponse = $"data: {JsonSerializer.Serialize(new { reply = completion })}\n\n";
                await response.WriteAsync(messageResponse);  // 写入响应
                await response.Body.FlushAsync();  // 确保数据立即发送

                // 将 AI 回复添加到提示中，以便进行下一轮对话
                prompt.Add(new ChatRequestAssistantMessage(completion));

                // 模拟延迟，实际中可以根据需要调整或替换为实际的异步操作
                await Task.Delay(1000);  // 可调整延迟
                input_text = "";  // 清空输入文本
            }
        }
        catch (Exception ex)
        {
            // 错误处理
            string errorMessage = $"data: {JsonSerializer.Serialize(new { error = "Error processing the request", details = ex.Message })}\n\n";
            await response.WriteAsync(errorMessage);
        }
        finally
        {
            // 确保响应结束
            await response.CompleteAsync();
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
