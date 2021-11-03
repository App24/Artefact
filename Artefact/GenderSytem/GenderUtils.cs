using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.GenderSystem
{
    static class GenderUtils
    {

        static Dictionary<Gender, GenderPronouns> genderPronouns = new Dictionary<Gender, GenderPronouns>()
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

    enum PronounType
    {
        Nominative,
        Objective,
        Possessive_Determiner,
        Possessive_Pronoun,
        Reflexive
    }

    enum Gender
    {
        Male,
        Female,
        Other,
        Custom
    }
}
