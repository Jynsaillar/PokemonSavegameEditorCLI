using System.IO;
using System.Collections.Generic;

namespace PokemonSaves
{
    public class TeamAndItemsRS : TeamAndItems
    {
        public new enum Offsets : long
        {
            TeamSize = 0x0234,
            TeamPokemonList = 0x0238,
            Money = 0x0490,
            Coins = 0x0494,
            PCItems = 0x0498,
            ItemPocket = 0x0560,
            KeyItemPocket = 0x05B0,
            BallItemPocket = 0x0600,
            TMCase = 0x0640,
            BerryPocket = 0x0740
        }

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
            // RS has 184 bytes allocated for berries / 4 = 46 unique berries may be stored at most.
            var berryPocketCount = (uint)(184 / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            BerryPocket = ReadItemList(binaryReader, startOffset + (long)Offsets.BerryPocket, gameID, berryPocketCount);
        }

        // Write functions:
        // TODO: Implement TeamAndItemsRS write functions.
        protected override void WriteTeamSize() { }
        protected override void WriteTeamPokemonList() { }
        protected override void WriteMoney() { }
        protected override void WriteCoins() { }
        protected override void WriteItemList() { }
        protected override void WritePCItems() { }
        protected override void WriteItemPocket() { }
        protected override void WriteKeyItemPocket() { }
        protected override void WriteBallItemPocket() { }
        protected override void WriteTMCase() { }
        protected override void WriteBerryPocket() { }
    }
}