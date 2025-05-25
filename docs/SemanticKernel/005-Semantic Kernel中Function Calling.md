
ChatCompletion最强大功能之一就是能够从模型中调用函数。这让你可以基于现有代码创建聊天机器人，实现自动化业务流程、生成代码片段等功能。

在 Semantic Kernel 中，函数调用的过程被大幅简化：它会自动将你的函数和参数描述给模型，并处理模型与代码之间的沟通。了解这一机制对优化代码、充分发挥函数调用的优势至关重要。

## 自动函数调用的工作原理

在 Semantic Kernel 中，自动函数调用是默认行为，也可选择手动调用。启用函数调用时，模型会依照以下步骤进行处理：

1. **序列化函数**  
    kernel 中所有可用的函数及其输入参数都使用 JSON schema 进行序列化。

2. **将消息和函数发送给模型**  
    序列化后的函数（以及聊天历史）作为模型输入的一部分发送给模型。

3. **模型处理输入**  
    模型生成响应，可包含普通聊天消息或一个或多个函数调用。

4. **处理响应**  
    如果是聊天消息，直接返回给调用方；如果是函数调用，则提取函数名称和参数。

5. **调用函数**  
    使用提取到的函数名称和参数，在 kernel 中调用对应的函数。

6. **返回函数结果**  
    函数结果被添加至聊天历史并再次发送给模型。该循环会持续，直到模型返回聊天消息或达到设置的最大迭代次数。

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


