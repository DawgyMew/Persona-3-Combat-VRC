
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
        3. If no or low mp use melee attack/wait
        4. uh idk roll a dice?
            a. chance to just do nothing because funny

        Appendixes because plink:
            try to avoid known reflects
            i guess try to preserve sp sometimes

    */

    private static final int LOWHEALTH = 20;
    private static final int SKIPCHANCE = 10;

    // returns move name and targets //
    public static string[] determineMove(Dictionaries dict, string enemyName){
        // get dictionaries of self
        int ownID = Dictionaries.findID(dict, enemyName);
        DataDictionary ownDict = Dictionaries.getDict(dict.self, ownID); // get own dictionary
        string[] skills = Dictionaries.getArray(ownDict, enemyName, "Skills");
        int randNum = Random.Range(0, 100);
        
        string chosenSkillName = "Wait";
        DataDictionary chosenSkill = Dictionaries.getSkillInfo(dict, chosenSkillName);
        string target = enemyName;
        if (randNum > SKIPCHANCE){ // 10% chance to skip
            // ai priority.
            // what the enemy should focus on
            int priority = 0;
            int targetPriority = 0;
            int skillPriority = 0;
            /*
            Priority:
            0 - None
            1 - Heal 
            2 - Recover Ailment
            3 - Attack
            4 - Buff
            5 - Debuff

            Target Priority: 
            0 - Self
            1 - Allies
            2 - Players
            3 - Secret fourth option
            */
            int sp = ownDict["SP"].Integer;

            if (!ownDict["Ailment"].String.Equals("")){
                priority = 2;
                targetPriority = 0;
            }
            // TODO: check if allies are ailmented
            if ((ownDict["Max HP"].Integer / ownDict["HP"].Integer) < LOWHEALTH * 0.01){
                priority = 1;
                targetPriority = 0;
            }
            // TODO: check if allies are low


            foreach (string skill in skills){
                DataDictionary skill = Dictionaries.getSkillInfo(dict, skill);
                if (skill["Cost"] <= sp){
                    // get the priority class of the move
                    // compare against last move chosen
                    // if a better priority class
                }
            }
        }
        
        // 
        return new string[] {chosenSkill["Name"].String, target};
    }
    
    
}
