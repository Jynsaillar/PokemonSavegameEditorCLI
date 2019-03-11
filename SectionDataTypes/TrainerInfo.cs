using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PokemonSaves
{
    public class TrainerInfo : IBinaryParsable
    {
        private long _playerName;
        private byte _playerGender;
        private TrainerId _trainerID;
        public long PlayerName { get => _playerName; set => _playerName = value; }
        public byte PlayerGender { get => _playerGender; set => _playerGender = value; }
        public TrainerId TrainerID { get => _trainerID; set => _trainerID = value; }
        public enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
            TimePlayed = 0x000E,
            Options = 0x00AC,
            SecurityKey = 0x0AF8
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

}