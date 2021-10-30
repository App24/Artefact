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

        const string RE_COLOR_SPLITTER_PATTERN = @"(\[[^\[][^\]]*\])";
        const string RE_COLOR_START_PATTERN = @"(\[[^\[\/][^\]]*\])";
        const string RE_COLOR_END_PATTERN = @"(\[[\/][^\]]*\])";

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

                if (Regex.IsMatch(piece, RE_COLOR_END_PATTERN))
                {
                    stringColors.Add(new StringColor(currentText, currentColor));
                    currentText = "";
                    currentColor = ConsoleColor.White;
                }
                else if (Regex.IsMatch(piece, RE_COLOR_START_PATTERN))
                {
                    string colorString = piece.Substring(1, piece.Length - 2);
                    if (Enum.TryParse(colorString, true, out ConsoleColor color))
                    {
                        stringColors.Add(new StringColor(currentText, currentColor));
                        currentText = "";
                        currentColor = color;
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

        public List<List<StringColor>> Split(string separtor)
        {
            List<List<StringColor>> toReturn = new List<List<StringColor>>();
            List<StringColor> current = new List<StringColor>();

            foreach (StringColor stringColor in stringColors)
            {
                string[] texts = stringColor.Text.Split(separtor);
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i > 0)
                    {
                        toReturn.Add(current);
                        current = new List<StringColor>();
                    }
                    current.Add(new StringColor(texts[i], stringColor.Color));
                }
            }
            toReturn.Add(current);

            return toReturn;
        }

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

        public override string ToString()
        {
            return $"[{Text}] ({Color})";
        }
    }
}
