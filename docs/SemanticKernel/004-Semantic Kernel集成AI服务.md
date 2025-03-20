Semantic Kernel 的一个主要功能是能够向Kernel添加不同的 AI 服务。这使您可以轻松切换不同的 AI 服务，以比较它们的性能，并利用最适合您需求的模型。在本节中，我们将提供示例代码，展示如何向内核添加不同的 AI 服务。

在 Semantic Kernel 内部，针对最常见的 AI 任务提供了相应的接口。下表列出了每个 SDK 支持的 AI 服务。

| **服务**                    | **C#** | **Python** | **Java** | **Notes** |
|----------------------------------|:------:|:---------:|:-------:|----------|
| **Chat completion**              | ✅     | ✅        | ✅      |          |
| **Text generation**              | ✅     | ✅        | ✅      |          |
| **Embedding generation** (Exp.)  | ✅     | ✅        | ✅      |          |
| **Text-to-image** (Exp.)         | ✅     | ✅        | ❌      |          |
| **Image-to-text** (Exp.)         | ✅     | ❌        | ❌      |          |
| **Text-to-audio** (Exp.)         | ✅     | ✅        | ❌      |          |
| **Audio-to-text** (Exp.)         | ✅     | ✅        | ❌      |          |
| **Realtime** (Exp.)              | ❌     | ✅        | ❌      |          |

## Chat completion

通过聊天补全（Chat Completion），您可以模拟与AI代理的往返对话。这不仅适用于创建聊天机器人，还可以用于构建自主代理，以执行业务流程、生成代码等。作为 OpenAI、Google、Mistral、Facebook 等公司提供的主要模型类型，聊天补全是您在 Semantic Kernel 项目中最常添加的 AI 服务。

在选择**聊天补全（Chat Completion）**模型时，您需要考虑以下因素：

模型支持哪些形态（例如：文本、图像、音频等）？
是否支持函数调用（Function Calling）？
生成和接收 token 的速度如何？
每个 token 的成本是多少？
重要提示
在上述所有问题中，最关键的是模型是否支持函数调用。
如果不支持，您将无法使用该模型来调用现有代码。
目前，OpenAI、Google、Mistral 和 Amazon 的大部分最新模型均支持函数调用，但小型语言模型的支持仍然有限。

### 设置本地环境变量

某些 AI 服务可以本地部署，并可能需要一些设置。以下是支持本地部署的服务的相关指南。

### 可以在本地设置的AI服务

某些 AI 服务可以本地部署，并可能需要一些设置。以下是支持本地部署的服务的相关指南。

| Service               | Local Setup Support |
|-----------------------|--------------------|
| **Azure OpenAI**      | No local setup.    |
| **OpenAI**           | No local setup.    |
| **Mistral**          | No local setup.    |
| **Google**           | No local setup.    |
| **Hugging Face**     | No local setup.    |
| **Azure AI Inference** | No local setup.    |
| **Ollama**           | ✅ Supports local setup via Docker |

### **Ollama Local Setup Instructions**

要使用 Docker 在本地运行 **Ollama**，请使用以下命令启动一个使用 CPU 的容器：
```bash
docker run -d -v "c:\temp\ollama:/root/.ollama" -p 11434:11434 --name ollama ollama/ollama
```

要使用 GPU 加速运行，请使用以下命令：

```bash
docker run -d --gpus=all -v "c:\temp\ollama:/root/.ollama" -p 11434:11434 --name ollama ollama/ollama
```
启动容器后，在 Docker 容器内打开终端：

如果使用 Docker Desktop，请从操作中选择“在终端中打开”。

然后，下载所需的模型，例如 phi3 模型：

```bash
ollama pull phi3
```

### 安装 AI 服务所需的包

在向您的 **Semantic Kernel** 添加聊天补全之前，您需要安装必要的包。  
以下是每个 AI 服务提供商所需的包。

| AI Service       | Required Package |
|-----------------|-----------------|
| **Azure OpenAI** | `Microsoft.SemanticKernel.Connectors.AzureOpenAI` |
| **OpenAI**       | `Microsoft.SemanticKernel.Connectors.OpenAI` |
| **Mistral**      | `Microsoft.SemanticKernel.Connectors.MistralAI` |
| **Google**       | `Microsoft.SemanticKernel.Connectors.Google` |
| **Hugging Face** | `Microsoft.SemanticKernel.Connectors.HuggingFace` |
| **Azure AI Inference** | `Microsoft.SemanticKernel.Connectors.AzureAIInference` |
| **Ollama**       | `Microsoft.SemanticKernel.Connectors.Ollama` |
| **Anthropic**    | `Microsoft.SemanticKernel.Connectors.Anthropic` |

