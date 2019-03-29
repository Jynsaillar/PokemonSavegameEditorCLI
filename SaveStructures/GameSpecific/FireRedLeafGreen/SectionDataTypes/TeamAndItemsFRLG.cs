using System.IO;
using System.Collections.Generic;

namespace PokemonSaves
{
    public partial class TeamAndItemsFRLG : TeamAndItems
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
    }
}