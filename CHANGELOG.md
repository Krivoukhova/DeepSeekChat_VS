# Changelog

All notable changes to this project are documented here.

## [1.0.1]

### Fixed
- Declared compatibility with .NET 8 and .NET 10 runtimes to remove the "supports only .NET 8" notice in Visual Studio 2026.

## [1.0.0]

### Security
- First public release.
- The API key is now encrypted with Windows DPAPI (`apikey.dat`). A plain-text `settings.txt` from earlier versions is migrated and deleted.
- Removed the "copy key" button.

### Fixed
- The model now receives earlier messages of the chat as context (last 20 messages / 60,000 characters).
- A reply that arrives after switching or deleting a chat is stored in the chat it belongs to.
- Deleting a chat also deletes its cached images.
- History and preference files are written atomically; an unreadable file is backed up as `*.corrupt` instead of being silently overwritten.
- API errors show a short message instead of the raw server response; request timeout set to 5 minutes.

### Changed
- Removed duplicated documentation pages; repository URLs filled in.
