using System.IO;

namespace PokemonSaves
{
    public class SaveDataSection : IDataSection, IBinaryParsable
    {
        private long _startOffset;
        SectionData _data;
        DataSectionTypes _sectionId;
        short _checksum;
        int _saveIndex;
        public long StartOffset { get => _startOffset; set => _startOffset = value; }
        public SectionData Data { get => _data; set => _data = value; }
        public DataSectionTypes SectionID { get => _sectionId; set => _sectionId = value; }
        public short Checksum { get => _checksum; set => _checksum = value; }
        public int SaveIndex { get => _saveIndex; set => _saveIndex = value; }

        public enum Offsets : long
        {
            Data = 0x0000,
            SectionID = 0x0FF4,
            Checksum = 0x0FF6,
            SaveIndex = 0x0FFC
        }

        protected void ReadSectionID(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SectionID, SeekOrigin.Begin);
            SectionID = (DataSectionTypes)binaryReader.ReadUInt16();
        }
        protected void ReadChecksum(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Checksum, SeekOrigin.Begin);
            Checksum = binaryReader.ReadInt16();
        }
        protected void ReadSaveIndex(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SaveIndex, SeekOrigin.Begin);
            SaveIndex = binaryReader.ReadInt32();
        }
        protected void ReadData(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Data, SeekOrigin.Begin); // Seeks start offset of SectionData.

            // Parsing of actual SectionData starts here since the SectionID is needed to determine the derived
            // type of SectionData (e.g. TrainerInfo, GameState, RivalInfo etc.).
            switch (SectionID)
            {
                case DataSectionTypes.TrainerInfo:
                    switch (gameID)
                    {
                        case GameIDs.FireRedLeafGreen:
                            var trainerInfoFRLG = new TrainerInfoFRLG(this);
                            trainerInfoFRLG.ReadFromBinary(binaryReader, gameID);
                            Data = trainerInfoFRLG; // Box TrainerInfo into SectionData type.
                            break;
                        case GameIDs.RubySapphire:
                            var trainerInfoRS = new TrainerInfoRS(this);
                            trainerInfoRS.ReadFromBinary(binaryReader, gameID);
                            Data = trainerInfoRS; // Box TrainerInfo into SectionData type.
                            break;
                        case GameIDs.Emerald:
                            var trainerInfoE = new TrainerInfoE(this);
                            trainerInfoE.ReadFromBinary(binaryReader, gameID);
                            Data = trainerInfoE; // Box TrainerInfo into SectionData type.
                            break;
                    }

                    break;
                case DataSectionTypes.TeamAndItems:
                    switch (gameID)
                    {
                        case GameIDs.FireRedLeafGreen:
                            var teamAndItemsFRLG = new TeamAndItemsFRLG(this);
                            teamAndItemsFRLG.ReadFromBinary(binaryReader, gameID);
                            Data = teamAndItemsFRLG; // Box TeamAndItems into SectionData type.
                            break;
                        case GameIDs.RubySapphire:
                            var teamAndItemsRS = new TeamAndItemsRS(this);
                            teamAndItemsRS.ReadFromBinary(binaryReader, gameID);
                            Data = teamAndItemsRS; // Box TeamAndItems into SectionData type.
                            break;
                        case GameIDs.Emerald:
                            var teamAndItemsE = new TeamAndItemsE(this);
                            teamAndItemsE.ReadFromBinary(binaryReader, gameID);
                            Data = teamAndItemsE; // Box TeamAndItems into SectionData type.
                            break;
                    }
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
            }

            /* Since reading the SectionData block's actual data section is finished but the stream is not at the end offset
                       of the total SectionData block, the stream position needs to be updated before proceeding/exiting the function
                       to prevent breaking future read operations. Each section is 4096 bytes in size. */
            binaryReader.BaseStream.Seek(startOffset + 4096, SeekOrigin.Begin);
        }
        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            StartOffset = binaryReader.BaseStream.Position;
            ReadSectionID(binaryReader, StartOffset, gameID); // SectionID
            ReadChecksum(binaryReader, StartOffset, gameID); // Checksum
            ReadSaveIndex(binaryReader, StartOffset, gameID); // SaveIndex
            ReadData(binaryReader, StartOffset, gameID); // Data
        }

        public void RecalculateChecksum(BinaryReader binaryReader, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(StartOffset + (long)Offsets.Data, SeekOrigin.Begin); // Seeks start offset of SectionData.

            int bytesToVerify = SectionsHelper.GetSectionSize(SectionID);
            int checksum = 0;
            var dataBlob = binaryReader.ReadBytes(bytesToVerify); // One single read operation vs. (bytesToVerify/4) read operations
            if (dataBlob.Length <= 0)
            {
                System.Console.WriteLine($"[Checksum Error]\nData section {SectionID} byte size returned 0, are you sure the offset is correct?");
                return;
            }
            if (dataBlob.Length % 4 != 0)
            {
                System.Console.WriteLine($"[Checksum Error]\nData section {SectionID} byte size is not divisible by 4 (integer size), are you sure the offset and the section ID are correct?");
                return;
            }

            for (int i = 0; i < bytesToVerify; i += 4)
            {
                checksum += System.BitConverter.ToInt32(dataBlob, i); // Extracts 4 bytes as int from dataBlob and adds it to the checksum.
            }

            var lowerChecksum = (short)(checksum >> 0);
            var upperChecksum = (short)(checksum >> 16);

            Checksum = (short)(lowerChecksum + upperChecksum);
        }

        public void WriteChecksum(BinaryWriter binaryWriter)
        {
            binaryWriter.BaseStream.Seek(StartOffset + (long)Offsets.Checksum, SeekOrigin.Begin);
            binaryWriter.Write(Checksum);
        }
    }

    interface IDataSection
    {
        SectionData Data { get; set; }
        DataSectionTypes SectionID { get; set; }
        short Checksum { get; set; }
        int SaveIndex { get; set; }

        void RecalculateChecksum(BinaryReader binaryReader, GameIDs gameID);
        void WriteChecksum(BinaryWriter binaryWriter);
    }

}