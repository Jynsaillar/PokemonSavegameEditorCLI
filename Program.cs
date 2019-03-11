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

            string filePath = args[0];
            if (!File.Exists(filePath))
            {
                Console.WriteLine($@"The file at path\n{filePath}\ndoes not exist!\n
                                    If the path contains spaces, surround it with brackets like this:\n
                                    ""C:\My folder with spaces in it\firered.sav""
                                    ");
            }

            var saveGame = new FireredLeafgreenSave();

            using (var stream = File.OpenRead(filePath))
            using (var binaryReader = new BinaryReader(stream))
            {
                // todo: save file parsing here
            }

            Console.WriteLine($"Loaded save {saveGame.GameName}, GEN: {saveGame.Generation}");

            Console.WriteLine("Bye World!");
        }
    }
}
