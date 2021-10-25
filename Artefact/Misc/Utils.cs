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
        public static string DLLHash
        {
            get
            {
                if (string.IsNullOrEmpty(dllHash))
                {
                    FileStream stream = new FileStream(Assembly.GetExecutingAssembly().Location, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                    MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

                    md5.ComputeHash(stream);

                    stream.Close();
                    dllHash = string.Join("", md5.Hash);
                }

                return dllHash;
            }
        }

        static string dllHash;

        /// <summary>
        /// Get the user to select an option from an array, they are also able to type the name of the item to select it
        /// </summary>
        /// <param name="options">The options available to choose from</param>
        /// <returns>Returns a number between 0 and length of <paramref name="options"/></returns>
        public static int GetSelection(params string[] options)
        {
            int index = -1;
            List<string> optionsList = new List<string>(options);
            StringColorBuilder stringColorBuilder = new StringColorBuilder(options.Map((option, i) => $"{i + 1}. {option}").Join("\n"));
            stringColorBuilder.WriteLine();
            while (index < 0 || index >= options.Length)
            {
                string selection = Console.ReadLine();
                if(!int.TryParse(selection, out index))
                {
                    index = optionsList.FindIndex(o => o.ToLower() == selection.ToLower())+1;
                }
                index -= 1;
                if (index < 0 || index >= options.Length)
                {
                    Console.WriteLine("Please enter a valid choice!");
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
                }else if (ValidNo.Contains(response))
                {
                    return false;
                }
            }
        }

        public static bool GetCharacterConfirmation(string inputText)
        {
            Type(inputText);
            string response = Console.ReadLine().ToLower();

            return ValidYes.Contains(response);
        }

        public static void WriteCenter(string text)
        {
            string[] lines = text.Split("\n");
            foreach (string line in lines)
            {
                StringColorBuilder stringColorBuilder = new StringColorBuilder(line);
                Console.SetCursorPosition((Console.WindowWidth - stringColorBuilder.Message.Length) / 2, Console.CursorTop);
                foreach (StringColor stringColor in stringColorBuilder.GetStringColors())
                {
                    Console.ForegroundColor = stringColor.Color;
                    Console.Write(stringColor.Text);
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        public static string CreateProgressBar(float percentage, int width = 20)
        {
            string text = "[";
            for (int i = 0; i < width; i++)
            {
                if ((i + 1) / (float)width <= percentage)
                    text += "#";
                else
                    text += "-";
            }
            text += "]";
            return text;
        }

        public static string CreateHealthBar(float percentage, int width = 20)
        {
            string text = "";
            for (int i = 0; i < width; i++)
            {
                if ((i + 1) / (float)width <= percentage)
                    text += "#";
                else
                    text += "-";
            }
            text = text.Map(x => { if (x == '#') return $"[green]{x}[/]"; return $"[red]{x}[/]"; }).Join("");
            return $"[{text}]";
        }

        public static void WriteColor(string message)
        {
            StringColorBuilder stringColorBuilder = new StringColorBuilder(message);

            foreach(StringColor stringColor in stringColorBuilder.GetStringColors())
            {
                Console.ForegroundColor = stringColor.Color;
                Console.Write(stringColor.Text);
            }

            Console.WriteLine();

            Console.ResetColor();
        }

        public static void Type(string text)
        {
            string[] lines = text.Split("\n");
            foreach (string line in lines)
            {
                StringColorBuilder stringColorBuilder = new StringColorBuilder(line);
                foreach (StringColor stringColor in stringColorBuilder.GetStringColors())
                {
                    Console.ForegroundColor = stringColor.Color;
                    foreach (char letter in stringColor.Text)
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
