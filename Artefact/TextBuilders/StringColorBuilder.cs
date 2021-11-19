using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Artefact.TextBuilders
{
    /// <summary>
    /// Separate an input string into a <see cref="List{T}"/> of <see cref="StringColor"/> which can then be used to
    /// print out text in different colors
    /// </summary>
    internal class StringColorBuilder : BaseTextBuilder<StringColor>
    {
        public const string RE_COLOR_SPLITTER_PATTERN = @"(\[[^\[][^\]]*\])";
        public const string RE_COLOR_START_PATTERN = @"\[([^\[\/][^\]]*)\]";
        public const string RE_COLOR_END_PATTERN = @"(\[[\/][^\]]*\])";

        public StringColorBuilder(string text) : base(text)
        {
        }

        protected override void BuildString()
        {
            string[] pieces = Regex.Split(Text, RE_COLOR_SPLITTER_PATTERN);
            BuildData = new List<StringColor>();

            List<ConsoleColor> previousForegroundColors = new List<ConsoleColor>();
            List<ConsoleColor> previousBackgroundColors = new List<ConsoleColor>();

            List<ColorType> colorTypes = new List<ColorType>();

            ConsoleColor currentForegroundColor = ConsoleColor.White;
            ConsoleColor currentBackgroundColor = ConsoleColor.Black;
            string currentText = "";
            foreach (string piece in pieces)
            {
                if (Regex.IsMatch(piece, RE_COLOR_END_PATTERN))
                {
                    BuildData.Add(new StringColor(currentText, currentForegroundColor, currentBackgroundColor));
                    currentText = "";
                    if (colorTypes.Count > 0)
                    {
                        ColorType colorType = colorTypes[colorTypes.Count - 1];
                        colorTypes.RemoveAt(colorTypes.Count - 1);
                        switch (colorType)
                        {
                            case ColorType.Foreground:
                                {
                                    if (previousForegroundColors.Count > 0)
                                    {
                                        currentForegroundColor = previousForegroundColors[previousForegroundColors.Count - 1];
                                        previousForegroundColors.RemoveAt(previousForegroundColors.Count - 1);
                                    }
                                    else
                                    {
                                        currentForegroundColor = ConsoleColor.White;
                                    }
                                }
                                break;
                            case ColorType.Background:
                                {
                                    if (previousBackgroundColors.Count > 0)
                                    {
                                        currentBackgroundColor = previousBackgroundColors[previousBackgroundColors.Count - 1];
                                        previousBackgroundColors.RemoveAt(previousBackgroundColors.Count - 1);
                                    }
                                    else
                                    {
                                        currentBackgroundColor = ConsoleColor.Black;
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (Regex.IsMatch(piece, RE_COLOR_START_PATTERN))
                {
                    // Removing [ and ] from the string piece
                    string colorString = Regex.Split(piece, RE_COLOR_START_PATTERN)[1];

                    string[] parts = colorString.Split("=");

                    ColorType colorType = ColorType.Foreground;

                    if (parts.Length > 1)
                    {
                        colorString = parts[1];
                        switch (parts[0].ToLower().Trim())
                        {
                            case "b":
                                {
                                    colorType = ColorType.Background;
                                }
                                break;
                        }
                    }

                    // Try convert it to a ConsoleColor
                    if (!int.TryParse(colorString.Trim(), out int _) && Enum.TryParse(colorString.Trim(), true, out ConsoleColor color))
                    {
                        switch (colorType)
                        {
                            case ColorType.Foreground:
                                {
                                    previousForegroundColors.Add(currentForegroundColor);
                                }
                                break;
                            case ColorType.Background:
                                {
                                    previousBackgroundColors.Add(currentBackgroundColor);
                                }
                                break;
                        }
                        BuildData.Add(new StringColor(currentText, currentForegroundColor, currentBackgroundColor));
                        currentText = "";
                        switch (colorType)
                        {
                            case ColorType.Foreground:
                                {
                                    currentForegroundColor = color;
                                }
                                break;
                            case ColorType.Background:
                                {
                                    currentBackgroundColor = color;
                                }
                                break;
                        }
                        colorTypes.Add(colorType);
                    }
                    else // If it fails, append the text to the currentText
                    {
                        currentText += piece;
                    }
                }
                else
                {
                    currentText += piece;
                }
            }

            if (!string.IsNullOrEmpty(currentText))
                BuildData.Add(new StringColor(currentText, currentForegroundColor, currentBackgroundColor));
        }
    }

    internal class StringColor : BaseTextBuildData<StringColor>
    {
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }

        public StringColor(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor) : base(text)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        public override StringColor Clone()
        {
            return new StringColor(Text, ForegroundColor, BackgroundColor);
        }
    }

    internal enum ColorType
    {
        Foreground,
        Background
    }
}
