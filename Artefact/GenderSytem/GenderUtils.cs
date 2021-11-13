using Artefact.Settings;
using System.Collections.Generic;

namespace Artefact.GenderSystem
{
    internal static class GenderUtils
    {
        private static Dictionary<Gender, GenderPronouns> genderPronouns = new Dictionary<Gender, GenderPronouns>()
        {
            { Gender.Male, new GenderPronouns("he", "him", "his", "his", "himself") },
            { Gender.Female, new GenderPronouns("she", "her", "her", "hers", "herself") },
            { Gender.Other, new GenderPronouns("they", "them", "their", "theirs", "themselves") }
        };

        public static string ReplacePronouns(string text)
        {
            if (GameSettings.Instance != null)
            {
                if (!genderPronouns.TryGetValue(GameSettings.PlayerGender, out GenderPronouns pronouns))
                {
                    pronouns = GameSettings.Pronouns;
                }
                foreach (GenderPronounData pronoun in pronouns)
                {
                    text = text.Replace($"[{pronoun.PronounType}]", pronoun.Text);
                }
            }
            return text;
        }
    }

    internal enum PronounType
    {
        Nominative,
        Objective,
        Possessive_Determiner,
        Possessive_Pronoun,
        Reflexive
    }

    internal enum Gender
    {
        Male,
        Female,
        Other,
        Custom
    }
}