要安装 **Azure OpenAI** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.AzureOpenAI
```

要安装 **OpenAI** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.OpenAI
```

要安装 **Mistral** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.MistralAI
```

要安装 **Google** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.Google
```

要安装 **Hugging Face** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.HuggingFace
```

要安装 **Azure AI Inference** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.AzureAIInference
```

要安装 **Ollama** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.Ollama
```

要安装 **Anthropic** 所需的包，请使用以下命令：
```bash
dotnet add package Microsoft.SemanticKernel.Connectors.Anthropic
```

### 创建ChatCompletion服务

现在，您已经安装了必要的包，可以创建聊天补全（Chat Completion）服务。以下是使用 Semantic Kernel 创建聊天补全服务的几种方法。

### 直接添加到 Kernel

要添加聊天补全（Chat Completion）服务，可以使用以下代码将其添加到 Kernel 的内部服务提供程序。

**Azure OpenAI** 服务
```csharp
using Microsoft.SemanticKernel;

IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddAzureOpenAIChatCompletion(
    deploymentName: "NAME_OF_YOUR_DEPLOYMENT",
    apiKey: "YOUR_API_KEY",
    endpoint: "YOUR_AZURE_ENDPOINT",
    modelId: "gpt-4", 
    serviceId: "YOUR_SERVICE_ID", 
    httpClient: new HttpClient()
);
Kernel kernel = kernelBuilder.Build();
```
**OpenAI** 服务
```csharp
using Microsoft.SemanticKernel;

IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOpenAIChatCompletion(
    modelId: "gpt-4",
    apiKey: "YOUR_API_KEY",
    orgId: "YOUR_ORG_ID", // Optional
    serviceId: "YOUR_SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
    httpClient: new HttpClient() // Optional; if not provided, the HttpClient from the kernel will be used
);
Kernel kernel = kernelBuilder.Build();
```
**Mistral** 服务
```csharp
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0070
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddMistralChatCompletion(
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Optional
    serviceId: "SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
    httpClient: new HttpClient() // Optional; for customizing HTTP client
);
Kernel kernel = kernelBuilder.Build();
```

**Google**服务
```csharp
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;

#pragma warning disable SKEXP0070
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddGoogleAIGeminiChatCompletion(
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    apiVersion: GoogleAIVersion.V1, // Optional
    serviceId: "SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
    httpClient: new HttpClient() // Optional; for customizing HTTP client
);
Kernel kernel = kernelBuilder.Build();
```

**HuggingFace**服务
```csharp
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0070
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddHuggingFaceChatCompletion(
    model: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Optional
    serviceId: "SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
    httpClient: new HttpClient() // Optional; for customizing HTTP client
);
Kernel kernel = kernelBuilder.Build();
```

**AzureAIInference**服务
```csharp
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0070
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddAzureAIInferenceChatCompletion(
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Optional
    serviceId: "SERVICE_ID", // Optional; for targeting specific services within Semantic Kernel
    httpClient: new HttpClient() // Optional; for customizing HTTP client
);
Kernel kernel = kernelBuilder.Build();
```
**ollama**服务

```csharp
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0070
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOllamaChatCompletion(
    modelId: "NAME_OF_MODEL",           // E.g. "phi3" if phi3 was downloaded as described above.
    endpoint: new Uri("YOUR_ENDPOINT"), // E.g. "http://localhost:11434" if Ollama has been started in docker as described above.
    serviceId: "SERVICE_ID"             // Optional; for targeting specific services within Semantic Kernel
);
Kernel kernel = kernelBuilder.Build();
```

**Bedrock**服务

```csharp
using Microsoft.SemanticKernel;

