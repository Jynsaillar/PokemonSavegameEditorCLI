namespace PokemonSaves
{
    public class SaveDataSection : IDataSection
    {
        SectionData _data;
        ushort _sectionId;
        short _checksum;
        uint _saveIndex;
        public SectionData Data { get => _data; set => _data = value; }
        public ushort SectionID { get => _sectionId; set => _sectionId = value; }
        public short Checksum { get => _checksum; set => _checksum = value; }
        public uint SaveIndex { get => _saveIndex; set => _saveIndex = value; }

        public enum Offsets : long
        {
            Data = 0x0000,
            SectionID = 0x0FF4,
            Checksum = 0x0FF6,
            SaveIndex = 0x0FFC
        }
    }

    interface IDataSection
    {
        SectionData Data { get; set; }
        ushort SectionID { get; set; }
        short Checksum { get; set; }
        uint SaveIndex { get; set; }
    }

}