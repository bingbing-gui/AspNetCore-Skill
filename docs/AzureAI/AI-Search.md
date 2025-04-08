# Azure AI 搜索 简介

Azure AI 搜索（Azure AI Search）是微软提供的一项 基于云的智能搜索服务，可以对各种结构化和非结构化数据源进行索引、语义理解和查询匹配，构建企业级搜索解决方案

✅ 借助 Azure AI 搜索，你可以：

索引多源数据

支持 PDF、Word、Excel、HTML、数据库、SharePoint、Blob 等
实现对文本、图像、语音、聊天记录等非结构化数据的统一接入

利用认知技能增强内容

启用 预训练 AI 能力：OCR、语言识别、实体识别、语音转文本、情感分析等
构建“语义理解 + 搜索”一体化平台

在知识存储中存储 AI 提取结果

提取出的结构化字段可导出到 SQL、Power BI 等系统做 BI 分析
支持将 AI 洞察用于下游系统或推荐引擎。

## Azure AI 搜索核心组件定义

## 1. Data Source（数据源）

**定义：**  

数据源是 Azure AI 搜索解决方案中用于提供原始内容的位置，可包括 Azure Blob 存储中的文件、Azure SQL 数据库中的表、Cosmos DB 文档等。系统可以从这些来源拉取内容用于后续的提取和索引。

---

## 2. Indexer（索引器）

**定义：**  
索引器是连接数据源与搜索索引的执行引擎，负责读取数据、调用认知技能进行数据加工，并将结构化结果写入搜索索引中。它支持定时运行、增量更新及智能映射字段。

---

## 3. Skillset（技能集）

**定义：**  
技能集是由一系列认知技能（Cognitive Skills）组成的处理流水线，用于在索引过程中对数据进行 AI 增强。它可自动识别语言、提取关键词、进行情感分析、图像文字识别等，丰富搜索内容的语义表达能力。

---

## 4. Index（索引）

**定义：**  
索引是 Azure AI 搜索系统中实际存储可搜索数据的结构化容器。每个索引由多个字段组成，支持配置为可搜索、可筛选、可排序等特性，并以 JSON 文档形式存储数据，供用户查询调用。

---

## 5. Search（搜索服务）

**定义：**  
搜索服务是对外提供查询接口的核心组件，支持通过关键词、向量、过滤器和建议器等方式从索引中检索数据。开发者可使用 REST API 或 SDK 构建具有智能搜索能力的企业级应用程序。


## Azure AI 搜索：索引流程总结

在 Azure AI 搜索中，每一个需要被检索的数据项（Entity）都会被处理为一个结构化的文档（Document）。系统将**原始数据字段**与通过**AI 认知技能（Cognitive Skills）**生成的**增强信息**合并，最终写入到 **Azure AI 搜索的索引（Index）**中。

---

### 1️. 创建文档（Document）

系统为每个数据实体生成一个 JSON 格式的文档，包含原始数据字段，例如：

```json
{
  "metadata_storage_name": "file.pdf",
  "metadata_author": "张三",
  "content": "文件正文内容"
}
```

### 2. 图像提取处理（可选）

若文档中包含图像，可提取为结构化的 normalized_images 集合：

```json
{
  "normalized_images": {
    "image0": { ... },
    "image1": { ... }
  }
}
```

### 3. 应用认知技能（Skillset）

通过配置的认知技能提取结构化信息，如语言识别、OCR 等：

```json
{
  "language": "zh-CN",
  "normalized_images": {
    "image0": { "Text": "提取文字0" },
    "image1": { "Text": "提取文字1" }
  }
}
```

### 4. 合并内容（Merge Skill）

使用 Merge Skill 将原始文本与提取的图像文本合并，形成完整内容：

```json
{
  "merged_content": "原始文本 + OCR 文本"
}
```

### 5. 写入索引（Index）

最终文档字段映射到 Azure 搜索 Index 中，成为可搜索的数据。

最终文档结构示例：
```json
{
  "metadata_storage_name": "file.pdf",
  "metadata_author": "张三",
  "content": "...",
  "normalized_images": {
    "image0": { "Text": "图片文字1" },
    "image1": { "Text": "图片文字2" }
  },
  "language": "zh-CN",
  "merged_content": "完整文本"
}
```

### 6. 流程图文字版

```css
[原始数据源]
       ↓
[创建 JSON 文档]
       ↓
[提取图像 → normalized_images]
       ↓
[应用认知技能 → 语言识别、OCR 等]
       ↓
[合并内容 → merged_content]
       ↓
[映射字段 → 写入 Index]
```

### 总结

在 Azure AI 搜索中，**索引器（Indexer）**将文档字段映射到你定义的索引结构（Index），字段来源包括：

原始数据（如 PDF、数据库字段）

