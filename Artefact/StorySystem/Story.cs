using Artefact.DialogSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.StorySystem
{
    static class Story
    {
        public static int Step { get; set; }
        public const int CPU_STEP = 99;
        public const int RAM_STEP = 100;
        public const int EMPTY_STEP = 1000;

        public static void NextStep()
        {
            Step++;
        }

        public static void DoStep()
        {

            //Prevent getting softlock due to save editing shenanigans
            if (Step > 4)
            {
                if (!Map.Player.Inventory.HasItem(new ItemData(Item.MapItem)))
                {
                    Dialog.Speak(Character.Clippy, "Where is your map? Here, you can have another!");
                    Map.Player.Inventory.AddItem(new ItemData(Item.MapItem), announce: true);
                }
            }
            switch (Step)
            {
                case 0:
                    {
                        Utils.Type("You wake up in a strange place");
                        Utils.Type("You look around and find yourself surrounded by short black building and tall black skyscrappers");
                        Utils.Type("You hear metal scraping behind you, you turn around and see a paperclip with eyes");

                        Dialog.Speak(Character.Clippy, "It looks like you're writi-lost. Would you like help?");

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
                                    Dialog.Speak(Character.Clippy, "You are inside the [darkred]user[/]'s computer.");
                                    Dialog.Speak(Character.Clippy, "Are you another software the user deleted? ^-^");
                                }
                                break;
                            case 1:
                                {
                                    Dialog.Speak(Character.Clippy, "I am [cyan]Clippy");
                                    GameSettings.KnowsClippy = true;
                                    Dialog.Speak(Character.Clippy, "I am, or was, a helpful piece of software that helped the [darkred]user[/] whenever them needed!");
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
                            Dialog.Speak(Character.Clippy, "By the way, my name is [cyan]Clippy");
                            GameSettings.KnowsClippy = true;
                        }

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
                            if(!Utils.GetCharacterConfirmation(Dialog.GetCharacterVoiceLine(Character.Clippy, $"So your name is [cyan]{name}[/]"))){
                                name = null;
                                Dialog.Speak(Character.Clippy, "What is your name then?");
                            }
                        }
#else
                        name = "Debug";
#endif

                        GameSettings.PlayerName = name;
                        Dialog.Speak(Character.Clippy, $"Nice to meet you, [cyan]{name}[/]");

                        Map.SpawnPlayer();

                        NextStep();
                    }
                    break;
                case 4:
                    {
                        Dialog.Speak(Character.Clippy, "If you need help or want to ask further questions, I am here, just type [darkmagenta]HELP[/]");
                        Dialog.Speak(Character.Clippy, "Oh, before I forget, I have a map here, I do not need anymore, you can have!");
                        Map.Player.Inventory.AddItem(new ItemData(Item.MapItem, 1), true);
                        Dialog.Speak(Character.Clippy, "And here is a recipe book, do not lose it, you will lose all your knowledge on recipes!");
                        Map.Player.Inventory.AddItem(new ItemData(Item.RecipeBookItem), true);
                        Dialog.Speak(Character.Clippy, "Why don't you type [darkmagenta]MAP[/] to find out where you are?");
                        NextStep();
                    }
                    break;
                case 5:
                    {
                        GameSettings.EnableCommands = true;
                    }
                    break;
                case CPU_STEP:
                    {
                        Dialog.Speak(Character.Clippy, "This is the [darkcyan]CPU[/]!");
                        Dialog.Speak(Character.Clippy, "This is where everything the [darkred]user[/] does is processed!");
                        Dialog.Speak(Character.Clippy, "It is a marvel sight!");
                        Dialog.Speak(Character.Clippy, "But it seems to be turned off right now, which is odd, since the [darkred]user[/] always has their computer turned on!");
                        GameSettings.CPUVisited = true;
                        Step = EMPTY_STEP;
                    }
                    break;
                case RAM_STEP:
                    {
                        Dialog.Speak(Character.Clippy, "Here is the [darkcyan]RAM[/]");
                        Dialog.Speak(Character.Clippy, "That is odd, there is no data in here, I guess that means that the [darkred]user[/] does not have the computer turned on!");
                        GameSettings.RAMVisited = true;
                        Step = EMPTY_STEP;
                    }
                    break;
            }
        }
    }
}
