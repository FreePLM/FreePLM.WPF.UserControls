# FreePLM.WPF.UserControls

Reusable WPF user controls library for FreePLM applications.

## Overview

This library contains reusable WPF user controls that can be shared across FreePLM desktop applications. All controls are built using the MVVM pattern with infrastructure provided by FreePLM.WPF.Core.

## Dependencies

- .NET 10.0 (Windows)
- FreePLM.WPF.Core 10.0.0
  - CommunityToolkit.Mvvm 8.4.0
  - FreePLM.Common 10.0.2

## Project Structure

```
FreePLM.WPF.UserControls/
├── Controls/           # Custom WPF controls
├── UserControls/       # Composite user controls
├── Styles/            # Shared styles and themes
├── Resources/         # Images, icons, and other resources
└── Behaviors/         # WPF behaviors and attached properties
```

## Usage

Add a reference to this package in your WPF application:

```xml
<PackageReference Include="FreePLM.WPF.UserControls" Version="10.0.0" />
```

Then use the controls in your XAML:

```xaml
<Window xmlns:controls="clr-namespace:FreePLM.WPF.UserControls.Controls;assembly=FreePLM.WPF.UserControls">
    <!-- Use controls here -->
</Window>
```

## Development

This project follows the FreePLM MVVM architecture:

- All ViewModels should inherit from `ViewModelBase` (FreePLM.WPF.Core)
- Commands should use `CommandsBase` (FreePLM.WPF.Core)
- Use dependency injection via `ServiceLocator` (FreePLM.WPF.Core)
- Leverage built-in converters from FreePLM.WPF.Core

## License

Free License 08-02-2025

## Package Information

- **PackageId**: FreePLM.WPF.UserControls
- **Version**: 10.0.0
- **Repository**: https://github.com/FreePLM/FreePLM.WPF.UserControls
