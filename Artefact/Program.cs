using Artefact.Commands.Misc;
using Artefact.Misc;
using Artefact.Saving;
using Artefact.Settings;
using Artefact.States;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Artefact
{
    class Program
    {
        /**
         * Disable resizing and maximise button
         * Credit: Patrik Bak
         * Link: https://social.msdn.microsoft.com/Forums/vstudio/en-US/1aa43c6c-71b9-42d4-aa00-60058a85f0eb/c-console-window-disable-resize?forum=csharpgeneral
         */

        const int MF_BYCOMMAND = 0x00000000;
        const int SC_MAXIMIZE = 0xF030;
        const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        /*
         * ASCII Art for text created in: https://patorjk.com/software/taag/
         * 
         */

        static void Main(string[] args)
        {
            IntPtr handle = GetConsoleWindow();
            IntPtr sysMenu = GetSystemMenu(handle, false);

            if (handle != IntPtr.Zero)
            {
                DeleteMenu(sysMenu, SC_MAXIMIZE, MF_BYCOMMAND);
                DeleteMenu(sysMenu, SC_SIZE, MF_BYCOMMAND);
            }

            Console.Title = "Artefact";

            CommandHandler commandHandler = new CommandHandler();
            CommandHandler.AddDefaultCommands(commandHandler);
            CommandHandler.Instance = commandHandler;

            SaveSystem.LoadSettings();

            StateMachine.AddState(new MenuState());

            while (GlobalSettings.Running)
            {
                StateMachine.ProcessStateChanges();

                if (!StateMachine.IsEmpty)
                {
                    StateMachine.ActiveState.Update();
                }
            }
        }
    }
}
