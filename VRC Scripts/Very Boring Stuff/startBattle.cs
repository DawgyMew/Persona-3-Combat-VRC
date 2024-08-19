
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class startBattle : UdonSharpBehaviour
{
    public turnLogic TL;
    public override void Interact(){
        this.gameObject.SetActive(false);
        TL.startBattle();
    }
}
