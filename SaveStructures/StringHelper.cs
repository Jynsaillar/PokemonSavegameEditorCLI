using System;
using System.Text;

namespace PokemonSaves
{
    public static class StringHelper
    {
        public static readonly string[] CharacterMapInternational = new string[]
        {
            " ", "À", "Á", "Â", "Ç", "È", "É", "Ê", "Ë", "Ì", "", "Î", "Ï", "Ò", "Ó", "Ô",
            "Œ", "Ù", "Ú", "Û", "Ñ", "ß", "à", "á", "", "ç", "è", "é", "ê", "ë", "ì", "",
            "î", "ï", "ò", "ó", "ô", "œ", "ù", "ú", "û", "ñ", "º", "ª", "ᵉʳ", "&", "+", "",
            "", "", "", "", "Lv", "=", ";", "", "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
            "▯", "¿", "¡", "ᴾᴋ", "ᴹɴ", "ᴾᴏ", "ᴷé", "ʙʟ", "ᴏᴄ", "ᴋ", "Í", "%", "(", ")", " ", " ",
            " ", " ", " ", " ", " ", "", "", "", "â", "", "", "", "", "", "", "í",
            "", "", "", "", "", "", "", "", "", "⬆", "⬇", "⬅", "➡", "*", "*", "*",
            "*", "*", "*", "*", "ᵉ", "<", ">", "", "", "", "", "", "", "", "", "",
            "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "",
            "ʳᵉ", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "!", "?", ".", "-", "・",
            "...", "“", "”", "‘", "’", "♂", "♀", "ᴾᴏᴷéᴰᴏᴸʟᴬʀ", ",", "×", "/", "A", "B", "C", "D", "E",
            "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U",
            "V", "W", "X", "Y", "Z", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k",
            "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "▶",
            ":", "Ä", "Ö", "Ü", "ä", "ö", "ü", "", "", "", "", "", "", "", "", ""

        };

        public static readonly string[] CharacterMapJapanese = new string[]
        {

        };

        /// <summary>
        /// Strings in Pokemon saves have a maximum length of 8 bytes terminated by 0xFF (thus a name can contain 7 chars at most).
        /// <para/>Each byte maps to a character in a reference table - for the mapping, see either 
        /// <code>
        /// StringHelper.CharacterMapInternational
        /// </code>
        /// <para/>or https://bulbapedia.bulbagarden.net/wiki/Character_encoding_in_Generation_III#International.
        /// </summary>
        public static string GameStringToReadableString(long gameString, bool international)
        {
            byte[] gameStringBytes = BitConverter.GetBytes(gameString);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in gameStringBytes)
            {
                stringBuilder.Append(international ? CharacterMapInternational[b] : CharacterMapJapanese[b]);
            }
            return stringBuilder.ToString();
        }
    }
}