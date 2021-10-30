using Artefact.DialogSystem;
using Artefact.FightSystem;
using Artefact.InventorySystem;
using Artefact.Items;
using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
                                    Dialog.Speak(Character.Clippy, $"You are inside the [{ColorConstants.USER_COLOR}]user[/]'s computer.");
                                    Dialog.Speak(Character.Clippy, "Are you another software the user deleted? ^-^");
                                }
                                break;
                            case 1:
                                {
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

                        Utils.WriteColor(@"[green]
                                                  
                         ...                      
                   .,,*//(((((//*.                
                 ..,/(         (#(/*              
               ..,/*             /#//             
              .,,/,               %(/.            
         ##@@@@@&@@@@             %(/,            
      %%,    ..*/                ,(//             
          ...,,/.                @@@@@@@@&,       
                 .*.            .,.*     .&&(     
       .###%&@@%   .*               ....    .&.   
  ,    @%%%&&@@@@. .*/            ..     .*   .   
   ,.   (@@@@@@@ ..**.         #%#%&&@@(  .,*     
    .**,.,.....,,*/,     .,   .@@@@@@@@@. .,*.    
          .*##%.           ,,.   (%&&%* ..**,     
           ,(%%              .*,,,,.,,***/,       
           ,###.               %&&%               
           ,###. (%%*          %%%#     .##,      
           ,##(. (##,          %%#/    *(#%       
           ,#((. /#(.          %%(,   *(#%        
           .#(/* /((.          ##(,   //#/        
            %(/* *((,          #%(.  ,(/#.        
            #(*/ *(/*          (%(.  *//#         
            /#** .(/*          *#(*  .(/(.        
            ,%/,. ((*.         ,(/*   (//,        
             %/., ,%/,         **/*   (///        
             /#*,  ,%(,,      ,,,/    ((//        
             .&/..   /%/,..,.*/(*  .  (#(/        
              (#,,      .,*/,.        /#(/        
               &(,,                   /#(/        
                %/..                  *((*        
                .%*.,                .*//.        
                  %(,,              .,*/.         
                   *%(,,,        ,,.,*,           
                     .%%#/,,,,,,**//.             
                                                  
");
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
                            if(!Utils.GetCharacterConfirmation(Dialog.GetCharacterVoiceLine(Character.Clippy, $"So your name is [{Constants.CHARACTER_COLOR}]{name}[/]?"))){
                                name = null;
                                Dialog.Speak(Character.Clippy, "What is your name then?");
                            }
                        }
#else
                        name = "Debug";
#endif

                        GameSettings.PlayerName = name;
                        Dialog.Speak(Character.Clippy, $"Nice to meet you, [{ColorConstants.CHARACTER_COLOR}]{name}[/]");

                        Map.SpawnPlayer();

                        NextStep();
                    }
                    break;
                case 4:
                    {
                        Dialog.Speak(Character.Clippy, $"If you need help or want to ask further questions, I am here, just type [{ColorConstants.COMMAND_COLOR}]HELP[/]");
                        Dialog.Speak(Character.Clippy, "Oh, before I forget, I have a map here, I do not need anymore, you can have!");
                        Map.Player.Inventory.AddItem(new ItemData(Item.MapItem, 1), true);
                        Dialog.Speak(Character.Clippy, "And here is a recipe book, do not lose it, you will lose all your knowledge on recipes!");
                        Map.Player.Inventory.AddItem(new ItemData(Item.RecipeBookItem), true);
                        Dialog.Speak(Character.Clippy, $"Why don't you type [{ColorConstants.COMMAND_COLOR}]MAP[/] to find out where you are?");
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
                        Dialog.Speak(Character.Clippy, $"This is the [{ColorConstants.LOCATION_COLOR}]CPU[/]!");
                        Dialog.Speak(Character.Clippy, $"This is where everything the [{ColorConstants.USER_COLOR}]user[/] does is processed!");
                        Dialog.Speak(Character.Clippy, "It is a marvel sight!");
                        Dialog.Speak(Character.Clippy, $"But it seems to be turned off right now, which is odd, since the [{ColorConstants.USER_COLOR}]user[/] always has their computer turned on!");
                        Dialog.Speak(Character.Clippy, ".........................");
                        Dialog.Speak(Character.Clippy, "Wha-what is that?!?");
                        Dialog.Speak(Character.Clippy, "RUN, IT'S A TROJAN!!!");
#if !BYPASS
                        Thread.Sleep((int)GlobalSettings.TextSpeed * 15);
#endif
                        GameSettings.EnableCommands = false;
                        Fight.StartFight(Map.Player.Location, Entities.EnemyType.Trojan, 1);
                        GameSettings.CPUVisited = true;
                        //Step = EMPTY_STEP;
                    }
                    break;
                case RAM_STEP:
                    {
                        Dialog.Speak(Character.Clippy, $"Here is the [{ColorConstants.LOCATION_COLOR}]RAM[/]");
                        Dialog.Speak(Character.Clippy, $"That is odd, there is no data in here, I guess that means that the [{ColorConstants.USER_COLOR}]user[/] does not have the computer turned on!");
                        GameSettings.RAMVisited = true;
                        Step = EMPTY_STEP;
                    }
                    break;
            }
        }
    }
}
