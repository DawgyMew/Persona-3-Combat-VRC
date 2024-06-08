
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class Capsule : UdonSharpBehaviour
{
    public Dictionaries dictionaries;
    
    public override void Interact(){
        //Debug.Log("meow");
        dictionaries.displayContents();
    }
}
