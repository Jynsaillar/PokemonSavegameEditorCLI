using System.IO;

namespace PokemonSaves
{
    public class PokemonData
    {
        private Pokemon _owningPokemon;
        private Growth _growth;
        private Attacks _attacks;
        private EVsAndConditions _evsAndConditions;
        private Miscellaneous _miscellaneous;
        public Pokemon OwningPokemon { get => _owningPokemon; private set => _owningPokemon = value; }
        public Growth Growth { get => _growth; set => _growth = value; }
        public Attacks Attacks { get => _attacks; set => _attacks = value; }
        public EVsAndConditions EVsAndConditions { get => _evsAndConditions; set => _evsAndConditions = value; }
        public Miscellaneous Miscellaneous { get => _miscellaneous; set => _miscellaneous = value; }

        public PokemonData()
        {
            System.Console.WriteLine("WARNING: Unexpected parameterless constructor call for class PokemonData, are you certain that you wanted to use this instead of PokemonData(Pokemon owningPokemon)?");
        }
        /// <summary>
        /// To determine the order of parsing Growth, Attacks, EVsAndConditions as well as Miscellaneous,
        /// the personality value of the Pokemon which possesses this PokemonData substructure is needed.
        /// This is why there is a constructor taking a Pokemon object which is
        /// called when the surrounding Pokemon object parses its PokemonData.
        /// </summary>
        public PokemonData(Pokemon owningPokemon)
        {
            OwningPokemon = owningPokemon;
        }
        protected void ReadGrowth(BinaryReader binaryReader, ref long startOffset, GameIDs gameID)
        {
            var decryptedSubstructure = ReadAndDecryptEncryptedSubstructure(binaryReader, startOffset, gameID);
            ExtractGrowthFromDecryptedSubstructure(decryptedSubstructure);
            // Substructure is 12 bytes in size and since the order of the substructures is random, the start offset is incremented for the next substructure read.
            startOffset += 12;
        }
        protected void ReadAttacks(BinaryReader binaryReader, ref long startOffset, GameIDs gameID)
        {
            var decryptedSubstructure = ReadAndDecryptEncryptedSubstructure(binaryReader, startOffset, gameID);
            ExtractAttacksFromDecryptedSubstructure(decryptedSubstructure);
            // Substructure is 12 bytes in size and since the order of the substructures is random, the start offset is incremented for the next substructure read.
            startOffset += 12;
        }
        protected void ReadEVsAndConditions(BinaryReader binaryReader, ref long startOffset, GameIDs gameID)
        {
            var decryptedSubstructure = ReadAndDecryptEncryptedSubstructure(binaryReader, startOffset, gameID);
            ExtractEVsAndConditionsFromDecryptedSubstructure(decryptedSubstructure);
            // Substructure is 12 bytes in size and since the order of the substructures is random, the start offset is incremented for the next substructure read.
            startOffset += 12;
        }
        protected void ReadMiscellaneous(BinaryReader binaryReader, ref long startOffset, GameIDs gameID)
        {
            var decryptedSubstructure = ReadAndDecryptEncryptedSubstructure(binaryReader, startOffset, gameID);
            ExtractMiscellaneousFromDecryptedSubstructure(decryptedSubstructure);
            // Substructure is 12 bytes in size and since the order of the substructures is random, the start offset is incremented for the next substructure read.
            startOffset += 12;
        }

        protected uint[] ReadAndDecryptEncryptedSubstructure(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            uint decryptionKey = PokemonDataHelper.GetPokemonDataDecryptionKey(OwningPokemon.OTID, OwningPokemon.PersonalityValue);
            uint[] decryptedSubstructure = new uint[3];

            binaryReader.BaseStream.Seek(startOffset, SeekOrigin.Begin);
            for (int i = 0; i < 3; i++)
            {
                decryptedSubstructure[i] = binaryReader.ReadUInt32() ^ decryptionKey;
            }

            return decryptedSubstructure;
        }

        protected void ExtractGrowthFromDecryptedSubstructure(uint[] decryptedSubstructure)
        {
            ushort species = (ushort)(decryptedSubstructure[0] >> 0); // Extracts lower two bytes as Species.
            ushort itemHeld = (ushort)(decryptedSubstructure[0] >> 16); // Extracts upper two bytes as ItemHeld.
            uint experience = decryptedSubstructure[1]; // Passes second uint unmodified since Experience is a uint as well.
            byte ppBonuses = (byte)(decryptedSubstructure[2] >> 0); // Extracts first byte as PPBonuses.
            byte friendship = (byte)(decryptedSubstructure[2] >> 8); // Extracts second byte as Friendship.
            ushort unknown = (ushort)(decryptedSubstructure[2] >> 16); // Extracts third and fourth byte as Unknown.

            Growth = new Growth(species, itemHeld, experience, ppBonuses, friendship, unknown);
        }

