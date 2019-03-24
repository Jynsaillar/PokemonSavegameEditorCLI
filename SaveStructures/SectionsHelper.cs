using System;

namespace PokemonSaves
{
    public static class SectionsHelper
    {
        // SectionType.Index is not strictly necessary, but makes things easier to read.
        public readonly static SectionType[] SectionTypes = {
            new SectionType(index: 0, name: DataSectionTypes.TrainerInfo, size: 3884),
            new SectionType(index: 1, name: DataSectionTypes.TeamAndItems, size: 3968),
            new SectionType(index: 2, name: DataSectionTypes.GameState, size: 3968),
            new SectionType(index: 3, name: DataSectionTypes.MiscData, size: 3968),
            new SectionType(index: 4, name: DataSectionTypes.RivalInfo, size: 3848),
            new SectionType(index: 5, name: DataSectionTypes.PCBufferA, size: 3968),
            new SectionType(index: 6, name: DataSectionTypes.PCBufferB, size: 3968),
            new SectionType(index: 7, name: DataSectionTypes.PCBufferC, size: 3968),
            new SectionType(index: 8, name: DataSectionTypes.PCBufferD, size: 3968),
            new SectionType(index: 9, name: DataSectionTypes.PCBufferE, size: 3968),
            new SectionType(index: 10, name: DataSectionTypes.PCBufferF, size: 3968),
            new SectionType(index: 11, name: DataSectionTypes.PCBufferG, size: 3968),
            new SectionType(index: 12, name: DataSectionTypes.PCBufferH, size: 3968),
            new SectionType(index: 13, name: DataSectionTypes.PCBufferI, size: 2000)
        };

        /// <summary> Returns the size of a section from its name. </summary>
        public static uint GetSectionSize(DataSectionTypes sectionName)
        {
            foreach (var sectionType in SectionTypes)
            {
                if (sectionType.Name == sectionName)
                {
                    return sectionType.Size;
                }
            }
            return 3968;
        }

        /// <summary> Returns the size of a section in bytes from its section ID. 
        /// The section sizes are fixed. </summary>
        public static uint GetSectionSize(uint sectionIndex)
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

    public struct SectionType
    {
        public uint Index;
        public DataSectionTypes Name;
        public uint Size;

        public SectionType(uint index, DataSectionTypes name, uint size)
        {
            Index = index;
            Name = name;
            Size = size;
        }
    }

    public enum DataSectionTypes : uint
    {
        TrainerInfo,
        TeamAndItems,
        GameState,
        MiscData,
        RivalInfo,
        PCBufferA,
        PCBufferB,
        PCBufferC,
        PCBufferD,
        PCBufferE,
        PCBufferF,
        PCBufferG,
        PCBufferH,
        PCBufferI
    }
}