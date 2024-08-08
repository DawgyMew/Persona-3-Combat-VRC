
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class showMenu : UdonSharpBehaviour
{
    
    public GameObject menu;
    
    public override void OnPickup(){
        if (GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.isLocal){// only displays the menu for the local user
            VRC_Pickup vrc_pickup = GetComponent<VRC.SDKBase.VRC_Pickup>(); // get the vrc pickup script to use
            var hand = vrc_pickup.currentHand; // print the hand that is holding the evoker
            
            menu.SetActive(true); // enable the menu

            int dirMult = 1;
            // move the menu based on which hand is holding it //
            if (hand == VRC_Pickup.PickupHand.Left){ // makes it match what current hand returns
                dirMult = -1;
            }
            menu.transform.localPosition = new Vector3(.29f * dirMult, .12f, 0);
        }
    }
    public override void OnDrop(){
        menu.SetActive(false);
    }

    // TODO: make the menu take user input by connecting it to the left or right stick
}
