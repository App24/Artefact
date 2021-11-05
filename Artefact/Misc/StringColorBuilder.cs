using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Artefact.Misc
{
    class StringColorBuilder
    {
        private string fullMessage;

        List<StringColor> stringColors;

        public const string RE_COLOR_SPLITTER_PATTERN = @"(\[[^\[][^\]]*\])";
        public const string RE_COLOR_START_PATTERN = @"(\[[^\[\/][^\]]*\])";
        public const string RE_COLOR_END_PATTERN = @"(\[[\/][^\]]*\])";

        public StringColorBuilder(string message)
        {
            fullMessage = message;
            BuildString();
        }

        public void BuildString()
        {
            string[] pieces = Regex.Split(fullMessage, RE_COLOR_SPLITTER_PATTERN);
            stringColors = new List<StringColor>();

            ConsoleColor currentColor = ConsoleColor.White;
            string currentText = "";
            foreach (string piece in pieces)
            {
                if (Regex.IsMatch(piece, RE_COLOR_END_PATTERN))
                {
                    stringColors.Add(new StringColor(currentText, currentColor));
                    currentText = "";
                    currentColor = ConsoleColor.White;
                }
                else if (Regex.IsMatch(piece, RE_COLOR_START_PATTERN))
                {
                    // Removing [ and ] from the string piece
                    string colorString = piece.Substring(1, piece.Length - 2);

                    // Try convert it to a ConsoleColor
                    if (Enum.TryParse(colorString, true, out ConsoleColor color))
                    {
                        stringColors.Add(new StringColor(currentText, currentColor));
                        currentText = "";
                        currentColor = color;
                    }
                    // If it fails, append the text to the currentText
                    else
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
                stringColors.Add(new StringColor(currentText, currentColor));
        }

        public List<StringColor> GetStringColors()
        {
            return stringColors;
        }

        public List<List<StringColor>> Split(string separtor)
        {
            List<List<StringColor>> toReturn = new List<List<StringColor>>();
            List<StringColor> currentLine = new List<StringColor>();

            foreach (StringColor stringColor in stringColors)
            {
                string[] texts = stringColor.Text.Split(separtor);
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i > 0)
                    {
                        toReturn.Add(currentLine);
                        currentLine = new List<StringColor>();
                    }
                    currentLine.Add(new StringColor(texts[i], stringColor.Color));
                }
            }
            toReturn.Add(currentLine);

            return toReturn;
        }

        [Obsolete("Use Utils.WriteColor")]
        public void WriteLine()
        {
            foreach (StringColor stringColor in stringColors)
            {
                Console.ForegroundColor = stringColor.Color;
                Console.Write(stringColor.Text);
            }
            Console.WriteLine();
            Console.ResetColor();
        }
    }

    struct StringColor
    {
        public string Text { get; }
        public ConsoleColor Color { get; }

        public StringColor(string text, ConsoleColor color)
        {
            Text = text;
            Color = color;
        }
    }
}
