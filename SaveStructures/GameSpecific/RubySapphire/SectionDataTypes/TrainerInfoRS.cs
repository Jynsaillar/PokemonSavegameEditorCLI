using System.IO;

namespace PokemonSaves
{
    public class TrainerInfoRS : TrainerInfo
    {
        public new enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
            TimePlayed = 0x000E,
            Options = 0x0013,
            GameCode = 0x00AC
        }

        public TrainerInfoRS(SaveDataSection saveDataSection) : base(saveDataSection) { }

        protected override void ReadPlayerName(BinaryReader binaryReader, long startOffset)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerName, SeekOrigin.Begin);
            PlayerName = binaryReader.ReadInt64();
        }

        protected override void ReadPlayerGender(BinaryReader binaryReader, long startOffset)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PlayerGender, SeekOrigin.Begin);
            PlayerGender = binaryReader.ReadByte();
        }

        protected override void ReadTrainerId(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            if (null == TrainerID)
            {
                TrainerID = new TrainerId();
            }
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TrainerID, SeekOrigin.Begin);
            TrainerID.ReadFromBinary(binaryReader, gameID);
        }

        protected override void ReadTimePlayed(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            if (null == TimePlayed)
            {
                TimePlayed = new TimePlayed();
            }
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TimePlayed, SeekOrigin.Begin);
            TimePlayed.ReadFromBinary(binaryReader, gameID);
        }

        protected override void ReadOptions(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            if (null == Options)
            {
                Options = new Options();
            }
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Options, SeekOrigin.Begin);
            Options.ReadFromBinary(binaryReader, gameID);
        }

        protected override void ReadGameCode(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // GameCode
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.GameCode, SeekOrigin.Begin);
            GameCode = binaryReader.ReadUInt32();
        }

        protected override void ReadSecurityKey(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // The SecurityKey is not used in Ruby/Sapphire, thus this method simply remains empty.
        }

    }
}