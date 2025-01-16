
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class reqTurnAttack : UdonSharpBehaviour
{   
    public turnLogic tl;
    public Material on;
    public Material off;
    
    public override void OnPlayerJoined(VRCPlayerApi player){
        if (tl.requireTurnToAttack){
            this.GetComponent<MeshRenderer> ().material = on;

        }
        else{
            this.GetComponent<MeshRenderer> ().material = off;
        }
    }

}
