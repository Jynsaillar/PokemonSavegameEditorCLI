using System;
using System.IO;
using System.Runtime.InteropServices;

namespace PokemonSaves
{
    public abstract class TrainerInfo : SectionData, IBinaryParsable
    {
        private long _startOffset;
        private long _playerName;
        private byte _playerGender;
        private TrainerId _trainerID;
        private TimePlayed _timePlayed;
        private Options _options;
        private uint _gameCode;
        private uint _securityKey;
        public long StartOffset { get => _startOffset; set => _startOffset = value; }
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
            PlayerGender = 0x0000,
            TrainerID = 0x0000,
            TimePlayed = 0x0000,
            Options = 0x0000,
            GameCode = 0x0000,
            SecurityKey = 0x0000
        }

        public TrainerInfo(SaveDataSection saveDataSection) : base(saveDataSection) { }

        protected abstract void ReadPlayerName(BinaryReader binaryReader, long startOffset);

        protected abstract void ReadPlayerGender(BinaryReader binaryReader, long startOffset);

        protected abstract void ReadTrainerId(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract void ReadTimePlayed(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract void ReadOptions(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract void ReadGameCode(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract void ReadSecurityKey(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        public virtual void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            StartOffset = binaryReader.BaseStream.Position;
            ReadPlayerName(binaryReader, StartOffset); // PlayerName
            ReadPlayerGender(binaryReader, StartOffset); // PlayerGender
            ReadTrainerId(binaryReader, StartOffset, gameID); // TrainerID
            ReadTimePlayed(binaryReader, StartOffset, gameID); // TimePlayed
            ReadOptions(binaryReader, StartOffset, gameID); // Options
            ReadGameCode(binaryReader, StartOffset, gameID); // GameCode
            ReadSecurityKey(binaryReader, StartOffset, gameID); // SecurityKey
        }

        // Write functions:

        protected abstract void WritePlayerName(BinaryWriter binaryWriter, long startOffset);

        protected abstract void WritePlayerGender(BinaryWriter binaryWriter, long startOffset);

        protected abstract void WriteTrainerId(BinaryWriter binaryWriter);

        protected abstract void WriteTimePlayed(BinaryWriter binaryWriter);

        protected abstract void WriteOptions(BinaryWriter binaryWriter);

        protected abstract void WriteGameCode(BinaryWriter binaryWriter, long startOffset);

        protected abstract void WriteSecurityKey(BinaryWriter binaryWriter, long startOffset);

        public virtual void WriteToBinary(BinaryWriter binaryWriter)
        {
            WritePlayerName(binaryWriter, StartOffset); // PlayerName
            WritePlayerGender(binaryWriter, StartOffset); // PlayerGender
            WriteTrainerId(binaryWriter); // TrainerId
            WriteTimePlayed(binaryWriter); // TimePlayed
            WriteOptions(binaryWriter); // Options
            WriteGameCode(binaryWriter, StartOffset); // GameCode
            WriteSecurityKey(binaryWriter, StartOffset); // SecurityKey
        }

    }

}