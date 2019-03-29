using System.IO;

namespace PokemonSaves
{
    public class EVsAndConditions
    {
        private byte _hpEV;
        private byte _attackEV;
        private byte _defenseEV;
        private byte _speedEV;
        private byte _specialAttackEV;
        private byte _specialDefenseEV;
        private byte _coolness;
        private byte _beauty;
        private byte _cuteness;
        private byte _smartness;
        private byte _toughness;
        private byte _feel;
        public byte HPEV { get => _hpEV; set => _hpEV = value; }
        public byte AttackEV { get => _attackEV; set => _attackEV = value; }
        public byte DefenseEV { get => _defenseEV; set => _defenseEV = value; }
        public byte SpeedEV { get => _speedEV; set => _speedEV = value; }
        public byte SpecialAttackEV { get => _specialAttackEV; set => _specialAttackEV = value; }
        public byte SpecialDefenseEV { get => _specialDefenseEV; set => _specialDefenseEV = value; }
        public byte Coolness { get => _coolness; set => _coolness = value; }
        public byte Beauty { get => _beauty; set => _beauty = value; }
        public byte Cuteness { get => _cuteness; set => _cuteness = value; }
        public byte Smartness { get => _smartness; set => _smartness = value; }
        public byte Toughness { get => _toughness; set => _toughness = value; }
        public byte Feel { get => _feel; set => _feel = value; }

        public enum Offsets : long
        {
            HPEV = 0x00,
            AttackEV = 0x01,
            DefenseEV = 0x02,
            SpeedEV = 0x03,
            SpecialAttackEV = 0x04,
            SpecialDefenseEV = 0x05,
            Coolness = 0x06,
            Beauty = 0x07,
            Cuteness = 0x08,
            Smartness = 0x09,
            Toughness = 0x0A,
            Feel = 0x0B
        }

        public EVsAndConditions(byte hpEV, byte attackEV, byte defenseEV, byte speedEV, byte specialAttackEV, byte specialDefenseEV,
                                byte coolness, byte beauty, byte cuteness, byte smartness, byte toughness, byte feel)
        {
            HPEV = hpEV;
            AttackEV = attackEV;
            DefenseEV = defenseEV;
            SpeedEV = speedEV;
            SpecialAttackEV = specialAttackEV;
            SpecialDefenseEV = specialDefenseEV;
            Coolness = coolness;
            Beauty = beauty;
            Cuteness = cuteness;
            Smartness = smartness;
            Toughness = toughness;
            Feel = feel;
        }
    }
}