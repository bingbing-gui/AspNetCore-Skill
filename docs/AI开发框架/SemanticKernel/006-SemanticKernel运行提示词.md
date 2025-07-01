# 🤖 Semantic Kernel提示词、模板与上下文记忆

---

随着大语言模型（LLM）逐渐成为智能应用的核心，**如何与模型进行高质量的“对话”设计**，成为每位开发者必须掌握的能力。Microsoft 的 Semantic Kernel（简称 SK）为我们提供了一整套**提示词系统（Prompt System）**，包括：

- ✍️ 提示词编写技巧  
- 🧩 模板化提示词管理（支持 Handlebars）  
- 🧠 聊天上下文记录（ChatHistory）

今天我们就来带大家深入理解这些关键能力，并结合代码实战，掌握构建智能对话应用的基础框架。

---

## ✍️ 一、提示词（Prompt）：让 AI 明白你的意图

在 Semantic Kernel 中，Prompt 是和大语言模型交流的方式。写得好，AI 懂你；写不好，AI 胡说。

### 常见提示词策略：

| 策略名               | 说明               | 文章                                                             |
|----------------------|--------------------|------------------------------------------------------------------|
| 🟢 Zero-shot         | 仅提供任务描述     | [文章一](/AI-Prompts/Zero-shot.md)                               |
| 🔵 Few-shot          | 提供 1~5 个示例    | [文章二](/AI-Prompts/Few-shot.md)                                |
| 🟡 Priming Prompting | 设定 AI 身份       | [文章三](/AI-Prompts/PrimingPrompting.md)                        |
| 🔴 Chain-of-thought  | 引导逐步推理       | [文章四](/AI-Prompts/ChainOfThought.md)                          |

---

## 🧩 二、提示词模板（Prompt Template）：构建可复用的智能模块

Prompt Template 就像“提示词函数”，不仅可以插入变量，还能调用其他 AI 函数、集成结构化模板，方便维护与组合。

### Template 有两种语法风格：

| 类型               | 特点                                  | 示例                                                              |
|--------------------|---------------------------------------|-------------------------------------------------------------------|
| ✅ 原生模板语法     | 使用 `{{$变量}}` 和 `{{函数}}` 语法    | `{{$city}} 天气是 {{weather.getForecast $city}}`                  |
| ✅ Handlebars 语法 | 类似前端模板引擎，结构强              | `{{request}}，{{#if image}}<image>{{image}}</image>{{/if}}`       |

---

### Template 支持多种来源：

| 来源方式               | 是否推荐 | 说明                          |
|------------------------|----------|-------------------------------|
| 📄 内联字符串          | ✅       | 简单测试快速开发              |
| 📝 YAML 文件           | ✅       | 推荐！支持元信息、可版本管理   |
| 📂 外部 .hbs/.txt 文件 | ✅       | 模板与代码分离，适合多人协作   |
| 📦 嵌入式资源          | ⚠️       | 用于封装 SDK、自定义程序集     |
| ⚙️ 动态构造模板         | ❌       | 易错，不利维护                 |

---

### Handlebars YAML 示例：

```yaml
name: image_prompt
template_format: handlebars
template: |
    <message>
        <text>{{description}}</text>
    </message>
input_variables:
    - name: description
        is_required: true
```

```csharp
var yaml = EmbeddedResource.Read("image_prompt.yaml");
var function = kernel.CreateFunctionFromPromptYaml(yaml, new HandlebarsPromptTemplateFactory());
```

---

## 🧠 三、ChatHistory：打造有记忆的 AI 对话助手

真正“聪明”的对话系统，必须记得前面说过什么。ChatHistory 是 Semantic Kernel 中用于记录上下文的关键类。

```csharp
ChatHistory chat = [];
chat.AddSystemMessage("You are a helpful assistant.");
chat.AddUserMessage("What's available?");
chat.AddAssistantMessage("Pizza, pasta, salad.");
```

支持图文消息、作者信息：

```csharp
chat.Add(new()
{
        Role = AuthorRole.User,
        AuthorName = "Alice",
        Items = [
                new TextContent { Text = "What's on the menu?" },
                new ImageContent { Uri = new Uri("https://example.com/menu.jpg") }
        ]
});
```

---

## 🧩 四、三者协同：构建完整智能对话流程

✅ Prompt 负责告诉 AI “做什么”  
✅ Template 让提示结构清晰、可复用  
✅ ChatHistory 让 AI 记住过去，理解上下文

这三者组合起来，就是打造语义感知、连续对话、可维护 AI 系统的核心架构。

---

### ✅ 总结一句话

使用 Semantic Kernel 的 Prompt + Template + ChatHistory 组合，你可以轻松构建真正智能、会“聊天”的 AI 应用。

不论是开发 AI 助手、客服机器人，还是个性化推荐系统，这一套设计都是值得借鉴的黄金架构。
