
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Data;
using VRC.Udon;

/*
    Class storing info for the players stats
*/
public class Persona : MonoBehaviour
{
    // persona exclusive information //
    [SerializeField]public string pName;
    [SerializeField]public int lvl;
    [SerializeField]public int strength; // damage of physically attacking moves
    [SerializeField]public int magic; // damage done by magic based attacks
    [SerializeField]public int endurance; // defence
    [SerializeField]public int agility; // decides turn order and accuracy
    [SerializeField]public int luck; // :)

    

    [SerializeField]public string[] weaknesses;
    [SerializeField]public string[] nulls;
    [SerializeField]public string[] strengths;
    [SerializeField]public string[] skills;

    // constructor for black frost as a default // 
    public Persona(){
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
        strengths = new string[]{"Fire", "Darkness"};
        nulls = new string[]{"Ice"};
        weaknesses = new string[]{"Light"};
        // Moves //
        skills = new string[]{"Mudo", "Agilao", "Bufula", "Marakunda", "Re Patra", "Ice Boost", "Rakukaja", "Trafuri"};
        // Mudo Agilao Bufula Marakunda Re Patra Ice Boost Rakukaja Trafuri
    }
    
    /*
    public string getName(){return (this.pName);}
    public int getLvl(){return (this.lvl);}
    public int getSt(){return (this.strength);}
    public int getMg(){return (this.magic);}
    public int getEn(){return (this.endurance);}
    public int getAg(){return (this.agility);}
    public int getLu(){return (this.luck);}
    public string[] getStr(){return (this.strengths);}
    public string[] getNul(){return (this.nulls);}
    public string[] getWk(){return (this.weaknesses);}
    public string[] getMoves(){return (this.skills);}
    */
}
