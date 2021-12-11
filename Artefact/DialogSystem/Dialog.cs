﻿using Artefact.Misc;
using Artefact.Settings;

namespace Artefact.DialogSystem
{
    internal static class Dialog
    {
        public static string GetCharacterVoiceLine(Character character, string text, bool showName = true)
        {
            string characterName = character.ToString();
            switch (character)
            {
                case Character.Clippy:
                    {
                        showName = GameSettings.KnowsClippy;
                    }
                    break;
                case Character.Player:
                    {
                        characterName = GameSettings.PlayerName;
                    }
                    break;
            }
            string name = showName ? characterName : "???";
            return $"[{ColorConstants.CHARACTER_COLOR}]{name}[/]: {text}";
        }

        public static void Speak(Character character, string text, bool showName = true, bool instant = false)
        {
            string[] lines = text.Split("\n");
            foreach (string line in lines)
            {
                if (!instant)
                    Utils.Type(GetCharacterVoiceLine(character, line, showName));
                else
                    Utils.WriteColor(GetCharacterVoiceLine(character, line, showName));
            }
        }
    }

    internal enum Character
    {
        Clippy,
        Player,
        BonziBuddy,
        Memz,
        MSN,
        Skype
    }
}
