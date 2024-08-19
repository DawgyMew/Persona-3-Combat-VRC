
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
    public Dictionaries dictionary;
    public displaySkills ds;
    public selectEnemy se;
    public networking ne;
    public barManager manager;
    public int skillSel = 0; // the current index of the skill to select
    public int enemySel = 0; // the current enemy to target
    RaycastHit hit; // object the raycast hit
    private int moveTimeUsed = 0; // use to add a buffer for the change of moves
    //private int enemySelTime = 0; // make the jump not run twice
    private int timeToWait = 100; //ms

    public TextMeshProUGUI plonk;

    // no more awake because the ACTUAL vrchat client was throwing a fit :)
    // isnt the most efficient to run it like this probably but plink
    // 

    // adjusts the hp and sp bar whenever someone picks up the evoker //
    public override void OnPickup(){
        var player = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer;
        if (player.isLocal){
            var obj = GameObject.Find("Dictionary");
            dictionary = (Dictionaries)obj.GetComponent(typeof(UdonBehaviour));
            
            string playerName = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.displayName;
            manager.updateValue("HP", int.Parse(Dictionaries.getStat(dictionary.self, playerName, "HP")), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max HP"))));
            manager.updateValue("SP", int.Parse(Dictionaries.getStat(dictionary.self, playerName, "SP")), int.Parse(Dictionaries.getStat(dictionary.self, playerName, ("Max SP"))));

            obj = GameObject.Find("enemySelection");
            se = (selectEnemy)obj.GetComponent(typeof(UdonBehaviour));

            obj  = GameObject.Find("Networking");
            ne = (networking)obj.GetComponent(typeof(UdonBehaviour));

            Networking.SetOwner(player, this.gameObject); // set the owner of the evoker to the one holding it
            moveTimeUsed = Networking.GetServerTimeInMilliseconds(); // add this so if the server time is in the negatives (for some reason) it should still work!
        }
    }
    // acts when the holder "shoots" the gun //
    public override void OnPickupUseDown(){
        fire(GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer); //player holding the evoker
        GetComponent<VRC.SDKBase.VRC_Pickup>().PlayHaptics();
    }
    
    /// Move Choosing ///
    // uses the right stick in vr to change which move the user has selected //
    // sucks in flat but i might fix that later lel
    public override void InputLookVertical(float value, VRC.Udon.Common.UdonInputEventArgs args){
        //Debug.Log("Last Time Used: " + moveTimeUsed + " | Time - " + timeToWait + ": " + (Networking.GetServerTimeInMilliseconds() - timeToWait));
        var change = 0;
        var player = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer;
        if (player != null){
            if (true){
                //plonk.text = (Networking.GetServerTimeInMilliseconds() - timeToWait) + " - " + moveTimeUsed; 
                if ((Networking.GetServerTimeInMilliseconds() - timeToWait) > moveTimeUsed){ // create a buffer to make selection easier?
                    moveTimeUsed = Networking.GetServerTimeInMilliseconds(); 
                    if (!player.IsUserInVR()){}
                    if (value <= -0.6){
                        change = 1;
                    }
                    else if(value >= 0.6){
                        change = -1;
                    }
                    else{
                        change = 0;
                    }
                    changeSelMove(player, change, dictionary);
                    }
                    else{
                        //Debug.Log("Too Soon");
                    }
                }
                //Debug.Log(change);
            }
    }
    // used to change which enemy the user is going to attack //
    public override void InputJump(bool value, VRC.Udon.Common.UdonInputEventArgs args){
        if (GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer != null && value){
            enemySel = (enemySel + 1) % (Dictionaries.countActive(dictionary, dictionary.self, "enemy"));
            se.moveSelect(Dictionaries.getStat(dictionary.self, enemySel + 4, "Name"));
        }
    }
    
    // sends a raycast to check if the target will be hit //
    // helpful since this wont be actually shooting bullets :3 //
    private void fire(VRCPlayerApi player){
        se.hide();
        //dictionary.networkTest();
        string playerName = player.displayName;
        string enemy = Dictionaries.getStat(dictionary.self, enemySel + 4, "Name");
        //Debug.Log("Enemy Targetted: " + enemy);
        string skill = getMove(playerName, skillSel, dictionary);
        var statUsed = Dictionaries.calculateDamage(dictionary, playerName, enemy, skill, player);
        ds.showAilment(Dictionaries.getStat(dictionary.self, playerName, "Ailment"));
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

    // return the string with the name of the skill to use //
    private static string getMove(string user, int moveNum, Dictionaries dictionary){
        string[] skillArr = Dictionaries.getArray(dictionary.self, user, "Skills", "Name");
        return (skillArr[moveNum]);
    }
    // Returns the number of skills the holder has //
    private static int getMoveCount(string user, Dictionaries dictionary){
        return (Dictionaries.getArray(dictionary.self, user, "Skills", "Name").Length - 1); // for some reason this throws an error on the actual vrchat client
    }
    
    private void changeSelMove(VRCPlayerApi player, int change, Dictionaries dictionary){
        if (change != 0){ds.changeSel(change);}
        skillSel += change;
        if (skillSel < 0){skillSel = 0;}
        else{
            int moveCount = getMoveCount(player.displayName, dictionary);
            if (skillSel > moveCount){skillSel = moveCount;}
            }
        ds.displayDesc(dictionary, getMove(player.displayName, skillSel, dictionary), player.displayName);
    }
}
