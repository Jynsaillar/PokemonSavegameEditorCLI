using System.IO;

namespace PokemonSaves
{
    public class Growth
    {
        private ushort _species;
        private ushort _itemHeld;
        private uint _experience;
        private byte _ppBonuses;
        private byte _friendship;
        private ushort _unknown;
        public ushort Species { get => _species; set => _species = value; }
        public ushort ItemHeld { get => _itemHeld; set => _itemHeld = value; }
        public uint Experience { get => _experience; set => _experience = value; }
        public byte PPBonuses { get => _ppBonuses; set => _ppBonuses = value; }
        public byte Friendship { get => _friendship; set => _friendship = value; }
        public ushort Unknown { get => _unknown; set => _unknown = value; }

        public enum Offsets : long
        {
            Species = 0x00,
            ItemHeld = 0x02,
            Experience = 0x04,
            PPBonuses = 0x06,
            Friendship = 0x08,
            Unknown = 0x0A
        }

        public Growth(ushort species, ushort itemHeld, uint experience, byte ppBonuses, byte friendship, ushort unknown)
        {
            Species = species;
            ItemHeld = itemHeld;
            Experience = experience;
            PPBonuses = ppBonuses;
            Friendship = friendship;
            Unknown = unknown;
        }
    }
}