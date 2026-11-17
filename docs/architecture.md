# Architecture

The extension uses the out-of-process `VisualStudio.Extensibility` model and targets `net8.0-windows8.0`.

## Main project areas

| Path | Responsibility |
| --- | --- |
| `DeepSeekChat/Hosting/` | Extension entry point and dependency registration |
| `DeepSeekChat/Commands/` | Visual Studio commands and selected-code request orchestration |
| `DeepSeekChat/ViewModels/` | Presentation state and UI commands |
| `DeepSeekChat/UI/` | Tool-window implementation and Remote UI markup |
| `DeepSeekChat/Services/` | API communication, persistence, attachments, and UI helpers |
| `DeepSeekChat/Models/` | API, chat, attachment, and preference models |
| `DeepSeekChat/Constants/` | API and UI constants |
| `DeepSeekChat/Localization/` | Language definitions and translations |
| `DeepSeekChat/Theming/` | Theme definitions and palettes |
| `DeepSeekChat/Images/` | Visual Studio command icons |
| `DeepSeekChat/Resources/` | Embedded extension resources |

`DeepSeekChat/Hosting/DeepSeekChatExtension.cs` is the extension entry point. Keep it in the project: it participates in extension registration and service setup.