        protected void ExtractAttacksFromDecryptedSubstructure(uint[] decryptedSubstructure)
        {
            ushort move1 = (ushort)(decryptedSubstructure[0] >> 0); // Extracts lower two bytes as Move1.
            ushort move2 = (ushort)(decryptedSubstructure[0] >> 16); // Extracts upper two bytes as Move2.
            ushort move3 = (ushort)(decryptedSubstructure[1] >> 0); // Extracts lower two bytes as Move3.
            ushort move4 = (ushort)(decryptedSubstructure[1] >> 16); // Extracts upper two bytes as Move4.

            byte pp1 = (byte)(decryptedSubstructure[2] >> 0); // Extracts first byte as PP1.
            byte pp2 = (byte)(decryptedSubstructure[2] >> 8); // Extracts second byte as PP2.
            byte pp3 = (byte)(decryptedSubstructure[2] >> 16); // Extracts third byte as PP3.
            byte pp4 = (byte)(decryptedSubstructure[2] >> 24); // Extracts fourth byte as PP4.

            Attacks = new Attacks(move1, move2, move3, move4, pp1, pp2, pp3, pp4);
        }

        protected void ExtractEVsAndConditionsFromDecryptedSubstructure(uint[] decryptedSubstructure)
        {
            byte hpEV = (byte)(decryptedSubstructure[0] >> 0); // Extracts first byte as HPEV.
            byte attackEV = (byte)(decryptedSubstructure[0] >> 8); // Extracts second byte as AttackEV.
            byte defenseEV = (byte)(decryptedSubstructure[0] >> 16); // Extracts third byte as DefenseEV.
            byte speedEV = (byte)(decryptedSubstructure[0] >> 24); // Extracts fourth byte as SpeedEV.
            byte specialAttackEV = (byte)(decryptedSubstructure[1] >> 0); // Extracts first byte as SpecialAttackEV.
            byte specialDefenseEV = (byte)(decryptedSubstructure[1] >> 8); // Extracts second byte as SpecialDefenseEV.

            byte coolness = (byte)(decryptedSubstructure[1] >> 16); // Extracts third byte as Coolness.
            byte beauty = (byte)(decryptedSubstructure[1] >> 24); // Extracts fourth byte as Beauty.
            byte cuteness = (byte)(decryptedSubstructure[2] >> 0); // Extracts first byte as Cuteness.
            byte smartness = (byte)(decryptedSubstructure[2] >> 8); // Extracts second byte as Smartness.
            byte toughness = (byte)(decryptedSubstructure[2] >> 16); // Extracts third byte as Toughness.
            byte feel = (byte)(decryptedSubstructure[2] >> 24); // Extracts fourth byte as Feel.

            EVsAndConditions = new EVsAndConditions(hpEV, attackEV, defenseEV, speedEV, specialAttackEV, specialDefenseEV,
                                                    coolness, beauty, cuteness, smartness, toughness, feel);
        }

        protected void ExtractMiscellaneousFromDecryptedSubstructure(uint[] decryptedSubstructure)
        {
            byte pokerusStatus = (byte)(decryptedSubstructure[0] >> 0); // Extracts first byte as PokerusStatus.
            byte metLocation = (byte)(decryptedSubstructure[0] >> 8); // Extracts second byte as MetLocation.
            ushort originsInfo = (ushort)(decryptedSubstructure[0] >> 16); // Extracts third and fourth byte as OriginsInfo.
            uint ivsEggAndAbility = decryptedSubstructure[1]; // Passes second uint unmodified since IVsEggAndAbility is a uint as well.
            uint ribbonsAndObedience = decryptedSubstructure[2]; // Passes third uint unmodified since RibbonsAndObedience is a uint as well.

            Miscellaneous = new Miscellaneous(pokerusStatus, metLocation, originsInfo, ivsEggAndAbility, ribbonsAndObedience);
        }

        /// <summary>
        /// Parses PokemonData.Growth, Attacks, Miscellaneous, EVsAndConditions (GAME)
        /// in the right order (determined by a Pokemon's personality value modulo 24).
        /// </summary>
        private void ParseGAMEInProperOrder(BinaryReader binaryReader, long startOffset, GameIDs gameID)
        {
            if (null == OwningPokemon)
            {
                System.Console.WriteLine("WARNING: OwningPokemon in PokemonData not set, skipping the parsing of Growth, Attacks, EVsAndConditions and Miscellaneous.");
                return;
            }
            uint orderID = PokemonDataHelper.DetermineSubstructureOrderID(OwningPokemon.PersonalityValue);

            switch (orderID)
            {
                case 00:
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    break;
                case 01:
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    break;
                case 02:
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    break;
                case 03:
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    break;
                case 04:
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    break;
                case 05:
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    break;
                case 06:
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    break;
                case 07:
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    break;
                case 08:
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    break;
                case 09:
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    break;
                case 10:
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    break;
                case 11:
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    break;
                case 12:
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    break;
                case 13:
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    break;
                case 14:
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    break;
                case 15:
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    break;
                case 16:
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    break;
                case 17:
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    break;
                case 18:
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    break;
                case 19:
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    break;
                case 20:
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    break;
                case 21:
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    break;
                case 22:
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    break;
                case 23:
                    ReadMiscellaneous(binaryReader, ref startOffset, gameID);
                    ReadEVsAndConditions(binaryReader, ref startOffset, gameID);
                    ReadAttacks(binaryReader, ref startOffset, gameID);
                    ReadGrowth(binaryReader, ref startOffset, gameID);
                    break;
            }
        }

        public void ReadFromBinary(BinaryReader binaryReader, GameIDs gameID)
        {
            long startOffset = binaryReader.BaseStream.Position;
            ParseGAMEInProperOrder(binaryReader, startOffset, gameID); // Growth, Attacks, Miscellaneous, EVsAndConditions (GAME)
        }
    }
}