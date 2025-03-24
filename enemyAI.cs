
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Data;
using VRC.Udon.Common.Interfaces;

// this does not use the boring ai it is just a bunch of ifs :)
public class enemyAI : UdonSharpBehaviour
{
    /// Class that determines the moves of the enemies ///
    
    /*
        Guess I should write a priority list?
        1. Heal if low and can
            a. Heal status if possible
        2. If player is weak to a skill target them
            a. If player is downed and possible for one more target someone else
        3. If no or low mp use melee attack
        4. uh idk roll a dice?
            a. chance to just do nothing because funny

        Appendixes because plink:
            try to avoid known reflects
            i guess try to preserve sp

    */
    // returns move name and targets //
    public static string[] determineMove(Dictionaries dict, string enemyName){
        // get dictionaries of self
        int ownID = Dictionaries.findID(dict, enemyName);
        DataDictionary ownDict = Dictionaries.getDict(dict.self, ownID);

        
        // 
        return new string[] {"Dia", "Self"};
    }
    
    
}
