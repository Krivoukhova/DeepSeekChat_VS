<div align="center">

# DeepSeek AI Assistant

**AI coding assistant for Visual Studio 2022 and 2026**

[![License](https://img.shields.io/badge/license-MIT-blue)](https://github.com/Krivoukhova/DeepSeekChat_VS/blob/master/LICENSE)
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2022%20%7C%202026-purple)](https://visualstudio.microsoft.com/)
[![Version](https://img.shields.io/badge/version-1.0-blue)](https://github.com/Krivoukhova/DeepSeekChat_VS)

</div>

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/main.png" alt="DeepSeek AI Assistant in Visual Studio">
</p>

## Features

- AI chat inside Visual Studio with conversation context
- Explain selected code
- Refactor selected code
- Quick Actions: explain, fix errors, write tests, optimize performance
- Image and file attachments
- Model selection
- Local chat history
- DeepSeek account information (key status and balance)
- API key stored encrypted for your Windows account
- 10 interface languages and several themes

## AI Chat

Chat with DeepSeek directly inside Visual Studio.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/chat.png" alt="DeepSeek AI Assistant">
</p>

## Explain Code

Select code in the editor and use **Explain with DeepSeek** from the context menu.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/explain.png" alt="Explain with DeepSeek">
</p>

The selected code is placed in the chat input; review it and send. DeepSeek's explanation appears in the chat.

## Refactor Code

Select code in the editor and use **Refactor with DeepSeek** from the context menu.

DeepSeek provides refactoring suggestions in the chat.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/refactor-code.png" alt="Refactor Code Result">
</p>

## Attachments

Attach text files and images to your message.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/adding-file.png" alt="Adding a file">
</p>

## Models

Choose the DeepSeek model for your requests.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/choose-model.png" alt="Choosing a model">
</p>

## Quick Actions

Quick Actions fill the input with a ready request for common tasks:

- **Explain this code**
- **Fix the error**
- **Write tests**
- **Optimize performance**

## Chat History

Conversations are saved locally and can be reopened or deleted later.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/chat-history.png" alt="Chat History">
</p>

## Account

View API key status, account balance and connection status.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/account.png" alt="DeepSeek Account">
</p>

## Settings

Configure your DeepSeek API key, language, theme and display name directly from Visual Studio.

<p align="center">
  <img src="https://raw.githubusercontent.com/Krivoukhova/DeepSeekChat_VS/master/docs/images/settings.png" alt="DeepSeek Settings">
</p>

## Getting Started

1. Install **DeepSeek AI Assistant**.
2. Open **View → Other Windows → DeepSeek AI Assistant**.
3. Open **Settings** and enter your DeepSeek API key.
4. Save the settings.
5. Start chatting with DeepSeek.

## Requirements

- Visual Studio 2022 (17.14 or later) or Visual Studio 2026
- A DeepSeek API key

## Documentation

For detailed documentation, visit the [GitHub repository](https://github.com/Krivoukhova/DeepSeekChat_VS).

## Privacy

- The API key is stored encrypted (Windows DPAPI) and is used only to authenticate requests to the DeepSeek API.
- Chat history and settings are stored locally on your computer. Chat history is not encrypted.
- **Your messages, selected code and attached files are sent to the DeepSeek API** when you submit a request. Do not send secrets or code you are not allowed to share. See [DeepSeek's privacy terms](https://platform.deepseek.com/).

## Project

GitHub: https://github.com/Krivoukhova/DeepSeekChat_VS
