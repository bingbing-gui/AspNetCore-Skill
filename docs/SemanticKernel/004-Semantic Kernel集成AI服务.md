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



### 流式ChatCompletion





