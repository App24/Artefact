using Artefact.DialogSystem;
using Artefact.Misc;
using Artefact.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Artefact.Commands.Misc
{
    class CommandHandler
    {
        List<ICommand> commands = new List<ICommand>();

        const string RE_ARG_MATCHER_PATTERN = @"""[^""\\]*(?:\\.[^""\\]*)*""|'[^'\\]*(?:\\.[^'\\]*)*'|\S+";
        const string RE_QUOTE_STRIP_PATTERN = @"^""+|""+$|^'+|'+$";

        string[] noCommandResponses = new string[] {
            "You are not sure you can do that!",
            "How do you do that again?",
            "Impossible to do that!"
        };

        int fails = 0;

        int pageIndex = 0;

        const int MAX_COMMANDS_PER_PAGE = 5;

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
            commandHandler.AddCommand(new TalkCommand());
            commandHandler.AddCommand(new MoveCommand());
            commandHandler.AddCommand(new UseCommand());
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
            commands.Sort((c1, c2) =>
            {
                return c1.Name.CompareTo(c2.Name);
            });
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
                if (pageIndex + 1 < Math.Ceiling(visibleCommands.Count / (float)MAX_COMMANDS_PER_PAGE)) texts.Add("Next");
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
                        args = matches.Map(match => Regex.Replace(match.Value, RE_QUOTE_STRIP_PATTERN, ""));
                    }
                }
                else
                {
                    string sel = texts[selection];
                    if (sel == "Next")
                    {
                        pageIndex++;
                    }
                    else
                    {
                        pageIndex--;
                    }
                    return false;
                }
            }
            else
            {
                string text = Console.ReadLine();
                if (string.IsNullOrEmpty(text)) return false;
                MatchCollection matches = reg.Matches(text);
                List<string> commandData = matches.Map(match => Regex.Replace(match.Value, RE_QUOTE_STRIP_PATTERN, ""));
                if (commandData.Count <= 0) return false;
                string commandName = commandData[0].ToLower();
                args = commandData.GetRange(1, commandData.Count - 1);

                command = commands.Find(c => c.Name.ToLower().Equals(commandName) || c.Aliases.Map((x) => x.ToLower()).Contains(commandName));
            }

            if (command != null)
            {
                if (command.HasArguments && args.Count <= 0)
                {
                    Console.WriteLine($"{command.NoArgsResponse}");
                    return false;
                }
                fails = 0;
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
                    return false;
                }
            }
            else
            {
                Random random = new Random();
                Utils.WriteColor($"[{ColorConstants.BAD_COLOR}]{noCommandResponses[random.Next(noCommandResponses.Length)]}");
                fails++;
                if (random.Next(101) <= fails)
                {
                    Dialog.Speak(Character.Clippy, $"You seem to be struggeling, you can type [{ColorConstants.COMMAND_COLOR}]HELP[/] to get a list of commands, or switch to simple mode in [{ColorConstants.COMMAND_COLOR}]SETTINGS[/]!");
                }
            }
            return false;
        }
    }
}
