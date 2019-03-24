using System.IO;

namespace PokemonSaves
{
    public class SaveFile : ISaveFile, IBinaryParsable
    {
        GameSave _gameSaveA;
        GameSave _gameSaveB;
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
            uint LastSaveIndexA = GameSaveA.GetLastSaveIndex();
            uint LastSaveIndexB = GameSaveB.GetLastSaveIndex();

            if (LastSaveIndexA > LastSaveIndexB || (LastSaveIndexA == 0 && LastSaveIndexB == 0))
            {
                return GameSaveA;
            }

            return GameSaveB;
        }

        public void ReadFromBinary(BinaryReader binaryReader)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // GameSave A
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.GameSaveA, SeekOrigin.Begin);
            GameSaveA.ReadFromBinary(binaryReader);
            //GameSave B
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.GameSaveB, SeekOrigin.Begin);
            GameSaveB.ReadFromBinary(binaryReader);

        }
    }

    interface ISaveFile
    {
        GameSave GameSaveA { get; set; }
        GameSave GameSaveB { get; set; }

        GameSave GetActiveSave();
    }
}