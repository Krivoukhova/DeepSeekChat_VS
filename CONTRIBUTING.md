# Contributing

Thank you for considering a contribution.

## Before you start

- Search existing issues and pull requests before opening a new one.
- For substantial changes, open an issue first to discuss the approach.
- Never include API keys, tokens, chat exports, user attachments, or other private data.

## Development setup

1. Use Windows with Visual Studio 2022 17.14+ and the **Visual Studio extension development** workload.
2. Install the .NET 8 SDK.
3. Clone the repository and open `DeepSeekChat.slnx`.
4. Restore packages and build the `DeepSeekChat` project.
5. Use F5 to debug in an experimental Visual Studio instance.

## Pull requests

- Keep changes focused and explain the user-visible impact.
- Follow the existing C# conventions and nullable-reference-type annotations.
- Add or update XML documentation for types and members.
- Keep UI strings in the localization layer.
- Include manual verification steps and test results in the pull request.
- Do not commit `bin/`, `obj/`, `.vs/`, `.vsix`, local settings, or secrets.

By submitting a contribution, you agree that it may be distributed under the
repository's MIT license.
