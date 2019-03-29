using System.IO;

namespace PokemonSaves
{
    public class Item : IBinaryParsable
    {
        ushort _itemIndex;
        ushort _itemQuantity;
        public ushort ItemIndex { get => _itemIndex; set => _itemIndex = value; }
        public ushort ItemQuantity { get => _itemQuantity; set => _itemQuantity = value; }

        public enum Offsets : long
        {
            ItemIndex = 0x00,
            ItemQuantity = 0x02
        }

        protected void ReadItemIndexAndQuantity(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.ItemIndex, SeekOrigin.Begin);
            var item = binaryReader.ReadUInt32(); // Reads complete item into a single uint.
            ItemIndex = (ushort)(item >> 0); // Extracts lower two bytes from item as ItemIndex.
            ItemQuantity = (ushort)(item >> 16); // Extracts upper two bytes from item as ItemQuantity.
        }

        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            ReadItemIndexAndQuantity(binaryReader, startOffset, gameID); // ItemIndex and ItemQuantity
        }
    }
}