using System.IO;
using System.Collections.Generic;

namespace PokemonSaves
{
    public class GameSave : IGameSave, IBinaryParsable
    {
        HashSet<SaveDataSection> _saveDataSections;
        public HashSet<SaveDataSection> SaveDataSections { get => _saveDataSections; set => _saveDataSections = value; }

        public GameSave()
        {
            // There are always 14 sections in total but their order does not matter,
            // thus a HashSet fits perfectly here.
            _saveDataSections = new HashSet<SaveDataSection>(14);
        }

        public uint GetLastSaveIndex()
        {
            // Since SaveDataSections is a HashSet and we want any SaveIndex (since they're all the same),
            // loop the sections and simply return the SaveIndex of the very first section found.
            foreach (var SaveDataSection in SaveDataSections)
            {
                return SaveDataSection.SaveIndex;
            }

            return 0;
        }

        public void ReadFromBinary(BinaryReader binaryReader)
        {
            foreach (var saveDataSection in SaveDataSections)
            {
                saveDataSection.ReadFromBinary(binaryReader);
            }
        }
    }

    interface IGameSave
    {
        HashSet<SaveDataSection> SaveDataSections { get; set; }
        uint GetLastSaveIndex();
    }
}