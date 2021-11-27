using Artefact.DialogSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;

namespace Artefact.Commands
{
    internal class TalkCommand : ICommand
    {
        public string Name => "talk";
        public string[] Aliases => new string[] { "speak" };

        public bool HasArguments => true;

        public string NoArgsResponse => "<Name>";

        public string Description => "Talk to NPC";

        public void OnRun(List<string> args)
        {
            string characterName = args.Join(" ");

            if (!Enum.TryParse(characterName, true, out Character character))
            {
                if (characterName.ToLower() == GameSettings.PlayerName.ToLower())
                {
                    character = Character.Player;
                }
                else
                {
                    Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]That character is not near the vicinity");
                    return;
                }
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
                        Dialog.Speak(Character.Clippy, $"*mumbling* I will defeat the [{ColorConstants.USER_COLOR}]us[/]-");
                        Utils.ClearPreviousLines();
                        Dialog.Speak(Character.Clippy, "How may I help you?");
                        int selection = Utils.GetSelection("How did I get here?", "How did you get here?", "Is there anyway to leave this place?", "Finish Talking");

                        switch (selection)
                        {
                            case 0:
                                {
                                    Dialog.Speak(Character.Clippy, $"I do not know.\nI just found you here.\nMaybe the [{ColorConstants.USER_COLOR}]user[/] deleted you too? ^-^");
                                    OnRun(args);
                                }
                                break;
                            case 1:
                                {
                                    Dialog.Speak(Character.Clippy, $"I am not exactly sure, one day I felt myself lighter and then appeared right outside my home, being unable to enter it.\nI think the [{ColorConstants.USER_COLOR}]user[/] deleted me :'(");
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
                    }
                    break;
            }
        }
    }
}
