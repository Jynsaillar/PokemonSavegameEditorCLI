/*
DISCLAIMER:
The way that the Main(...) method is written is, from a programming POV, terrible.
In its current iteration it only serves as a quick-and-dirty debugging solution which also demonstrates one way of using this project in practice.
Later on all of this debugging code will likely moved into the appropriate .ToString() method for each class.
The goal is to keep the Main(...) method as short as possible with Debug prints and everything else being offloaded to method calls, e.g.

static void Main(string[] args){
VerifyCommandLineArguments(args);
var saveFile = ParseSaveFromFile(args[0]);
Console.WriteLine(saveFile.ToString());
ModifySaveFile(saveFile);
}

*/

using System;
using System.IO;
using PokemonSaves;

namespace PokemonSavegameEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            if (args.Length < 1)
            {
                Console.WriteLine("Not enough arguments provided (expected [1]: Path to Pokemon save file");
                return;
            }

            // These indices are declared based on the assumption that:
            // 1) The first argument contains the path to a FireRed or LeafGreen save file;
            // 2) The second argument contains the path to a Ruby or Sapphire save file;
            // 3) The third argument contains the path to an Emerald save file.
            // Modified ROMs will most likely use different offsets (e.g. the FireRed-based ROM hack LiquidCrystal does not use all of the FireRed offsets),
            // and as such they will probably not work.
            int fireRedLeafGreen = 0;
            int rubySapphire = 1;
            int emerald = 2;

            // Change this to change the game save that will be loaded.
            int gameid = fireRedLeafGreen;

            string filePath = args[gameid];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($@"
                        The file at path
                        {filePath}
                        does not exist!
                        If the path contains spaces, surround it with double quotes like this:
                        ""C:\My folder with spaces in it\firered.sav""
                        ");
            }

            using (var stream = File.OpenRead(filePath))
                using (var binaryReader = new BinaryReader(stream))
                {
                    var saveFile = new FireredLeafgreenSave(binaryReader);
                    // var saveFile = new RubySapphireSave(binaryReader);
                    // var saveFile = new EmeraldSave(binaryReader);

                    Console.WriteLine($"Loaded save {saveFile.GameName}, GEN: {saveFile.Generation}");

                    var activeSave = saveFile.GetActiveSave();
                    var trainerInfo = (TrainerInfoFRLG)activeSave.GetTrainerInfo(saveFile.GameID);
                    // var trainerInfo = (TrainerInfoRS)activeSave.GetTrainerInfo(saveFile.GameID);
                    // var trainerInfo = (TrainerInfoE)activeSave.GetTrainerInfo(saveFile.GameID);

                    // charset determines which character lookup table is used (Japanese characters or international alphabet and special symbols)
                    StringHelper.Charset charset = StringHelper.Charset.International;

                    Console.WriteLine($"PlayerName: {trainerInfo.PlayerName:X} = {StringHelper.GameStringToReadableString(trainerInfo.PlayerName, charset)}");
                    Console.WriteLine($"Gender: {(trainerInfo.PlayerGender == 0 ? "male" : "female")}");
                    Console.WriteLine($"TrainerID: {trainerInfo.TrainerID.PublicID:D5}");
                    Console.WriteLine($"SecretID: {trainerInfo.TrainerID.SecretID:D5}");

                    // Cast as the proper type depending on the game save file selected further above, e.g.
                    // (TeamAndItemsRS)activeSave.GetTeamAndItems(saveFile.GameID) if using a Ruby/Sapphire save file or
                    // (TeamAndItemsE)activeSave.GetTeamAndItems(saveFile.GameID) if using an Emerald save file.
                    var teamAndItems = (TeamAndItemsFRLG)activeSave.GetTeamAndItems(saveFile.GameID);
                    // var teamAndItems = (TeamAndItemsRS)activeSave.GetTeamAndItems(saveFile.GameID);
                    // var teamAndItems = (TeamAndItemsE)activeSave.GetTeamAndItems(saveFile.GameID);
                    Console.WriteLine($"Money: {teamAndItems.Money ^ trainerInfo.SecurityKey}");
                    // All item quantities not stored in the PC are encrypted and need to be decrypted with the lower 16 bits of the TrainerInfo.SecurityKey.
                    // The same applies to the Coins the player has.
                    var lowerSecurityKey = (ushort)(trainerInfo.SecurityKey & 0xFFFF);
                    Console.WriteLine($"Coins: {teamAndItems.Coins ^ lowerSecurityKey}");

                    int teamPokemonCounter = 1;
                    foreach (var pokemon in teamAndItems.TeamPokemonList)
                    {
                        Console.WriteLine($"Pokemon StartOffset: 0x{pokemon.StartOffset:X}");
                        var species = pokemon.PokemonData.Growth.Species;
                        var itemHeld = pokemon.PokemonData.Growth.ItemHeld;
                        var experience = pokemon.PokemonData.Growth.Experience;
                        var ppBonuses = pokemon.PokemonData.Growth.PPBonuses;
                        var friendship = pokemon.PokemonData.Growth.Friendship;
                        var unknown = pokemon.PokemonData.Growth.Unknown;
                        var otID = pokemon.OTID & 0xFFFF; // Extracts lower 16 bits as ushort, which represent the Original Trainer ID (OTID).
                        var move1 = pokemon.PokemonData.Attacks.Move1;
                        var move2 = pokemon.PokemonData.Attacks.Move2;
                        var move3 = pokemon.PokemonData.Attacks.Move3;
                        var move4 = pokemon.PokemonData.Attacks.Move4;
                        var pp1 = pokemon.PokemonData.Attacks.PP1;
                        var pp2 = pokemon.PokemonData.Attacks.PP2;
                        var pp3 = pokemon.PokemonData.Attacks.PP3;
                        var pp4 = pokemon.PokemonData.Attacks.PP4;
                        // Technically the following types are container types, so displaying their value unmodified is not really useful.
                        // See https://bulbapedia.bulbagarden.net/wiki/Pok%C3%A9mon_data_substructures_in_Generation_III#PP_bonuses for more detailed information.
                        var pokerusStatus = pokemon.PokemonData.Miscellaneous.PokerusStatus;
                        var metLocation = pokemon.PokemonData.Miscellaneous.MetLocation;
                        var originsInfo = pokemon.PokemonData.Miscellaneous.OriginsInfo;
                        var ivsEggAndAbility = pokemon.PokemonData.Miscellaneous.IVsEggAndAbility;
                        var ribbonsAndObedience = pokemon.PokemonData.Miscellaneous.RibbonsAndObedience;
                        var otName = StringHelper.GameStringToReadableString(pokemon.OTName, charset);
                        var nickname = StringHelper.GameStringToReadableString(pokemon.Nickname, charset);

                        // If the species is 000, this slot is either empty or occupied by a Pokemon with species ID #000 as a result of corrupted save data.
                        if(species == 0)
                        {
                            Console.WriteLine("<None>");
                            teamPokemonCounter++;
                            continue;
                        }

                        Console.WriteLine($@"Pokemon {teamPokemonCounter}: Species {species:D3}, ItemHeld {itemHeld}, Experience {experience}, PPBonuses {ppBonuses}, Friendship {friendship}, Unknown {unknown}, OTID {otID:D5}, OTName {otName}, Nickname: {nickname}");
                        Console.WriteLine($"PokerusStatus {pokerusStatus}, MetLocation {metLocation}, OriginsInfo {originsInfo}, Egg IVs/Ability {ivsEggAndAbility}, Ribbons/Obedience {ribbonsAndObedience}");
                        Console.WriteLine($"Moveset: Move1 {move1} PP1 {pp1}");
                        Console.WriteLine($"Moveset: Move2 {move2} PP2 {pp2}");
                        Console.WriteLine($"Moveset: Move3 {move3} PP3 {pp3}");
                        Console.WriteLine($"Moveset: Move4 {move4} PP4 {pp4}");

                        teamPokemonCounter++;
                    }

                    var pcItems = teamAndItems.PCItems;
                    foreach (var item in pcItems)
                    {
                        if ((item.ItemIndex & item.ItemQuantity) != 0) // Skips all empty items.
                        {
                            Console.WriteLine($"PCItem: Index {item.ItemIndex} ({item.ItemQuantity})");
                        }
                    }

                    var itemPocket = teamAndItems.ItemPocket;
                    foreach (var item in itemPocket)
                    {
                        var decryptedQuantity = (ushort)(item.ItemQuantity ^ lowerSecurityKey);
                        if ((item.ItemIndex & decryptedQuantity) != 0) // Skips all empty items.
                        {
                            Console.WriteLine($"Item in ItemPocket: Index {item.ItemIndex} ({decryptedQuantity})");
                        }
                    }

                    var keyItemPocket = teamAndItems.KeyItemPocket;
                    foreach (var item in keyItemPocket)
                    {
                        var decryptedQuantity = (ushort)(item.ItemQuantity ^ lowerSecurityKey);
                        if ((item.ItemIndex & decryptedQuantity) != 0) // Skips all empty items.
                        {
                            Console.WriteLine($"KeyItem in KeyItemPocket: Index {item.ItemIndex} ({decryptedQuantity})");
                        }
                    }

                    var ballItemPocket = teamAndItems.BallItemPocket;
                    foreach (var item in ballItemPocket)
                    {
                        var decryptedQuantity = (ushort)(item.ItemQuantity ^ lowerSecurityKey);
                        if ((item.ItemIndex & decryptedQuantity) != 0) // Skips all empty items.
                        {
                            Console.WriteLine($"Ball in BallItemPocket: Index {item.ItemIndex} ({decryptedQuantity})");
                        }
                    }

                    var tmCase = teamAndItems.TMCase;
                    foreach (var item in tmCase)
                    {
                        var decryptedQuantity = (ushort)(item.ItemQuantity ^ lowerSecurityKey);
                        if ((item.ItemIndex & decryptedQuantity) != 0) // Skips all empty items.
                        {
                            Console.WriteLine($"TM/HM in TMCase: Index {item.ItemIndex} ({decryptedQuantity})");
                        }
                    }

                    var berryPocket = teamAndItems.BerryPocket;
                    foreach (var item in berryPocket)
                    {
                        var decryptedQuantity = (ushort)(item.ItemQuantity ^ lowerSecurityKey);
                        if ((item.ItemIndex & decryptedQuantity) != 0) // Skips all empty items.
                        {
                            Console.WriteLine($"Berry in BerryPocket: Index {item.ItemIndex} ({decryptedQuantity})");
                        }
                    }
                }

            Console.WriteLine("Bye World!");
        }
    }
}
