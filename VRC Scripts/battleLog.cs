
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using TMPro;

public class battleLog : UdonSharpBehaviour
{
    public TextMeshProUGUI textBox;
    [UdonSynced] public string displayText = "Battle Log: \n";
    public void addToLog(string newText){
        Networking.SetOwner(Networking.LocalPlayer, this.gameObject); // make the player who is adding to the log the owner | if its an enemy the master will be used here
        displayText += newText + "\n";
        textBox.text = displayText;
        SendCustomNetworkEvent(NetworkEventTarget.All, "rs");
    }
    // does the same thing as the other 2 
    public void rs(){RequestSerialization();}
    public override void OnDeserialization(){textBox.text = displayText;}
}
