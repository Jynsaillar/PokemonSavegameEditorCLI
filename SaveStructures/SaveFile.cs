using System.IO;

namespace PokemonSaves
{
    public class SaveFile : ISaveFile, IBinaryParsable
    {
        private long _startOffset;
        GameSave _gameSaveA;
        GameSave _gameSaveB;
        public long StartOffset { get => _startOffset; set => _startOffset = value; }
        public GameSave GameSaveA { get => _gameSaveA; set => _gameSaveA = value; }
        public GameSave GameSaveB { get => _gameSaveB; set => _gameSaveB = value; }
        /// TODO: HallOfFame
        /// TODO: MysteryGiftEReader
        /// TODO: RecordedBattle

        public SaveFile()
        {
            GameSaveA = new GameSave();
            GameSaveB = new GameSave();
        }

        public enum Offsets : long
        {
            GameSaveA = 0x000000,
            GameSaveB = 0x00E000,
            HallOfFame = 0x01C000,
            MysteryGiftEReader = 0x1E000,
            RecordedBattle = 0x01F000
        }

        public GameSave GetActiveSave()
        {
            int LastSaveIndexA = GameSaveA.GetLastSaveIndex();
            int LastSaveIndexB = GameSaveB.GetLastSaveIndex();

            if (LastSaveIndexA > LastSaveIndexB || (LastSaveIndexA <= 0 && LastSaveIndexB <= 0))
            {
                return GameSaveA;
            }

            return GameSaveB;
        }

        protected void ReadGameSaveA(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.GameSaveA, SeekOrigin.Begin);
            GameSaveA.ReadFromBinary(binaryReader, gameID);
        }
        protected void ReadGameSaveB(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.GameSaveB, SeekOrigin.Begin);
            GameSaveB.ReadFromBinary(binaryReader, gameID);
        }
        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            StartOffset = binaryReader.BaseStream.Position;
            ReadGameSaveA(binaryReader, StartOffset, gameID);// GameSave A
            ReadGameSaveB(binaryReader, StartOffset, gameID);//GameSave B
        }
    }

    interface ISaveFile
    {
        GameSave GameSaveA { get; set; }
        GameSave GameSaveB { get; set; }

        GameSave GetActiveSave();
    }
}