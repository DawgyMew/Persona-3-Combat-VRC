
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
            2 - Unassigned ?Set Stat?
            3 - Unassigned
            4 - Unassigned
            5 - Unassigned
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
                sharedInfo[0] = 1;
                for (int i = 0; i < info.Length; i++){
                    sharedInfo[i + 1] = info[i];
                }
                break;
            case "test":
                sharedInfo[0] = 255;
                break;
        }
        //sharedInfo[0] = 15; // temp test

        SendCustomNetworkEvent(NetworkEventTarget.All, "rs");
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
        switch (instruction){
            case 0:
                break;
            case 1:
                changeNumI(dictionary, sharedInfo);
                break;
            case 255:
                dictionary.displayPlayers();
                break;

        }
        
    }

    // prepares a packet to instruct others to change an entities stat //
    /*
        0 - Target ID
        1 - Stat ID
        [2 3] - amount
        4 - [bool] sign (0 - Positive | 1 - Negative)
        5 - [bool] display (unused)
        6 - [bool] cant go under 0
    */
    public void changeNumO(Dictionaries dict, string target, string statToChange, int num, bool display, VRCPlayerApi player, bool cantGoUnder=false){
        // get id for target and stat
        byte[] data = new byte[7];
        data[0] = (byte)Dictionaries.findID(dict.self, target); // target
        data[1] = (byte)Array.IndexOf(dict.syncStats, statToChange);
        var numByte = convertBytes(num);
        // 2 3 4
        for (int i = 0; i < 3; i++){
            data[i + 2] = numByte[i];
        }
        if (display){data[5] = 1;} else{data[5] = 0;}
        if (cantGoUnder){data[6] = 1;} else{data[6] = 0;}

        KSCNE("changeNum", data, player);
    }
    // extracts packet to change an entities stat //
    private static void changeNumI(Dictionaries dict, byte[] data){
        string target = Dictionaries.getStat(dict.self, data[1], "Name");
        string statToChange = dict.syncStats[data[2]];
        byte[] byteNum = {data[3], data[4], data[5]};
        var changeAmt = convertBytes(byteNum);
        bool display = (data[5] == 1);
        bool cantGoUnder = (data[6] == 1);
        dict.changeNum(target, statToChange, changeAmt, dict.self, cantGoUnder);
    }

    // converting numbers to and from bytes //
    private static byte[] convertBytes(int num){
        int absol = Mathf.Abs(num);
        byte[] bytes = new byte[3];
        bytes[0] = (byte)(absol / 256); // nothing to handle numbers bigger than 65k lel 
        bytes[1] = (byte)(absol % 256); // i could make it so that it would require an int to make the array that big but i dont think i need to worry about that for now
        if (num < 0){bytes[2] = 1;} // negative is 1
        else{bytes[2] = 1;} // i probably should just use signed bytes lmao

        return bytes;
    }
    private static int convertBytes(byte[] bytes){
        int sign;
        if(bytes[2] == 0){sign = 1;}
        else{sign = -1;}
        return (((bytes[0] * 256) + bytes[1]) * sign); 
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
    public void displayInfo(TextMeshPro textBox){
        string displayText = "";
        foreach (var num in sharedInfo){
            displayText += num + ", ";
        }
        textBox.text = displayText;
    }
}

