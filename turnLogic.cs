
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Data;

public class turnLogic : UdonSharpBehaviour
{
    public Dictionaries dict;
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
        if (stats["isDown"].Bool){
            Dictionaries.setStat(dict.self, name, "isDown", false);
        }
        Dictionaries.decreaseStatTimer(dict, name);
        // !regen if they have the passive skills
        // !roll if ailment will skip turn
        // it will skip their turn on the first turn they are dizzy
        //      could base it off of whether or not they are down
        return (true);
    }
    public void turn(){
        
    }
    // Determine the order of who will go when //
    // Run at the start and whenever the agility changes //
    // i dont think increased agility affects the turn order until everyone else has gone?
    // no baton pass
    public static string[] determineTurnOrder(Dictionaries dictionary){
        var amtActive = Dictionaries.countActive(dictionary, dictionary.self);
        var totalNum = dictionary.self.Count;
        var count = 0;
        int[,] speeds = new int[amtActive, 2];
        for (int i = 0; i < totalNum; i++){
            var name = Dictionaries.getStat(dictionary.self, i, "Name");
            if (!name.Equals("_") || !name.Equals("")){
                speeds[count, 0] = i;
                speeds[count, 1] = int.Parse(Dictionaries.getStat(dictionary.self, i, "Ag")); // store in an array {id, ag}
                count++;
            }
        }
        var sortArr = sort(speeds);
        return (sortArr);
    }

    private static int[,] sort(int[,] arr, int col){
        // plink
        // bubble sort??
        return (arr);
    }

    public void runescape(){
        RequestSerialization();
    }
}
