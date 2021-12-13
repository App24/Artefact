using Artefact.DialogSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Artefact.Commands.Misc
{
    internal class CommandHandler
    {
        private List<ICommand> commands = new List<ICommand>();
        // Used to separate what the user enters into valid parameters
        // Example: craft "ram chip" 6, will turn into
        // craft
        // ram chip
        // 6
        private const string RE_ARG_MATCHER_PATTERN = @"""[^""\\]*(?:\\.[^""\\]*)*""|'[^'\\]*(?:\\.[^'\\]*)*'|\S+";
        private const string RE_QUOTE_STRIP_PATTERN = @"^""+|""+$|^'+|'+$";
        private string[] noCommandResponses = new string[] {
            "You are not sure you can do that!",
            "How do you do that again?",
            "Walking into a brick wall is more successful!",
            "Never gonna give you up",
            "You accidentally cured cancer, congrats! But that isn't a command",
            "That command activated all the nuclear ICBMs on the planet, thankfully people caught on and deactivated them, try again!",
            "You typing that non-existing command caused an earthquake is Japan",
            "Never gonna let you down",
            "Doc Brown came from the future to tell you that is not a command!",
            "Who you gonna call? Ghostbusters!",
            "Casper the Friendly Ghost isn't very friendly after you put that invalid command!",
            "What is love?",
            "What is an invalid command but a command the developer did not implement",
            "To be or not to be, that is the question",
            "That which we call a rose by any other name would smell as sweet",
            "It took Phil Connors 10 years to break out of the time loop, you can take your time",
            "Never gonna run around and desert you",
            "Much wrong! So impress!",
            "All your base are belong to us.",
            "I don't always type a correct command... but when I do, I do a good damn job",
            "I don't know, therefore aliens",
            "One does not simply input a correct command",
            "Oh baby, don't hurt me. Don't hurt me. No more",
            "I am once again asking you for a correct command",
            "Charlie bit my finger",
            "What colour is the dress? [black][b=white]black[/][/] and [blue]blue[/] or [white]white[/] and [yellow]gold"
        };
        private int pageIndex = 0;
        private const int MAX_COMMANDS_PER_PAGE = 5;

        public static CommandHandler Instance { get; set; }

        public static void AddDefaultCommands(CommandHandler commandHandler)
        {
            commandHandler.AddCommand(new ExitCommand());
            commandHandler.AddCommand(new SettingsCommand());
            commandHandler.AddCommand(new MapCommand());
            commandHandler.AddCommand(new SaveCommand());
            commandHandler.AddCommand(new LoadCommand());
            commandHandler.AddCommand(new StatusCommand());
            commandHandler.AddCommand(new InventoryCommand());
            commandHandler.AddCommand(new CraftCommand());
            commandHandler.AddCommand(new EquipCommand());
            // commandHandler.AddCommand(new TalkCommand());
            commandHandler.AddCommand(new MoveCommand());
            commandHandler.AddCommand(new UseCommand());
            commandHandler.AddCommand(new RecipesCommand());
            commandHandler.AddCommand(new InteractCommand());
#if DEBUG
            commandHandler.AddCommand(new GiveCommand());
            commandHandler.AddCommand(new ForceFightCommand());
            commandHandler.AddCommand(new GiveXpCommand());
            commandHandler.AddCommand(new DieCommand());
#endif
            commandHandler.AddCommand(new HelpCommand(commandHandler.commands));
        }

        public void AddCommand(ICommand command)
        {
            commands.Add(command);
            commands.Sort((c1, c2) => c1.Name.CompareTo(c2.Name));
        }

        public List<ICommand> GetCommands()
        {
            return commands;
        }

        // Right click crashes it. what?????
        // OHHHHH, it just pastes your clipboard
        public bool OnCommand()
        {
            Regex reg = new Regex(RE_ARG_MATCHER_PATTERN);
            ICommand command = null;
            List<string> args = null;
            if (GlobalSettings.SimpleMode)
            {
                Console.WriteLine();
                List<ICommand> visibleCommands = commands;
                List<ICommand> newCommands = visibleCommands.GetRange(pageIndex * MAX_COMMANDS_PER_PAGE, Math.Min(MAX_COMMANDS_PER_PAGE, visibleCommands.Count - pageIndex * MAX_COMMANDS_PER_PAGE));

                List<string> texts = new List<string>();
                texts.AddRange(newCommands.Map((c) => $"[{ColorConstants.COMMAND_COLOR}]{c.Name}[/]"));
                if ((visibleCommands.Count - pageIndex * MAX_COMMANDS_PER_PAGE) > MAX_COMMANDS_PER_PAGE) texts.Add("Next");
                if (pageIndex > 0) texts.Add("Previous");

                int selection = Utils.GetSelection(texts.ToArray());

                if (selection < newCommands.Count)
                {
                    command = newCommands[selection];
                    if (command.HasArguments)
                    {
                        Utils.WriteColor($"Write arguments for the command {command.NoArgsResponse}");
                        string argsText = Console.ReadLine();
                        MatchCollection matches = reg.Matches(argsText);
                        args = matches.Map(match => Regex.Replace(match.Value, RE_QUOTE_STRIP_PATTERN, "")).ToList();
                    }
                }
                else
                {
                    int sel = selection - newCommands.Count;
                    switch (sel)
                    {
                        case 0:
                            {
                                pageIndex++;
                            }
                            break;
                        case 1:
                            {
                                pageIndex--;
                            }
                            break;
                    }
                    return false;
                }
            }
            else
            {
                string text = Console.ReadLine();
                if (string.IsNullOrEmpty(text)) return false;
                MatchCollection matches = reg.Matches(text);
                List<string> commandData = matches.Map(match => Regex.Replace(match.Value, RE_QUOTE_STRIP_PATTERN, "")).ToList();
                if (commandData.Count <= 0) return false;
                string commandName = commandData[0].ToLower();
                args = commandData.GetRange(1, commandData.Count - 1);

                command = commands.Find(c => c.Name.ToLower().Equals(commandName) || c.Aliases.Map(x => x.ToLower()).Contains(commandName));
            }

            if (command != null)
            {
                if (command.HasArguments && args.Count <= 0)
                {
                    Console.WriteLine($"{command.NoArgsResponse}");
                    return false;
                }
                try
                {
                    command.OnRun(args);
                    return true;
                }
                catch (CommandException e)
                {
                    if (!string.IsNullOrEmpty(e.Message))
                    {
                        Utils.WriteColor($"[{ColorConstants.ERROR_COLOR}]{e.Message}");
                    }
                }
            }
            else
            {
                Random random = new Random();
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]{noCommandResponses[random.Next(noCommandResponses.Length)]}");
                if (random.Next(20) <= 1)
                {
                    Dialog.Speak(Character.Clippy, $"You seem to be struggeling, you can type [{ColorConstants.COMMAND_COLOR}]HELP[/] to get a list of commands, or switch to simple mode in [{ColorConstants.COMMAND_COLOR}]SETTINGS[/]!", instant: true);
                }
            }
            return false;
        }
    }
}
