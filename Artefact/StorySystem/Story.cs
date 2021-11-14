using Artefact.DialogSystem;
using Artefact.GenderSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;

namespace Artefact.StorySystem
{
    internal static class Story
    {
        public static int Step { get; set; }

        public static void NextStep()
        {
            Step++;
        }

        public static void DoStep()
        {
            //Prevent getting softlock due to save editing shenanigans/errors
            if (Step > 4)
            {
                if (!Map.Player.Inventory.HasItem(new ItemData(Item.MapItem)))
                {
                    Dialog.Speak(Character.Clippy, $"Where is your [{ColorConstants.ITEM_COLOR}]{Item.MapItem.Name}[/]? Here, you can have another!");
                    Map.Player.Inventory.AddItem(new ItemData(Item.MapItem));
                }
                if (!Map.Player.Inventory.HasItem(new ItemData(Item.RecipeBookItem)))
                {
                    Dialog.Speak(Character.Clippy, $"Where is your [{ColorConstants.ITEM_COLOR}]{Item.RecipeBookItem.Name}[/]? Here, you can have another!");
                    Map.Player.Inventory.AddItem(new ItemData(Item.RecipeBookItem));
                }
            }
            switch (Step)
            {
                case 0:
                    {
                        Utils.Type("You wake up in a strange place");
                        Utils.Type("You look around and find yourself surrounded by short black building and tall black skyscrappers");
                        Utils.Type("You hear metal scraping behind you, you turn around and see a paperclip with eyes");

                        Dialog.Speak(Character.Clippy, "It looks like you're writing[backspace=7]lost. Would you like help?");

                        NextStep();
#if BYPASS
                        NextStep();
#endif
                    }
                    break;
                case 1:
                    {
                        int selection = Utils.GetSelection("Where am I?", "Who are you?", "Finish talking");

                        switch (selection)
                        {
                            case 0:
                                {
                                    Dialog.Speak(Character.Clippy, $"You are inside the [{ColorConstants.USER_COLOR}]user[/]'s computer.");
                                    Dialog.Speak(Character.Clippy, $"Are you another software the [{ColorConstants.USER_COLOR}]user[/] deleted? ^-^");
                                }
                                break;
                            case 1:
                                {
                                    Dialog.Speak(Character.Clippy, $"I am [{ColorConstants.CHARACTER_COLOR}]GO");
                                    Utils.ClearPreviousLines();
                                    Dialog.Speak(Character.Clippy, $"I am [{ColorConstants.CHARACTER_COLOR}]Clippy");
                                    GameSettings.KnowsClippy = true;
                                    Dialog.Speak(Character.Clippy, $"I am, or was, a helpful piece of software that helped the [{ColorConstants.USER_COLOR}]user[/] whenever them needed!");
                                }
                                break;
                            case 2:
                                {
                                    NextStep();
                                }
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        if (!GameSettings.KnowsClippy)
                        {
                            Dialog.Speak(Character.Clippy, $"By the way, my name is [{ColorConstants.CHARACTER_COLOR}]Clippy");
                            GameSettings.KnowsClippy = true;
                        }

                        Dialog.Speak(Character.Clippy, "Now talk about yourself!");
                        Dialog.Speak(Character.Clippy, "May I ask what your name is?");
                        NextStep();
                    }
                    break;
                case 3:
                    {
                        string name = null;
#if !BYPASS
                        while (string.IsNullOrEmpty(name))
                        {
                            name = Console.ReadLine();
                            if (!Utils.GetCharacterConfirmation(Character.Clippy, $"So your name is [{ColorConstants.CHARACTER_COLOR}]{name}[/]?"))
                            {
                                name = null;
                                Dialog.Speak(Character.Clippy, "What is your name then?");
                            }
                        }
#else
                        name = "Debug";
#endif
                        Dialog.Speak(Character.Clippy, "What are your gender pronouns?");
                        Gender gender = Gender.Other;
#if !BYPASS
                        while (true)
                        {
                            gender = (Gender)Utils.GetSelection(Enum.GetNames(typeof(Gender)));
                            if (Utils.GetCharacterConfirmation(Character.Clippy, $"So your gender pronouns are {gender}?"))
                            {
                                break;
                            }
                        }

                        if (gender == Gender.Custom)
                        {
                            Dialog.Speak(Character.Clippy, "I will now ask you questions to set up your pronouns properly");
                            CreateGender();
                        }
#endif

                        GameSettings.PlayerName = name;
                        GameSettings.PlayerGender = gender;
                        Dialog.Speak(Character.Clippy, $"Nice to meet you, [{ColorConstants.CHARACTER_COLOR}]{name}[/]");

                        Map.SpawnPlayer();

                        NextStep();
                    }
                    break;
                case 4:
                    {
                        Dialog.Speak(Character.Clippy, $"If you need help or want to ask further questions, I am here, just type [{ColorConstants.COMMAND_COLOR}]HELP[/]");
                        Dialog.Speak(Character.Clippy, "Oh, before I forget, I have a map here, I do not need anymore, you can have!");
                        Map.Player.Inventory.AddItem(new ItemData(Item.MapItem));
                        Dialog.Speak(Character.Clippy, "And here is a recipe book, do not lose it, you will lose all your knowledge on recipes!");
                        Map.Player.Inventory.AddItem(new ItemData(Item.RecipeBookItem));
                        Dialog.Speak(Character.Clippy, $"Why don't you type [{ColorConstants.COMMAND_COLOR}]MAP[/] to find out where you are?");
                        NextStep();
                    }
                    break;
                case 5:
                    {
                        GameSettings.EnableCommands = true;
                    }
                    break;
            }
        }

        private static void CreateGender()
        {
            string nominative = GetGenderPronoun("____ defeated an armada of paper clips. (Nominative Case)");
            string objective = GetGenderPronoun("I defeated ____. (Objective Case)");
            string possessiveDeterminer = GetGenderPronoun("____ inventory is full. (Possessive Determiner Case)");
            string possessivePronoun = GetGenderPronoun("That health is ____. (Possessive Pronoun Case)");
            string reflexive = GetGenderPronoun($"{nominative} helped ____. (Reflexive Case)");
            GameSettings.Pronouns = new GenderPronouns(nominative, objective, possessiveDeterminer, possessivePronoun, reflexive);
        }

        private static string GetGenderPronoun(string example)
        {
            string pronoun = null;
            while (string.IsNullOrEmpty(pronoun))
            {
                Dialog.Speak(Character.Clippy, "Complete the following example with your relevant pronoun:");
                Utils.WriteColor(example);
                pronoun = Console.ReadLine().Trim();
                Dialog.Speak(Character.Clippy, "So the example would be:");
                if (!Utils.GetConfirmation(example.Replace("____", pronoun)))
                {
                    Dialog.Speak(Character.Clippy, "Please type your valid pronoun!");
                    pronoun = null;
                }
            }
            return pronoun;
        }
    }
}
