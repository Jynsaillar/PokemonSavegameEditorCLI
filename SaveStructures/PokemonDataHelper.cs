using System;

namespace PokemonSaves
{
    public static class PokemonDataHelper
    {
        public static uint DetermineSubstructureOrderID(uint personalityValue)
        {
            return personalityValue % 24;
        }

        public static uint GetPokemonDataDecryptionKey(uint OTID, uint personalityValue)
        {
            return OTID ^ personalityValue;
        }

        public static (byte HPIV, byte AttackIV, byte DefenseIV, byte SpeedIV, byte SpecialAttackIV, byte SpecialDefenseIV, byte IsEgg, byte Ability) ExtractIVs(Miscellaneous miscellaneous)
        {
            var ivsEggAndAbility = miscellaneous.IVsEggAndAbility;
            byte hpIV = (byte)((ivsEggAndAbility >> 0) & 0x1F);
            byte attackIV = (byte)((ivsEggAndAbility >> 5) & 0x1F);
            byte defenseIV = (byte)((ivsEggAndAbility >> 10) & 0x1F);
            byte speedIV = (byte)((ivsEggAndAbility >> 15) & 0x1F);
            byte specialAttackIV = (byte)((ivsEggAndAbility >> 20) & 0x1F);
            byte specialDefenseIV = (byte)((ivsEggAndAbility >> 25) & 0x1F);
            byte isEgg = (byte)((ivsEggAndAbility >> 30) & 1);
            byte ability = (byte)((ivsEggAndAbility >> 31) & 1);
            return (hpIV, attackIV, defenseIV, speedIV, specialAttackIV, specialDefenseIV, isEgg, ability);
        }
    }
}