using System;

namespace PokemonSaves
{
    public static class OptionsHelper
    {

        /// <summary>
        /// Joins multiple byte flags into a single byte flag using binary OR.
        /// </summary>
        /// <remarks>
        /// <code>
        /// byte helmetStyle = 0b10000000;
        /// byte armorStyle  = 0b00100000;
        /// byte weaponStyle = 0b00001000;
        /// byte shieldStyle = 0b00000010;
        /// byte combinedStyle = OptionsHelper.CombineOptions(helmetStyle, armorStyle, weaponStyle, shieldStyle);
        /// // combinedStyle = 0b10101010;
        /// </code>
        /// </remarks>
        public static byte CombineOptions(params byte[] options)
        {
            byte resultFlag = 0b00000000;
            foreach (byte flag in options)
            {
                resultFlag |= flag;
            }

            return resultFlag;
        }

        /// <summary>
        /// Combines a text speed flag and a frame style flag into a single byte (for an Options block).
        /// </summary>
        public static byte CombineTextSpeedFrameStyle(TextSpeed textSpeed, FrameStyle frameStyle)
        {
            return CombineOptions((byte)textSpeed, (byte)frameStyle);
        }

        /// <summary>
        /// Combines a sound mode flag, a battle style flag and a battle scene flag into a single byte (for an Options block).
        /// </summary>
        public static byte CombineSoundModeBattleStyleBattleScene(SoundMode soundMode, BattleStyle battleStyle, BattleScene battleScene)
        {
            return CombineOptions((byte)soundMode, (byte)battleStyle, (byte)battleScene);
        }

        public static TextSpeed ExtractTextSpeed(byte textSpeedFrameStyleFlags)
        {
            // TextSpeed is defined by the rightmost 3 bits, so we extract only those 3 bits.
            return (TextSpeed)(textSpeedFrameStyleFlags & 0b00000111);
        }

        public static FrameStyle ExtractFrameStyle(byte textSpeedFrameStyleFlags)
        {
            // FrameStyle is defined by the leftmost 5 bits, so we extract only those 5 bits.
            return (FrameStyle)(textSpeedFrameStyleFlags & 0b11111000);
        }

        public static SoundMode ExtractSoundMode(byte soundModeBattleStyleBattleSceneFlags)
        {
            // SoundMode is defined by the rightmost bit, so we extract only the rightmost bit.
            return (SoundMode)(soundModeBattleStyleBattleSceneFlags & 0b00000001);
        }

        public static BattleStyle ExtractBattleStyle(byte soundModeBattleStyleBattleSceneFlags)
        {
            // BattleStyle is defined by the second bit from the right, so we extract only that bit.
            return (BattleStyle)(soundModeBattleStyleBattleSceneFlags & 0b00000010);
        }

        public static BattleScene ExtractBattleScene(byte soundModeBattleStyleBattleSceneFlags)
        {
            // BattleScene is defined by the third bit from the right, so we extract only that bit.
            return (BattleScene)(soundModeBattleStyleBattleSceneFlags & 0b00000100);
        }

        // Conditional attribute flag exists to enable this method in Debug mode specifically.
        // Debug.WriteLine(...) would only be compiled in Debug mode, but the wrapper method would also be available in Release mode otherwise.
        [System.Diagnostics.Conditional("DEBUG")]
        public static void PrintByteFlagAsBinaryString(byte byteFlag)
        {
            System.Diagnostics.Debug.WriteLine(Convert.ToString(byteFlag, 2).PadLeft(8, '0'));
        }
    }
    /// <summary>
    /// The first byte of the Options block determines the button mode (rightmost bit).
    /// </summary>
    public enum ButtonMode : byte
    {
        NormalHelp = 0x01, // Normal in Ruby, Sapphire, Emerald, Help in FireRed, LeafGreen
        LR = 0x02,
        LA = 0x03 // L=A, pressing L has the same effect as pressing the A-button
    }

    /// <summary>
    /// The second byte of the Options block determines the text speed and the frame,
    /// with the rightmost 3 bits setting the text speed.
    /// </summary>
    public enum TextSpeed : byte
    {
        Slow = 0b00000000,
        Medium = 0b0000001,
        Fast = 0b00000010
    }

    /// <summary>
    /// The second byte of the Options block determines the text speed and the frame,
    /// with the leftmost 5 bits setting the frame style.
    /// <para/>In Ruby, Sapphire, Emerald there are 20 frames.
    /// <para/>In FireRed, LeafGreen there are 10 frames.
    /// </summary>
    /// <remarks>
    /// See this link for a list of Gen III frames: https://bulbapedia.bulbagarden.net/wiki/Options/Frames#Generation_III
    /// </remarks>
    public enum FrameStyle : byte
    {
        Frame01 = 0b00000000,
        Frame02 = 0b00001000,
        Frame03 = 0b00010000,
        Frame04 = 0b00011000,
        Frame05 = 0b00100000,
        Frame06 = 0b00101000,
        Frame07 = 0b00110000,
        Frame08 = 0b00111000,
        Frame09 = 0b01000000,
        Frame10 = 0b01001000,
        Frame11 = 0b01010000,
        Frame12 = 0b01011000,
        Frame13 = 0b01100000,
        Frame14 = 0b01101000,
        Frame15 = 0b01110000,
        Frame16 = 0b01111000,
        Frame17 = 0b10000000,
        Frame18 = 0b10001000,
        Frame19 = 0b10010000,
        Frame20 = 0b10011000
    }

    /// <summary>
    /// The third byte of the Options block determines
    /// <para/>sound mode setting (mono, stereo), 
    /// <para/>battle style (switch, set) and
    /// <para/>battle scene (on, off).
    /// <para/>The rightmost bit toggles the sound setting.
    /// </summary>
    public enum SoundMode : byte
    {
        Mono = 0b00000000,
        Stereo = 0b00000001,
    }

    /// <summary>
    /// The third byte of the Options block determines
    /// <para/>sound mode setting (mono, stereo), 
    /// <para/>battle style (switch, set) and
    /// <para/>battle scene (on, off).
    /// <para/>The second bit from the right toggles the battle style.
    /// </summary>
    public enum BattleStyle : byte
    {
        Switch = 0b00000000, // Default. Asks for switching Pokemon between each round.
        Set = 0b00000010 // Skips switching Pokemon (continuous battle).
    }

    /// <summary>
    /// The third byte of the Options block determines
    /// <para/>sound mode setting (mono, stereo), 
    /// <para/>battle style (switch, set) and
    /// <para/>battle scene (on, off).
    /// <para/>The third bit from the right toggles the battle scene on or off.
    /// </summary>
    public enum BattleScene : byte
    {
        On = 0b00000000,
        Off = 0b00000100
    }
}