﻿using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.DialogSystem
{
    static class Dialog
    {
        public static string GetCharacterVoiceLine(Character character, string text, bool showName = true)
        {
            string characterName = character.ToString();
            switch (character)
            {
                case Character.Clippy:
                    showName = GameSettings.KnowsClippy;
                    break;
                case Character.Player:
                    characterName = GameSettings.PlayerName;
                    break;
            }
            string name = showName ? characterName : "???";
            return $"[cyan]{name}[/]: {text}";
        }

        public static void Speak(Character character, string text, bool showName = true)
        {
            string[] lines = text.Split("\n");
            foreach (string line in lines)
            {
                Utils.Type(GetCharacterVoiceLine(character, line, showName));
            }
        }
    }

    enum Character
    {
        Clippy,
        Player
    }
}