﻿using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Artefact.Misc
{
    static class Utils
    {
        public static string[] ValidYes { get; } = new string[] { "y", "yes", "yeah", "yea" };
        public static string[] ValidNo { get; } = new string[] { "n", "no", "nah" };

        static Dictionary<Gender, Dictionary<PronounReference, string>> pronounReferences = new Dictionary<Gender, Dictionary<PronounReference, string>>()
        {
            { Gender.Male, new Dictionary<PronounReference, string>()
                {
                    { PronounReference.Nominative, "he" },
                    { PronounReference.Objective, "him" },
                    { PronounReference.Possessive_Determiner, "his" },
                    { PronounReference.Possessive_Pronoun, "his" },
                    { PronounReference.Reflexive, "himself" }
                }
            },
            { Gender.Female, new Dictionary<PronounReference, string>()
                {
                    { PronounReference.Nominative, "she" },
                    { PronounReference.Objective, "her" },
                    { PronounReference.Possessive_Determiner, "her" },
                    { PronounReference.Possessive_Pronoun, "hers" },
                    { PronounReference.Reflexive, "herself" }
                }
            },
            { Gender.Other, new Dictionary<PronounReference, string>()
                {
                    { PronounReference.Nominative, "they" },
                    { PronounReference.Objective, "them" },
                    { PronounReference.Possessive_Determiner, "their" },
                    { PronounReference.Possessive_Pronoun, "theirs" },
                    { PronounReference.Reflexive, "themselves" }
                }
            }
        };

        /// <summary>
        /// Get the user to select an option from an array, they are also able to type the name of the item to select it
        /// </summary>
        /// <param name="options">The options available to choose from</param>
        /// <returns>Returns a number between 0 and length of <paramref name="options"/> - 1</returns>
        public static int GetSelection(params string[] options)
        {
            int index = -1;
            List<string> optionsList = new List<string>(options);
            WriteColor(options.Map((option, i) => $"{i + 1}. {option}[/]").Join("\n"));
            while (index < 0 || index >= options.Length)
            {
                string selection = Console.ReadLine().ToLower();
                if (!int.TryParse(selection, out index))
                {
                    index = optionsList.FindIndex(o => selection == o.ToLower().TrimColor()) + 1;
                }
                index -= 1;
                if (index < 0 || index >= options.Length)
                {
                    WriteColor($"[{ColorConstants.BAD_COLOR}]Please enter a valid choice!");
                }
            }
            return index;
        }

        public static bool GetConfirmation(string inputText)
        {
            while (true)
            {
                WriteColor(inputText);
                string response = Console.ReadLine().ToLower().Trim();

                if (ValidYes.Contains(response))
                {
                    return true;
                }
                else if (ValidNo.Contains(response))
                {
                    return false;
                }
            }
        }

        public static bool GetCharacterConfirmation(Character character, string inputText)
        {
            Type(Dialog.GetCharacterVoiceLine(character, inputText));
            string response = Console.ReadLine().ToLower().Trim();

            return ValidYes.Contains(response);
        }

        public static void WriteCenter(string text)
        {
            StringColorBuilder stringColorBuilder = new StringColorBuilder(text);
            foreach (List<StringColor> stringColors in stringColorBuilder.Split("\n"))
            {
                Console.SetCursorPosition((Console.WindowWidth - stringColors.Map(s => s.Text).Join(" ").Length) / 2, Console.CursorTop);
                foreach (StringColor stringColor in stringColors)
                {
                    Console.ForegroundColor = stringColor.Color;
                    Console.Write(stringColor.Text);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        public static void WriteColor(string text)
        {
            StringColorBuilder stringColorBuilder = new StringColorBuilder(text);

            foreach (List<StringColor> stringColors in stringColorBuilder.Split("\n"))
            {
                foreach (StringColor stringColor in stringColors)
                {
                    Console.ForegroundColor = stringColor.Color;
                    Console.Write(ReplacePronouns(stringColor.Text));
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        public static string CreateProgressBar(float percentage, int width = 30, ConsoleColor barColor = ConsoleColor.Green)
        {
            string text = "";
            for (int i = 0; i < width; i++)
            {
                if ((i / (float)width) < percentage)
                    text += $"[{barColor}]#[/]";
                else
                    text += "-";
            }
            return $"[{text}]";
        }

        public static string CreateProgressBar(float value, float maxValue, int width = 30, ConsoleColor barColor = ConsoleColor.Green)
        {
            string progressBar = CreateProgressBar(value / maxValue, width, barColor);
            progressBar += $" [{barColor}]{value}[/]/[{barColor}]{maxValue}[/]";
            return progressBar;
        }

        public static string CreateHealthBar(Entity entity, int width = 30, ConsoleColor barColor = ConsoleColor.Green)
        {
            return CreateProgressBar(entity.Health, entity.MaxHealth, width, barColor);
        }

        public static string CreateXPBar(Entity entity, int width = 30, ConsoleColor barColor = ColorConstants.XP_COLOR)
        {
            return CreateProgressBar(entity.XP, entity.GetLevelXP(), width, barColor);
        }

        public static void Type(string text)
        {
            StringColorBuilder stringColorBuilder = new StringColorBuilder(text);
            foreach (List<StringColor> stringColors in stringColorBuilder.Split("\n"))
            {
                foreach (StringColor stringColor in stringColors)
                {
                    Console.ForegroundColor = stringColor.Color;
                    foreach (char letter in ReplacePronouns(stringColor.Text))
                    {
                        Console.Write(letter);
#if !BYPASS
                        Thread.Sleep((int)GlobalSettings.TextSpeed);
#endif
                    }
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        static string ReplacePronouns(string text)
        {
            if (GameSettings.Instance != null)
            {
                if (!pronounReferences.TryGetValue(GameSettings.PlayerGender, out var references))
                {
                    references = GameSettings.Pronouns;
                }
                foreach (PronounReference pronounReference in Enum.GetValues(typeof(PronounReference)))
                {
                    if (references.TryGetValue(pronounReference, out string reference))
                    {
                        text = text.Replace($"[{pronounReference}]", reference);
                    }
                }
            }
            return text;
        }
    }

    enum PronounReference
    {
        Nominative,
        Objective,
        Possessive_Determiner,
        Possessive_Pronoun,
        Reflexive
    }
}
