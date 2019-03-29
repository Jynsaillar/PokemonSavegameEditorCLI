using System.IO;

namespace PokemonSaves
{
    public class Miscellaneous
    {
        private byte _pokerusStatus;
        private byte _metLocation;
        private ushort _originsInfo;
        private uint _ivsEggAndAbility;
        private uint _ribbonsAndObedience;
        public byte PokerusStatus { get => _pokerusStatus; set => _pokerusStatus = value; }
        public byte MetLocation { get => _metLocation; set => _metLocation = value; }
        public ushort OriginsInfo { get => _originsInfo; set => _originsInfo = value; }
        public uint IVsEggAndAbility { get => _ivsEggAndAbility; set => _ivsEggAndAbility = value; }
        public uint RibbonsAndObedience { get => _ribbonsAndObedience; set => _ribbonsAndObedience = value; }

        public enum Offsets : long
        {
            PokerusStatus = 0x00,
            MetLocation = 0x01,
            OriginsInfo = 0x02,
            IVsEggAndAbility = 0x04,
            RibbonsAndObedience = 0x08
        }

        public Miscellaneous(byte pokerusStatus, byte metLocation, ushort originsInfo, uint ivsEggAndAbility, uint ribbonsAndObedience)
        {
            PokerusStatus = pokerusStatus;
            MetLocation = metLocation;
            OriginsInfo = originsInfo;
            IVsEggAndAbility = ivsEggAndAbility;
            RibbonsAndObedience = ribbonsAndObedience;
        }
    }
}