using Artefact.DialogSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Artefact.Commands
{
    class TalkCommand : ICommand
    {
        public string Name => "talk";
        public string[] Aliases => new string[] { "speak" };

        public bool HasArguments => true;

        public string NoArgsResponse => "<Name>";

        public void OnRun(List<string> args)
        {
            string characterName = args.Join(" ");

            Character character;

            if(!Enum.TryParse(typeof(Character), characterName, true, out object c)){
                if (characterName.ToLower() == GameSettings.PlayerName.ToLower())
                    character = Character.Player;
                else
                {
                    Console.WriteLine("That character is not near the vicinity");
                    return;
                }
            }
            else
            {
                character = (Character)c;
            }

            switch (character)
            {
                case Character.Player:
                    {
                        Utils.Type("You ponder on how you will leave this place!");
                    }
                    break;
                case Character.Clippy:
                    {
                        int selection = Utils.GetSelection("How did I get here?", "How did you get here?", "Is there anyway to leave this place?", "Finish Talking");

                        switch (selection)
                        {
                            case 0:
                                {
                                    Dialog.Speak(Character.Clippy, "I do not know.\nI just found you here.\nMaybe the [darkred]user[/] deleted you too? ^-^");
                                    OnRun(args);
                                }break;
                            case 1:
                                {
                                    Dialog.Speak(Character.Clippy, "I am not exactly sure, one day I felt myself lighter and then appeared right outside my home, being unable to enter it.\nI think the [darkred]user[/] deleted me :'(");
                                    OnRun(args);
                                }
                                break;
                            case 2:
                                {
                                    Dialog.Speak(Character.Clippy, "I have been trying to for years, I have been failing ever since. I do not think it is possible");
                                    OnRun(args);
                                }
                                break;
                        }
                    }break;
            }
        }
    }
}
