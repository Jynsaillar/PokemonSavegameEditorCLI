using System.IO;
using System.Collections.Generic;

namespace PokemonSaves
{
    public class TeamAndItemsFRLG : TeamAndItems
    {
        public new enum Offsets : long
        {
            TeamSize = 0x0034,
            TeamPokemonList = 0x0038,
            Money = 0x0290,
            Coins = 0x0294,
            PCItems = 0x0298,
            ItemPocket = 0x0310,
            KeyItemPocket = 0x03B8,
            BallItemPocket = 0x0430,
            TMCase = 0x0464,
            BerryPocket = 0x054C
        }

        public TeamAndItemsFRLG(SaveDataSection saveDataSection) : base(saveDataSection) { }

        // Read functions:

        protected override void ReadTeamSize(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TeamSize, SeekOrigin.Begin);
            TeamSize = binaryReader.ReadUInt32();
        }

        protected override void ReadTeamPokemonList(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TeamPokemonList, SeekOrigin.Begin);
            if (null == TeamPokemonList)
            {
                TeamPokemonList = new List<Pokemon>();
            }
            for (int i = 0; i < 6; i++)
            {
                Pokemon pokemon = new Pokemon();
                pokemon.ReadFromBinary(binaryReader, gameID);
                TeamPokemonList.Add(pokemon);
            }
        }

        protected override void ReadMoney(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Money, SeekOrigin.Begin);
            Money = binaryReader.ReadUInt32();
        }

        protected override void ReadCoins(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Coins, SeekOrigin.Begin);
            Coins = binaryReader.ReadUInt16();
        }
        protected override List<Item> ReadItemList(BinaryReader binaryReader, long startOffset, GameIDs gameID, uint itemCount)
        {
            binaryReader.BaseStream.Seek(startOffset, SeekOrigin.Begin);
            List<Item> items = new List<Item>();
            for (int i = 0; i < itemCount; i++)
            {
                var item = new Item();
                item.ReadFromBinary(binaryReader, gameID);
                items.Add(item);
            }

            return items;
        }
        protected override void ReadPCItems(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            var pcItemCount = (uint)((Offsets.ItemPocket - Offsets.PCItems) / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            PCItems = ReadItemList(binaryReader, startOffset + (long)Offsets.PCItems, gameID, pcItemCount);
        }
        protected override void ReadItemPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            var itemPocketCount = (uint)((Offsets.KeyItemPocket - Offsets.ItemPocket) / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            ItemPocket = ReadItemList(binaryReader, startOffset + (long)Offsets.ItemPocket, gameID, itemPocketCount);
        }
        protected override void ReadKeyItemPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            var keyItemPocketCount = (uint)((Offsets.BallItemPocket - Offsets.KeyItemPocket) / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            KeyItemPocket = ReadItemList(binaryReader, startOffset + (long)Offsets.KeyItemPocket, gameID, keyItemPocketCount);
        }
        protected override void ReadBallItemPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            var ballItemPocketCount = (uint)((Offsets.TMCase - Offsets.BallItemPocket) / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            BallItemPocket = ReadItemList(binaryReader, startOffset + (long)Offsets.BallItemPocket, gameID, ballItemPocketCount);
        }
        protected override void ReadTMCase(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            var tmCasePocketCount = (uint)((Offsets.BerryPocket - Offsets.TMCase) / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            TMCase = ReadItemList(binaryReader, startOffset + (long)Offsets.TMCase, gameID, tmCasePocketCount);
        }

        protected override void ReadBerryPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // Since this is the last entry in a dynamically allocated block the size has to be provided.
            // Since FRLG, RS and E differ in berry pocket size, the function has to be overriden in version-specific child classes (e.g. TeamAndItemsFRLG.ReadBerryPocket(...)).
            // FRLG has 172 bytes allocated for berries / 4 = 43 unique berries may be stored at most.
            var berryPocketCount = (uint)(172 / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            BerryPocket = ReadItemList(binaryReader, startOffset + (long)Offsets.BerryPocket, gameID, berryPocketCount);
        }

        // Write functions:
        protected override void WriteTeamSize(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.TeamSize, SeekOrigin.Begin);
            binaryWriter.Write(TeamSize);
        }
        protected override void WriteTeamPokemonList(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.TeamPokemonList, SeekOrigin.Begin);
            foreach (var pokemon in TeamPokemonList)
            {
                pokemon.WriteToBinary(binaryWriter);
            }
        }
        protected override void WriteMoney(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.Money, SeekOrigin.Begin);
            binaryWriter.Write(Money);
        }
        protected override void WriteCoins(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.Coins, SeekOrigin.Begin);
            binaryWriter.Write(Coins);
        }
        protected override void WriteItemList(BinaryWriter binaryWriter, List<Item> items)
        {
            foreach (var item in items)
            {
                item.WriteToBinary(binaryWriter);
            }
        }
        protected override void WritePCItems(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.PCItems, SeekOrigin.Begin);
            WriteItemList(binaryWriter, PCItems);
        }
        protected override void WriteItemPocket(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.ItemPocket, SeekOrigin.Begin);
            WriteItemList(binaryWriter, ItemPocket);
        }
        protected override void WriteKeyItemPocket(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.KeyItemPocket, SeekOrigin.Begin);
            WriteItemList(binaryWriter, KeyItemPocket);
        }
        protected override void WriteBallItemPocket(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.BallItemPocket, SeekOrigin.Begin);
            WriteItemList(binaryWriter, BallItemPocket);
        }
        protected override void WriteTMCase(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.TMCase, SeekOrigin.Begin);
            WriteItemList(binaryWriter, TMCase);
        }
        protected override void WriteBerryPocket(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.BerryPocket, SeekOrigin.Begin);
            WriteItemList(binaryWriter, BerryPocket);
        }
    }
}