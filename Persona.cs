
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

/*
    Class storing info for the players stats
*/
public class Persona : MonoBehaviour
{
    
    
    // persona exclusive information //
    private string pName;
    private int strength; // damage of physically attacking moves
    private int magic; // damage done by magic based attacks
    private int endurance; // defence
    private int agility; // decides turn order and accuracy
    private int luck; // :)

    // TODO: make something that holds type affinities and move lists

    // constructor for black frost as a default // 
    public Persona(){
        // using him because he had an interesting set of moves and alright stats

        //pName = "Black Frost";
        //strength = 29;
        //magic = 31;
        //endurance = 25;
        //agility = 27;
        //luck = 36;

        // Type Affinities //
        // slash bash pierce | Fire Ice Elec Wind Light Dark
        // ---   ---   ---   | Str  Nul ---  ---  Wk   Str
        
        // Moves //
        // Mudo Agilao Bufula Marakunda Re Patra Ice Boost Rakukaja Trafuri
    }
}
