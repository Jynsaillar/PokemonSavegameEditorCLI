namespace PokemonSaves
{
    public class SaveFile : ISaveFile
    {
        GameSave _gameSaveA;
        GameSave _gameSaveB;
        public GameSave GameSaveA { get => _gameSaveA; set => _gameSaveA = value; }
        public GameSave GameSaveB { get => _gameSaveB; set => _gameSaveB = value; }
        /// todo: HallOfFame
        /// todo: MysteryGiftEReader
        /// todo: RecordedBattle

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

            if (LastSaveIndexA > LastSaveIndexB)
            {
                return GameSaveA;
            }

            return GameSaveB;
        }
    }

    interface ISaveFile
    {
        GameSave GameSaveA { get; set; }
        GameSave GameSaveB { get; set; }

        GameSave GetActiveSave();
    }
}