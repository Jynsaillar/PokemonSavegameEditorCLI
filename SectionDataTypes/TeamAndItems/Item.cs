using System.IO;

namespace PokemonSaves
{
    public class Item : IBinaryParsable
    {
        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // TODO: Implementation of Item.ReadFromBinary(...).
        }
    }
}