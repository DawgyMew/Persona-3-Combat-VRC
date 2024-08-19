
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Data;
using VRC.Udon.Common.Interfaces;
using TMPro; 
using System.Collections;
using VRC.Udon.Common.Enums;

public class turnLogic : UdonSharpBehaviour
{
    public Dictionaries dict;
    public networking network;
    public TextMeshPro turnText;
    public TextMeshPro activePlayer;
    /*
    Future Turn Logic or something
    its mostly empty for now
    */
    [UdonSynced]public string[] turnOrder = new string[10]; // <-- important to keep synced
    [UdonSynced]public int currentTurn = 0;

    private void beforeBattle(){
        turnOrder = determineTurnOrder(dict);
        SendCustomNetworkEvent(NetworkEventTarget.All, "runescape");

        // !activate automatic skills for everyone
    }

    // returns if the target will be able to do their turn //
    private bool beforeTurn(Dictionaries dict, string name, DataDictionary stats){
        // decrease stat change timers
        if (stats["isDown"].Boolean){
            Dictionaries.setStat(dict.self, name, "isDown", false);
        }
        Dictionaries.decreaseStatTimer(dict, name);
        // !regen if they have the passive skills
        // !roll if ailment will skip turn
        // it will skip their turn on the first turn they are dizzy
        //      could base it off of whether or not they are down
        return (true);
    }
    // activate when the current player value changes //
    public void turn(){
        if (!Dictionaries.getStat(dict.self, turnOrder[currentTurn], "Tag").Equals("enemy")){
            if (Networking.LocalPlayer.displayName.Equals(turnOrder[currentTurn])){
                // allow the player to use a skill
                // set a delayed function to wait 15 seconds or so to force the player to pass their turn if they take too long
                    // but make the function be able to be interupted or something when the player activates their skill so players wont have to wait every time.
            }
        }   
        else{
            // if its an enemy turn put it up to the instance masters machine
            if (Networking.IsMaster){
                // TODO: enemy ai :plink:
                // temp just heal self
                Dictionaries.calculateDamage(dict, turnOrder[currentTurn], turnOrder[currentTurn], "Dia", Networking.LocalPlayer);
                nextTurn();
            }
        }
    }
    
    public bool afterTurn(){
        // poison the player if applicable
        // check if the player downed an enemy and return true if so
        // if they dont move on to next turn
        return (false);
    }
    // Increase the active turn number by one //
    public void nextTurn(){
        var oneMore = afterTurn();

        // TODO: Check if the player made the last enemy downed -> choice to all out attack

        // check if all players or enemies are dead 
        int actEnemies = Dictionaries.countActive(dict, dict.self, "enemy");
        int actPlayers = Dictionaries.countActive(dict, dict.self, "player");
        if (actEnemies > 0 && actPlayers > 0){
            if (!oneMore){
                currentTurn = (currentTurn + 1) % turnOrder.Length;
                showActivePlayer();
            }
            turn(); // recursive loop :3
        }
        else{
            // something something end of battle
        }
    }
    // Determine the order of who will go when //
    // Run at the start of the battle, when a new player joins the battle, and whenever the agility changes //
    // i dont think increased agility affects the turn order until everyone else has gone?
    // no baton pass
    // udon only allows jagged arrays
    public string[] determineTurnOrder(Dictionaries dictionary){
        var amtActive = Dictionaries.countActive(dictionary, dictionary.self);
        var totalNum = dictionary.self.Count;
        var count = 0;
        int[][] speeds = new int[amtActive][];
        for (int i = 0; i < totalNum; i++){
            var name = Dictionaries.getStat(dictionary.self, i, "Name");
            if (!name.Equals("_") && !name.Equals("")){
                speeds[count] = new int[] {i, int.Parse(Dictionaries.getStat(dictionary.self, i, "Ag"))};
                count++;
            }
        }
        var sortArr = sort2D(speeds, 1);
        string[] returnArr = new string[sortArr.Length];
        // make an array of the names in the order of speed 
        for (int i = 0; i < sortArr.Length; i++){
            returnArr[i] = Dictionaries.getStat(dictionary.self, sortArr[i][0], "Name");
        }
        turnOrder = returnArr;
        //SendCustomNetworkEvent(NetworkEventTarget.All, "runescape"); // serialize it to everyone else
        return (returnArr);
    }

    /// <param name="arr">2D Array to sort</param>
    /// <param name="col">Colomn to base the sort off of</param>
    private static int[][] sort2D(int[][] arr, int col){
        // plink
        // bubble sort
        // the yacht dice project really came in handy here :P
        int[] temp;
        int length = arr.Length;
        bool swapped;
        for (int i = 0; i < length - 1; i++){
            swapped = false;
            for (int j = 0; j < length - i - 1; j++){
                if (arr[j][col] < arr[j + 1][col]){
                    temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped){break;}
        }
        return (arr);
    }

    // i have to remember that functions called by SCNE have to be public
    public void showActivePlayer(){activePlayer.text = turnOrder[currentTurn] + " its your turn :3";}
    // networking //
    public void runescape(){RequestSerialization();}
    public override void OnDeserialization(){showTurnOrder();}
    public void startBattle(){SendCustomNetworkEvent(NetworkEventTarget.All, "turn");}

    // recalculate the turn order whenever someone joins
    public override void OnPlayerJoined(VRCPlayerApi player){
        // TODO: assign player a persona immediately
        SendCustomEventDelayedSeconds("displayTurns", 1, EventTiming.Update); // sets a delay to send the event
        // SCEDS doesnt stop the events under it from happening
        //function
        SendCustomEventDelayedSeconds("showActivePlayer", 1.1f, EventTiming.Update);
    }

    public void displayTurns(){
        determineTurnOrder(dict);
        showTurnOrder();
    }
    public void showTurnOrder(){
        string dt = "Turn Order: \n";
        foreach (var turn in turnOrder){
            dt += turn + ", ";
        }
        turnText.text = dt;
    }

    
}
