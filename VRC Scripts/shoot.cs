
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class shoot : UdonSharpBehaviour
{
    [SerializeField] public TextMeshPro textBox; // get tmp objec
    //[SerializeField] public TextMeshProUGUI test;
    [SerializeField] private GameObject maw;
    public int shots = 0;
    public Dictionaries dictionary;
    
    RaycastHit hit; // object the raycast hit

    void Awake(){

        dictionary = GameObject.Find("Dictionary").GetComponent<Dictionaries>();
        
    }
    // acts when the holder "shoots" the gun //
    public override void OnPickupUseDown(){
        fire(GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer); //player holding the evoker
    }

    
    // sends a raycast to check if the target will be hit //
    // helpful since this wont be actually shooting bullets :3 //
    private void fire(VRCPlayerApi player){
        // currently hard coded into only using bufula on enemy1 //
        string enemy = "enemy1";
        string skill = "Bufula";
        Dictionaries.calculateDamage(dictionary, player.displayName, enemy, skill);
        if (Physics.Raycast(maw.transform.position, maw.transform.forward, out hit)){
            var hitObj = hit.collider.gameObject;
            textBox.text = hitObj.name;
        }
        else{
            textBox.text = "miss lol";
        }
    }
}
