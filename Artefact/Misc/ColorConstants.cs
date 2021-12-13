﻿using System;

namespace Artefact.Misc
{
    /// <summary>
    /// Where constants for different uses are stored, allows for easy changing of colors for different objects
    /// </summary>
    internal static class ColorConstants
    {
        public const ConsoleColor ITEM_COLOR = ConsoleColor.Magenta;
        public const ConsoleColor CHARACTER_COLOR = ConsoleColor.Cyan;
        public const ConsoleColor ENEMY_COLOR = ConsoleColor.DarkRed;
        public const ConsoleColor COMMAND_COLOR = ConsoleColor.Blue;
        public const ConsoleColor LOCATION_COLOR = ConsoleColor.DarkCyan;
        public const ConsoleColor USER_COLOR = ConsoleColor.DarkRed;
        public const ConsoleColor THOUGHT_COLOR = ConsoleColor.DarkGray;

        public const ConsoleColor ERROR_COLOR = ConsoleColor.DarkRed;
        public const ConsoleColor BAD_COLOR = ConsoleColor.Red;
        public const ConsoleColor WARNING_COLOR = ConsoleColor.Yellow;
        public const ConsoleColor GOOD_COLOR = ConsoleColor.Green;
        public const ConsoleColor XP_COLOR = ConsoleColor.Cyan;
    }
}
