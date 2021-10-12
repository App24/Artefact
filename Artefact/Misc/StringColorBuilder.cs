using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Artefact.Misc
{
    class StringColorBuilder
    {
        public string Message { get; private set; }

        public string FullMessage { get; set; }

        List<StringColor> stringColors;

        const string RE_COLOR_SPLITTER_PATTERN = @"(\[[^\/\W][^\]]*\])([^\[]*)(\[[^\]]*\])?";

        public StringColorBuilder(string message)
        {
            FullMessage = message;
            BuildString();
        }

        public void BuildString()
        {
            var pieces = Regex.Split(FullMessage, RE_COLOR_SPLITTER_PATTERN);
            stringColors = new List<StringColor>();
            Message = "";

            ConsoleColor currentColor = ConsoleColor.White;
            string currentText = "";
            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];

                if (piece.StartsWith("[/") && piece.EndsWith("]"))
                {
                    stringColors.Add(new StringColor(currentText, currentColor));
                    currentText = "";
                    currentColor = ConsoleColor.White;
                }
                else if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    string color = piece.Substring(1, piece.Length - 2);
                    if (Enum.TryParse(typeof(ConsoleColor), color, true, out object e))
                    {
                        stringColors.Add(new StringColor(currentText, currentColor));
                        currentText = "";
                        currentColor = (ConsoleColor)e;
                    }
                    else
                    {
                        currentText += piece;
                        Message += piece;
                    }
                }
                else
                {
                    currentText += piece;
                    Message += piece;
                }
            }

            stringColors.Add(new StringColor(currentText, currentColor));
        }

        public List<StringColor> GetStringColors()
        {
            return stringColors;
        }

        public void WriteLine()
        {
            foreach(StringColor stringColor in stringColors)
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

        public override string ToString()
        {
            return $"[{Text}] ({Color})";
        }
    }
}
