
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class shoot : UdonSharpBehaviour
{
    [SerializeField] public TextMeshProUGUI textBox; // get tmp objec
    //[SerializeField] public TextMeshProUGUI test;
    [SerializeField] private GameObject maw;
    public int shots = 0;
    public Dictionaries dictionary;
    public barManager manager;
    
    RaycastHit hit; // object the raycast hit

    void Awake(){

        dictionary = GameObject.Find("Dictionary").GetComponent<Dictionaries>();
        
    }
    // adjusts the hp and sp bar whenever someone picks up the evoker //
    public override void OnPickup(){
        string playerName = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.displayName;
        manager.updateValue("HP", int.Parse(Dictionaries.getStat(dictionary.self, playerName, "HP")), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max HP"))));
        manager.updateValue("SP", int.Parse(Dictionaries.getStat(dictionary.self, playerName, "SP")), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max SP"))));
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
        string skill = "Bash";
        var statUsed = Dictionaries.calculateDamage(dictionary, playerName, enemy, skill);
        if (statUsed != null){ // always hp or sp or null
            // updates the bars on the evoker //
            textBox.text = skill + " used.";
            manager.updateValue(statUsed, int.Parse(Dictionaries.getStat(dictionary.self, playerName, statUsed)), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max " + statUsed))));
        }
        if (Physics.Raycast(maw.transform.position, maw.transform.forward, out hit)){
            var hitObj = hit.collider.gameObject;
        }
    }
}
