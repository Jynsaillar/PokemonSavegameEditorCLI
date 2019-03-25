using System.IO;

namespace PokemonSaves
{
    public class TrainerInfoE : TrainerInfo
    {
        public new enum Offsets : long
        {
            PlayerName = 0x0000,
            PlayerGender = 0x0008,
            TrainerID = 0x000A,
            TimePlayed = 0x000E,
            Options = 0x0013,
            SecurityKey = 0x0AC
        }

        protected new void ReadGameCode(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            // GameCode is not used in Emerald, thus this method simply remains empty.
        }
    }
}