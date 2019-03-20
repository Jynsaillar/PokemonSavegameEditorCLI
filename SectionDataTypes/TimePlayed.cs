using System;
using System.IO;

namespace PokemonSaves
{
    public class TimePlayed : IBinaryParsable
    {
        private ushort _hours;
        private byte _minutes;
        private byte _seconds;
        private byte _frames;
        public ushort Hours { get => _hours; set => _hours = value; }
        public byte Minutes { get => _minutes; set => _minutes = value; }
        public byte Seconds { get => _seconds; set => _seconds = value; }

        // A frame is a sixtieth of a second, or in other terms, a third (minute, second, third, fourth).
        // Other terms for this are jiffy (think Commodore64/Vic 20) or Tick (general game development).
        public byte Frames { get => _frames; set => _frames = value; }

        public enum Offsets : long
        {
            Hours = 0x0000,
            Minutes = 0x0002,
            Seconds = 0x0003,
            Frames = 0x0004
        }

        public void ReadFromBinary(BinaryReader binaryReader)
        {
            long startOffset = binaryReader.BaseStream.Position;
            // Hours
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Hours, SeekOrigin.Begin);
            Hours = binaryReader.ReadUInt16();
            // Minutes
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Minutes, SeekOrigin.Begin);
            Minutes = binaryReader.ReadByte();
            // Seconds
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Seconds, SeekOrigin.Begin);
            Seconds = binaryReader.ReadByte();
            // Frames
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Frames, SeekOrigin.Begin);
            Frames = binaryReader.ReadByte();
        }
    }
}