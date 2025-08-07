# Tooltips in Dev-mode Mod

## Mod Overview and Purpose

The "Tooltips in Dev-mode" mod for RimWorld is designed to enhance the utility and user experience of the development mode interface. In-game development tools often feature buttons with text that exceeds the fixed width of the button, making it difficult for modders to identify specific tools at a glance. This mod aims to alleviate this issue by adding tooltips to buttons in dev-mode windows, showing full label text whenever the displayed text is truncated. This small yet significant enhancement aids mod creators and testers in finding the right tools more efficiently and without the need for trial and error.

## Key Features and Systems

- **Dynamic Tooltips**: The mod automatically generates tooltips for buttons in the dev-mode windows when the button text is truncated, showing the complete label text for easy identification.

## Coding Patterns and Conventions

The mod is built with C# and follows these coding standards:

- Static classes are used where appropriate for methods that don't rely on instance-specific data (`DevGUI_CheckboxPinnable`, `DevGUI_Label`, `DevTooltipCache_ClearOnClose`, `TooltipsInDevmode`).
- Use of PascalCase for class and method names for consistency with C# conventions.
- Clear code separation between static utility classes and main functionality classes.

## XML Integration

- The mod does not directly rely on XML for defining content or configurations but interacts with existing XML structures to determine when a tooltip is necessary.

## Harmony Patching

- Harmony is used for patching methods within the RimWorld dev-mode functionality. This allows the mod to hook into the existing button generation code and determine when to add tooltips based on text width constraints.
- Ensure Harmony patches are targeted and do not introduce conflicts with other dev-mode enhancements.

## Suggestions for Copilot

When using GitHub Copilot for further development on this mod, consider the following suggestions:

- **Automate Repetitive Code**: Use Copilot to suggest boilerplate code for new classes or methods that adhere to existing conventions, helping maintain consistency and reduce manual errors.
- **Tooltip Logic**: Implement additional features to tooltips such as customizing the appearance or behavior through Copilot’s suggestions for UI enhancements.
- **Performance Optimization**: Suggest optimizations for handling tooltips efficiently, especially when dealing with a large number of buttons.
- **Extend Functionality**: Brainstorm and prototype additional dev-mode features with Copilot, like filtering or categorizing buttons, to improve modder workflow.
- **Testing Patches**: Use Copilot to aid in writing testing routines that ensure Harmony patches do not introduce unexpected behaviors.

By using these guidelines and suggestions, Copilot can effectively assist in the development and refinement of the "Tooltips in Dev-mode" mod to enhance its utility and performance. Happy modding!