#pragma warning disable SKEXP0070
IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddBedrockChatCompletionService(
    modelId: "NAME_OF_MODEL",
    bedrockRuntime: amazonBedrockRuntime, // Optional; An instance of IAmazonBedrockRuntime, used to communicate with Azure Bedrock.
    serviceId: "SERVICE_ID"               // Optional; for targeting specific services within Semantic Kernel
);
Kernel kernel = kernelBuilder.Build();
```

### 使用依赖注入

如果您使用依赖注入（Dependency Injection），通常会希望将 AI 服务直接添加到 服务提供程序（Service Provider）。
这样做的好处是可以创建 单例（Singleton） AI 服务，并在 临时（Transient） Kernel 中重复使用它们。

**Azure OpenAI** 服务

```csharp
using Microsoft.SemanticKernel;
var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddAzureOpenAIChatCompletion(
    deploymentName: "NAME_OF_YOUR_DEPLOYMENT",
    apiKey: "YOUR_API_KEY",
    endpoint: "YOUR_AZURE_ENDPOINT",
    modelId: "gpt-4", // Optional name of the underlying model if the deployment name doesn't match the model name
    serviceId: "YOUR_SERVICE_ID" // Optional; for targeting specific services within Semantic Kernel
);
builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});
```

**OpenAI** 服务

```csharp
using Microsoft.SemanticKernel;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddOpenAIChatCompletion(
    modelId: "gpt-4",
    apiKey: "YOUR_API_KEY",
    orgId: "YOUR_ORG_ID", // Optional; for OpenAI deployment
    serviceId: "YOUR_SERVICE_ID" // Optional; for targeting specific services within Semantic Kernel
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});
```

**Mistral** 服务

```csharp
using Microsoft.SemanticKernel;

var builder = Host.CreateApplicationBuilder(args);

#pragma warning disable SKEXP0070
builder.Services.AddMistralChatCompletion(
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Optional
    serviceId: "SERVICE_ID" // Optional; for targeting specific services within Semantic Kernel
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});

```

**Google**服务

```csharp
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Connectors.Google;

var builder = Host.CreateApplicationBuilder(args);

#pragma warning disable SKEXP0070
builder.Services.AddGoogleAIGeminiChatCompletion(
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    apiVersion: GoogleAIVersion.V1, // Optional
    serviceId: "SERVICE_ID" // Optional; for targeting specific services within Semantic Kernel
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});

```

**HuggingFace**服务

```csharp
using Microsoft.SemanticKernel;

var builder = Host.CreateApplicationBuilder(args);

#pragma warning disable SKEXP0070
builder.Services.AddHuggingFaceChatCompletion(
    model: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Optional
    serviceId: "SERVICE_ID" // Optional; for targeting specific services within Semantic Kernel
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});

```

**AzureAIInference**服务

```csharp
using Microsoft.SemanticKernel;

var builder = Host.CreateApplicationBuilder(args);

#pragma warning disable SKEXP0070
builder.Services.AddAzureAIInferenceChatCompletion(
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Optional
    serviceId: "SERVICE_ID" // Optional; for targeting specific services within Semantic Kernel
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});
```

**ollama**服务

```csharp
using Microsoft.SemanticKernel;

var builder = Host.CreateApplicationBuilder(args);

#pragma warning disable SKEXP0070
builder.Services.AddOllamaChatCompletion(
    modelId: "NAME_OF_MODEL",           // E.g. "phi3" if phi3 was downloaded as described above.
    endpoint: new Uri("YOUR_ENDPOINT"), // E.g. "http://localhost:11434" if Ollama has been started in docker as described above.
    serviceId: "SERVICE_ID"             // Optional; for targeting specific services within Semantic Kernel
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});
```

**Bedrock**服务

```csharp
using Microsoft.SemanticKernel;

var builder = Host.CreateApplicationBuilder(args);

#pragma warning disable SKEXP0070
builder.Services.AddBedrockChatCompletionService(
    modelId: "NAME_OF_MODEL",
    bedrockRuntime: amazonBedrockRuntime, // Optional; An instance of IAmazonBedrockRuntime, used to communicate with Azure Bedrock.
    serviceId: "SERVICE_ID"               // Optional; for targeting specific services within Semantic Kernel
);

builder.Services.AddTransient((serviceProvider)=> {
    return new Kernel(serviceProvider);
});
```

### 单独创建实例（Creating Standalone Instances）

最后，您可以直接创建服务的实例，以便：稍后添加到 Kernel，或者
直接在代码中使用，而无需将其注入到 Kernel 或服务提供程序中。

**Azure OpenAI** 服务

```csharp
using Microsoft.SemanticKernel.Connectors.AzureOpenAI;

AzureOpenAIChatCompletionService chatCompletionService = new (
    deploymentName: "NAME_OF_YOUR_DEPLOYMENT",
    apiKey: "YOUR_API_KEY",
    endpoint: "YOUR_AZURE_ENDPOINT",
    modelId: "gpt-4", // Optional name of the underlying model if the deployment name doesn't match the model name
    httpClient: new HttpClient() // Optional; if not provided, the HttpClient from the kernel will be used
);
```

**OpenAI** 服务

```csharp
using Microsoft.SemanticKernel.Connectors.OpenAI;

