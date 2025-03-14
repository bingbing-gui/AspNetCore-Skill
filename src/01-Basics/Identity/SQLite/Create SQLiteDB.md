# 创建 SQLite 数据库步骤

1. **安装 SQLite 工具**
    - 下载并安装 SQLite 命令行工具 [SQLite Download Page](https://www.sqlite.org/download.html)。
    ![](/src/01-Basics/Identity/SQLite/Materials/SQLite-download.jpg)

2. **Window系统配置 SQLite**
    - 下载对应的sqlite-dll(x86 或者 x64)以及tools
    - 在C盘创建SQLite文件夹，将下载的文件复制到SQLite文件夹下
    - 配置环境变量

    ![第一步](/src/01-Basics/Identity/SQLite/Materials/1.jpg)  
    ![第二步](/src/01-Basics/Identity/SQLite/Materials/2.jpg)  
    ![第三步](/src/01-Basics/Identity/SQLite/Materials/3.jpg)
3. **验证**
    ![](/src/01-Basics/Identity/SQLite/Materials/4.jpg)
4. **创建数据库文件**
    - 打开命令行工具，导航到你想要创建数据库文件的目录。
    - 执行以下命令创建数据库文件：
      ```sh
      sqlite3 mydatabase.db
      ```

5. **创建表**
    - 在 SQLite 命令行中，执行以下 SQL 语句创建表：
      ```sql
      CREATE TABLE users (
            id INTEGER PRIMARY KEY,
            username TEXT NOT NULL,
            password TEXT NOT NULL
      );
      ```

6. **插入数据**
    - 使用以下 SQL 语句插入数据：
      ```sql
      INSERT INTO users (username, password) VALUES ('user1', 'password1');
      INSERT INTO users (username, password) VALUES ('user2', 'password2');
      ```

7. **查询数据**
    - 使用以下 SQL 语句查询数据：
      ```sql
      SELECT * FROM users;
      ```

8. **退出 SQLite 命令行**
    - 输入 `.exit` 或按 `Ctrl + D` 退出 SQLite 命令行。

以上步骤将帮助你创建一个简单的 SQLite 数据库并进行基本操作。