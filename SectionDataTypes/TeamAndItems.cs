using System.IO;
using System.Collections.Generic;

namespace PokemonSaves
{
    public partial class TeamAndItems : SectionData, IBinaryParsable
    {
        private uint _teamSize; // Capped at 6.
        private List<Pokemon> _teamPokemonList; // 600 bytes / 100 bytes per Pokemon entry = 6 Pokemon max in team
        private uint _money; // Capped at 999 999 PokeDollar.
        private ushort _coins;
        private List<Item> _pcItems;
        private List<Item> _itemPocket;
        private List<Item> _keyItemPocket;
        private List<Item> _ballItemPocket;
        private List<Item> _tmCase;
        private List<Item> _berryPocket;

        public uint TeamSize { get => _teamSize; set => _teamSize = value; }
        public List<Pokemon> TeamPokemonList { get => _teamPokemonList; set => _teamPokemonList = value; }
        public uint Money { get => _money; set => _money = value; }
        public ushort Coins { get => _coins; set => _coins = value; }
        public List<Item> PCItems { get => _pcItems; set => _pcItems = value; }
        public List<Item> ItemPocket { get => _itemPocket; set => _itemPocket = value; }
        public List<Item> KeyItemPocket { get => _keyItemPocket; set => _keyItemPocket = value; }
        public List<Item> BallItemPocket { get => _ballItemPocket; set => _ballItemPocket = value; }
        public List<Item> TMCase { get => _tmCase; set => _tmCase = value; }
        public List<Item> BerryPocket { get => _berryPocket; set => _berryPocket = value; }

        public enum Offsets : long
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

        public TeamAndItems()
        {
            TeamPokemonList = new List<Pokemon>();
            PCItems = new List<Item>();
            ItemPocket = new List<Item>();
            KeyItemPocket = new List<Item>();
            BallItemPocket = new List<Item>();
            TMCase = new List<Item>();
            BerryPocket = new List<Item>();
        }

        protected void ReadTeamSize(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TeamSize, SeekOrigin.Begin);
            TeamSize = binaryReader.ReadUInt32();
        }

        protected void ReadTeamPokemonList(BinaryReader binaryReader, long startOffset, GameIDs gameID)
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

        protected void ReadMoney(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Money, SeekOrigin.Begin);
            Money = binaryReader.ReadUInt32();
        }

        protected void ReadCoins(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Coins, SeekOrigin.Begin);
            Coins = binaryReader.ReadUInt16();
        }

        protected void ReadPCItems(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.PCItems, SeekOrigin.Begin);
            var pcItemCount = (uint)((Offsets.ItemPocket - Offsets.PCItems) / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            for (int i = 0; i < pcItemCount; i++)
            {
                var item = new Item();
                item.ReadFromBinary(binaryReader, gameID);
                PCItems.Add(item);
            }
        }
        protected void ReadItemPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.ItemPocket, SeekOrigin.Begin);
            var itemPocketCount = (uint)((Offsets.KeyItemPocket - Offsets.ItemPocket) / sizeof(uint)); // Item contains two ushorts, so each item is 4 bytes in size.
            for (int i = 0; i < itemPocketCount; i++)
            {
                var item = new Item();
                item.ReadFromBinary(binaryReader, gameID);
                ItemPocket.Add(item);
            }
        }
        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // TODO: TeamAndItems.ReadFromBinary(...) implementation.
            ReadTeamSize(binaryReader, startOffset, gameID); // TeamSize
            ReadTeamPokemonList(binaryReader, startOffset, gameID); // TeamPokemonList
            ReadMoney(binaryReader, startOffset, gameID); // Money
            ReadCoins(binaryReader, startOffset, gameID); // Coins
            ReadPCItems(binaryReader, startOffset, gameID); // PCItems
            ReadItemPocket(binaryReader, startOffset, gameID); // ItemPocket
        }
    }
}