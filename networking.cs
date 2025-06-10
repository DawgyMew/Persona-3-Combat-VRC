
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.Udon.Common.Interfaces;
using TMPro;
using System.Linq;
using System;
public class networking : UdonSharpBehaviour
{
    public Dictionaries dictionary;
    public TextMeshPro currentTB;
    public TextMeshPro syncedTB;
    // update this list with the contents and share it
    [UdonSynced]public byte[] sharedInfo = new byte[10];

    /*
        This is my way of sending out custom network events through udon with variables.
        It aint great but it should be lightweight for the network.
        Still a WIP!

        Shared Info is the Important array that will be parsed.
        Indexes:
        0 - The Instruction:
            0 - Nothing
            1 - Change Stat by Amount
            2 - Unassigned ?Set Stat to num?
            3 - Unassigned ?Set Preset?
            4 - Change stat to index in array
            5 - Unassigned ?status changes?
            6 - Unassigned
            7 - Unassigned
            8 - Unassigned
            9 - Unassigned
            10 - Unassigned
            11 - Unassigned
            12 - Unassigned
            13 - Unassigned
            14 - Unassigned


            255 - Displays the Player Board

            "test" => 255
            "changeNum" => 1;
        1 - 9: 
    */
    // KatsSendCustomNetworkEvent
    public void KSCNE(string instruction, byte[] info, VRCPlayerApi player){
        Networking.SetOwner(player, this.gameObject);
        switch(instruction){
            case "flush":
                for (int i = 0; i < sharedInfo.Length; i++){
                    sharedInfo[i] = 0;
                }
                break;
            case "changeNum":
            case "statFromArrayIndex": 
                sharedInfo = info;
                break;
            case "test":
                sharedInfo[0] = 255;
                break;
        }
        //sharedInfo[0] = 15; // temp test

        RequestSerialization();
        // SendCustomNetworkEvent(NetworkEventTarget.All, "rs");
    }

    // forces everyone to request that the variable is synced and run OnDeserialization
    public void rs(){
        RequestSerialization();
    }
    
    // run the current instruction that was given
    public override void OnDeserialization(){
        displayInfo(syncedTB);
        var packetLength = sharedInfo.Length;
        var instruction = sharedInfo[0];
        Debug.Log(instruction);
        switch (instruction){
            case 0:
                break;
            case 1:
                changeNumI(dictionary, sharedInfo);
                break;
            case 4:
                statFromArrayIndexI(dictionary, sharedInfo);
                break;
            case 255:
                dictionary.displayPlayers();
                break;

        }
        
    }


    /// -- OUTPUT -- ///
    
    
    // 1 [OUT] - prepares a packet to instruct others to change an entities stat //
    /*
        0 - Instruction
        1 - Target ID
        2 - Stat ID
        [3 4] - amount
        5 - [bool] sign (0 - Positive | 1 - Negative)
        6 - [bool] display (unused)
        7 - [bool] cant go under 0
    */
    public void changeNumO(Dictionaries dict, string target, string statToChange, int num, bool display, VRCPlayerApi player, bool cantGoUnder=false){
        // get id for target and stat
        byte[] data = new byte[10];
        data[0] = 1; // instruction
        data[1] = (byte)Dictionaries.findID(dict.self, target); // target
        data[2] = (byte)Array.IndexOf(dict.syncStats, statToChange); // stat id
        var numByte = convertBytes(num);
        // 3 4 5
        for (int i = 0; i < 3; i++){
            data[i + 3] = numByte[i];
        }
        if (display){data[6] = 1;} else{data[6] = 0;}
        if (cantGoUnder){data[7] = 1;} else{data[7] = 0;}

        KSCNE("changeNum", data, player);
    }

