
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Data;
using VRC.Udon;
using TMPro;

// have to store things in a data dictionary because udon hates OOP :D

/*
    Players
    Enemies
*/
public class Dictionaries : UdonSharpBehaviour
{
    public TextMeshPro board;
    // store everything as string for simplicity //
    /// Players ///
    public DataDictionary players = new DataDictionary(){
        // hard code the options and apply the information from that //
        {0, new DataDictionary(){
            {"Name", ""},
            {"HP", "354"},
            {"SP", "165"},
            {"PLV", "34"},
            // persona stats //
            {"pName", "Black Frost"},
            {"St", "29"},
            {"Mg", "31"},
            {"En", "25"},
            {"Ag", "27"},
            {"Lu", "36"},
            
            // persona type affinities //
            {"Strengths", "Fire, Darkness"},
            {"Nullifies", "Ice"},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", "Light"},
            // skills //
            {"Skills", "Mudo, Agilao, Bufula, Marakunda, Re Patra, Ice Boost, Rakukaja, Trafuri"}
        }},
        {1, new DataDictionary(){
            {"Name", ""},
            {"Shots", "0"},
            {"HP", "1"},
            {"SP", "2"},
            {"PLV", "2"},
            // persona stats //
            {"pName", ""},
            {"St", "2"},
            {"Mg", "2"},
            {"En", "2"},
            {"Ag", "2"},
            {"Lu", "2"},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", ""}
        }},
        {2, new DataDictionary(){
            {"Name", ""},
            {"HP", "1"},
            {"SP", "2"},
            {"PLV", "2"},
            // persona stats //
            {"pName", ""},
            {"St", "2"},
            {"Mg", "2"},
            {"En", "2"},
            {"Ag", "2"},
            {"Lu", "2"},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", ""}
        }}
    };

    public DataDictionary activeEnemies = new DataDictionary(){
        // hard code the options and apply the information from that //
        {0, new DataDictionary(){
            {"Name", "Shouting Tiara"},
            {"HP", "242"},
            {"SP", "138"},
            {"LVL", "35"},
            // persona stats //
            {"pName", ""},
            {"St", "19"},
            {"Mg", "31"},
            {"En", "19"},
            {"Ag", "22"},
            {"Lu", "21"},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", "Light"},
            {"Absorb", "Fire"},
            {"Reflect", ""},
            {"Weak", "Ice, Dark"},

            {"Skills", "Maragi, Agilao, Maragion, Mahama, Media"}
        }},
        {1, new DataDictionary(){
            {"Name", ""},
            {"HP", "1"},
            {"SP", "2"},
            {"LVL", "2"},
            // persona stats //
            {"pName", ""},
            {"St", "2"},
            {"Mg", "2"},
            {"En", "2"},
            {"Ag", "2"},
            {"Lu", "2"},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", ""}
        }},
        {2, new DataDictionary(){
            {"Name", ""},
            {"HP", "1"},
            {"SP", "2"},
            {"LVL", "2"},
            // persona stats //
            {"pName", ""},
            {"St", "2"},
            {"Mg", "2"},
            {"En", "2"},
            {"Ag", "2"},
            {"Lu", "2"},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", ""}
        }}
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
    public static int findID(DataDictionary dict, string strToFind, string keyToSrch = "Name"){
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
    public static string getStat(DataDictionary dict, int id, string key){
        var stat = (dict[id].DataDictionary[key]);
        //Debug.Log($"getStat returns {stat}");
        return (stat.String);
    }
    // get stat for if you don't know the id //
    // Requires the Dictionary, the key to search for, the unique string, and the stat to show //
    public static string getStat(DataDictionary dict, string uStr, string statToShow, string key = "Name"){
        var id = findID(dict, uStr, key);
        return (getStat(dict, id, statToShow));
    }
    // return a string array //
    public static string[] getArray(DataDictionary dict, string uStr, string statToShow, string key){
        var strStat = getStat(dict, uStr, statToShow, key);
        return (strStat.Split(", "));
    }
    // replaces the current stat with a new string //
    public static void setStat(DataDictionary dict, string uStr, string statToChange, string newStat){
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
