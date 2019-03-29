using System.IO;

namespace PokemonSaves
{
    public class Attacks
    {
        private ushort _move1;
        private ushort _move2;
        private ushort _move3;
        private ushort _move4;
        private byte _pp1;
        private byte _pp2;
        private byte _pp3;
        private byte _pp4;
        public ushort Move1 { get => _move1; set => _move1 = value; }
        public ushort Move2 { get => _move2; set => _move2 = value; }
        public ushort Move3 { get => _move3; set => _move3 = value; }
        public ushort Move4 { get => _move4; set => _move4 = value; }
        public byte PP1 { get => _pp1; set => _pp1 = value; }
        public byte PP2 { get => _pp2; set => _pp2 = value; }
        public byte PP3 { get => _pp3; set => _pp3 = value; }
        public byte PP4 { get => _pp4; set => _pp4 = value; }

        public enum Offsets : long
        {
            Move1 = 0x00,
            Move2 = 0x02,
            Move3 = 0x04,
            Move4 = 0x06,
            PP1 = 0x08,
            PP2 = 0x09,
            PP3 = 0x0A,
            PP4 = 0x0B
        }

        public Attacks(ushort move1, ushort move2, ushort move3, ushort move4, byte pp1, byte pp2, byte pp3, byte pp4)
        {
            Move1 = move1;
            Move2 = move2;
            Move3 = move3;
            Move4 = move4;
            PP1 = pp1;
            PP2 = pp2;
            PP3 = pp3;
            PP4 = pp4;
        }
    }
}