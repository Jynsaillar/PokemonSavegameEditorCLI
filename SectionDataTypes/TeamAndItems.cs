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
        private List<Item> _pcItems; // 120 bytes / 4 bytes per item = 30 items max in PC
        private List<Item> _itemPocket; // 168 bytes / 4 bytes per item = 42 items max in item pocket
        private List<Item> _keyItemPocket; // 120 bytes / 4 bytes per key item = 30 key items max in key item pocket
        private List<Item> _ballItemPocket; // 52 bytes / 4 bytes per ball = 13 unique balls max in ball pocket
        private List<Item> _tmCase; // 232 bytes / 4 bytes per TM/HM = 50 TMs + 8 HMs max in TM/HM Case
        private List<Item> _berryPocket; // 172 bytes / 4 bytes per berry = 43 unique berries max in berry pocket

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

        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // TODO: TeamAndItems.ReadFromBinary(...) implementation.
        }
    }
}