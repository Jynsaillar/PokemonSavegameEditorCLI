using System.IO;

namespace PokemonSaves
{
    public class FireredLeafgreenSave : SaveFile, IGameInfo
    {
        int _generation { get; } = 3;
        string _gameName { get; } = "Firered/Leafgreen [FRLG]";
        GameIDs _gameID { get; } = GameIDs.FireRedLeafGreen;
        public int Generation { get => _generation; }
        public string GameName { get => _gameName; }
        public GameIDs GameID { get => _gameID; }

        /// <summary>
        /// Reads a FireRed/LeafGreen save file from a binary reader's stream using
        /// <code>
        /// ReadFromBinary(BinaryReader binaryReader)
        /// </code>.
        /// </summary>
        public FireredLeafgreenSave(BinaryReader binaryReader)
        {
            ReadFromBinary(binaryReader, GameID);
        }
    }
}