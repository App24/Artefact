using Artefact.MapSystem;
using Artefact.Misc;
using Artefact.Settings;
using Artefact.StorySystem;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Artefact.Saving
{
    static class SaveSystem
    {
        const string SETTINGS_FILE = "settings.dat";
        const string SAVE_FILE = "save.dat";
        public const string CHECKPOINT_FILE = "checkpoint.dat";
        const string SAVE_FOLDER = "saves";
        const int SAVE_SLOTS = 3;

        public static bool HasAnySaveGames => Directory.Exists(SAVE_FOLDER);

        static void SaveClass<T>(string fileName, T value)
        {
            string directory = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);
            FileStream stream = File.Create(fileName);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, value);
            stream.Close();
        }

        static LoadDetails<T> LoadClass<T>(string fileName) where T : class
        {
            if (!File.Exists(fileName))
            {
                return new LoadDetails<T>(LoadResult.NoFile, null);
            }

            FileStream stream = File.OpenRead(fileName);

            if (stream.Length <= 0)
            {
                stream.Close();
                return new LoadDetails<T>(LoadResult.InvalidFile, null);
            }

            T value;

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                value = (T)formatter.Deserialize(stream);
            }
            catch
            {
                stream.Close();
                return new LoadDetails<T>(LoadResult.InvalidFile, null);
            }
            stream.Close();

            return new LoadDetails<T>(LoadResult.Success, value);
        }

        public static void SaveSettings()
        {
            SaveClass(SETTINGS_FILE, GlobalSettings.Instance);
        }

        public static void LoadSettings()
        {
            LoadDetails<GlobalSettings> loadDetails = LoadClass<GlobalSettings>(SETTINGS_FILE);
            if (loadDetails.LoadResult == LoadResult.Success)
                GlobalSettings.Instance = loadDetails.Saveable;
            else
                new GlobalSettings();
        }

        public static void SaveGame(string fileName = SAVE_FILE)
        {
            long currentTime = DateTime.UtcNow.Ticks;
            GameSettings.GameTime += currentTime - GameSettings.SessionStartDate;
            GameSettings.SessionStartDate = currentTime;
            SaveClass(Path.Combine(SAVE_FOLDER, GameSettings.SaveSlot.ToString(), fileName), new Save());
        }

        public static LoadResult LoadGame(string fileName = SAVE_FILE, int slot = 0)
        {
            if (slot <= 0)
                slot = GameSettings.SaveSlot;
            LoadDetails<Save> loadDetails = LoadClass<Save>(Path.Combine(SAVE_FOLDER, slot.ToString(), fileName));
            if (loadDetails.LoadResult == LoadResult.Success)
            {
                Utils.WriteColor($"[{ColorConstants.GOOD_COLOR}]Save loaded successfully!");
                Save save = loadDetails.Saveable;
                GameSettings.Instance = save.GameSettings;
                Story.Step = save.StoryStep;
                Map.Instance = save.Map;
                GlobalSettings.JustLoaded = true;
                GameSettings.SessionStartDate = DateTime.UtcNow.Ticks;
            }
            else
            {
                Utils.WriteColor($"[{ColorConstants.ERROR_COLOR}]There was a problem loading the save game!");
                switch (loadDetails.LoadResult)
                {
                    case LoadResult.InvalidFile:
                        {
                            Utils.WriteColor($"[{ColorConstants.ERROR_COLOR}]Save file is an invalid file!");
                        }
                        break;
                    case LoadResult.NoFile:
                        {
                            Utils.WriteColor($"[{ColorConstants.ERROR_COLOR}]There is no save file!");
                        }
                        break;
                    case LoadResult.InvalidVersion:
                        {
                            Utils.WriteColor($"[{ColorConstants.ERROR_COLOR}]The save version is not a match!");
                        }
                        break;
                }
            }
            return loadDetails.LoadResult;
        }

        public static void NewGame()
        {
            new GameSettings();
            GameSettings.GameStartDate = DateTime.UtcNow.Ticks;
            GameSettings.SessionStartDate = DateTime.UtcNow.Ticks;
            Story.Step = 0;
            new Map();
        }

        public static List<string> GetSaveGameNames()
        {
            List<string> saves = new List<string>();
            for (int i = 1; i < SAVE_SLOTS + 1; i++)
            {
                string saveName = "";
                if (!Directory.Exists(Path.Combine(SAVE_FOLDER, saveName)))
                {
                    saveName = $"[{ColorConstants.BAD_COLOR}]Empty[/]";
                }
                else
                {
                    LoadDetails<Save> loadDetails = LoadClass<Save>(Path.Combine(SAVE_FOLDER, i.ToString(), SAVE_FILE));
                    if (loadDetails.LoadResult != LoadResult.Success)
                        saveName = $"[{ColorConstants.BAD_COLOR}]Empty[/]";
                    else
                    {
                        saveName = $"[{ColorConstants.CHARACTER_COLOR}]{loadDetails.Saveable.GameSettings.playerName}[/]";
                        DateTime gameTime = new DateTime(loadDetails.Saveable.GameSettings.gameTime);
                        saveName += $" - [{ColorConstants.GOOD_COLOR}]{gameTime.ToString("H:mm:ss")}[/]";
                    }
                }
                saves.Add(saveName);
            }

            return saves;
        }

        public static bool HasSavegame(int slot)
        {
            return LoadClass<Save>(Path.Combine(SAVE_FOLDER, slot.ToString(), SAVE_FILE)).LoadResult == LoadResult.Success;
        }
    }

    class LoadDetails<T>
    {
        public LoadResult LoadResult { get; }
        public T Saveable { get; }

        public LoadDetails(LoadResult loadResult, T saveable)
        {
            LoadResult = loadResult;
            Saveable = saveable;
        }
    }

    enum LoadResult
    {
        Success,
        InvalidVersion,
        NoFile,
        InvalidFile
    }
}
