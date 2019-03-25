using System.IO;

namespace PokemonSaves
{
    public class TrainerInfoFRLG : TrainerInfo
    {
        public new enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
            TimePlayed = 0x000E,
            Options = 0x0013,
            GameCode = 0x00AC,
            SecurityKey = 0x0AF8
        }

    }
}