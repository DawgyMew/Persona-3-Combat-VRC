using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRC.SDKBase;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public VRCPlayerApi Player;
    [SerializeField]private GameObject evoker;
    [SerializeField]private TextMeshPro textBox; 
    
    [SerializeField]private int PLV; // players current level
    [SerializeField]private int HP; // health points
    [SerializeField]private int SP; // mana
    //[SerializeField]private Persona persona; // object for the users persona
    [SerializeField]private string username;

    // emppty constructor //
    public PlayerStats(){
        // vrchat way of keeping track of players
        // using level 34 koromaru for hp and sp because funny
        PLV = 34;
        HP = 354;
        SP = 165;
    }
    void Awake(){
        //persona = new Persona(); // auto assign to black frost
        Player = Networking.LocalPlayer;
        username = Player.displayName;
        changeText();
    }
    public void changeText(){
        textBox.text = getUser();
    }

    public string getUser(){return (this.username);}
}