AI 认知技能生成的字段（如 OCR 结果、语言识别）

## 🔍 Azure AI 搜索：索引查询（Search an Index）机制解析

在你创建并填充好一个搜索索引后（比如索引了 PDF、Word、聊天记录等文档），就可以通过查询来搜索索引中的内容了。

虽然可以通过字段值进行简单匹配查询，但大多数搜索系统都会使用**全文搜索（Full Text Search）**来实现更强大、更灵活的语义搜索能力。

---

### 📘 什么是全文搜索？

全文搜索指的是对文本文档进行语言学分析，**解析用户的查询关键词**，在文档中找到相关匹配项。  
Azure AI Search 的全文搜索功能基于 **Lucene 查询语法**，支持丰富的查询功能，如：

- 搜索（Search）
- 过滤（Filter）
- 排序（Sort）

Azure 支持 Lucene 语法的两种模式：

### Simple 模式（简单模式）

一种直观语法，适合基础查询，匹配用户输入的“原始关键词”即可。

### Full 模式（完整模式）

支持复杂查询：

- 多字段过滤
- 正则表达式
- 逻辑运算（AND / OR）
- 分组等

---

### 🧾 查询时常用的参数

你可以在请求中带上一些参数来控制搜索行为，例如：

| 参数名        | 说明 |
|---------------|------|
| `search`      | 用户要查找的关键词或短语（如 `"AI"` 或 `"free parking"`） |
| `queryType`   | 查询语法类型：`simple` 或 `full` |
| `searchFields`| 指定在哪些字段中进行搜索 |
| `select`      | 指定结果中返回哪些字段 |
| `searchMode`  | 匹配策略：<br>• `Any` = 包含任一关键词即匹配<br>• `All` = 必须包含所有关键词才匹配 |

### 示例说明:

```http
search=comfortable hotel
searchMode=Any → 匹配包含 "comfortable" 或 "hotel" 的文档  
searchMode=All → 仅匹配同时包含 "comfortable" 和 "hotel" 的文档
```

### ⚙️ 查询处理的四个阶段

Azure AI 搜索在执行全文搜索时，会将你的查询请求拆解并分四个阶段逐步处理，以获得最相关的文档结果。

---

### 查询解析（Query Parsing）

将搜索表达式解析为一棵“查询树”，用于进一步处理。

#### 子查询类型包括:

- **术语查询（Term Query）**：匹配单个关键词  
  示例：`hotel`
- **短语查询（Phrase Query）**：匹配用引号包裹的词组  
  示例：`"free parking"`
- **前缀查询（Prefix Query）**：匹配特定前缀的词  
  示例：`air*` 可匹配 `airway`, `air-conditioning`, `airport` 等

---

### 2️⃣ 🧠 词法分析（Lexical Analysis）

对查询词应用语言规则进行预处理，使搜索更智能。

#### 📌 处理内容包括：

- 转小写：`HOTEL` → `hotel`
- 去除停用词：如 `"the"`, `"a"`, `"is"` 等常见无效词
- 词干化（Stemming）：将词简化为根词  
  示例：`comfortable` → `comfort`
- 拆分复合词：如 `notebookcomputer` → `notebook` + `computer`

---

### 3️⃣ 📄 文档检索（Document Retrieval）

将处理过的查询词与**索引中的内容进行匹配**，筛选出满足条件的文档。

---

### 4️⃣ 🏆 结果打分（Scoring）

对匹配文档进行相关性评分，排序返回。

#### 📌 使用评分机制：

- **TF-IDF 算法**（词频 / 逆文档频率）  
  用于衡量一个词对于当前文档是否“重要”

#### ✅ 结果排序逻辑：

- 分数越高 → 排名越靠前  
- 搜索结果最终呈现顺序 = 按评分从高到低排序

---
### 🔄 全文搜索处理流程：顺序执行的四个步骤

| 步骤         | 作用                             | 输入                                | 输出                             |
|--------------|----------------------------------|-------------------------------------|----------------------------------|
| 1️⃣ 查询解析     | 把搜索词解析为结构化语法树              | 原始查询词（如 `"free parking"`）   | 子查询结构（关键词、短语、前缀）   |
| 2️⃣ 词法分析     | 进行语言处理，如小写化、词干化等         | 子查询                             | 优化后的词项                     |
| 3️⃣ 文档检索     | 从索引中找出与词项匹配的文档              | 处理过的词项                         | 匹配文档集合                     |
| 4️⃣ 结果打分     | 按照匹配程度计算相关性得分并排序结果       | 匹配文档集合                         | 最终结果集（带相关性分数）         |

你可以将这四个阶段理解为一次智能搜索的“幕后流程”，它帮助系统理解查询语义，找到真正与你问题最相关的内容。

