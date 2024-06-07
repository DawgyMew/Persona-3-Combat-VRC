using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.SDKBase;

public class PlayerStats : MonoBehaviour
{
    public VRCPlayerApi Player;
    
    private int PLV; // players current level
    private int HP; // health points
    private int SP; // mana
    private Persona persona; // object for the users persona

    // emppty constructor //
    public PlayerStats(){
        // vrchat way of keeping track of players
        // using level 34 koromaru for hp and sp because funny
        //PLV = 34;
        //HP = 354;
        //SP = 165;
        //persona = new Persona(); // auto assign to black frost
    }
    public Vector3 getPosition(){
        return Player.GetPosition();
    }
}
