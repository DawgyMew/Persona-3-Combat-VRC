
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Data;

public class turnLogic : UdonSharpBehaviour
{
    /*
    Future Turn Logic or something
    its mostly empty for now
    */

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
            var name = Dictionaries.getStat(dictionary, i, "Name");
            if (!name.Equals("_") || !name.Equals("")){
                speeds[count, 0] = i;
                speeds[count, 1] = Dictionaries.getStat(dictionary, i, "Ag"); // store in an array {id, ag}
                count++;
            }
        }
    }

    private static int[,] sort(int[,] arr, int col){
        // plink
        // bubble sort??
    }
}
