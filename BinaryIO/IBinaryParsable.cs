namespace PokemonSaves
{
    interface IBinaryParsable
    {
        void ReadFromBinary(System.IO.BinaryReader binaryReader);
    }
}