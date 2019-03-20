using System;
using System.IO;

namespace PokemonSaves
{
    public class TrainerId : IBinaryParsable
    {

        private ushort _publicID;
        private ushort _secretID;
        public ushort PublicID { get => _publicID; set => _publicID = value; }
        public ushort SecretID { get => _secretID; set => _secretID = value; }

        public enum Offsets : long
        {
            PublicID = 0x0000,
            SecretID = 0x0002
        }

        public void ReadFromBinary(BinaryReader binaryReader)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // PublicID
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PublicID, SeekOrigin.Begin);
            PublicID = binaryReader.ReadUInt16();
            // SecretID
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SecretID, SeekOrigin.Begin);
            SecretID = binaryReader.ReadUInt16();
        }
    }
}