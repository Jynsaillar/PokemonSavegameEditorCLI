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
    }
}