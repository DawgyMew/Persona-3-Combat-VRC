
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class Capsule : UdonSharpBehaviour
{
    [SerializeField] public TextMeshPro spotForText; // get tmp object
    
    public override void Interact(){
        Debug.Log("meow");
        spotForText.text = "capsule";
    }
}