    // 4 [OUT] - Create a packet to set a stat to a specific index 
    public void statFromArrayIndexO(Dictionaries dict, string target, string statToChange, byte arrayID, string statSave, VRCPlayerApi player){
        byte[] data = new byte[10];
        string[] array = arrayFromID(dict, arrayID);
        byte targetID = (byte)Dictionaries.findID(dict.self, target);
        byte statName = (byte)Array.IndexOf(dict.syncStats, statToChange);
        byte statIndex = (byte)Array.IndexOf(array, statSave);

        data[0] = 4; // instruction
        data[1] = targetID; // target 
        data[2] = statName; // stat to change
        data[3] = arrayID; // id of the array to draw from
        data[4] = statIndex; //index of the string to draw

        KSCNE("statFromArrayIndex", data, player);
    }

    //// -- INPUT -- ////
    

    // 1 [IN] - extracts packet to change an entities stat //
    /*
        0 - Instruction
        1 - Target ID
        2 - Index of Stat to change
        3, 4, 5 - Amount to Change
        6 - display (unused)
        7 - cant go under 0
    */
    private static void changeNumI(Dictionaries dict, byte[] data){
        string target = Dictionaries.getStat(dict.self, data[1], "Name");
        string statToChange = dict.syncStats[data[2]];
        byte[] byteNum = {data[3], data[4], data[5]};
        var changeAmt = convertBytes(byteNum);
        bool display = (data[6] == 1);
        bool cantGoUnder = (data[7] == 1);
        dict.changeNum(target, statToChange, changeAmt, dict.self, cantGoUnder, false); // false at the end to prevent infinite loop 
    }


    // 4 [IN] - Changes a stat on a target to a specific index in a specific array //
    /*
        0 - Instruction
        1 - Target
        2 - Stat to change
        3 - Array to get from (i doubt ill need more than 255 public arrays)
            0 - Ailments
            1 - Offensive Elements
        4 - Index of item 
    */
    private static void statFromArrayIndexI(Dictionaries dict, byte[] data){
        string target = Dictionaries.getStat(dict.self, data[1], "Name");
        string statToChange = dict.syncStats[data[2]];
        var index = data[4];
        var newStat = arrayFromID(dict, data[3], data[4]);
        Debug.Log("Set stat on " + target + " to " + newStat + " on " + statToChange);
        dict.setStat(dict.self, target, statToChange, newStat);
    }



    // converting numbers to and from bytes //
    public static byte[] convertBytes(int num){
        int absol = Mathf.Abs(num);
        byte[] bytes = new byte[3];
        bytes[0] = (byte)(absol / 256); // nothing to handle numbers bigger than 65k lel 
        bytes[1] = (byte)(absol % 256); // i could make it so that it would require an int to make the array that big but i dont think i need to worry about that for now
        if (num < 0){bytes[2] = 1;} // negative is 1
        else{bytes[2] = 1;} // i probably should just use signed bytes lmao

        return bytes;
    }
    public static int convertBytes(byte[] bytes){
        int sign;
        if(bytes[2] == 0){sign = 1;}
        else{sign = -1;}
        return (((bytes[0] * 256) + bytes[1]) * sign); 
    }

    // unique and stupid way to pull info from an array! //
    public static string arrayFromID(Dictionaries dict, byte id, byte index){
        string newStat;
        var arr = arrayFromID(dict, id);
        if (arr != null){
            return (arr[index]);
        }
        else{
            return "";
        }
    }

    public static string[] arrayFromID(Dictionaries dict, byte id){
        switch (id){
            case 0:
                return (dict.AILMENTS);
            case 1:
                return (dict.offensiveElements);
            default:
                return null;
        }
    }
    //


    /// Debugging ///
    /*
    public void Start(){
        changeNumO(dictionary, "enemy1", "HP", -500, true);
    }*/

    // update the debug text that can be enabled //
    public void Update(){
        displayInfo(currentTB);
    }
    // 255 - test that displays the list of players on the wall // 
    public void displayInfo(TextMeshPro textBox){
        string displayText = "";
        foreach (var num in sharedInfo){
            displayText += num + ", ";
        }
        textBox.text = displayText;
    }

    
}

