
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3;
using VRC.Udon;

/*
    Class storing info for the players stats
*/
public class Persona : MonoBehaviour
{
    
    
    // persona exclusive information //
    private string pName;
    private int lvl;
    private int strength; // damage of physically attacking moves
    private int magic; // damage done by magic based attacks
    private int endurance; // defence
    private int agility; // decides turn order and accuracy
    private int luck; // :)

    
    private string[] weaknesses;
    private string[] nulls;
    private string[] strengths;
    private string[] skills;

    // constructor for black frost as a default // 
    public DataDictionary Persona(){
        // using him because he had an interesting set of moves and alright stats

        pName = "Black Frost";
        lvl = 34;
        strength = 29;
        magic = 31;
        endurance = 25;
        agility = 27;
        luck = 36;

        // Type Affinities //

        // slash bash pierce | Fire Ice Elec Wind Light Darkness
        // ---   ---   ---   | Str  Nul ---  ---  Wk   Str
        strengths = {"Fire", "Darkness"};
        nulls = {"Ice"};
        weaknesses = {"Light"};
        // Moves //
        skills = {"Mudo", "Agilao", "Bufula", "Marakunda", "Re Patra", "Ice Boost", "Rakukaja", "Trafuri"};
        // Mudo Agilao Bufula Marakunda Re Patra Ice Boost Rakukaja Trafuri
    }

    public string getName(){return pName;}
    public int getLvl(){return lvl;}
    public int getStr(){return strength;}
    public int getMg(){return magic;}
    public int getEn(){return endurance;}
    public int getAg(){return agility;}
    public int getLu(){return luck;}
    public string[] getStr{return strengths;}
    public string[] getNul{return nulls;}
    public string[] getWk{return weaknesses;}
    public string[] getMoves{return skills;}
}
