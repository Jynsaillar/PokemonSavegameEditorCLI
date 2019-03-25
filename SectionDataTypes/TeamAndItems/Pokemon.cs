using System.IO;

namespace PokemonSaves
{
    public class Pokemon : IBinaryParsable
    {
        // TODO: Pokemon implementation.
        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOfffset = binaryReader.BaseStream.Position;
            // TODO: Implementation of Pokemon.ReadFromBinary(...).
        }
    }
}