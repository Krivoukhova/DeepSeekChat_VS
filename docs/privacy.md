# Privacy and security

DeepSeek AI Assistant sends prompt content to the DeepSeek API when a user submits a request. Depending on the operation, this may include selected source code, conversation content, and attached files or images.

Do not submit credentials, secrets, personal information, or proprietary source code unless you are authorized to share that information with the service. Review DeepSeek's current privacy terms and API documentation before use.

## Local information

Data is stored under `%APPDATA%\DeepSeekChat\`:

- `apikey.dat` — the API key, encrypted with Windows DPAPI for the current Windows user.
- `chats.json` — chat history (plain text, not encrypted).
- `ui.json` — UI preferences.
- `images/` — copies of sent images; removed together with the chat they belong to.

Anyone with access to your unlocked Windows account can use the extension and read the history. Do not include local application data in commits, bug reports, or public attachments.

For security vulnerability reporting, follow [SECURITY.md](../SECURITY.md).
