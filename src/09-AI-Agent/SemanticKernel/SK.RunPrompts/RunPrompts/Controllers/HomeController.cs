using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RunPrompts.Models;

namespace RunPrompts.Controllers;

public class HomeController : Controller
{
    private readonly Kernel _kernel;

    private readonly IChatCompletionService _chatCompletionService;

    public HomeController(Kernel kernel)
    {
        _kernel = kernel;
        _chatCompletionService = _kernel.GetRequiredService<IChatCompletionService>();
    }

    public IActionResult Index()
    {
        var connectionId = HttpContext.Connection.Id;

        // 从缓存中获取或初始化聊天记录
        var chatHistory = _chatHistoryCache.GetOrCreate(connectionId);

        // Get the reply from the chat completion service
        ChatMessageContent reply = await _chatCompletionService.GetChatMessageContentAsync(
            chatHistory,
            kernel: kernel
        );
        Console.WriteLine("Assistant: " + reply.ToString());
        chatHistory.AddAssistantMessage(reply.ToString());

        // Create a semantic kernel prompt template
        var skTemplateFactory = new KernelPromptTemplateFactory();
        var skPromptTemplate = skTemplateFactory.Create(new PromptTemplateConfig(
            """
            You are a helpful career advisor. Based on the users's skills and interest, suggest up to 5 suitable roles.
            Return the output as JSON in the following format:
            "Role Recommendations":
            {
            "recommendedRoles": [],
            "industries": [],
            "estimatedSalaryRange": ""
            }

            My skills are: . My interests are: . What are some roles that would be suitable for me?
            """
        ));
        // Render the Semanitc Kernel prompt with arguments
        var skRenderedPrompt = await skPromptTemplate.RenderAsync(
            kernel,
            new KernelArguments
            {
                ["skills"] = "Software Engineering, C#, Python, Drawing, Guitar, Dance",
                ["interests"] = "Education, Psychology, Programming, Helping Others"
            }
        );
        // Add the Semanitc Kernel prompt to the chat history and get the reply
        chatHistory.AddUserMessage(skRenderedPrompt);
        await GetReply();

        return View();
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
