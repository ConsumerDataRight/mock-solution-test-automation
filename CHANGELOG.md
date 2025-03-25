# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [3.0.0] - 2025-03-19
### Changed
- Fixed multiple build warnings to improve code quality and maintainability.

### Removed
- Removed all OIDC Hybrid Flow related code and functionality.

## [2.0.0] - 2024-08-06
### Changed
- Updated nuget package versions.
- Migrated from .Net 6 to .Net 8
- Changed Banking Get Accounts API to only support version 2


## [1.1.0] - 2024-03-13

### Added
- Added support Authorisation Code Flow (ACF).
- Added helper method to modify client claims.

### Changed
- Updated nuget package versions to avoid vulnerabilities.
- Updated methods to use ACF as default instead of Hybrid Flow.
- Updated Playwright Locators to use new Id's for the consent workflow UI's.


## [1.0.0] - 2023-11-23

### Added
- First release of the Mock Solution Test Automation project
