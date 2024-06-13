
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
    
    public override void Interact(){
        //Debug.Log("meow");
        SendCustomNetworkEvent(NetworkEventTarget.All, nameof(Plink));
        Plink();
        //dictionaries.displayContents();
    }

    private void Plink(){
        dictionaries.displayPlayers();
    }
}
