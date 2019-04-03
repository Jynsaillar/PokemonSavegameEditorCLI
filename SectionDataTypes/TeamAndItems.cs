using System.IO;
using System.Collections.Generic;

namespace PokemonSaves
{
    public abstract class TeamAndItems : SectionData, IBinaryParsable
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
            TeamSize = 0x0000,
            TeamPokemonList = 0x0000,
            Money = 0x0000,
            Coins = 0x0000,
            PCItems = 0x0000,
            ItemPocket = 0x0000,
            KeyItemPocket = 0x0000,
            BallItemPocket = 0x0000,
            TMCase = 0x0000,
            BerryPocket = 0x0000
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

        protected abstract void ReadTeamSize(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract void ReadTeamPokemonList(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract void ReadMoney(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract void ReadCoins(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        protected abstract List<Item> ReadItemList(BinaryReader binaryReader, long startOffset, GameIDs gameID, uint itemCount);

        protected abstract void ReadPCItems(BinaryReader binaryReader, long startOffset, GameIDs gameID);
        protected abstract void ReadItemPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID);
        protected abstract void ReadKeyItemPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID);
        protected abstract void ReadBallItemPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID);
        protected abstract void ReadTMCase(BinaryReader binaryReader, long startOffset, GameIDs gameID);
        protected abstract void ReadBerryPocket(BinaryReader binaryReader, long startOffset, GameIDs gameID);

        public virtual void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            ReadTeamSize(binaryReader, startOffset, gameID); // TeamSize
            ReadTeamPokemonList(binaryReader, startOffset, gameID); // TeamPokemonList
            ReadMoney(binaryReader, startOffset, gameID); // Money
            ReadCoins(binaryReader, startOffset, gameID); // Coins
            ReadPCItems(binaryReader, startOffset, gameID); // PCItems
            ReadItemPocket(binaryReader, startOffset, gameID); // ItemPocket
            ReadKeyItemPocket(binaryReader, startOffset, gameID); // KeyItemPocket
            ReadBallItemPocket(binaryReader, startOffset, gameID); // BallItemPocket
            ReadTMCase(binaryReader, startOffset, gameID); // TMCase
            ReadBerryPocket(binaryReader, startOffset, gameID); // BerryPocket
        }
    }
}