OpenAIChatCompletionService chatCompletionService = new (
    modelId: "gpt-4",
    apiKey: "YOUR_API_KEY",
    organization: "YOUR_ORG_ID", // Optional
    httpClient: new HttpClient() // Optional; if not provided, the HttpClient from the kernel will be used
);
```

**Mistral** 服务

```csharp
using Microsoft.SemanticKernel.Connectors.MistralAI;

#pragma warning disable SKEXP0070
MistralAIChatCompletionService chatCompletionService = new (
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Optional
    httpClient: new HttpClient() // Optional; for customizing HTTP client
);
```

**Google**服务

```csharp
using Microsoft.SemanticKernel.Connectors.Google;

#pragma warning disable SKEXP0070
GoogleAIGeminiChatCompletionService chatCompletionService = new (
    modelId: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    apiVersion: GoogleAIVersion.V1, // Optional
    httpClient: new HttpClient() // Optional; for customizing HTTP client
);
```

**HuggingFace**服务

```csharp
using Microsoft.SemanticKernel.Connectors.HuggingFace;

#pragma warning disable SKEXP0070
HuggingFaceChatCompletionService chatCompletionService = new (
    model: "NAME_OF_MODEL",
    apiKey: "API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT") // Optional
);
```

**AzureAIInference**服务

```csharp
using Microsoft.SemanticKernel.Connectors.AzureAIInference;

#pragma warning disable SKEXP0070
AzureAIInferenceChatCompletionService chatCompletionService = new (
    modelId: "YOUR_MODEL_ID",
    apiKey: "YOUR_API_KEY",
    endpoint: new Uri("YOUR_ENDPOINT"), // Used to point to your service
    httpClient: new HttpClient() // Optional; if not provided, the HttpClient from the kernel will be used
);
```

**ollama**服务

```csharp
using Microsoft.SemanticKernel.ChatCompletion;
using OllamaSharp;

#pragma warning disable SKEXP0070
using var ollamaClient = new OllamaApiClient(
    uriString: "YOUR_ENDPOINT"    // E.g. "http://localhost:11434" if Ollama has been started in docker as described above.
    defaultModel: "NAME_OF_MODEL" // E.g. "phi3" if phi3 was downloaded as described above.
);

IChatCompletionService chatCompletionService = ollamaClient.AsChatCompletionService();
```

**Bedrock**服务

```csharp
using Microsoft.SemanticKernel.Connectors.Amazon;

#pragma warning disable SKEXP0070
BedrockChatCompletionService chatCompletionService = new BedrockChatCompletionService(
    modelId: "NAME_OF_MODEL",
    bedrockRuntime: amazonBedrockRuntime // Optional; An instance of IAmazonBedrockRuntime, used to communicate with Azure Bedrock.
);
```

### 获取ChatCompletion服务

一旦您将 聊天补全（Chat Completion） 服务添加到 Kernel，就可以使用 get service 方法来检索它。
以下是从 Kernel 中检索 聊天补全服务 的示例代码。

``` csharp
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
```

### 使用聊天补全服务（Using Chat Completion Services）

现在您已经拥有 聊天补全（Chat Completion） 服务，可以使用它来从 AI 代理 生成响应。
主要有两种使用方式：

1. 非流式（Non-streaming）：等待服务生成完整的响应后，再返回给用户。
2. 流式（Streaming）：响应会被逐步生成，并在创建的同时返回给用户。

以下是使用 聊天补全服务 生成响应的两种方式。

### 非流式ChatCompletion

要使用 非流式（Non-streaming） 聊天补全，您可以使用以下代码从 AI 代理 生成响应。

```csharp
ChatHistory history = [];
history.AddUserMessage("Hello, how are you?");

var response = await chatCompletionService.GetChatMessageContentAsync(
    history,
    kernel: kernel
);
```

### 流式ChatCompletion

要使用 流式（Streaming） 聊天补全，您可以使用以下代码从 AI 代理 生成响应。

```csharp
ChatHistory history = [];
history.AddUserMessage("Hello, how are you?");
var response = chatCompletionService.GetStreamingChatMessageContentsAsync(
    chatHistory: history,
    kernel: kernel
);

