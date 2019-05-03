using System.IO;
using System.Collections.Generic;

namespace PokemonSaves
{
    public class GameSave : IGameSave, IBinaryParsable
    {
        private long _startOffset;
        HashSet<SaveDataSection> _saveDataSections;
        public long StartOffset { get => _startOffset; set => _startOffset = value; }
        public HashSet<SaveDataSection> SaveDataSections { get => _saveDataSections; set => _saveDataSections = value; }

        public GameSave()
        {
            // There are always 14 sections in total but their order does not matter,
            // thus a HashSet fits perfectly here.
            SaveDataSections = new HashSet<SaveDataSection>(14);
        }

        public int GetLastSaveIndex()
        {
            // Since SaveDataSections is a HashSet and we want any SaveIndex (since they're all the same),
            // loop the sections and simply return the SaveIndex of the very first section found.
            foreach (var SaveDataSection in SaveDataSections)
            {
                return SaveDataSection.SaveIndex;
            }

            return 0;
        }

        public object GetTrainerInfo(GameIDs gameID)
        {
            foreach (var saveDataSection in SaveDataSections)
            {
                if (saveDataSection.SectionID == DataSectionTypes.TrainerInfo)
                {
                    switch (gameID)
                    {
                        case GameIDs.FireRedLeafGreen:
                        case GameIDs.LiquidCrystal:
                            return (TrainerInfoFRLG)saveDataSection.Data;
                        case GameIDs.RubySapphire:
                            return (TrainerInfoRS)saveDataSection.Data;
                        case GameIDs.Emerald:
                            return (TrainerInfoE)saveDataSection.Data;
                    }
                }
            }
            return null;
        }

        public object GetTeamAndItems(GameIDs gameID)
        {
            foreach (var saveDataSection in SaveDataSections)
            {
                if (saveDataSection.SectionID == DataSectionTypes.TeamAndItems)
                {
                    switch (gameID)
                    {
                        case GameIDs.FireRedLeafGreen:
                        case GameIDs.LiquidCrystal:
                            return (TeamAndItemsFRLG)saveDataSection.Data;
                        case GameIDs.RubySapphire:
                            return (TeamAndItemsRS)saveDataSection.Data;
                        case GameIDs.Emerald:
                            return (TeamAndItemsE)saveDataSection.Data;
                    }
                }
            }
            return null;
        }

        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            StartOffset = binaryReader.BaseStream.Position;
            SaveDataSections.Clear();
            // 14 SaveDataSections in total.
            for (int i = 0; i < 14; i++)
            {
                var saveDataSection = new SaveDataSection();
                saveDataSection.ReadFromBinary(binaryReader, gameID);
                SaveDataSections.Add(saveDataSection);
            }
        }

        // Write functions:

        public void WriteToBinary(BinaryWriter binaryWriter, GameIDs gameID)
        {
            var relativeOffset = 0;
            foreach (var saveDataSection in SaveDataSections)
            {
                binaryWriter.BaseStream.Seek(StartOffset + relativeOffset, SeekOrigin.Begin);
                saveDataSection.WriteToBinary(binaryWriter, gameID);
                relativeOffset += 4096;
            }
        }
    }

    interface IGameSave
    {
        HashSet<SaveDataSection> SaveDataSections { get; set; }
        int GetLastSaveIndex();
    }
}