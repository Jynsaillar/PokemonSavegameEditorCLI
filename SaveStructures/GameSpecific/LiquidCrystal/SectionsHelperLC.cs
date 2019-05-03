using System;

namespace PokemonSaves
{
    public static class SectionsHelperLC
    {
        // SectionType.Index is not strictly necessary, but makes things easier to read.
        public readonly static SectionType[] SectionTypes = {
            new SectionType(index: 0, name: DataSectionTypes.TrainerInfo, size: 3876), // Mismatch?
            new SectionType(index: 1, name: DataSectionTypes.TeamAndItems, size: 4080),
            new SectionType(index: 2, name: DataSectionTypes.GameState, size: 4080),
            new SectionType(index: 3, name: DataSectionTypes.MiscData, size: 4080),
            new SectionType(index: 4, name: DataSectionTypes.RivalInfo, size: 3476), // Mismatch?
            new SectionType(index: 5, name: DataSectionTypes.PCBufferA, size: 4080),
            new SectionType(index: 6, name: DataSectionTypes.PCBufferB, size: 4080),
            new SectionType(index: 7, name: DataSectionTypes.PCBufferC, size: 4080),
            new SectionType(index: 8, name: DataSectionTypes.PCBufferD, size: 4080),
            new SectionType(index: 9, name: DataSectionTypes.PCBufferE, size: 4080),
            new SectionType(index: 10, name: DataSectionTypes.PCBufferF, size: 4080),
            new SectionType(index: 11, name: DataSectionTypes.PCBufferG, size: 4080),
            new SectionType(index: 12, name: DataSectionTypes.PCBufferH, size: 4080),
            new SectionType(index: 13, name: DataSectionTypes.PCBufferI, size: 1104) // Mismatch?
        };

        /// <summary> Returns the size of a section from its name. </summary>
        public static int GetSectionSize(DataSectionTypes sectionName)
        {
            foreach (var sectionType in SectionTypes)
            {
                if (sectionType.Name == sectionName)
                {
                    return sectionType.Size;
                }
            }
            return 4084;
        }

        /// <summary> Returns the size of a section in bytes from its section ID. 
        /// The section sizes are fixed. </summary>
        public static int GetSectionSize(uint sectionIndex)
        {
            foreach (var sectionType in SectionTypes)
            {
                if (sectionType.Index == sectionIndex)
                {
                    return sectionType.Size;
                }

            }
            return 0;
        }
    }
}