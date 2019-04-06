using System.Collections.Generic;
namespace PokemonSaves
{
    public class SectionData
    {
        private SaveDataSection _saveDataSection;
        public SaveDataSection SaveDataSection { get => _saveDataSection; set => _saveDataSection = value; }
        public SectionData(SaveDataSection saveDataSection)
        {
            SaveDataSection = saveDataSection;
        }
    }
}