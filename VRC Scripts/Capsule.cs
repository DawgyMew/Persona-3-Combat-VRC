﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using TMPro;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
// test item for testing //
public class Capsule : UdonSharpBehaviour
{
    public Dictionaries dictionaries;
    public GameObject syncText;
    public GameObject syncText2;
    [UdonSynced] private bool textShown;

    public hitText HT;
    public updateText UT;
    public networking network;

    private int ailNum = 0;
    

    public override void Interact(){
        //Debug.Log("meow");
        
        //Plink();
        //dictionaries.displayContents();
        //updateText.enemyHitText("enemy1", "Miss");
        //updateText.changeEnemyText("enemy1", "plonk");

        // poison the player for the funny
        var player = Networking.LocalPlayer;
        //Debug.Log(player.displayName);
        ailNum = (ailNum + 1) % dictionaries.AILMENTS.Length; 
        string ailment = dictionaries.AILMENTS[ailNum];
        network.statFromArrayIndexO(dictionaries, player.displayName, "Ailment", 0, ailment, player);
        Dictionaries.setStat(dictionaries.self, player.displayName, "Ailment", ailment);

        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Plink");
    }

    // this needed to be public .-.
    public void Plink(){
        textShown = !textShown;
        syncText.SetActive(textShown);
        syncText2.SetActive(textShown);
        dictionaries.displayPlayers();
    }
    public override void OnDeserialization(){
        syncText.SetActive(textShown);
        syncText2.SetActive(textShown);
    }
}
