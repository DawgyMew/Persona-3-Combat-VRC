
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
    public VRCPlayerApi player; 
    private Vector3 playerPos;
    public Dictionaries dictionary;
    
    RaycastHit hit; // object the raycast hit

    void Start(){
        if(maw == null || textBox == null){
            Debug.LogWarning("you forgot to assign something");
        }
        dictionary = GameObject.Find("Dictionary").GetComponent<Dictionaries>();
        
    }
    void Update(){
        //playerPos = player.GetPosition();
        //test.text = (playerPos.ToString());
    }
    // acts when the holder "shoots" the gun //
    public override void OnPickupUseDown(){
        fire(GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer); //player holding the evoker
    }

    
    // sends a raycast to check if the target will be hit //
    // helpful since this wont be actually shooting bullets :3 //
    private void fire(VRCPlayerApi player){
        dictionary.changeNum(player.displayName, "Shots", 1);
        if (Physics.Raycast(maw.transform.position, maw.transform.forward, out hit)){
            var hitObj = hit.collider.gameObject;
            textBox.text = hitObj.name;
        }
        else{
            textBox.text = "miss lol";
        }
    }
}
