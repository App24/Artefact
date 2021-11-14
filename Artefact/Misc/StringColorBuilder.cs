using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Artefact.Misc
{
    /// <summary>
    /// Separate an input string into a <see cref="List{T}"/> of <see cref="StringColor"/> which can then be used to
    /// print out text in different colors
    /// </summary>
    internal class StringColorBuilder
    {
        public string Text { get { return strText; } set { strText = value; BuildString(); } }

        private string strText;

        public List<StringColor> StringColors { get; private set; }

        public const string RE_COLOR_SPLITTER_PATTERN = @"(\[[^\[][^\]]*\])";
        public const string RE_COLOR_START_PATTERN = @"\[([^\[\/][^\]]*)\]";
        public const string RE_COLOR_END_PATTERN = @"(\[[\/][^\]]*\])";

        public StringColorBuilder(string str)
        {
            Text = str;
        }

        private void BuildString()
        {
            string[] pieces = Regex.Split(Text, RE_COLOR_SPLITTER_PATTERN);
            StringColors = new List<StringColor>();

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
                    StringColors.Add(new StringColor(currentText, currentForegroundColor, currentBackgroundColor));
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
                        StringColors.Add(new StringColor(currentText, currentForegroundColor, currentBackgroundColor));
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
                StringColors.Add(new StringColor(currentText, currentForegroundColor, currentBackgroundColor));
        }

        public List<List<StringColor>> Split(string separtor)
        {
            List<List<StringColor>> toReturn = new List<List<StringColor>>();
            List<StringColor> currentLine = new List<StringColor>();

            foreach (StringColor stringColor in StringColors)
            {
                string[] texts = stringColor.Text.Split(separtor);
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i > 0)
                    {
                        toReturn.Add(currentLine);
                        currentLine = new List<StringColor>();
                    }
                    currentLine.Add(new StringColor(texts[i], stringColor.ForegroundColor, stringColor.BackgroundColor));
                }
            }
            toReturn.Add(currentLine);

            return toReturn;
        }
    }

    internal struct StringColor
    {
        public string Text { get; }
        public ConsoleColor ForegroundColor { get; }
        public ConsoleColor BackgroundColor { get; }

        public StringColor(string text, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Text = text;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }
    }

    internal enum ColorType
    {
        Foreground,
        Background
    }
}
