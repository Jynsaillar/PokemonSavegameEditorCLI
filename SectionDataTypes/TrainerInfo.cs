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
        private TimePlayed _timePlayed;
        private Options _options;
        public long PlayerName { get => _playerName; set => _playerName = value; }
        public byte PlayerGender { get => _playerGender; set => _playerGender = value; }
        public TrainerId TrainerID { get => _trainerID; set => _trainerID = value; }
        public TimePlayed TimePlayed { get => _timePlayed; set => _timePlayed = value; }
        public Options Options { get => _options; set => _options = value; }
        // TODO: Add missing fields & corresponding properties for GameCode, SecurityKey
        public enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
            TimePlayed = 0x000E,
            Options = 0x0013,
            GameCode = 0x00AC,
            SecurityKey = 0x0AF8
        }

        public void ReadFromBinary(BinaryReader binaryReader)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // PlayerName
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerName, SeekOrigin.Begin);
            PlayerName = binaryReader.ReadInt64();
            // PlayerGender
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerGender, SeekOrigin.Begin);
            PlayerGender = binaryReader.ReadByte();

            // TrainerID
            if (null == TrainerID)
            {
                TrainerID = new TrainerId();
            }
            TrainerID.ReadFromBinary(binaryReader);

            // TimePlayed
            if (null == TimePlayed)
            {
                TimePlayed = new TimePlayed();
            }
            TimePlayed.ReadFromBinary(binaryReader);

            // Options
            if (null == Options)
            {
                Options = new Options();
            }
            Options.ReadFromBinary(binaryReader);
        }

    }

}