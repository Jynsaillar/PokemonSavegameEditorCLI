using System.IO;

namespace PokemonSaves
{
    public class TrainerInfoRS : TrainerInfo
    {
        public new enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
            TimePlayed = 0x000E,
            Options = 0x0013,
            GameCode = 0x00AC
        }

        protected new void ReadSecurityKey(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // The SecurityKey is not used in Ruby/Sapphire, thus this method simply remains empty.
        }

    }
}