using System;

namespace PokemonSaves
{
    public class FireredLeafgreenSave : SaveFile, IGameInfo
    {
        int _generation { get; } = 3;
        string _gameName { get; } = "Firered/Leafgreen [FRLG]";
        GameIDs _gameID { get; } = GameIDs.FireRedLeafGreen;
        public int Generation => _generation;
        public string GameName => _gameName;
        public GameIDs GameID => _gameID;
    }
}