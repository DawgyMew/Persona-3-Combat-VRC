
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

// udon doesnt like OOP remember that dont forget it even
public class shoot : UdonSharpBehaviour
{
    [SerializeField] public TextMeshPro textBox; // get tmp objec
    [SerializeField] public TextMeshProUGUI test;
    [SerializeField] private GameObject maw;
    public int shots = 0;
    public VRCPlayerApi player; 
    private Vector3 playerPos;

    RaycastHit hit; // object the raycast hit
    

    void Start(){
        if(maw == null || textBox == null){
            Debug.LogWarning("you forgot to assign something");
        }
        player = Networking.LocalPlayer;
        
    }
    void Update(){
        playerPos = player.GetPosition();
        test.text = (playerPos.ToString());
    }
    // acts when the holder "shoots" the gun //
    public override void OnPickupUseDown(){
        fire();
    }

    
    // sends a raycast to check if the target will be hit //
    // helpful since this wont be actually shooting bullets :3 //
    private void fire(){
        
        if (Physics.Raycast(maw.transform.position, maw.transform.forward, out hit)){
            if (hit.collider.gameObject.name == "wall"){
                textBox.text = "good job";
            }
            else{
                textBox.text = "Hit " + hit.collider.gameObject.name;
            }
            
        }
        else{
            textBox.text = "miss lol";
        }
    }
}
