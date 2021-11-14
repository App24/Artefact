using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Artefact.Misc
{
    internal class TextEffectBuilder
    {
        public string Text { get { return strText; } set { strText = value; BuildString(); } }

        private string strText;

        public List<TextEffect> TextEffects { get; private set; }

        public const string RE_COLOR_SPLITTER_PATTERN = @"(\[[^\[][^\]]*\])";
        public const string RE_COLOR_START_PATTERN = @"\[([^\[\/][^\]]*)\]";

        public TextEffectBuilder(string text)
        {
            Text = text;
        }

        private void BuildString()
        {
            string[] pieces = Regex.Split(Text, RE_COLOR_SPLITTER_PATTERN);
            TextEffects = new List<TextEffect>();

            string currentText = "";
            foreach (string piece in pieces)
            {
                if (Regex.IsMatch(piece, RE_COLOR_START_PATTERN))
                {
                    // Removing [ and ] from the string piece
                    string colorString = Regex.Split(piece, RE_COLOR_START_PATTERN)[1];
                    string[] parts = colorString.Split("=");

                    if (!Enum.TryParse(parts[0], true, out TextEffectType textEffectType))
                    {
                        currentText += piece;
                        continue;
                    }

                    switch (textEffectType)
                    {
                        case TextEffectType.Backspace:
                            {
                                if (parts.Length <= 1)
                                {
                                    currentText += piece;
                                    continue;
                                }
                                if (!int.TryParse(parts[1], out int value))
                                {
                                    currentText += piece;
                                    continue;
                                }
                                TextEffects.Add(new TextEffect(currentText, textEffectType, value));
                                currentText = "";
                            }
                            break;
                    }
                }
                else
                {
                    currentText += piece;
                }
            }

            if (!string.IsNullOrEmpty(currentText))
                TextEffects.Add(new TextEffect(currentText, TextEffectType.None, 0));
        }

        public List<List<TextEffect>> Split(string separtor)
        {
            List<List<TextEffect>> toReturn = new List<List<TextEffect>>();
            List<TextEffect> currentLine = new List<TextEffect>();

            foreach (TextEffect textEffect in TextEffects)
            {
                string[] texts = textEffect.Text.Split(separtor);
                for (int i = 0; i < texts.Length; i++)
                {
                    if (i > 0)
                    {
                        toReturn.Add(currentLine);
                        currentLine = new List<TextEffect>();
                    }
                    currentLine.Add(new TextEffect(texts[i], textEffect.TextEffectType, textEffect.Value));
                }
            }
            toReturn.Add(currentLine);

            return toReturn;
        }
    }

    internal struct TextEffect
    {
        public TextEffect(string text, TextEffectType textEffectType, int value)
        {
            Text = text;
            TextEffectType = textEffectType;
            Value = value;
        }

        public string Text { get; }
        public TextEffectType TextEffectType { get; }
        public int Value { get; }
    }

    internal enum TextEffectType
    {
        None,
        Backspace
    }
}
