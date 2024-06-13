
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class spawnEvoker : UdonSharpBehaviour
{
    [SerializeField] private GameObject evoker;
    public override void OnPlayerJoined(VRCPlayerApi player){
        Instantiate (evoker, this.gameObject.transform.position, this.gameObject.transform.rotation);
    }
    
}
