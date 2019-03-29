using System.IO;

namespace PokemonSaves
{
    public class Pokemon : IBinaryParsable
    {
        private uint _personalityValue;
        private uint _otID;
        private byte[] _nickname; // 10 bytes
        private ushort _language;
        private long _otName;
        private byte _markings;
        private ushort _checksum;
        private ushort _unknownValue;
        private PokemonData _pokemonData;
        private uint _statusCondition;
        private byte _level;
        private byte _pokerusRemaining;
        private ushort _currentHP;
        private ushort _totalHP;
        private ushort _attack;
        private ushort _defense;
        private ushort _speed;
        private ushort _specialAttack;
        private ushort _specialDefense;
        public uint PersonalityValue { get => _personalityValue; set => _personalityValue = value; }
        public uint OTID { get => _otID; set => _otID = value; }
        public byte[] Nickname { get => _nickname; set => _nickname = value; }
        public ushort Language { get => _language; set => _language = value; }
        public long OTName { get => _otName; set => _otName = value; }
        public byte Markings { get => _markings; set => _markings = value; }
        public ushort Checksum { get => _checksum; set => _checksum = value; }
        public ushort UnknownValue { get => _unknownValue; set => _unknownValue = value; }
        public PokemonData PokemonData { get => _pokemonData; set => _pokemonData = value; }
        public uint StatusCondition { get => _statusCondition; set => _statusCondition = value; }
        public byte Level { get => _level; set => _level = value; }
        public byte PokerusRemaining { get => _pokerusRemaining; set => _pokerusRemaining = value; }
        public ushort CurrentHP { get => _currentHP; set => _currentHP = value; }
        public ushort TotalHP { get => _totalHP; set => _totalHP = value; }
        public ushort Attack { get => _attack; set => _attack = value; }
        public ushort Defense { get => _defense; set => _defense = value; }
        public ushort Speed { get => _speed; set => _speed = value; }
        public ushort SpecialAttack { get => _specialAttack; set => _specialAttack = value; }
        public ushort SpecialDefense { get => _specialDefense; set => _specialDefense = value; }

        public enum Offsets : long
        {
            PersonalityValue = 0x00,
            OTID = 0x04,
            Nickname = 0x08,
            Language = 0x12,
            OTName = 0x14,
            Markings = 0x1B,
            Checksum = 0x1C,
            UnknownValue = 0x1E,
            PokemonData = 0x20,
            StatusCondition = 0x50,
            Level = 0x54,
            PokerusRemaining = 0x55,
            CurrentHP = 0x56,
            TotalHP = 0x58,
            Attack = 0x5A,
            Defense = 0x5C,
            Speed = 0x5E,
            SpecialAttack = 0x60,
            SpecialDefense = 0x62
        }

        protected void ReadPersonalityValue(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PersonalityValue, SeekOrigin.Begin);
            PersonalityValue = binaryReader.ReadUInt32();
        }
        protected void ReadOTID(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.OTID, SeekOrigin.Begin);
            OTID = binaryReader.ReadUInt32();
        }
        protected void ReadNickname(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Nickname, SeekOrigin.Begin);
            Nickname = binaryReader.ReadBytes(10);
        }
        protected void ReadLanguage(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Language, SeekOrigin.Begin);
            Language = binaryReader.ReadUInt16();
        }
        protected void ReadOTName(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.OTName, SeekOrigin.Begin);
            OTName = binaryReader.ReadInt64();
        }
        protected void ReadMarkings(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Markings, SeekOrigin.Begin);
            Markings = binaryReader.ReadByte();
        }
        protected void ReadChecksum(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Checksum, SeekOrigin.Begin);
            Checksum = binaryReader.ReadUInt16();
        }
        protected void ReadUnknownValue(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.UnknownValue, SeekOrigin.Begin);
            UnknownValue = binaryReader.ReadUInt16();
        }
        protected void ReadPokemonData(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PokemonData, SeekOrigin.Begin);
            if (null == PokemonData)
            {
                PokemonData = new PokemonData(this);
            }
            PokemonData.ReadFromBinary(binaryReader, gameID);
        }
        protected void ReadStatusCondition(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.StatusCondition, SeekOrigin.Begin);
            StatusCondition = binaryReader.ReadUInt32();
        }

        protected void ReadLevel(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Level, SeekOrigin.Begin);
            Level = binaryReader.ReadByte();
        }
        protected void ReadPokerusRemaining(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PokerusRemaining, SeekOrigin.Begin);
            PokerusRemaining = binaryReader.ReadByte();
        }
        protected void ReadCurrentHP(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.CurrentHP, SeekOrigin.Begin);
            CurrentHP = binaryReader.ReadUInt16();
        }
        protected void ReadTotalHP(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TotalHP, SeekOrigin.Begin);
            TotalHP = binaryReader.ReadUInt16();
        }
        protected void ReadAttack(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Attack, SeekOrigin.Begin);
            Attack = binaryReader.ReadUInt16();
        }
        protected void ReadDefense(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Defense, SeekOrigin.Begin);
            Defense = binaryReader.ReadUInt16();
        }
        protected void ReadSpeed(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Speed, SeekOrigin.Begin);
            Speed = binaryReader.ReadUInt16();
        }
        protected void ReadSpecialAttack(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SpecialAttack, SeekOrigin.Begin);
            SpecialAttack = binaryReader.ReadUInt16();
        }
        protected void ReadSpecialDefense(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SpecialDefense, SeekOrigin.Begin);
            SpecialDefense = binaryReader.ReadUInt16();
        }


        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            ReadPersonalityValue(binaryReader, startOffset, gameID); // PersonalityValue
            ReadOTID(binaryReader, startOffset, gameID); // OTID
            ReadNickname(binaryReader, startOffset, gameID); // Nickname
            ReadLanguage(binaryReader, startOffset, gameID); // Language
            ReadOTName(binaryReader, startOffset, gameID); // OTName
            ReadMarkings(binaryReader, startOffset, gameID); // Markings
            ReadUnknownValue(binaryReader, startOffset, gameID); // UnknownValue
            ReadPokemonData(binaryReader, startOffset, gameID); // PokemonData
            ReadStatusCondition(binaryReader, startOffset, gameID); // StatusCondition
            ReadLevel(binaryReader, startOffset, gameID); // Level
            ReadPokerusRemaining(binaryReader, startOffset, gameID); // PokerusRemaining
            ReadCurrentHP(binaryReader, startOffset, gameID); // CurrentHP
            ReadTotalHP(binaryReader, startOffset, gameID); // TotalHP
            ReadAttack(binaryReader, startOffset, gameID); // Attack
            ReadDefense(binaryReader, startOffset, gameID); // Defense
            ReadSpeed(binaryReader, startOffset, gameID); // Speed
            ReadSpecialAttack(binaryReader, startOffset, gameID); // SpecialAttack
            ReadSpecialDefense(binaryReader, startOffset, gameID); // SpecialDefense
        }
    }
}