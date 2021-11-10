﻿using Artefact.DialogSystem;
using Artefact.Entities;
using Artefact.GenderSystem;
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
        public static List<string> ValidYes { get; } = new List<string>() { "y", "yes", "yeah", "yea" };
        public static List<string> ValidNo { get; } = new List<string>() { "n", "no", "nah" };

        /// <summary>
        /// Get the user to select an option from an array, they are also able to type the name of the item to select it
        /// </summary>
        /// <param name="options">The options available to choose from</param>
        /// <returns>Returns a number between 0 (inclusive) and length of <paramref name="options"/> (exclusive)</returns>
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
                    index = optionsList.FindIndex(option => selection == option.ToLower().TrimColor()) + 1;
                }
                index -= 1;
                if (index < 0 || index >= options.Length)
                {
                    WriteColor($"[{ColorConstants.BAD_COLOR}]Please enter a valid choice!");
                }
            }
            return index;
        }

        /// <summary>
        /// Get the user to confirm something
        /// </summary>
        /// <param name="inputText">Text to be displayed to the user for them to confirm</param>
        /// <returns><see langword="true"/> if the user said yes, <see langword="false"/> if they said no</returns>
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

        /// <summary>
        /// Get the user to confirm something that <paramref name="character"/> said
        /// </summary>
        /// <param name="character">Character to say <paramref name="inputText"/></param>
        /// <param name="inputText">Text for <paramref name="character"/> to say</param>
        /// <returns><see langword="true"/> if the user said yes, <see langword="false"/> if they said no</returns>
        public static bool GetCharacterConfirmation(Character character, string inputText)
        {
            while (true)
            {
                Type(Dialog.GetCharacterVoiceLine(character, inputText));
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

        /// <summary>
        /// Write text in the middle of the console window
        /// </summary>
        /// <param name="text">The text to be written to the console window</param>
        public static void WriteCenter(string text)
        {
            StringColorBuilder stringColorBuilder = new StringColorBuilder(text);
            foreach (List<StringColor> stringColors in stringColorBuilder.Split("\n"))
            {
                Console.SetCursorPosition((Console.WindowWidth - stringColors.Map(s => s.Text).Join("").Length) / 2, Console.CursorTop);
                foreach (StringColor stringColor in stringColors)
                {
                    Console.ForegroundColor = stringColor.Color;
                    Console.Write(stringColor.Text);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Write colored text
        /// </summary>
        /// <param name="text">Text with colored formatting</param>
        public static void WriteColor(string text)
        {
            StringColorBuilder stringColorBuilder = new StringColorBuilder(text);

            foreach (List<StringColor> stringColors in stringColorBuilder.Split("\n"))
            {
                foreach (StringColor stringColor in stringColors)
                {
                    Console.ForegroundColor = stringColor.Color;
                    Console.Write(GenderUtils.ReplacePronouns(stringColor.Text));
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        /// <summary>
        /// Create a progress bar in a string format
        /// </summary>
        /// <param name="percentage">Value between 0 and 1</param>
        /// <param name="width">How many characters wide the progress bar should be</param>
        /// <param name="barColor">The color of the progress bar</param>
        /// <returns>Progress bar in string format with colored string</returns>
        public static string CreateProgressBar(float percentage, int width = 30, ConsoleColor barColor = ConsoleColor.Green)
        {
            string text = "";
            for (int i = 0; i < width - 2; i++)
            {
                if ((i / (float)width) < percentage)
                    text += $"[{barColor}]#[/]";
                else
                    text += "-";
            }
            return $"[{text}]";
        }

        /// <summary>
        /// Create a progress bar in a string format
        /// </summary>
        /// <param name="value">The current value of the progress bar</param>
        /// <param name="maxValue">The maximum value the progress bar can be</param>
        /// <param name="width">How many characters wide the progress bar should be</param>
        /// <param name="barColor">The color of the progress bar</param>
        /// <returns>Progress bar in string format with colored string</returns>
        public static string CreateProgressBar(float value, float maxValue, int width = 30, ConsoleColor barColor = ConsoleColor.Green)
        {
            string progressBar = CreateProgressBar(value / maxValue, width, barColor);
            progressBar += $" [{barColor}]{value}[/]/[{barColor}]{maxValue}[/]";
            return progressBar;
        }

        /// <summary>
        /// Create a health bar in string format
        /// </summary>
        /// <param name="entity">The entity to get the health from</param>
        /// <param name="width">How many characters wide the health bar should be</param>
        /// <param name="barColor">The color of the health bar</param>
        /// <returns>Health bar in string format with colored string</returns>
        public static string CreateHealthBar(Entity entity, int width = 30, ConsoleColor barColor = ConsoleColor.Green)
        {
            return CreateProgressBar(entity.Health, entity.MaxHealth, width, barColor);
        }

        /// <summary>
        /// Create a xp bar in string format
        /// </summary>
        /// <param name="entity">The entity to get the xp from</param>
        /// <param name="width">How many characters wide the xp bar should be</param>
        /// <param name="barColor">The color of the xp bar</param>
        /// <returns>Xp bar in string format with colored string</returns>
        public static string CreateXPBar(Entity entity, int width = 30, ConsoleColor barColor = ColorConstants.XP_COLOR)
        {
            return CreateProgressBar(entity.XP, entity.GetLevelXP(), width, barColor);
        }

        /// <summary>
        /// Slowly write out <paramref name="text"/> to the console window
        /// </summary>
        /// <param name="text">Text to type</param>
        public static void Type(string text)
        {
            StringColorBuilder stringColorBuilder = new StringColorBuilder(text);
            foreach (List<StringColor> stringColors in stringColorBuilder.Split("\n"))
            {
                foreach (StringColor stringColor in stringColors)
                {
                    Console.ForegroundColor = stringColor.Color;
                    foreach (char letter in GenderUtils.ReplacePronouns(stringColor.Text))
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


    }
}
