
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class shoot : UdonSharpBehaviour
{
    //[SerializeField] public TextMeshProUGUI textBox; // get tmp objec
    //[SerializeField] public TextMeshProUGUI test;
    [SerializeField] private GameObject maw;
    public int shots = 0;
    public Dictionaries dictionary;
    public barManager manager;
    
    RaycastHit hit; // object the raycast hit

    // no more awake because the ACTUAL vrchat client was throwing a fit :)
    // isnt the most efficient to run it like this probably but plink
    // 

    // adjusts the hp and sp bar whenever someone picks up the evoker //
    public override void OnPickup(){
        if (GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.isLocal){
            var dictionaryGO = GameObject.Find("Dictionary");
            dictionary = (Dictionaries)dictionaryGO.GetComponent(typeof(UdonBehaviour));
            string playerName = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.displayName;
            manager.updateValue("HP", int.Parse(Dictionaries.getStat(dictionary.self, playerName, "HP")), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max HP"))));
            manager.updateValue("SP", int.Parse(Dictionaries.getStat(dictionary.self, playerName, "SP")), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max SP"))));
        }
    }
    // acts when the holder "shoots" the gun //
    public override void OnPickupUseDown(){
        fire(GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer); //player holding the evoker
    }

    
    // sends a raycast to check if the target will be hit //
    // helpful since this wont be actually shooting bullets :3 //
    private void fire(VRCPlayerApi player){
        // currently hard coded into only using bufula on enemy1 //
        string playerName = player.displayName;
        string enemy = "enemy1";
        string skill = "Gale Slash";
        var statUsed = Dictionaries.calculateDamage(dictionary, playerName, enemy, skill);
        //Debug.Log(statUsed);
        if (statUsed != null){ // always hp or sp or null
            // updates the bars on the evoker //
            //textBox.text = skill + " used on " + enemy + ".";
            manager.updateValue(statUsed, int.Parse(Dictionaries.getStat(dictionary.self, playerName, statUsed)), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max " + statUsed))));
        }
        
        /*
        // this segment of code has given me more problems just by sitting here so get sent to the comment jail >:D
        if (Physics.Raycast(maw.transform.position, maw.transform.forward, out hit)){
            var hitObj = hit.collider.gameObject;
        }
        */
    }
}