## 应用过滤与排序（Apply Filtering and Sorting）

在搜索解决方案中，用户通常希望通过**字段值的过滤和排序**来进一步精细化搜索结果。Azure AI Search 提供了对这两种能力的原生支持，通过查询 API 实现。

---

### 🔍 结果过滤（Filtering Results）

你可以通过以下两种方式对查询结果应用过滤条件：

1. 在简单搜索表达式中包含过滤条件  
2. 使用完整语法查询时，通过 `$filter` 参数传入 OData 过滤表达式

你可以对任何在索引中被设置为 `filterable` 的字段应用过滤器。

### 📌 示例：

如果你想查找：  
包含关键词 `"London"` 且 `author` 字段为 `"Reviewer"` 的文档：

### 方式 1：使用简单语法**

```http
search=London+author='Reviewer'
queryType=Simple
```

### 使用 Facets 进行筛选（Filtering with Facets）

**Facets（分面）**是一种非常实用的交互方式，可以根据字段值对结果进行分类、筛选。
适用于字段值数量较少、可枚举的字段，如作者、类型、标签等。

如何启用分面：
在初始查询中，指定你希望启用分面的字段（这些字段需为 facetable）

```json
search=*
facet=author
```

此查询会返回 author 字段的所有可选值及其出现次数，你可以在 UI 中将这些值显示为筛选项。
用户点击筛选值后：
再次发起一个查询，添加 $filter 来限制结果：

```http
search=*
$filter=author eq 'selected-facet-value-here'
```

### 排序结果（Sorting Results）

默认情况下，搜索结果会根据相关性评分（relevance score）从高到低排序。
但你也可以通过 $orderby 参数自定义排序方式，按任意 sortable 字段升序（asc）或降序（desc）排列。

📌 示例：

按最近修改时间降序排序：

```http
search=*
$orderby=last_modified desc
```

## 在 Azure AI 搜索中增强索引功能

创建好基础索引和客户端后，搜索系统已经具备基本功能。但为了提供更优秀的用户体验，Azure AI 搜索还支持多种增强手段来提升搜索效果。

---

### 搜索建议（Search-as-You-Type）

提供用户在输入时的实时提示，提高搜索的交互效率和准确率。

### 两种类型的交互方式：

- **建议（Suggestions）**：用户输入时，即时返回可能的匹配项，无需提交查询。  
- **自动补全（Autocomplete）**：根据索引字段中的值，自动完成用户输入的词语。

### 启用方式：

1. 为索引中一个或多个字段添加 **Suggester**
2. 使用以下方式实现该功能：

#### ✅ REST API 接口：

- `suggest()`  
- `autocomplete()`

#### ✅ .NET SDK 方法：

- `DocumentsOperationsExtensions.Suggest()`  
- `DocumentsOperationsExtensions.Autocomplete()`

📘 [了解更多：添加搜索建议和自动补全](https://learn.microsoft.com/zh-cn/azure/search/search-autocomplete)

---

### 📈 自定义打分与结果加权（Scoring & Boosting）

默认情况下，搜索结果通过 **TF/IDF（词频/逆文档频率）** 算法进行排序。你可以通过定义 **打分配置（Scoring Profile）** 来自定义这个逻辑。

#### 应用场景：

- 提高某些**关键字段**的权重，例如标题或标签字段。
- 根据字段值进行加权，例如文档的**更新时间**或**大小**。

#### 使用方法：

1. 在索引中定义一个 **Scoring Profile**
2. 应用方式包括：
   - 在单次搜索请求中指定
   - 设置为索引的默认打分规则

📘 [了解更多：Scoring Profiles](https://learn.microsoft.com/zh-cn/azure/search/search-how-scoring-profiles-work)

---

### 同义词支持（Synonyms）

不同用户可能用不同词语描述同一事物（例如 "UK"、"United Kingdom"、"Great Britain"）。

### 解决方案：

- 创建 **同义词映射（Synonym Map）**，将相关词语绑定在一起。
- 将该映射应用到特定字段，搜索时即可命中所有同义词。

这样可以显著提升搜索的覆盖范围，即使用户输入的关键词与你索引中的内容不完全一致。

📘 [了解更多：使用同义词映射](https://learn.microsoft.com/zh-cn/azure/search/search-synonyms)

---

## ✅ 总结

| 功能类别       | 带来的好处                             | 配置方式                    |
|----------------|----------------------------------------|-----------------------------|
| 搜索建议       | 提升用户体验，提供实时提示和自动补全   | 添加 Suggester              |
| 打分与加权     | 精准控制结果排序，提高重要内容权重     | 定义 Scoring Profile        |
| 同义词匹配     | 扩展搜索词覆盖范围，提升搜索命中率     | 创建并应用 Synonym Map      |
