# Publishing

This document describes the release process at a high level. It is not a release checklist and does not imply that a package has already passed Microsoft's validation.

## GitHub

1. Confirm the repository URL (`https://github.com/Krivoukhova/DeepSeekChat_VS`) in the README and CI badge.
2. Update the project version in `DeepSeekChat/DeepSeekChat.csproj` and describe user-visible changes in `CHANGELOG.md`.
3. Build and validate the Release configuration on Windows with the Visual Studio extension tooling installed.
4. Create a GitHub Release and attach the exact `.vsix` package produced by the validated build.

## Visual Studio Marketplace

The Marketplace publisher identity and extension metadata must match the values registered with Microsoft. In particular, verify the publisher name configured in `DeepSeekChat/Hosting/DeepSeekChatExtension.cs`, along with the extension ID, display name, description, supported Visual Studio versions, and generated VSIX manifest.

Prepare the Marketplace listing information requested by Microsoft, such as the description, icon, screenshots, support information, and privacy details. Submit the same VSIX package that was built and tested. Complete any publisher verification and package validation steps shown in the Marketplace publishing portal.

Marketplace publication is subject to Microsoft's current requirements and approval process. Consult the official [Visual Studio Marketplace publishing documentation](https://learn.microsoft.com/visualstudio/extensibility/publishing).
