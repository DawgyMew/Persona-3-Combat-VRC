
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Data;
using VRC.Udon;
using TMPro;

// have to store things in a data dictionary because udon hates OOP :D
// also cant create a dictionary outside when adding a new key to the dictionary
// its gonna be so much fun 
public class Dictionaries : UdonSharpBehaviour
{
    public TextMeshPro board;
    // store everything as string for simplicity //
    /// Players ///
    public DataDictionary players = new DataDictionary(){
        // hard code the options and apply the information from that //
        {0, new DataDictionary(){
            {"Name", ""},
            {"Shots", "0"},
            {"HP", "1"},
            {"SP", "2"}
        }},
        {1, new DataDictionary(){
            {"Name", ""},
            {"Shots", "0"},
            {"HP", "3"},
            {"SP", "4"}
        }},
    };

    public override void OnPlayerJoined(VRCPlayerApi player){
        // add the player to the dictionary when they join //
        setStat(players, "", "Name", player.displayName);
    }

    public override void OnPlayerLeft(VRCPlayerApi player){
        setStat(players, player.displayName, "Name", "");
    }

    /// METHODS TO INTERACT WITH THE DICTIONARY //

    // Goes through the list of keys to find out which id stores the value //
    // defaults to "Name" because thats the most likely to need to find an id of //
    private static int findID(DataDictionary dict, string strToFind, string keyToSrch = "Name"){
        DataList keys = dict.GetKeys();
        keys.Sort();
        for (int i = 0; i < keys.Count; i++){
            if (dict.TryGetValue(keys[i], TokenType.DataDictionary, out DataToken value)){
                if (value.DataDictionary[keyToSrch] == strToFind){
                    return i;
                }
            }
        }
        Debug.LogWarning("Could not Find in Dictionary");
        return -1;
    } 

    // Get Stat for if you know the id //
    private static string getStat(DataDictionary dict, int id, string key){
        var stat = (dict[id].DataDictionary[key]);
        //Debug.Log($"getStat returns {stat}");
        return (stat.String);
    }
    // get stat for if you don't know the id //
    // Requires the Dictionary, the key to search for, the unique string, and the stat to show //
    private static string getStat(DataDictionary dict, string uStr, string statToShow, string key = "Name"){
        var id = findID(dict, uStr, key);
        return (getStat(dict, id, statToShow));
    }
    // replaces the current stat with a new string //
    private static void setStat(DataDictionary dict, string uStr, string statToChange, string newStat){
        var id = findID(dict, uStr);
        Debug.Log($"{newStat} replacing {statToChange} at id {id}");
        dict[id].DataDictionary[statToChange] = newStat;
    }
    ////////

    public void displayUser(VRCPlayerApi player, TextMeshPro textBox){
        textBox.text = getStat(players, player.displayName, "HP");
    }

    public void displayContents(){
        string displayText = "";
        DataList keys = players.GetKeys();
        keys.Sort();

        for (int i = 0; i < keys.Count; i++){
            displayText += i + ". ";
            if (players.TryGetValue(keys[i], TokenType.DataDictionary, out DataToken dict)){
                DataList newKeys = dict.DataDictionary.GetKeys();
                for (int j = 0; j < newKeys.Count; j++){
                displayText += newKeys[j].String + ": " + getStat(players, i, newKeys[j].String) + " ";
                }
            }
            displayText += "\n";
        }
        board.text = displayText;
    }

    public void changeNum(string uName, string numKey, int changeInNum){
        string num = getStat(players, uName, numKey);
        bool result = int.TryParse(num, out int intNum);
        if (result){
            intNum += changeInNum;
            num = intNum.ToString(); // convert back to string :>
            setStat(players, uName, numKey, num); // change contents of dict
            displayContents(); // update board
        }

    }
}
