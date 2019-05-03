using System;
using System.IO;

namespace PokemonSaves
{
    public class TimePlayed : IBinaryParsable
    {
        private long _startOffset;
        private ushort _hours;
        private byte _minutes;
        private byte _seconds;
        private byte _frames;
        public long StartOffset { get => _startOffset; set => _startOffset = value; }
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
        protected void ReadHours(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Hours, SeekOrigin.Begin);
            Hours = binaryReader.ReadUInt16();
        }
        protected void ReadMinutes(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Minutes, SeekOrigin.Begin);
            Minutes = binaryReader.ReadByte();
        }
        protected void ReadSeconds(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Seconds, SeekOrigin.Begin);
            Seconds = binaryReader.ReadByte();
        }
        protected void ReadFrames(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.Frames, SeekOrigin.Begin);
            Frames = binaryReader.ReadByte();
        }

        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            StartOffset = binaryReader.BaseStream.Position;
            ReadHours(binaryReader, StartOffset, gameID);// Hours
            ReadMinutes(binaryReader, StartOffset, gameID);// Minutes
            ReadSeconds(binaryReader, StartOffset, gameID);// Seconds
            ReadFrames(binaryReader, StartOffset, gameID);// Frames
        }

        // Write functions:

        protected void WriteHours(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.Hours, SeekOrigin.Begin);
            binaryWriter.Write(Hours);
        }

        protected void WriteMinutes(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.Minutes, SeekOrigin.Begin);
            binaryWriter.Write(Minutes);
        }

        protected void WriteSeconds(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.Seconds, SeekOrigin.Begin);
            binaryWriter.Write(Seconds);
        }

        protected void WriteFrames(BinaryWriter binaryWriter, long startOffset)
        {
            binaryWriter.BaseStream.Seek(startOffset + (long)Offsets.Frames, SeekOrigin.Begin);
            binaryWriter.Write(Frames);
        }

        public void WriteToBinary(BinaryWriter binaryWriter)
        {
            WriteHours(binaryWriter, StartOffset); // Hours
            WriteMinutes(binaryWriter, StartOffset); // Minutes
            WriteSeconds(binaryWriter, StartOffset); // Seconds
            WriteFrames(binaryWriter, StartOffset); // Frames
        }
    }
}