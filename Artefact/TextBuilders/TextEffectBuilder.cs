using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Artefact.TextBuilders
{
    internal class TextEffectBuilder : BaseTextBuilder<TextEffect>
    {
        public const string RE_COLOR_SPLITTER_PATTERN = @"(\[[^\[][^\]]*\])";
        public const string RE_COLOR_START_PATTERN = @"\[([^\[\/][^\]]*)\]";

        public TextEffectBuilder(string text) : base(text)
        {
        }

        protected override void BuildString()
        {
            string[] pieces = Regex.Split(Text, RE_COLOR_SPLITTER_PATTERN);
            BuildData = new List<TextEffect>();

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
                                BuildData.Add(new TextEffect(currentText, textEffectType, value));
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
                BuildData.Add(new TextEffect(currentText, TextEffectType.None, 0));
        }
    }

    internal class TextEffect : BaseTextBuildData<TextEffect>
    {
        public TextEffectType TextEffectType { get; }
        public int Value { get; }

        public TextEffect(string text, TextEffectType textEffectType, int value) : base(text)
        {
            TextEffectType = textEffectType;
            Value = value;
        }

        public override TextEffect Clone()
        {
            return new TextEffect(Text, TextEffectType, Value);
        }
    }

    internal enum TextEffectType
    {
        None,
        Backspace
    }
}
