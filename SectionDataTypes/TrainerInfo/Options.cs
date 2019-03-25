using System;
using System.IO;

namespace PokemonSaves
{
    public class Options : IBinaryParsable
    {
        private ButtonMode _buttonMode;
        private TextSpeed _textSpeed;
        private FrameStyle _frameStyle;
        private SoundMode _soundMode;
        private BattleStyle _battleStyle;
        private BattleScene _battleScene;
        public ButtonMode ButtonMode { get => _buttonMode; set => _buttonMode = value; }
        public TextSpeed TextSpeed { get => _textSpeed; set => _textSpeed = value; }
        public FrameStyle FrameStyle { get => _frameStyle; set => _frameStyle = value; }
        public SoundMode SoundMode { get => _soundMode; set => _soundMode = value; }
        public BattleStyle BattleStyle { get => _battleStyle; set => _battleStyle = value; }
        public BattleScene BattleScene { get => _battleScene; set => _battleScene = value; }
        public enum Offsets : long
        {
            ButtonMode = 0x0013,
            TextSpeedFrameStyle = 0x0014,
            SoundModeBattleStyleBattleScene = 0x0015
        }
        protected void ReadButtonMode(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.ButtonMode, SeekOrigin.Begin);
            ButtonMode = (ButtonMode)binaryReader.ReadByte();
        }
        protected void ReadTextSpeedFrameStyle(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.TextSpeedFrameStyle, SeekOrigin.Begin);
            byte textSpeedFrameStyleFlags = binaryReader.ReadByte();
            TextSpeed = OptionsHelper.ExtractTextSpeed(textSpeedFrameStyleFlags);
            FrameStyle = OptionsHelper.ExtractFrameStyle(textSpeedFrameStyleFlags);
        }
        protected void ReadSoundModeBattleStyleBattleScene(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            binaryReader.BaseStream.Seek(startOffset + (long)Offsets.SoundModeBattleStyleBattleScene, SeekOrigin.Begin);
            byte soundModeBattleStyleBattleSceneFlags = binaryReader.ReadByte();
            SoundMode = OptionsHelper.ExtractSoundMode(soundModeBattleStyleBattleSceneFlags);
            BattleStyle = OptionsHelper.ExtractBattleStyle(soundModeBattleStyleBattleSceneFlags);
            BattleScene = OptionsHelper.ExtractBattleScene(soundModeBattleStyleBattleSceneFlags);
        }

        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            ReadButtonMode(binaryReader, startOffset, gameID);// ButtonMode
            ReadTextSpeedFrameStyle(binaryReader, startOffset, gameID);// TextSpeed & FrameStyle
            ReadSoundModeBattleStyleBattleScene(binaryReader, startOffset, gameID);// SoundMode, BattleStyle & BattleScene
        }
    }
}