using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PokemonSaves
{
    public class TrainerInfo : SectionData, IBinaryParsable
    {
        private long _playerName;
        private byte _playerGender;
        private TrainerId _trainerID;
        private TimePlayed _timePlayed;
        private Options _options;
        private uint _gameCode;
        private uint _securityKey;
        public long PlayerName { get => _playerName; set => _playerName = value; }
        public byte PlayerGender { get => _playerGender; set => _playerGender = value; }
        public TrainerId TrainerID { get => _trainerID; set => _trainerID = value; }
        public TimePlayed TimePlayed { get => _timePlayed; set => _timePlayed = value; }
        public Options Options { get => _options; set => _options = value; }

        /// <summary> 
        /// <para/>Ruby, Sapphire = 0x00000000
        /// <para/>FireRed, LeafGreen = 0x00000001
        /// <para/>Emerald = any offset not 0x00000000 or 0x00000001, determines the offset for the security key as well.
        /// </summary>
        public uint GameCode { get => _gameCode; set => _gameCode = value; }
        /// <summary>
        /// <para/>Emerald is the only Generation III game in which the security key is utilized.
        /// <para/>If used, it encrypts values like money and item quantities by simply running an XOR operation with the target bytes.
        /// <para/>Obtain the decrypted values by simply running an XOR operation with the security key again.
        /// </summary>
        public uint SecurityKey { get => _securityKey; set => _securityKey = value; }
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

        protected void ReadPlayerName(BinaryReader binaryReader, long startOffset)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerName, SeekOrigin.Begin);
            PlayerName = binaryReader.ReadInt64();
        }

        protected void ReadPlayerGender(BinaryReader binaryReader, long startOffset)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerGender, SeekOrigin.Begin);
            PlayerGender = binaryReader.ReadByte();
        }

        protected void ReadTrainerId(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            if (null == TrainerID)
            {
                TrainerID = new TrainerId();
            }
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TrainerID, SeekOrigin.Begin);
            TrainerID.ReadFromBinary(binaryReader, gameID);
        }

        protected void ReadTimePlayed(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            if (null == TimePlayed)
            {
                TimePlayed = new TimePlayed();
            }
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TimePlayed, SeekOrigin.Begin);
            TimePlayed.ReadFromBinary(binaryReader, gameID);
        }

        protected void ReadOptions(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            if (null == Options)
            {
                Options = new Options();
            }
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Options, SeekOrigin.Begin);
            Options.ReadFromBinary(binaryReader, gameID);
        }

        protected void ReadGameCode(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // GameCode
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.GameCode, SeekOrigin.Begin);
            GameCode = binaryReader.ReadUInt32();
        }

        protected void ReadSecurityKey(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // SecurityKey
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SecurityKey, SeekOrigin.Begin);
            SecurityKey = binaryReader.ReadUInt32();
        }

        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            ReadPlayerName(binaryReader, startOffset); // PlayerName
            ReadPlayerGender(binaryReader, startOffset); // PlayerGender
            ReadTrainerId(binaryReader, startOffset, gameID); // TrainerID
            ReadTimePlayed(binaryReader, startOffset, gameID); // TimePlayed
            ReadOptions(binaryReader, startOffset, gameID); // Options
            ReadGameCode(binaryReader, startOffset, gameID); // GameCode
            ReadSecurityKey(binaryReader, startOffset, gameID); // SecurityKey
        }

    }

}