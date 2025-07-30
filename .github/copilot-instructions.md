# Copilot Instructions for RimWorld Modding Project

## Mod Overview and Purpose

This mod is designed to enhance the developer experience in RimWorld by adding additional debugging tools and user interface enhancements. The primary goal is to provide developers with a more intuitive and efficient way to visualize and manipulate game data directly within the game's development environment.

## Key Features and Systems

- **Developer UI Enhancements**: The mod introduces new UI components such as pinnable checkboxes and labels to organize and control the display of developer information easily.
- **Tooltips Assistance**: Improved tooltips in development mode to provide quick reference details without cluttering the main interface.
- **Version Compatibility**: Supports multiple .NET Framework versions, ensuring broader compatibility across different development environments.

## Coding Patterns and Conventions

- **Static Classes**: Utilizes static classes, such as `DevGUI_CheckboxPinnable`, `DevGUI_Label`, and `TooltipsInDevmode`, to encapsulate specific functionality related to the developer UI enhancements.
- **Consistent Naming**: Classes and methods follow PascalCase naming conventions to maintain clarity and consistency.
- **Code Organization**: Organizes related utilities and functionalities into separate static classes for modularity and ease of maintenance.

## XML Integration

- **XML Data**: Although not indicated in the provided summary, if XML files are used to define UI components or configure settings, ensure they are well-structured and adhere to RimWorld's modding standards.
- **Patching and Overrides**: When integrating XML data, utilize PatchOperation instructions to modify and extend base game definitions safely.

## Harmony Patching

- **Patch Application**: Use Harmony, a library for patching .NET assemblies, to override and extend RimWorld's base methods without modifying the original game files.
- **Target Methods**: Identify and target specific game methods that require enhancement or additional behaviors, ensuring that patches are non-destructive and reversible.
- **Version Control**: Always test Harmony patches against different game versions to avoid compatibility issues.

## Suggestions for Copilot

1. **Code Generation Templates**: Use Copilot to assist in generating templates for new classes or methods, adhering to the project's existing coding patterns.
2. **UI Component Suggestions**: Leverage Copilot to brainstorm new UI components or improvements on existing ones, with a focus on enhancing developer usability.
3. **Refactoring Assistance**: Employ Copilot to refactor legacy code or simplify complex logic while maintaining functionality.
4. **Harmony Integration Guidance**: Use Copilot to explore examples and suggestions for efficient Harmony patching, focusing on method injections and replacements.
5. **Error Handling**: Implement comprehensive error checking and logging mechanisms with Copilot's assistance to ensure robustness and ease of debugging.

By following these instructions, contributors can work cohesively within the existing framework and utilize GitHub Copilot effectively to advance the mod's development.
