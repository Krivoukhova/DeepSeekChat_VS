# Security policy

## Supported versions

Security fixes are applied to the latest published release where practical.
Users should upgrade to the latest release.

## Reporting a vulnerability

Please do not disclose security vulnerabilities in public issues. Use GitHub's
**Report a vulnerability** feature in the repository's Security tab, if enabled.
If private vulnerability reporting is unavailable, contact the repository
maintainer privately through a verified contact method listed on the maintainer's
GitHub profile.

Include the affected version, impact, reproduction steps, and any relevant logs.
Remove API keys, tokens, personal data, source code, and chat content from reports.

## Sensitive data

The extension sends submitted prompts, selected code, and attachments to the
DeepSeek API. API key is stored encrypted with Windows DPAPI
(current user) in `%APPDATA%\DeepSeekChat\apikey.dat`; chat history in the same
directory is not encrypted. Treat the Windows user profile as sensitive and never
share its contents in bug reports.
