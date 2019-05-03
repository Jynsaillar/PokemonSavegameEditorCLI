using System.IO;

namespace PokemonSaves
{
    public class LiquidCrystalSave : SaveFile, IGameInfo
    {
        int _generation { get; } = 3;
        string _gameName { get; } = "LiquidCrystal [LC]";
        GameIDs _gameID { get; } = GameIDs.LiquidCrystal;
        public int Generation { get => _generation; }
        public string GameName { get => _gameName; }
        public GameIDs GameID { get => _gameID; }

        /// <summary>
        /// Reads a LiquidCrystal save file from a binary reader's stream using
        /// <code>
        /// ReadFromBinary(BinaryReader binaryReader)
        /// </code>.
        /// </summary>
        public LiquidCrystalSave(BinaryReader binaryReader)
        {
            ReadFromBinary(binaryReader, GameID);
        }
    }
}