await foreach (var chunk in response)
{
    Console.Write(chunk);
}
```

### 聊天历史

#### 创建一个ChatList对象

聊天历史对象本质上是一个列表，因此可以轻松创建并向其中添加消息。

```csharp
using Microsoft.SemanticKernel.ChatCompletion;
// 创建一个聊天历史对象
ChatHistory chatHistory = [];
// 添加系统消息
chatHistory.AddSystemMessage("你是一个乐于助人的助手。");
// 添加用户消息
chatHistory.AddUserMessage("有什么可以点的？");
// 添加助手消息（AI 回复）
chatHistory.AddAssistantMessage("我们有披萨、意大利面和沙拉可供点餐。你想点什么？");
// 添加用户消息
chatHistory.AddUserMessage("我想要第一个选项，谢谢。");
```

### 丰富聊天消息内容

```csharp
using Microsoft.SemanticKernel.ChatCompletion;
// 添加系统消息
chatHistory.Add(
    new() {
        Role = AuthorRole.System,
        Content = "你是一个乐于助人的助手"
    }
);
// 添加带图片的用户消息
chatHistory.Add(
    new() {
        Role = AuthorRole.User,
        AuthorName = "Laimonis Dumins",
        Items = [
            new TextContent { Text = "这个菜单上有什么可选的？" },
            new ImageContent { Uri = new Uri("https://example.com/menu.jpg") }
        ]
    }
);
// 添加助手消息
chatHistory.Add(
    new() {
        Role = AuthorRole.Assistant,
        AuthorName = "餐厅助手",
        Content = "我们有披萨、意大利面和沙拉可供点餐。您想点什么？"
    }
);

// 添加来自另一位用户的额外消息
chatHistory.Add(
    new() {
        Role = AuthorRole.User,
        AuthorName = "Ema Vargova",
        Content = "我想要第一个选项，谢谢。"
    }
);

```

### 模拟函数调用

除了用户（User）、**助手（Assistant）和系统（System）这几种角色外，你还可以使用工具（Tool）**角色来模拟函数调用。这对于让 AI 学习如何使用插件，或者在对话中提供额外的背景信息非常有帮助。
比如，如果你想让 AI 了解用户的信息（比如过敏情况），但又不希望用户每次都手动输入，也不想让 AI 反复询问，你可以用 工具（Tool） 角色直接把这些信息提供给 AI。
下面是一个示例，我们通过模拟调用“用户插件”，把用户的过敏信息直接提供给助手，让 AI 知道用户的饮食限制。

模拟函数调用在提供当前用户的详细信息时特别有用。如今的大型语言模型（LLM）对用户信息非常敏感。即使你在系统消息中提供了用户信息，
LLM 仍然可能选择忽略它。但如果你通过**用户消息（User Message）或工具消息（Tool Message）**提供这些信息，LLM 更有可能正确使用它。

```csharp
// 添加一个来自助手的模拟函数调用
chatHistory.Add(
    new() {
        Role = AuthorRole.Assistant,
        Items = [
            new FunctionCallContent(
                functionName: "get_user_allergies", // 函数名称：获取用户过敏信息
                pluginName: "User", // 插件名称：用户（User）
                id: "0001", // 调用 ID
                arguments: new () { {"username", "laimonisdumins"} } // 参数：用户名 laimonisdumins
            ),
            new FunctionCallContent(
                functionName: "get_user_allergies", // 函数名称：获取用户过敏信息
                pluginName: "User", // 插件名称：用户（User）
                id: "0002", // 调用 ID
                arguments: new () { {"username", "emavargova"} } // 参数：用户名 emavargova
            )
        ]
    }
);

