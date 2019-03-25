using System.IO;

namespace PokemonSaves
{
    public class RubySapphireSave : SaveFile, IGameInfo
    {
        int _generation { get; } = 3;
        string _gameName { get; } = "Ruby/Sapphire [RS]";
        GameIDs _gameID { get; } = GameIDs.RubySapphire;
        public int Generation { get => _generation; }
        public string GameName { get => _gameName; }
        public GameIDs GameID { get => _gameID; }

        /// <summary>
        /// Reads a Ruby/Sapphire save file from a binary reader's stream using
        /// <code>
        /// ReadFromBinary(BinaryReader binaryReader)
        /// </code>.
        /// </summary>
        public RubySapphireSave(BinaryReader binaryReader)
        {
            ReadFromBinary(binaryReader, GameID);
        }
    }
}