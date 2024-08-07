
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
    
    public override void Interact(){
        //Debug.Log("meow");
        SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "Plink");
        //Plink();
        //dictionaries.displayContents();
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
