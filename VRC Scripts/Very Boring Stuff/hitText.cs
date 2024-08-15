
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
// using VRC.Udon.Common.Interfaces;
public class hitText : UdonSharpBehaviour
{
    public Material miss;
    public Material weak;
    public Material crit;

    public ParticleSystem particle;
    public ParticleSystemRenderer particleRend;

    public void sendText(string text){
        if (particle != null){
            if (text.Equals("Crit") || text.Equals("Miss") || text.Equals("Weak")){ // make sure nothing else goes thru
                SendCustomNetworkEvent(VRC.Udon.Common.Interfaces.NetworkEventTarget.All, "display" + text);
            }
            else{
                Debug.Log("Not a valid material :3");
            }
        }
    }
    // store it like this for ease of sending along the network
    public void displayCrit(){
        particleRend.material = crit;  
        particle.Emit(1);
    }
    public void displayMiss(){
        particleRend.material = miss;  
        particle.Emit(1);
    }
    public void displayWeak(){
        particleRend.material = weak;  
        particle.Emit(1);
    }
}
