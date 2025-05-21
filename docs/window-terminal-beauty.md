Windows Terminal 是一个专为开发者打造的现代终端工具，适合那些天天用命令行的朋友，比如 PowerShell 用户。在这篇文章里，教你如何把 Windows Terminal 和 PowerShell 打造成一个酷炫又实用的终端，让你的命令行更有风格。

## 安装 Nerd Font 字体

Nerd Font 在流行编程字体的基础上添加了大量图标符号（如 Devicons、Font Awesome、Octicons 等），非常适合命令行美化。

- 集成超 50+ 种编程字体，并进行了图标补丁
- 支持终端美化工具显示图标
- 完整支持 Powerline 符号、Devicons、Font Awesome、Material Icons 等

github:https://www.nerdfonts.com/font-downloads  
website:https://www.nerdfonts.com/

可从其官网 (https://www.nerdfonts.com/font-downloads) 下载喜欢的字体并安装。作者使用的是：JetBrainsMono Nerd Font，下载完所有字体之后，全部选择字体文件并进行安装。

## 安装 Oh My Posh

Oh My Posh 是一个用于美化终端提示符的现代化主题引擎，支持在 Windows、macOS 和 Linux 上各种终端环境中使用，配合 Nerd Font 实现丰富的图标和配色效果。  
GitHub: https://github.com/JanDeDobbeleer/oh-my-posh  
website:https://ohmyposh.dev/

## Windows 包管理工具

winget 是 Windows 上的官方包管理工具，可通过命令行快速安装、升级、删除和搜索软件包。

首先检测一下本地是否安装winget

```shell
winget --version
```

如果没有安装可以通过window store进行安装  
github地址：https://github.com/microsoft/winget-cli，github上面也提供了对应的安装方式。

使用 winget 安装 Oh My Posh:

```shell
winget install JanDeDobbeleer.OhMyPosh
```

安装后会在 C:\Users\bingb\AppData\Local\Programs\oh-my-posh\themes 目录生成大量主题文件。

## 设置主题

在终端中执行：

```shell
oh-my-posh --init --shell pwsh --config ~/AppData/Local/Programs/oh-my-posh/themes/powerlevel10k_rainbow.omp.json | Invoke-Expression
```

然后重启终端。

使用下面方式创建并打开 $PROFILE 文件：

```powershell
if (!(Test-Path -Path $PROFILE)) {
    New-Item -ItemType File -Path $PROFILE -Force
}
notepad $PROFILE
```

在记事本中添加：

```powershell
oh-my-posh --init --shell pwsh --config "$env:USERPROFILE\AppData\Local\Programs\oh-my-posh\themes\jandedobbeleer.omp.json" | Invoke-Expression
```

保存后，重新打开 PowerShell / Windows Terminal。

## 更改主题

在 Windows Terminal 中执行：

```shell
notepad $PROFILE
```

将下面命令写入文件并保存：

```shell
oh-my-posh --init --shell pwsh --config ~/AppData/Local/Programs/oh-my-posh/themes/powerlevel10k_rainbow.omp.json | Invoke-Expression
```

重启终端即可。

本文介绍了如何使用 Nerd Font 和 Oh My Posh 等工具为 Windows Terminal 和 PowerShell 进行美化。首先安装带有大量图标补丁的 Nerd Font 字体，然后通过 winget 安装 Oh My Posh 并选择合适的主题，在 PowerShell 的配置文件中添加初始化命令以启用美化效果。最后可根据需要更换主题并重启终端完成配置。