// 添加来自工具角色的模拟函数返回结果
chatHistory.Add(
    new() {
        Role = AuthorRole.Tool,
        Items = [
            new FunctionResultContent(
                functionName: "get_user_allergies", // 函数名称：获取用户过敏信息
                pluginName: "User", // 插件名称：用户（User）
                id: "0001", // 调用 ID
                result: "{ \"allergies\": [\"peanuts\", \"gluten\"] }" // 返回结果：用户对花生和麸质过敏
            )
        ]
    }
);
chatHistory.Add(
    new() {
        Role = AuthorRole.Tool,
        Items = [
            new FunctionResultContent(
                functionName: "get_user_allergies", // 函数名称：获取用户过敏信息
                pluginName: "User", // 插件名称：用户（User）
                id: "0002", // 调用 ID
                result: "{ \"allergies\": [\"dairy\", \"soy\"] }" // 返回结果：用户对乳制品和大豆过敏
            )
        ]
    }
);
```

在模拟工具（Tool）返回结果时，必须始终提供与之对应的函数调用 ID。这是让 AI 理解返回结果上下文的重要信息。
某些大型语言模型（LLM），比如 OpenAI，如果缺少 ID 或 ID 与函数调用不匹配，可能会抛出错误。

### 检查聊天历史对象

当你把聊天历史对象传递给支持自动函数调用的聊天服务时，它会自动添加函数调用和返回结果，你不需要手动去加这些信息。
这样做的好处是，你可以随时查看聊天记录，直接看到 AI 调用了哪些函数，以及返回了什么结果。
不过，你还是需要手动添加最终的聊天消息，让对话完整。下面是一个示例，展示如何检查聊天记录，看看 AI 都做了哪些函数调用和返回了什么数据。

### 聊天历史优化（Chat History Reduction）

管理聊天历史对于保持上下文感知的对话至关重要，同时还能确保高效的性能。随着对话的进行，聊天历史对象会不断增长，
可能超出模型的上下文窗口限制，从而影响回复质量并降低处理速度。通过结构化的方法来优化聊天历史，可以确保保留最相关的信息，
同时避免不必要的开销。

### 为什么要优化聊天历史？

1. 性能优化（Performance Optimization）
聊天记录过大会增加处理时间，影响AI生成回复的速度。适当缩减历史记录可以保持交互流畅、响应快速。

2. 上下文窗口管理（Context Window Management）
语言模型的上下文窗口是固定的，一旦聊天记录超过这个限制，旧消息会被丢弃。合理管理历史记录，可以确保最重要的上下文信息不会丢失。

3. 内存效率（Memory Efficiency）
在资源受限的环境（如移动应用、嵌入式系统）中，过长的聊天历史可能导致内存占用过大，影响系统性能。适当减少聊天记录可以避免性能下降。

4. 隐私与安全（Privacy and Security）
保留过多的聊天记录 可能会增加敏感信息暴露的风险。结构化的优化策略 可以减少不必要的数据存储，同时保留关键对话信息，提高隐私保护和数据安全性。

### 减少聊天历史的策略（Strategies for Reducing Chat History）

为了在保留关键信息的同时，让聊天历史保持可管理的状态，可以使用以下几种方法：

1. 截断（Truncation）
当聊天历史超过预设的限制时，删除最早的消息，确保只保留最近的对话记录。适用于不需要长期上下文的对话，如客服聊天、即时问答等。

2. 摘要（Summarization）
将较旧的消息压缩成摘要，保留关键内容，同时减少存储的消息数量。适用于长时间的连续对话，比如咨询、技术支持等场景。

3. 基于 Token 的优化（Token-Based）
计算聊天记录的 Token 数量，确保总 Token 量不超过模型的上下文窗口限制。当超出 Token 限制时，删除或总结旧消息，确保 AI 仍然能理解当前上下文。
适用于LLM 生成式 AI 交互，如 OpenAI、Azure AI 等。

```csharp
namespace Microsoft.SemanticKernel.ChatCompletion;

[Experimental("SKEXP0001")]
public interface IChatHistoryReducer
{
    Task<IEnumerable<ChatMessageContent>?> ReduceAsync(IReadOnlyList<ChatMessageContent> chatHistory, CancellationToken cancellationToken = default);
}
```

该接口允许自定义实现聊天历史优化。此外，Semantic Kernel 提供了内置的聊天历史优化器（Reducers），包括：

1. ChatHistoryTruncationReducer（聊天历史截断优化器）
作用：将聊天历史截断到指定大小，并直接丢弃被移除的消息。
触发条件：当聊天历史的长度超过设定的限制时，自动执行截断操作。

2. ChatHistorySummarizationReducer（聊天历史摘要优化器）
作用：在截断聊天历史的同时，对被移除的消息进行摘要总结，然后将该摘要作为一条消息添加回聊天历史。
优点：不会完全丢失上下文，而是用更简短的方式保留重要信息。

```csharp
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

// 创建 OpenAI 聊天补全服务
var chatService = new OpenAIChatCompletionService(
    modelId: "<model-id>",  // 模型 ID
    apiKey: "<api-key>");    // API 密钥

// 创建聊天历史截断优化器，仅保留系统消息和最近两条用户消息
var reducer = new ChatHistoryTruncationReducer(targetCount: 2); 

// 初始化聊天历史，并设定系统消息
var chatHistory = new ChatHistory("你是一位图书管理员，精通关于城市的书籍");

