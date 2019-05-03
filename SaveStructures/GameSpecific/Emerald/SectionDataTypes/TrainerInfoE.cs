using System.IO;

namespace PokemonSaves
{
    public class TrainerInfoE : TrainerInfo
    {
        public new enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
            TimePlayed = 0x000E,
            Options = 0x0013,
            SecurityKey = 0x0AC
        }

        public TrainerInfoE(SaveDataSection saveDataSection) : base(saveDataSection) { }

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
            // GameCode is not used in Emerald, thus this method simply remains empty.
        }

        protected override void ReadSecurityKey(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // SecurityKey
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SecurityKey, SeekOrigin.Begin);
            SecurityKey = binaryReader.ReadUInt32();
        }

        // Write functions:
        protected override void WritePlayerName(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.PlayerName, SeekOrigin.Begin);
            binaryWriter.Write(PlayerName);
        }

        protected override void WritePlayerGender(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.PlayerGender, SeekOrigin.Begin);
            binaryWriter.Write(PlayerGender);
        }

        protected override void WriteTrainerId(BinaryWriter binaryWriter)
        {
            if (null != TrainerID)
            {
                TrainerID.WriteToBinary(binaryWriter);
            }
        }

        protected override void WriteTimePlayed(BinaryWriter binaryWriter)
        {
            if (null != TimePlayed)
            {
                TimePlayed.WriteToBinary(binaryWriter);
            }
        }

        protected override void WriteOptions(BinaryWriter binaryWriter)
        {
            if (null != Options)
            {
                Options.WriteToBinary(binaryWriter);
            }
        }

        protected override void WriteGameCode(BinaryWriter binaryWriter, long startOffset)
        {
            // GameCode is not used in Emerald, so this function remains empty.
        }

        protected override void WriteSecurityKey(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.SecurityKey, SeekOrigin.Begin);
            binaryWriter.Write(SecurityKey);
        }
    }
}