/*  
    Each class has an interface that corresponds to its name to ensure that
    all members of a save file type are implemented.
    Of course, to edit a savefile one could always simply calculate the offsets and
    modify data from there, but parsing a save in its entirety bears the merit
    of  being able to modify multiple attributes without having to recalculate, e.g.
    var activeSave = SaveFile.GetActiveSave();
    activeSave.GetTrainerInfo()
*/

using System;

namespace PokemonSaves
{
    interface IGameInfo
    {
        /// <value> Returns the generation of the Pokemon save game,\n
        // e.g. 3 for Gen III.</value>
        int Generation { get; }
        string GameName { get; }
        GameIDs GameID { get; }
    }

    /// <summary> Arbitrary game IDs to help determining offsets for save data etc. </summary>
    public enum GameIDs : uint
    {
        FireRedLeafGreen = 0,
        RubySapphire = 1,
        Emerald = 2
    }
}