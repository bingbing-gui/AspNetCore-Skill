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

