namespace PokemonSaves
{
    interface IBinaryParsable
    {
        long StartOffset { get; set; }
        void ReadFromBinary(System.IO.BinaryReader binaryReader, GameIDs gameID);
    }
}