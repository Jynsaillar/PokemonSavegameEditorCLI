using System.IO;

namespace PokemonSaves
{
    public class SaveDataSection : IDataSection, IBinaryParsable
    {
        SectionData _data;
        DataSectionTypes _sectionId;
        short _checksum;
        uint _saveIndex;
        public SectionData Data { get => _data; set => _data = value; }
        public DataSectionTypes SectionID { get => _sectionId; set => _sectionId = value; }
        public short Checksum { get => _checksum; set => _checksum = value; }
        public uint SaveIndex { get => _saveIndex; set => _saveIndex = value; }

        public enum Offsets : long
        {
            Data = 0x0000,
            SectionID = 0x0FF4,
            Checksum = 0x0FF6,
            SaveIndex = 0x0FFC
        }

        public void ReadFromBinary(BinaryReader binaryReader)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // SectionID
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SectionID, SeekOrigin.Begin);
            SectionID = (DataSectionTypes)binaryReader.ReadUInt16();
            // Checksum
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Checksum, SeekOrigin.Begin);
            Checksum = binaryReader.ReadInt16();
            // SaveIndex
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SaveIndex, SeekOrigin.Begin);
            SaveIndex = binaryReader.ReadUInt32();
            // Data
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Data, SeekOrigin.Begin); // Seeks start offset of SectionData.

            // Parsing of actual SectionData starts here since the SectionID is needed to determine the derived
            // type of SectionData (e.g. TrainerInfo, GameState, RivalInfo etc.).
            switch (SectionID)
            {
                case DataSectionTypes.TrainerInfo:
                    var trainerInfo = new TrainerInfo();
                    trainerInfo.ReadFromBinary(binaryReader); // Parses SectionData as TrainerInfo since the SectionID matches TrainerInfo.
                    Data = trainerInfo; // Box TrainerInfo into SectionData type.
                    break;
                case DataSectionTypes.TeamAndItems:
                    // TODO: Implement case TeamAndItems.
                    break;
                case DataSectionTypes.GameState:
                    // TODO: Implement case GameState.
                    break;
                case DataSectionTypes.MiscData:
                    // TODO: Implement case MiscData.
                    break;
                case DataSectionTypes.RivalInfo:
                    // TODO: Implement case RivalInfo.
                    break;
                case DataSectionTypes.PCBufferA:
                    // TODO: Implement case PCBufferA.
                    break;
                case DataSectionTypes.PCBufferB:
                    // TODO: Implement case PCBufferB.
                    break;
                case DataSectionTypes.PCBufferC:
                    // TODO: Implement case PCBufferC.
                    break;
                case DataSectionTypes.PCBufferD:
                    // TODO: Implement case PCBufferD.
                    break;
                case DataSectionTypes.PCBufferE:
                    // TODO: Implement case PCBufferE.
                    break;
                case DataSectionTypes.PCBufferF:
                    // TODO: Implement case PCBufferF.
                    break;
                case DataSectionTypes.PCBufferG:
                    // TODO: Implement case PCBufferG.
                    break;
                case DataSectionTypes.PCBufferH:
                    // TODO: Implement case PCBufferH.
                    break;
                case DataSectionTypes.PCBufferI:
                    // TODO: Implement case PCBufferI.
                    break;
                default:
                    Data = new SectionData();
                    break;
            }

            /* Since reading the SectionData block's actual data section is finished but the stream is not at the end offset
                       of the total SectionData block, the stream position needs to be updated before proceeding/exiting the function
                       to prevent breaking future read operations. Each section is 4096 bytes in size. */
            binaryReader.BaseStream.Seek(startOffset + 4096, SeekOrigin.Begin);
        }
    }

    interface IDataSection
    {
        SectionData Data { get; set; }
        DataSectionTypes SectionID { get; set; }
        short Checksum { get; set; }
        uint SaveIndex { get; set; }
    }

}