# Development

## Prerequisites

- Windows.
- Visual Studio 2022 17.14 or later, or a compatible newer Visual Studio release.
- The **Visual Studio extension development** workload for local development.
- .NET 8 SDK.

The project targets `net8.0-windows8.0` and declares `net8.0` and `net10.0` as supported runtimes in `DeepSeekChatExtension.cs` and references the `Microsoft.VisualStudio.Extensibility` SDK and build packages in the 17.14 release line.

## Clone and build

```powershell
git clone https://github.com/Krivoukhova/DeepSeekChat_VS.git
cd DeepSeekChat_VS
dotnet restore .\DeepSeekChat\DeepSeekChat.csproj
dotnet build .\DeepSeekChat\DeepSeekChat.csproj --configuration Release
```

Open `DeepSeekChat.slnx` in Visual Studio to debug the extension with F5. The extension SDK build generates a VSIX package under the project's Release output directory. The exact output path can vary with the installed SDK and target framework; locate `DeepSeekChat.vsix` under `DeepSeekChat/bin/Release/`.

## Development conventions

- Keep user-facing text in the localization layer.
- Preserve cancellation-token flow for asynchronous operations.
- Dispose HTTP and file resources deterministically.
- Keep changes focused and document public or otherwise important members.
- Never commit `bin/`, `obj/`, `.vs/`, `.vsix` output, API keys, or user data.

See [CONTRIBUTING.md](../CONTRIBUTING.md) for contribution expectations.