string[] userMessages = [
    "推荐一些关于西雅图的书籍", 
    "推荐一些关于都柏林的书籍", 
    "推荐一些关于阿姆斯特丹的书籍", 
    "推荐一些关于巴黎的书籍", 
    "推荐一些关于伦敦的书籍"
];

int totalTokenCount = 0;

foreach (var userMessage in userMessages)
{
    // 添加用户消息
    chatHistory.AddUserMessage(userMessage);

    Console.WriteLine($"\n>>> 用户:\n{userMessage}");

    // 执行聊天历史优化，减少历史记录
    var reducedMessages = await reducer.ReduceAsync(chatHistory);

    if (reducedMessages is not null)
    {
        chatHistory = new ChatHistory(reducedMessages);
    }

    // 发送优化后的聊天历史给 AI 并获取回复
    var response = await chatService.GetChatMessageContentAsync(chatHistory);

    // 将 AI 的回复添加到聊天历史
    chatHistory.AddAssistantMessage(response.Content!);

    Console.WriteLine($"\n>>> 助手:\n{response.Content!}");

    // 计算总 Token 使用量
    if (response.InnerContent is OpenAI.Chat.ChatCompletion chatCompletion)
    {
        totalTokenCount += chatCompletion.Usage?.TotalTokenCount ?? 0;
    }
}

Console.WriteLine($"总 Token 计数: {totalTokenCount}");

```

### 在聊天补全中使用图片

许多 AI 服务支持同时使用图像、文本等多种输入，这使得开发者可以融合不同的输入方式。
这样可以实现诸如传递一张图片，并向 AI 模型提问等应用场景。

Semantic Kernel 的聊天补全连接器支持同时向聊天补全 AI 模型传递图像和文本。
但需要注意，并非所有 AI 模型或 AI 服务都支持此功能。在按照聊天补全文章中的步骤构建聊天补全服务后，
你可以使用以下方式提供图像和文本。

```csharp
// Load an image from disk.
byte[] bytes = File.ReadAllBytes("path/to/image.jpg");
// Create a chat history with a system message instructing
// the LLM on its required role.
var chatHistory = new ChatHistory("Your job is describing images.");
// Add a user message with both the image and a question
// about the image.
chatHistory.AddUserMessage(
[
    new TextContent("What’s in this image?"),
    new ImageContent(bytes, "image/jpeg"),
]);
// Invoke the chat completion model.
var reply = await chatCompletionService.GetChatMessageContentAsync(chatHistory);
Console.WriteLine(reply.Content);
```

### 在聊天补全中调用函数

聊天补全最强大的功能之一是能够调用模型中的函数。这使你可以创建一个能够与现有代码交互的聊天机器人，从而实现业务流程自动化、代码片段生成等功能。
在Semantic Kernel中，我们简化了函数调用的使用流程，能够自动向模型描述你的函数及其参数，并处理模型与代码之间的通信。
不过，在使用函数调用时，了解其底层执行逻辑是很有必要的。这样，你可以优化代码，充分发挥这一功能的优势。

### 自动函数调用的工作原理

### **自动函数调用的执行步骤**

| **步骤** | **描述** |
|----------|---------|
| **1. 序列化函数** | 将所有可用的函数（及其输入参数）使用 JSON Schema 进行序列化。 |
| **2. 发送消息和函数到模型** | 将序列化后的函数（以及当前聊天历史）作为输入发送到模型。 |
| **3. 模型处理输入** | 模型处理输入，并生成响应。响应可以是普通的聊天消息，也可以是一个或多个函数调用。 |
| **4. 处理模型返回的响应** | 如果返回的是普通聊天消息，直接返回给调用方。如果返回的是函数调用，Semantic Kernel 解析出函数名称及其参数。 |
| **5. 执行函数** | 使用提取出的函数名称和参数，在 Kernel 中调用相应的函数。 |
| **6. 返回函数结果** | 函数调用的结果会作为聊天历史的一部分发送回模型。然后，步骤 2-6 会重复执行，直到模型返回普通的聊天消息或达到最大迭代次数。 |

C# 版 Pizza 订购插件

```csharp
public class PizzaOrderPlugin
{
    private readonly Dictionary<string, int> _cart = new(); // 购物车（存储披萨名称及数量）

