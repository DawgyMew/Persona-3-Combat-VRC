﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Data;
using VRC.Udon.Common.Interfaces;
using TMPro; 
using System.Collections;
using VRC.Udon.Common.Enums;

[UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
public class turnLogic : UdonSharpBehaviour
{
    public Dictionaries dict;
    //public enemyAI eai;
    public networking network;
    public TextMeshPro turnText;
    public TextMeshPro activePlayer;
    public TextMeshPro timer;
    public battleLog bl;

    [UdonSynced] public string[] turnOrder = new string[10]; // <-- important to keep synced
    [UdonSynced, FieldChangeCallback(nameof(PlayerUp))] public int _playerUp = 0;

    public int PlayerUp
    {
        get => _playerUp;
        set
        {
            _playerUp = value;
            showActivePlayer();
            beforeTurn();
        }
    }
    [UdonSynced] public int turnNum = 0;
    [UdonSynced] public string turnOwner = "";
    [UdonSynced] public bool turnTaken = false; // this is going to cause problems in the future i have a feeling

    [UdonSynced] public bool requireTurnToAttack = true; // debug variable that if disabled allows any player to attack at any time.


    

    public void beforeBattle()
    {
        // summons enemies //
        string[] enemyPresets = {"Shouting Tiara", "Amorous Snake"};

        for (int i = 0; i < enemyPresets.Length; i++)
        {
            // dont allow for slots that dont exist
            if (i < dict.ENEMYSLOTS)
            {
                int nameId = i + 1;
                string enemyName = "enemy" + nameId;
                int id = i + dict.PLAYERSLOTS;
                Dictionaries.addEnemy(id, enemyName, enemyPresets[i]);
            }
            else { break; }
        }
        // start battle //

        turnOrder = determineTurnOrder(dict);
        RequestSerialization();
        // SendCustomNetworkEvent(NetworkEventTarget.All, "runescape");

        // !activate automatic skills for everyone

        beforeTurn();
    }

    // returns if the target will be able to do their turn //
    [RecursiveMethod]
    public void beforeTurn()
    {
        turnNum++;
        bl.addToLog("Turn #" + turnNum + ":");
        turnOwner = turnOrder[PlayerUp];
        Dictionaries.refreshMenu();
        var stats = Dictionaries.getDict(dict.self, Dictionaries.findID(dict.self, turnOwner));
        // decrease stat change timers
        if (stats["isDown"].Boolean)
        {
            dict.setStat(dict.self, turnOwner, "isDown", false);
        }
        Dictionaries.decreaseStatTimer(dict, turnOwner);
        // !regen if they have the passive skills
        // roll if ailment will skip turn
        int randNum = Random.Range(0, 100);
        string ailment = stats["Ailment"].String;
        // alments :3 //
        switch (ailment)
        {
            case "Freeze":
            case "Shocked":
                dict.setStat(dict.self, turnOwner, "Ailment", "");
                afterTurn();
                break;
            case "Charm": // should write to potentially harm teamates but that sounds hard right now its been like 6 months
            case "Fear":
                if (randNum <= 80)
                {
                    afterTurn();
                }
                break;
            default:
                break;
        }

        // it will skip their turn on the first turn they are dizzy
        //      could base it off of whether or not they are down

        // TODO: roll to heal some ailments
        SendCustomNetworkEvent(NetworkEventTarget.All, "turn");
    }
    // activate when the current player value changes //
    [RecursiveMethod]
    public void turn()
    {
        isActive = true;
        timerCount = 0;
        if (!Dictionaries.getStat(dict.self, turnOwner, "Tag").Equals("enemy"))
        {
            if (Networking.LocalPlayer.displayName.Equals(turnOwner))
            {


                isActive = true;
                timerCount = 0;

            }
        }
        else
        {
            // if its an enemy turn put it up to the instance masters machine

            //
            if (Networking.IsMaster)
            {
                // get move to use
                string[] move = enemyAI.determineMove(dict, turnOwner);
                // check target and call the move 
                if (move != null)
                {
                    Dictionaries.calculateDamage(dict, turnOwner, move[1], move[0], Networking.LocalPlayer);
                }
                else
                {
                    bl.addToLog(turnOwner + " is dead."); // easier than removing them from the order rn
                }
                SendCustomNetworkEvent(NetworkEventTarget.All, "nextTurn");
                // nextTurn();
            }
        }
    }

    // able to be called by SCNE while and then run the desired function after a certain amount of time
    public void bufferNextTurn()
    {
        SendCustomEventDelayedSeconds("nextTurn", 2);
    }
    public void nextTurn()
    {


        if (true)
        {
            turnTaken = true; // should make it so that it doesnt do the end of turn logic twice for one player
            bool oneMore = afterTurn();

            // TODO: Check if the player made the last enemy downed -> choice to all out attack
            // check if all players or enemies are dead 
            int actEnemies = Dictionaries.countActive(dict, dict.self, "enemy");
            int actPlayers = Dictionaries.countActive(dict, dict.self, "player");
            if (actEnemies > 0 && actPlayers > 0)
            {
                if (!oneMore)
                {
                    turnOrder = determineTurnOrder(dict);
                    PlayerUp = (PlayerUp + 1) % turnOrder.Length;
                    RequestSerialization();
                    // SendCustomNetworkEvent(NetworkEventTarget.All, "showActivePlayer");
                    // SendCustomNetworkEvent(NetworkEventTarget.All, "beforeTurn");

                    //showActivePlayer();

                }
                else
                {
                    SendCustomNetworkEvent(NetworkEventTarget.All, "turn"); // recursive loop :3
                }
                // should sync the turns to everyone??
            }
            else
            {
                SendCustomNetworkEvent(NetworkEventTarget.All, "afterBattle");

            }
        }

    }
    public bool afterTurn()
    {
        if (Dictionaries.getStat(dict.self, turnOwner, "Ailment").Equals("Poison"))
        {
            int healthLost = (int)((int.Parse(Dictionaries.getStat(dict.self, turnOwner, "Max HP")) * .3) * -1);
            dict.changeNum(turnOwner, "HP", healthLost, dict.self, true);
            dict.changeNum(turnOwner, "HP", 1, dict.self, true); // increase by one because poison cant kill :3
        }
        // poison the player if applicable
        // check if the player downed an enemy and return true if so
        // if they dont move on to next turn
        RequestSerialization(); // serialize something to the other players dont know yet lmao
        Dictionaries.refreshMenu();
        return (false);
    }

    // something something end of battle
    public void afterBattle()
    {
        activePlayer.text = "you're winner !";
    }

    
    // Determine the order of who will go when //
    // Run at the start of the battle, when a new player joins the battle, and whenever the agility changes //
    // i dont think increased agility affects the turn order until everyone else has gone?
    // no baton pass
    // udon only allows jagged arrays
    public string[] determineTurnOrder(Dictionaries dictionary)
    {
        var amtActive = Dictionaries.countActive(dictionary, dictionary.self);
        var totalNum = dictionary.self.Count;
        var count = 0;
        int[][] speeds = new int[amtActive][];
        for (int i = 0; i < totalNum; i++)
        {
            var name = Dictionaries.getStat(dictionary.self, i, "Name");
            if (!name.Equals("_") && !name.Equals(""))
            {
                speeds[count] = new int[] { i, int.Parse(Dictionaries.getStat(dictionary.self, i, "Ag")) };
                count++;
            }
        }
        var sortArr = sort2D(speeds, 1);
        string[] returnArr = new string[sortArr.Length];
        // make an array of the names in the order of speed 
        for (int i = 0; i < sortArr.Length; i++)
        {
            returnArr[i] = Dictionaries.getStat(dictionary.self, sortArr[i][0], "Name");
        }
        turnOrder = returnArr;
        RequestSerialization();
        //SendCustomNetworkEvent(NetworkEventTarget.All, "runescape"); // serialize it to everyone else
        return (returnArr);
    }

    /// <param name="arr">2D Array to sort</param>
    /// <param name="col">Colomn to base the sort off of</param>
    private static int[][] sort2D(int[][] arr, int col)
    {
        // plink
        // bubble sort
        // the yacht dice project really came in handy here :P
        int[] temp;
        int length = arr.Length;
        bool swapped;
        for (int i = 0; i < length - 1; i++)
        {
            swapped = false;
            for (int j = 0; j < length - i - 1; j++)
            {
                if (arr[j][col] < arr[j + 1][col])
                {
                    temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                    swapped = true;
                }
            }
            if (!swapped) { break; }
        }
        return (arr);
    }

    public bool isPlayerTurn(string playerName)
    {
        Debug.Log(requireTurnToAttack + " " + turnOrder[PlayerUp] + " " + playerName);
        if (requireTurnToAttack)
        {
            if (playerName.Equals(turnOrder[PlayerUp]))
            {
                return true;
            }
            else { return false; }
        }
        // if requireTurnToAttack is disabled; allow anyone to attack at any time.
        else { return true; }
    }
    // i have to remember that functions called by SCNE have to be public
    public void showActivePlayer()
    {
        if (Dictionaries.countActive(dict, dict.self, "player") != 0)
        {
            activePlayer.text = turnOrder[PlayerUp] + " its your turn :3";
        }
        else
        {
            activePlayer.text = "you died womp womp";
        }
    }
    // networking //
    // public void runescape() { RequestSerialization(); }
    public override void OnDeserialization() { showTurnOrder(); }
    public void startBattle() { SendCustomNetworkEvent(NetworkEventTarget.All, "beforeBattle"); }

    // recalculate the turn order whenever someone joins
    public override void OnPlayerJoined(VRCPlayerApi player)
    {
        // TODO: assign player a persona immediately
        SendCustomEventDelayedSeconds("displayTurns", 1, EventTiming.Update); // sets a delay to send the event
        // SCEDS doesnt stop the events under it from happening
        //function
        SendCustomEventDelayedSeconds("showActivePlayer", 1.1f, EventTiming.Update);
    }

    public void displayTurns()
    {
        determineTurnOrder(dict);
        showTurnOrder();
    }
    public void showTurnOrder()
    {
        string dt = "Turn Order: \n";
        foreach (var turn in turnOrder)
        {
            dt += turn + ", ";
        }
        turnText.text = dt;
    }

    // TIMER
    public float TIMERLENGTH = 15;
    [UdonSynced] public float timerCount = 0;

    private bool isActive = false;
    public void Update()
    {
        if (isActive)
        {
            if (timerCount >= TIMERLENGTH)
            {
                isActive = false;
                timerCount = 0.0f;
                bl.addToLog(turnOwner + " passed.");
                nextTurn();
            }
            else
            {
                timerCount += Time.deltaTime;
                timer.text = timerCount + "";
            }
        }

    }

}
