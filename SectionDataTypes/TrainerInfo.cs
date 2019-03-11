using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PokemonSaves
{
    public class TrainerInfo : IBinaryParsable
    {
        public long PlayerName;
        public byte PlayerGender;
        public TrainerId TrainerID;
        public enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
        }

        public void Read(BinaryReader binaryReader)
        {
            long startOffset = binaryReader.BaseStream.Position;
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerName, SeekOrigin.Begin);
            PlayerName = binaryReader.ReadInt64();
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerGender, SeekOrigin.Begin);
            PlayerGender = binaryReader.ReadByte();

            if (null == TrainerID)
            {
                TrainerID = new TrainerId();
            }

            TrainerID.Read(binaryReader);

        }

    }

    public class TrainerId : IBinaryParsable
    {
        public ushort PublicId;
        public ushort SecretId;

        public enum Offsets : long
        {
            PublicId = 0x0000,
            SecretId = 0x0002
        }

        public void Read(BinaryReader binaryReader)
        {
            long startOffset = binaryReader.BaseStream.Position;
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PublicId, SeekOrigin.Begin);
            PublicId = binaryReader.ReadUInt16();
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SecretId, SeekOrigin.Begin);
            SecretId = binaryReader.ReadUInt16();
        }
    }
}