    // 获取披萨菜单
    public List<string> GetPizzaMenu()
    {
        return new List<string> { "Margherita", "Pepperoni", "Hawaiian", "BBQ Chicken", "Veggie" };
    }
    // 将披萨添加到购物车
    public string AddPizzaToCart(string pizzaName)
    {
        if (!_cart.ContainsKey(pizzaName))
        {
            _cart[pizzaName] = 0;
        }
        _cart[pizzaName]++;

        return $"{pizzaName} 已加入购物车。当前数量: {_cart[pizzaName]}";
    }
    // 从购物车中移除披萨
    public string RemovePizzaFromCart(string pizzaName)
    {
        if (_cart.ContainsKey(pizzaName) && _cart[pizzaName] > 0)
        {
            _cart[pizzaName]--;
            if (_cart[pizzaName] == 0)
            {
                _cart.Remove(pizzaName);
            }
            return $"{pizzaName} 已从购物车中移除。";
        }

        return $"购物车中没有 {pizzaName}。";
    }

    // 获取购物车中特定披萨的信息
    public string GetPizzaFromCart(string pizzaName)
    {
        if (_cart.ContainsKey(pizzaName))
        {
            return $"{pizzaName}: {_cart[pizzaName]} 份";
        }

        return $"购物车中没有 {pizzaName}。";
    }

    // 获取购物车中的所有披萨
    public Dictionary<string, int> GetCart()
    {
        return _cart;
    }

    // 结账
    public string Checkout()
    {
        if (_cart.Count == 0)
        {
            return "购物车为空，无法结账。";
        }

        string orderDetails = string.Join(", ", _cart.Select(p => $"{p.Key} x{p.Value}"));
        _cart.Clear(); // 清空购物车
        return $"订单已提交: {orderDetails}。感谢您的订购！";
    }
}

```

1. 函数序列化（Serializing the Functions）

当你在Semantic Kernel中创建 OrderPizzaPlugin 时，Kernel会自动序列化所有函数及其参数，以便AI能够理解这些函数的用途和输入格式。
订购披萨插件（OrderPizzaPlugin）的序列化结果
对于上面定义的 OrderPizzaPlugin，自动生成的序列化 JSON Schema 可能如下所示：

```json
{
  "functions": [
    {
      "name": "get_pizza_menu",
      "description": "返回可选披萨的列表",
      "parameters": {}
    },
    {
      "name": "add_pizza_to_cart",
      "description": "将披萨添加到用户的购物车",
      "parameters": {
        "pizzaName": {
          "type": "string",
          "description": "要添加的披萨名称"
        }
      }
    },
    {
      "name": "remove_pizza_from_cart",
      "description": "从用户的购物车中移除披萨",
      "parameters": {
        "pizzaName": {
          "type": "string",
          "description": "要移除的披萨名称"
        }
      }
    },
    {
      "name": "get_pizza_from_cart",
      "description": "获取购物车中特定披萨的详情",
      "parameters": {
        "pizzaName": {
          "type": "string",
          "description": "要查询的披萨名称"
        }
      }
    },
    {
      "name": "get_cart",
      "description": "返回用户的当前购物车内容",
      "parameters": {}
    },
    {
      "name": "checkout",
      "description": "结算购物车中的订单",
      "parameters": {}
    }
  ]
}
```

序列化函数

1. 函数模式的冗长程度（Verbosity of function schema）
序列化函数并非免费，模式越复杂，模型需要处理的 Token 就越多，这可能会降低响应速度并增加成本。

2. 参数类型（Parameter types）
在模式中，你可以指定每个参数的类型，这有助于模型理解期望的输入数据类型。
在上面的示例中：
    size 参数是 枚举（enum）。
    toppings 参数是 枚举数组（array of enums）。
    这样可以让模型生成更准确的输入。

3. 必填参数（Required parameters）
你可以指定哪些参数是必填的（Required），这样模型就能知道哪些参数是函数调用必须提供的。
在 Step 3，模型会尽可能只提供必要的参数来调用函数，从而优化调用效率。

4. 函数描述（Function descriptions）
函数描述是可选的，但它可以帮助模型更准确地使用函数，特别是在返回类型不会被序列化的情况下，描述可以告诉模型应该期待什么结果。
如果模型误用函数，可以在描述中添加示例或指导，以帮助模型正确调用。

5. 插件名称（Plugin name）
在序列化的函数中，每个函数都有一个 name 属性。
Semantic Kernel 使用插件名称来命名空间化（namespace）函数，这样可以防止函数名称冲突。
例如，你可能有多个搜索服务插件，每个都有自己的 search 函数。通过使用不同的插件名称，就能避免函数名称冲突，并让模型知道应该调用哪个插件的 search 函数。

