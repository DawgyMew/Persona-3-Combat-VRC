
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Data;
using VRC.Udon;
using TMPro;
using System;
// have to store things in a data dictionary because udon hates OOP :D
// This is a central hub for scripts to access other scripts 
// it aint a good way to do it but its a way 

/*
    Local Info:
        self: Information on the local player (you!) and the active enemies and also basic information on the other players.
    Skills: 
        skills: Full Dictionary containing every skill in persona 3
*/
public class Dictionaries : UdonSharpBehaviour
{
    public TextMeshPro board;
    public GameObject hi; // dummy object
    public updateText textUpdater;
    public networking network;
    public damageCalc damage;
    public battleLog log;
    public Presets presetList;

    // CONSTANTS //
    public string[] offensiveElements = {"Slash", "Strike", "Pierce", "Fire", "Ice", "Elec", "Wind", "Almighty"}; // ID: 1
    
    public string[] statChanges = {"Attack", "Defense", "Evasion", "Crit Rate", "Ailment Sus", "All Stats"};
    public string[] shortStatChanges = {"atk", "df", "ev", "crit", "ail", "all"};
    // Place to Reference for the IDs of the stats that sync 
    public string[] syncStats = {"Name", "HP", "Max HP", "SP", "Max SP", "LVL", "pName", "Ag", "Ailment", "isDown", "Stat Changes"};
    public string[] AILMENTS = {"Fear", "Panic", "Distress", "Poison", "Charm", "Rage", "Freeze", "Shock", "Dizzy"}; // ID: 0
    public string[] PATRA = {"Panic", "Fear", "Distress"};

    /// Players ///
    // save these on the local machine or something 
    // self now contains all the local data not skills tho
    public DataDictionary self = new DataDictionary(){
        // hard code the options and apply the information from that //

        /*
            ID: The number that the datadictionary is interally marked by.
            Name: The unique identifier, either the players username or the enemy's internal name
            HP, Max HP: Current Health Point and Max Health Point
            SP, Max SP: Current Spirit Point and Max Spirit Points
                HP and SP will change while Max HP and Max SP will always remain the same
            LVL: The Current level of the character
            Tag: What type of entity is taking up this slot.
            pName: The name of the persona or shadow that the holder has, usually used for pulling a model
            St, Mg, En, Ag, Lu: The Persona's stats
                Strength, Magic, Endurance, Agility, Luck
            
            Strengths: Elements of these types by will deal less damage.
            Nullifies: Elements of these types will deal no damage.
            Absorb: Elements of these types will heal the target for the amount of damage that would've been dealt.
            Reflect: Elements of these types will deal no damage to the target and deal that damage to the user.
            Weak: Elements of these types will deal extra damage and knock them down.

            Skills: The skills that they are able to use.
            Passive: The skills that affect the player but they cannot cast.
            Preset Base: The UID of the preset used to generate the persona. * at the end denotes a change from the original preset.
            Ailment: The current negative ailment
            IsDown: Whether or not they are down and will take more damage from attacks
            Stat Changes: How certain stats have been changed.
                Stored as string in format of: "[stat changed][+ or -][number of turns left on the timer]" followed by the next stat seperated with a ,
                                                atk+3,df-2
                atk: attack, changes the power of the move used
                df: defense, changes how much damage they will take
                ev: evasion/accuracy, changes whether the user has better aim or is more likely to dodge an attack.
                crit: critical hit chance, changes the likelyhood of hitting a critical hit with a physical skill
        */      
        {0, new DataDictionary(){ 
            // personal stats //
            {"Name", ""},
            {"HP", 354},
            {"Max HP", 354},
            {"SP", 165},
            {"Max SP", 165},
            {"LVL", 34},
            {"Tag", "player"},
            // persona stats //
            {"pName", "Plink plonk Black Frost"},
            {"St", 29},
            {"Mg", 31},
            {"En", 25},
            {"Ag", 27},
            {"Lu", 36},
            
            // persona type affinities //
            {"Strengths", "Fire,Darkness"},
            {"Nullifies", "Ice"},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", "Light"},
            // skills //
            {"Skills", "Bufula,Gale Slash,Mudo,Agilao,Marakunda,Re Patra,Rakukaja,Mediarama,Wait,Guard"},
            {"Passive", "Ice Boost"},
            {"Preset Base", ""},
        
            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", "atk+3,df-1,ev+2,crit+3"}, // atk+3,df-1,ev+2,crit+3
        }},
        
        // other players //
        // i think that it downloads a copy of the instance owners funny thing so it should get all the past information
        // so the players cannot have their own personal thing

        {1, new DataDictionary(){ 
            {"Name", ""},
            {"HP", 354},
            {"Max HP", 354},
            {"SP", 165},
            {"Max SP", 165},
            {"LVL", 34},
            {"Tag", "player"},
            // persona stats //
            {"pName", "Plink plonk Black Frost"},
            {"St", 29},
            {"Mg", 31},
            {"En", 25},
            {"Ag", 21},
            {"Lu", 36},
            
            // persona type affinities //
            {"Strengths", "Fire,Darkness"},
            {"Nullifies", "Ice"},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", "Light"},
            // skills //
            {"Skills", "Bufula,Gale Slash,Mudo,Agilao,Marakunda,Re Patra,Rakukaja,Wait"},
            {"Passive", "Ice Boost"},
            {"Preset Base", ""},
        
            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", ""}, // atk+3,df-1,ev+2,crit+3
        }},
        {2, new DataDictionary(){ 
            {"Name", ""},
            {"HP", 354},
            {"Max HP", 354},
            {"SP", 165},
            {"Max SP", 165},
            {"LVL", 34},
            {"Tag", "player"},
            // persona stats //
            {"pName", "Plink plonk Black Frost"},
            {"St", 29},
            {"Mg", 31},
            {"En", 25},
            {"Ag", 27},
            {"Lu", 36},
            
            // persona type affinities //
            {"Strengths", "Fire,Darkness"},
            {"Nullifies", "Ice"},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", "Light"},
            // skills //
            {"Skills", "Bufula,Gale Slash,Mudo,Agilao,Marakunda,Re Patra,Rakukaja,Wait"},
            {"Passive", "Ice Boost"},
            {"Preset Base", ""},
        
            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", ""}, // atk+3,df-1,ev+2,crit+3
        }},
        {3, new DataDictionary(){ 
            {"Name", ""},
            {"HP", 354},
            {"Max HP", 354},
            {"SP", 165},
            {"Max SP", 165},
            {"LVL", 34},
            {"Tag", "player"},
            // persona stats //
            {"pName", "Plink plonk Black Frost"},
            {"St", 29},
            {"Mg", 31},
            {"En", 25},
            {"Ag", 27},
            {"Lu", 36},
            
            // persona type affinities //
            {"Strengths", "Fire,Darkness"},
            {"Nullifies", "Ice"},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", "Light"},
            // skills //
            {"Skills", "Bufula,Gale Slash,Mudo,Agilao,Marakunda,Re Patra,Rakukaja,Wait"},
            {"Passive", "Ice Boost"},
            {"Preset Base", ""},

            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", ""}, // atk+3,df-1,ev+2,crit+3
        }},

        // enemies //

        {4, new DataDictionary(){
            {"Name", "enemy1"}, // unique identifier
            {"HP", 242},
            {"Max HP", 242},
            {"SP", 138},
            {"Max SP", 138},
            {"LVL", 35},
            {"Tag", "enemy"},
            // persona stats //
            {"pName", "Shouting Tiara"},
            {"St", 19},
            {"Mg", 31},
            {"En", 19},
            {"Ag", 22},
            {"Lu", 21},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", "Light"},
            {"Absorb", "Fire"},
            {"Reflect", ""},
            {"Weak", "Ice,Dark"},
            // skills //
            {"Skills", "Maragi,Agilao,Maragion,Mahama,Media,Wait"},
            {"Passive", ""},
            {"Preset Base", ""},
            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", ""}, 
        }},
        {5, new DataDictionary(){
            // currently just a clone of enemy1
            {"Name", "enemy2"}, // unique identifier
            {"HP", 150},
            {"Max HP", 242},
            {"SP", 138},
            {"Max SP", 138},
            {"LVL", 35},
            {"Tag", "enemy"},
            // persona stats //
            {"pName", "Shouting Tiara"},
            {"St", 19},
            {"Mg", 31},
            {"En", 19},
            {"Ag", 99},
            {"Lu", 21},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", "Light"},
            {"Absorb", "Fire"},
            {"Reflect", ""},
            {"Weak", "Ice,Dark"},
            // skills //
            {"Skills", "Maragi,Agilao,Maragion,Mahama,Media,Wait"},
            {"Passive", ""},
            {"Preset Base", ""},
            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", ""}, 
        }},
        {6, new DataDictionary(){
            {"Name", "_"}, // empty placehilder 
            {"HP", 1},
            {"Max HP", 1},
            {"SP", 2},
            {"Max SP", 2},
            {"LVL", 2},
            {"Tag", "enemy"},
            // persona stats //
            {"pName", ""},
            {"St", 2},
            {"Mg", 2},
            {"En", 2},
            {"Ag", 2},
            {"Lu", 2},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", "Wait"},
            {"Passive", ""},
            {"Preset Base", ""},
            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", ""},
        }},
        {7, new DataDictionary(){
            {"Name", "_"},
            {"HP", 1},
            {"Max HP", 1},
            {"SP", 2},
            {"Max SP", 2},
            {"LVL", 2},
            {"Tag", "enemy"},
            // persona stats //
            {"pName", ""},
            {"St", 2},
            {"Mg", 2},
            {"En", 2},
            {"Ag", 2},
            {"Lu", 2},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", "Wait"},
            {"Passive", ""},
            {"Preset Base", ""},
            // other //
            {"Ailment", ""},
            {"isDown", false},
            {"Stat Changes", ""},
        }},
    };

    // all the skill s//
    public DataDictionary skillDict = new DataDictionary(){
        /*
            Element: The Type of Skill
                Slash, Strike, Pierce - Physical
                Fire, Ice, Elec, Wind - Magic
                Almighty - Magic but Funnier
                Light, Darkness - Instant Kills
                Recovery - Healing
                SP Recovery - Recovers SP
                Down - Makes the user no longer downed
                Revive - Revive from death
                Attack, Defense, Evasion, Crit Rate, Ailment Sus, All Stats - Stat Changes
                Reflect Phys, Reflect Magic - Barriers that reflect physical/magic skills once
                Patra[Fear, Panic, Distress], Poison, Charm, Rage, Ailments - Cause/Heal Ailments
                HP Drain, SP Drain - Transfers HP/SP from the opponent to the user.
                Counter - Passive, Chance to Reflect physical attacks
            Power: How much damage the attack does. (Healing power for recovery moves)
            Accuracy: How likely the move is to hit. (1.00 is guarenteed to hit(maybe not if evasion lowered))
            Cost: The amount of SP/Percentage of HP used when the move is used.
                -1 is to pass through an exception
            Targets: Who the move will hit.
                One/All: Opposing Team (Enemies)
                Ally/Party: Team Using the ability (Party)
                Everyone: Everyone alive on the field
                Self: The user of the skill
            Critical: Chance to knock down the opponent even if they are not weak to the element
            Ailment Chance: Chance to apply an ailment such as shock or poison.
        */
        {0, new DataDictionary(){ // nest it like this so it can fit in with its new friends :)
            /// Physical Skills ///
            // slash skills //
            {"Cleave", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 30},
                {"Accuracy", .90},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.10},
                {"Ailment Chance", 0.00}
            }},
            {"Power Slash", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 88},
                {"Accuracy", .92},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Zan-ei", new DataDictionary(){ // 1.5x new moon
                {"Element", "Slash"},
                {"Power", 120},
                {"Accuracy", .80},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.03},
                {"Ailment Chance", 0.00}
            }},
            {"Getsu-ei", new DataDictionary(){ // 1.5x Full Moon
                {"Element", "Slash"},
                {"Power", 120},
                {"Accuracy", .90},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.03},
                {"Ailment Chance", 0.00}
            }},
            {"Gale Slash", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", .14},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0.03},
                {"Ailment Chance", 0.00}
            }},
            {"Mighty Swing", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 220},
                {"Accuracy", .95},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Fatal End", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 230},
                {"Accuracy", .95},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Blade of Fury", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 100},
                {"Accuracy", .92},
                {"Cost", .16},
                {"Targets", "All"},
                {"Times Hit", 3}, //2-4
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Deathbound", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 370},
                {"Accuracy", .88},
                {"Cost", .19},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0.20},
                {"Ailment Chance", 0.00}
            }},
            {"Tempest Slash", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 350},
                {"Accuracy", .95},
                {"Cost", .30},
                {"Targets", "One"},
                {"Times Hit", 2}, //1-3
                {"Critical", 0.10},
                {"Ailment Chance", 0.00}
            }},
            {"Heaven's Blade", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 500},
                {"Accuracy", .95},
                {"Cost", .13},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.25},
                {"Ailment Chance", 0.00}
            }},
            {"Brave Blade", new DataDictionary(){
                {"Element", "Slash"},
                {"Power", 550},
                {"Accuracy", .99},
                {"Cost", .20},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Vorpal Blade", new DataDictionary(){ //1.5x Great Condition
                {"Element", "Slash"},
                {"Power", 500},
                {"Accuracy", .99},
                {"Cost", .21},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},

            // strike skills //

            {"Bash", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 30},
                {"Accuracy", .90},
                {"Cost", .07},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Sonic Punch", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 70},
                {"Accuracy", .95},
                {"Cost", .09},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.12},
                {"Ailment Chance", 0.00}
            }},
            {"Assault Dive", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 90},
                {"Accuracy", .95},
                {"Cost", .09},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Kill rush", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 110},
                {"Accuracy", .85},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 2},//1-3
                {"Critical", 0.03},
                {"Ailment Chance", 0.00}
            }},
            {"Swift Strike", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 95},
                {"Accuracy", .80},
                {"Cost", .15},
                {"Targets", "All"},
                {"Times Hit", 2}, //1-3
                {"Critical", 0.03},
                {"Ailment Chance", 0.00}
            }},
            {"Herculean Strike", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 210},
                {"Accuracy", .85},
                {"Cost", .18},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0.03},
                {"Ailment Chance", 0.00}
            }},
            {"Gigantic Fist", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 315},
                {"Accuracy", .80},
                {"Cost", .12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.15},
                {"Ailment Chance", 0.00}
            }},
            {"Heat Wave", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 280},
                {"Accuracy", .90},
                {"Cost", .16},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0.10},
                {"Ailment Chance", 0.00}
            }},
            {"Vicious Strike", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 340},
                {"Accuracy", .92},
                {"Cost", .19},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0.01},
                {"Ailment Chance", 0.00}
            }},
            {"Weary Thrust", new DataDictionary(){ //1.5x tired condition
                {"Element", "Strike"},
                {"Power", 405},
                {"Accuracy", .99},
                {"Cost", .13},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Akasha Arts", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 350},
                {"Accuracy", .85},
                {"Cost", .19},
                {"Targets", "All"},
                {"Times Hit", 2},//1-2 hits, got tails on a coin flip :>
                {"Critical", 0.10},
                {"Ailment Chance", 0.00}
            }},
            {"Gods Hand", new DataDictionary(){
                {"Element", "Strike"},
                {"Power", 600},
                {"Accuracy", .90},
                {"Cost", .14},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.10},
                {"Ailment Chance", 0.00}
            }},

            // pierce skills //

            {"Single Shot", new DataDictionary(){
                {"Element", "Pierce"},
                {"Power", 28},
                {"Accuracy", .90},
                {"Cost", .08},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Double Fangs", new DataDictionary(){
                {"Element", "Pierce"},
                {"Power", 20},
                {"Accuracy", .85},
                {"Cost", .09},
                {"Targets", "One"},
                {"Times Hit", 2},
                {"Critical", 0.01},
                {"Ailment Chance", 0.00}
            }},
            {"Holy Arrow", new DataDictionary(){ // charms!
                {"Element", "Pierce"},
                {"Power", 60},
                {"Accuracy", .92},
                {"Cost", .09},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.25}
            }},
            {"Twin Shot", new DataDictionary(){
                {"Element", "Pierce"},
                {"Power", 100},
                {"Accuracy", .85},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 2},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Torrent Shot", new DataDictionary(){
                {"Element", "Pierce"},
                {"Power", 50},
                {"Accuracy", .82},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 3}, //1-3
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Cruel Attack", new DataDictionary(){ // 1.5x to downed foes
                {"Element", "Pierce"},
                {"Power", 225},
                {"Accuracy", .99},
                {"Cost", .10},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.01},
                {"Ailment Chance", 0.00}
            }},
            {"Poison Arrow", new DataDictionary(){ // poisons
                {"Element", "Pierce"},
                {"Power", 300},
                {"Accuracy", .95},
                {"Cost", .13},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.03},
                {"Ailment Chance", 0.50}
            }},
            {"Vile Assault", new DataDictionary(){ //1.5x downed foes
                {"Element", "Pierce"},
                {"Power", 295},
                {"Accuracy", .99},
                {"Cost", .12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.01},
                {"Ailment Chance", 0.00}
            }},
            {"Arrow Rain", new DataDictionary(){
                {"Element", "Pierce"},
                {"Power", 130},
                {"Accuracy", .80},
                {"Cost", .19},
                {"Targets", "All"},
                {"Times Hit", 2},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Myriad Arrows", new DataDictionary(){
                {"Element", "Pierce"},
                {"Power", 140},
                {"Accuracy", .97},
                {"Cost", .19},
                {"Targets", "All"},
                {"Times Hit", 3}, // 1-3, 3 to differentiate from Arrow Rain
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Primal Force", new DataDictionary(){
                {"Element", "Pierce"},
                {"Power", 580},
                {"Accuracy", .95},
                {"Cost", .21},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0.05},
                {"Ailment Chance", 0.00}
            }},
            {"Pralaya", new DataDictionary(){ // instills fear
                {"Element", "Pierce"},
                {"Power", 600},
                {"Accuracy", .95},
                {"Cost", .18},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0.30},
                {"Ailment Chance", 0.50}
            }},

            /// Magic Skills ///
            // fire skills
            {"Agi", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 3},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Maragi", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 6},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Agilao", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Maragion", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Agidyne", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Maragidyne", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Ragnarok", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 30},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Maralagidyne", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 370},
                {"Accuracy", .95},
                {"Cost", 32},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Ice Skills //

            {"Bufu", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 4},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .10}
            }},
            {"Mabufu", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 8},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .08}
            }},
            {"Bufula", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 8},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .10}
            }},
            {"Mabufula", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 16},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .08}
            }},
            {"Bufudyne", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 16},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .10}
            }},
            {"Mabufudyne", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 32},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .08}
            }},
            {"Niflheim", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 32},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            
            // Electricity Skills //

            {"Zio", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 4},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .10}
            }},
            {"Mazio", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 8},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .08}
            }},
            {"Zionga", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 8},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", .10}
            }},
            {"Mazionga", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 16},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .08}
            }},
            {"Ziodyne", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 16},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .10}
            }},
            {"Maziodyne", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 32},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", .08}
            }},
            {"Thunder Reign", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 32},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},

            // Wind Skills //

            {"Garu", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 3},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Magaru", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 6},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Garula", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Magarula", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Garudyne", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Magarudyne", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Panta Rhei", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 30},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            
            // Light Skills //

            {"Hama", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mahama", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .25},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Hamaon", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .40},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mahamaon", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .35},
                {"Cost", 24},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Samsara", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            
            // Darkness Skills //

            {"Mudo", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mamudo", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .25},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mudoon", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .40},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mamudoon", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .35},
                {"Cost", 24},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Die For Me!", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Recovery Skills //
            // power is how much they restore
            
            // Health Restore //
            
            {"Dia", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 50}, 
                {"Accuracy", 1.00},
                {"Cost", 4},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Media", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 40},
                {"Accuracy", 1.00},
                {"Cost", 8},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Diarama", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 160},
                {"Accuracy", 1.00},
                {"Cost", 8},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mediarama", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 50},
                {"Accuracy", 1.00},
                {"Cost", 16},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0}, // i somehow forgot crit on this one
                {"Ailment Chance", 0.00}
            }},
            {"Diarahan", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 1000},
                {"Accuracy", 1.00},
                {"Cost", 20},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mediarahan", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 1000},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            
            // Revival Skills //

            {"Recarm", new DataDictionary(){
                {"Element", "Revive"},
                {"Power", .50},
                {"Accuracy", 1.00},
                {"Cost", 20},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Samarecarm", new DataDictionary(){
                {"Element", "Revive"},
                {"Power", 1.00},
                {"Accuracy", 1.00},
                {"Cost", 20},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            
            // !!! Not properly filled out yet !!
            {"Patra", new DataDictionary(){
                {"Element", "Patra"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 3},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            {"Re Patra", new DataDictionary(){
                {"Element", "Down"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 3},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            {"Me Patra", new DataDictionary(){
                {"Element", "Patra"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            
            ///

            {"Posumudi", new DataDictionary(){
                {"Element", "Poison"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            {"Charmdi", new DataDictionary(){
                {"Element", "Charm"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            {"Enradi", new DataDictionary(){
                {"Element", "Rage"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            {"Amrita", new DataDictionary(){
                {"Element", "Ailments"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 25},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},
            {"Salvation", new DataDictionary(){
                {"Element", "Poison"}, 
                {"Power", 999},
                {"Accuracy", 1.00},
                {"Cost", 60},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 1.00}
            }},


            // Enhancing Skills //

            {"Tarukaja", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Tarunda", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Matarukaja", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Matarunda", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Rakukaja", new DataDictionary(){
                {"Element", "Defense"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Rakunda", new DataDictionary(){
                {"Element", "Defense"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Marakukaja", new DataDictionary(){
                {"Element", "Defense"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Marakunda", new DataDictionary(){
                {"Element", "Defense"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Sukukaja", new DataDictionary(){
                {"Element", "Evasion"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Sukunda", new DataDictionary(){
                {"Element", "Evasion"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Masukukaja", new DataDictionary(){
                {"Element", "Evasion"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Masukunda", new DataDictionary(){
                {"Element", "Evasion"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Dekunda", new DataDictionary(){
                {"Element", "All Stats"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 15},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Dekaja", new DataDictionary(){
                {"Element", "All Stats"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 15},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Rebellion", new DataDictionary(){
                {"Element", "Crit Rate"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "Everyone"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Revolution", new DataDictionary(){
                {"Element", "Crit Rate"}, 
                {"Power", 1},
                {"Accuracy", 1.00},
                {"Cost", 12},
                {"Targets", "Everyone"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Power Charge", new DataDictionary(){
                {"Element", "Power"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 15},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mind Charge", new DataDictionary(){
                {"Element", "Mind"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 15},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            /*
                TODO: Break Spells
                Fire Break
                Ice Break
                Elec Break
                Wind Break

                40 SP each
            */
            // !!These arent done yet they just have entries so the ui stuff doesnt throw a fit in the future!! //
            {"Fire Break", new DataDictionary(){
                {"Element", "Break"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 40},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Ice Break", new DataDictionary(){
                {"Element", "Break"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 40},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Elec Break", new DataDictionary(){
                {"Element", "Break"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 40},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Wind Break", new DataDictionary(){
                {"Element", "Break"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 40},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            ///// 
            
            {"Tetrakarn", new DataDictionary(){
                {"Element", "Reflect Phys"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 55},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Makarakarn", new DataDictionary(){
                {"Element", "Reflect Magic"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 55},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Ailment Skills //

            {"Evil Touch", new DataDictionary(){
                {"Element", "Fear"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            {"Evil Smile", new DataDictionary(){
                {"Element", "Fear"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 10},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            {"Ghastly Wail", new DataDictionary(){ //instant kill if fearful
                {"Element", "Fear"}, 
                {"Power", 1},
                {"Accuracy", 1.00},
                {"Cost", 15},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Pulinpa", new DataDictionary(){
                {"Element", "Panic"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            {"Tentarafoo", new DataDictionary(){
                {"Element", "Panic"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 10},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            
            {"Bewilder", new DataDictionary(){
                {"Element", "Distress"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            {"Eerie Sound", new DataDictionary(){
                {"Element", "Distress"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 10},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.35}
            }},

            {"Poisma", new DataDictionary(){
                {"Element", "Poison"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            {"Poison Mist", new DataDictionary(){
                {"Element", "Poison"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 10},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.35}
            }},
            // skipping virus breath for now:)

            {"Marin Karin", new DataDictionary(){
                {"Element", "Charm"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            {"Sexy Dance", new DataDictionary(){
                {"Element", "Charm"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 10},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},

            {"Provoke", new DataDictionary(){
                {"Element", "Rage"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.40}
            }},
            {"Infuriate", new DataDictionary(){
                {"Element", "Rage"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 10},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.35}
            }},

            // Almighty Skills :> //
            {"Life Drain", new DataDictionary(){
                {"Element", "HP Drain"}, 
                {"Power", 35},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Spirit Drain", new DataDictionary(){
                {"Element", "SP Drain"}, 
                {"Power", 20},
                {"Accuracy", 1.00},
                {"Cost", 5},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Foul Breath", new DataDictionary(){
                {"Element", "Ailment Sus"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 15},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Stagnant Air", new DataDictionary(){
                {"Element", "Ailment Sus"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 15},
                {"Targets", "Everyone"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Megido", new DataDictionary(){
                {"Element", "Almighty"}, 
                {"Power", 180},
                {"Accuracy", 0.95},
                {"Cost", 45},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Megidola", new DataDictionary(){
                {"Element", "Almighty"}, 
                {"Power", 320},
                {"Accuracy", 0.95},
                {"Cost", 65},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Megidolaon", new DataDictionary(){
                {"Element", "Almighty"}, 
                {"Power", 650},
                {"Accuracy", 0.95},
                {"Cost", 85},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Black Viper", new DataDictionary(){
                {"Element", "Almighty"}, 
                {"Power", 950},
                {"Accuracy", 1.00},
                {"Cost", 60},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Morning Star", new DataDictionary(){
                {"Element", "Almighty"}, 
                {"Power", 800},
                {"Accuracy", 1.00},
                {"Cost", 80},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            
            // Pass your turn //
            {"Wait", new DataDictionary(){ // assign this until guard is more implemented
                {"Element", "Pass"},  // default to wait if the user takes too long to choose a move
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Guard", new DataDictionary(){
                {"Element", "Pass"}, 
                {"Power", 1},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Error", new DataDictionary(){ 
                {"Cost", -1}
            }}
        }
    }
    };
    public DataDictionary passiveSkills = new DataDictionary(){
            // Passive Skills //
            /* 
                All Cost = 0
                Power is the multiplier
                    Negative Numbers is defensive
                Resist, Repel, and Absorb will add it to the persona's list
                    Add it when its iterating through the spells to display them
                Times Hit indicates whether it will resist, repel, or absorb the attack
                    1 - Resist
                    2 - Null
                    3 - Repel
                    4 - Absorb
                Accuracy is the chance that the skill will activate
            */
        {0, new DataDictionary(){
            /// Defence ///
            // slash //
            {"Dodge Slash", new DataDictionary(){
                {"Element", "Slash"}, 
                {"Power", -2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Evade Slash", new DataDictionary(){
                {"Element", "Slash"}, 
                {"Power", -3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Slash", new DataDictionary(){
                {"Element", "Slash"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Slash", new DataDictionary(){
                {"Element", "Slash"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Slash", new DataDictionary(){
                {"Element", "Slash"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Absorb Slash", new DataDictionary(){
                {"Element", "Slash"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 4},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Strike //
            {"Dodge Strike", new DataDictionary(){
                {"Element", "Strike"}, 
                {"Power", -2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Evade Strike", new DataDictionary(){
                {"Element", "Strike"}, 
                {"Power", -3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Strike", new DataDictionary(){
                {"Element", "Strike"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Strike", new DataDictionary(){
                {"Element", "Strike"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Strike", new DataDictionary(){
                {"Element", "Strike"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Absorb Strike", new DataDictionary(){
                {"Element", "Strike"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 4},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Pierce //
            {"Dodge Pierce", new DataDictionary(){
                {"Element", "Pierce"}, 
                {"Power", -2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Evade Pierce", new DataDictionary(){
                {"Element", "Pierce"}, 
                {"Power", -3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Pierce", new DataDictionary(){
                {"Element", "Pierce"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Pierce", new DataDictionary(){
                {"Element", "Pierce"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Pierce", new DataDictionary(){
                {"Element", "Pierce"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Absorb Pierce", new DataDictionary(){
                {"Element", "Pierce"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 4},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            
            // Fire //
            {"Dodge Fire", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", -2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Evade Fire", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", -3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Fire", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Fire", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Fire", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Absorb Fire", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 4},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            
            // Ice //
            {"Dodge Ice", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", -2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Evade Ice", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", -3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Ice", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Ice", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Ice", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Absorb Ice", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 4},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Elec //
            {"Dodge Elec", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", -2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Evade Elec", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", -3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Elec", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Elec", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Elec", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Absorb Elec", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 4},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Wind //
            {"Dodge Wind", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", -2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Evade Wind", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", -3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Wind", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Wind", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Wind", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Absorb Wind", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 4},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Light //
            {" Light", new DataDictionary(){
                {"Element", "Light"}, 
                {"Power", 0},
                {"Accuracy", 0.50},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Endure Light", new DataDictionary(){
                {"Element", "Light"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Light", new DataDictionary(){
                {"Element", "Light"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Light", new DataDictionary(){
                {"Element", "Light"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Light", new DataDictionary(){
                {"Element", "Light"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Darkness //
            {"Survive Dark", new DataDictionary(){
                {"Element", "Darkness"}, 
                {"Power", 0},
                {"Accuracy", 0.50},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Endure Dark", new DataDictionary(){
                {"Element", "Darkness"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Resist Dark", new DataDictionary(){
                {"Element", "Darkness"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 1},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Null Dark", new DataDictionary(){
                {"Element", "Darkness"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 2},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Repel Dark", new DataDictionary(){
                {"Element", "Darkness"}, 
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 3},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            /// Offensive ///
            // Fire //
            {"Fire Boost", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", 1.25},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Fire Amp", new DataDictionary(){
                {"Element", "Fire"}, 
                {"Power", 1.50},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Ice //
            {"Ice Boost", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", 1.25f},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Ice Amp", new DataDictionary(){
                {"Element", "Ice"}, 
                {"Power", 1.50},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Elec //
            {"Elec Boost", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", 1.25},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Elec Amp", new DataDictionary(){
                {"Element", "Elec"}, 
                {"Power", 1.50},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Wind //
            {"Wind Boost", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", 1.25},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Wind Amp", new DataDictionary(){
                {"Element", "Wind"}, 
                {"Power", 1.50},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Light/Dark //
            // increases the success rate of hama and mudo skills
            // the way im doing this rn doesnt make the most sense but im tired
            {"Hama Boost", new DataDictionary(){
                {"Element", "Light"}, 
                {"Power", 0},
                {"Accuracy", 1.50},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Mudo Boost", new DataDictionary(){
                {"Element", "Darkness"}, 
                {"Power", 0},
                {"Accuracy", 1.50},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // Ailments //
            {"Fear Boost", new DataDictionary(){
                {"Element", "Fear"}, 
                {"Power", 0},
                {"Accuracy", 1.50},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Panic Boost", new DataDictionary(){
                {"Element", "Panic"}, 
                {"Power", 0},
                {"Accuracy", 1.50},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            /*
            TODO: 
                Null Fear
                Null Panic
                Null Poison,
                Null Charm
                Null Rage
                Null Shock
                Null Freeze
                Unshaken Will - Asura Exclusive, Protects from all ailments
                    I wonder what these do
            */

            // skills that activate at the start of the battle //
            
            {"Auto-Tarukaja", new DataDictionary(){
                {"Element", "Attack"}, 
                {"Power", 0},
                {"Accuracy", 1},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Auto-Tarunda", new DataDictionary(){
                {"Element", "Attack"}, 
                {"Power", 0},
                {"Accuracy", 1},
                {"Cost", 0},
                {"Targets", "Party"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Auto-Rakukaja", new DataDictionary(){
                {"Element", "Defense"}, 
                {"Power", 0},
                {"Accuracy", 1},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Auto-Maraku", new DataDictionary(){
                {"Element", "Defense"}, 
                {"Power", 0},
                {"Accuracy", 1},
                {"Cost", 0},
                {"Targets", "Party"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            {"Auto-Sukukaja", new DataDictionary(){
                {"Element", "Evasion"}, 
                {"Power", 0},
                {"Accuracy", 1},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Auto-Masuku", new DataDictionary(){
                {"Element", "Evasion"}, 
                {"Power", 0},
                {"Accuracy", 1},
                {"Cost", 0},
                {"Targets", "Party"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            /// Recovery ///
            // strengrthens recovery magic by 100% 
            {"Divine Grace", new DataDictionary(){
                {"Element", "Recovery"}, 
                {"Power", 2},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // regen hp //
            // these do stack :)
            {"Regenerate 1", new DataDictionary(){
                {"Element", "Recovery"}, 
                {"Power", 0.02},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Regenerate 2", new DataDictionary(){
                {"Element", "Recovery"}, 
                {"Power", 0.04},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Regenerate 3", new DataDictionary(){
                {"Element", "Recovery"}, 
                {"Power", 0.06},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Spring of Life", new DataDictionary(){ // trismegistus exclusive
                {"Element", "Recovery"}, 
                {"Power", 0.08},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            // regen sp each turn //
            // these also stack :)
            {"Invigorate 1", new DataDictionary(){
                {"Element", "SP Recovery"}, 
                {"Power", 3},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Invigorate 2", new DataDictionary(){
                {"Element", "SP Recovery"}, 
                {"Power", 5},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Invigorate 3", new DataDictionary(){
                {"Element", "SP Recovery"}, 
                {"Power", 7},
                {"Accuracy", 1.00},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},

            /// The Rest ///
            /*
            TODO:
                Cool Breeze
                Victory Cry
                    Restores HP and SP after the battle
                    touch up on these after the turn logic is built
                Endure
                Enduring Soul
                    Restores health upon death once per battle
                    these do stack with eachother
            */
            // Counter Skills //
            {"Counter", new DataDictionary(){
                {"Element", "Counter"}, 
                {"Power", 0},
                {"Accuracy", 0.15},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"Counterstrike", new DataDictionary(){
                {"Element", "Counter"}, 
                {"Power", 0},
                {"Accuracy", 0.30},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            {"High Counter", new DataDictionary(){
                {"Element", "Counter"}, 
                {"Power", 0},
                {"Accuracy", 0.50},
                {"Cost", 0},
                {"Targets", "Self"},
                {"Times Hit", 0},
                {"Critical", 0},
                {"Ailment Chance", 0.00}
            }},
            /*
                Apt Pupil
                    Increases users crit rate
                Sharp Student
                    Decreases chance to be hit by crits
                Raging Tiger
                    Doubles attack while enraged
                Weapons Master
                    Doubles damage dealt with weapons
                    may add if i add weapons
                Arms Master
                Spell Master
                    Halves cost for skills
                Magic Skill up (Messiah)
                    Strengthens all magical attacks, including almighty, by 25%

            */
            // no Alertness, Fast Retreat, Growth 1-3, trafuri

            // return this if the skill cannot be found //
        }}};    

    public override void OnPlayerJoined(VRCPlayerApi player){
        if (player.isLocal){
            setStat(self, "", "Name", player.displayName);
            //setStat(others, "", "Name", player.displayName);
        }
        else{
            bool space = setStat(self, "", "Name", player.displayName); // was different when there was a seperate dictionary for others
            if (!space){
                Debug.Log("Haha " + player.displayName + " cant fit in the dictionary haha");
            }
        }
        // add the player to the dictionary when they join //
        
    }

    public override void OnPlayerLeft(VRCPlayerApi player){
        if (!player.isLocal){ // i dont need to remove the local player from the dictionary because thats not the local players problem anymore :->
            clearEntry(player.displayName, true);
            setStat(self, player.displayName, "Name", "");
        }
    }

    /// METHODS TO INTERACT WITH THE DICTIONARY ///
    
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
        Debug.LogWarning("Could not Find " + strToFind + " under " + keyToSrch + " in Dictionary");
        return (-1);
    } 

    // Get Stat for if you know the id //
    public static string getStat(DataDictionary dict, int id, string key){
        if (id != -1){
            var stat = (dict[id].DataDictionary[key]);
            //Debug.Log($"getStat returns {stat}");
            return (stat.ToString());
        }
        return ("Something went wrong");
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
        return (strStat.Split(','));
    }

    // replaces the current stat with a new string //
    public static bool setStat(DataDictionary dict, string uStr, string statToChange, string newStat){
        var id = findID(dict, uStr);
        //Debug.Log($"{newStat} replacing {statToChange} at id {id}");
        if (id != -1){
            dict[id].DataDictionary[statToChange] = newStat;
            return true;
        }
        else{return false;}
    }

    // changes the boolean //
    public static void setStat(DataDictionary dict, string uStr, string statToChange, bool newStat){
        var id = findID(dict, uStr);
        dict[id].DataDictionary[statToChange] = newStat;
    }
    // returns an entire data dictionary segment //
    public static DataDictionary getDict(DataDictionary dict, int id){
        if (dict.TryGetValue(id, TokenType.DataDictionary, out DataToken value)){
            return (value.DataDictionary);
        }
        else{return null;}
    }
    // simple way to get back a specific skill's information //
    public static DataDictionary getSkillInfo(Dictionaries mainDict, string skill){
        var dict = Dictionaries.getDict(mainDict.skillDict, 0);
        if (dict.TryGetValue(skill, TokenType.DataDictionary, out DataToken value)){
            return (value.DataDictionary);
        }
        else{
            return (dict["Error"].DataDictionary);
        }
    }
    // returns the number of active entities in the self dicitonary //
    // check which ones have names
    // i can maybe update this one for the other players for when they attack the entire party
    public static int countActive(Dictionaries mainDict, DataDictionary dict, string tag="*"){
        var count = 0;
        for (int i = 0; i < dict.Count; i++){
            var name = Dictionaries.getStat(dict, i, "Name");
            var tags = Dictionaries.getStat(dict, i, "Tag");
            if (tag == "*" || tags == tag){ // allow any options if tag is left as *
                if (name != "" && name != "_"){
                count++;
                }
            }
        }
        return (count);
    }

    // returns the unique names for everyone that falls under the tag //
    public static string[] getActiveArray(Dictionaries mainDict, DataDictionary dict, string tag="*"){
        string[] returnArr = new string[countActive(mainDict, dict, tag)];
        int count = 0;
        for (int i = 0; i < dict.Count; i++){
            var name = getStat(dict, i, "Name");
            var tags = getStat(dict, i, "Tag");
            if (tag == "*" || tags == tag){
                if (name != "" && name != "_"){
                    returnArr[count] = name;
                    count++;
                }
            }
        }
        return (returnArr);
    }
    
    ////////

    // displays the stats from the entire dictionary //
    public void displayPlayers(){
        string displayText = "";
        /*
        var selfDict = getDict(self, 0);
        DataList selfKeys = selfDict.GetKeys();
        for (int i = 0; i < selfKeys.Count; i++){
            displayText += selfKeys[i] + ": " + selfDict[selfKeys[i]] + " "; 
        }
        displayText += "\n";
        */
        DataList keys = self.GetKeys();
        keys.Sort();

        for (int i = 0; i < keys.Count; i++){
            displayText += i + ". ";
            if (self.TryGetValue(i, TokenType.DataDictionary, out DataToken dict)){
                DataList newKeys = dict.DataDictionary.GetKeys();
                for (int j = 0; j < newKeys.Count; j++){
                    displayText += newKeys[j].String + ": " + getStat(self, i, newKeys[j].String) + " ";
                }
            }
            displayText += "\n";
        }
        board.text = displayText;
    }

    private void copyPreset(string name, string presetName){
        var tag = getStat(self, name, "Tag");
        var id = findID(self, name);
        DataDictionary preset = null; // get the preset dictionary 
        if (tag.Equals("player")){
            preset = Presets.getDict(presetList.personas, "presetName");
        }
        else{
            preset = Presets.getDict(presetList.personas, "presetName");
        }
        if (preset != null){
            DataList keys = preset.GetKeys();
            keys.Sort();
            for (int i = 0; i < keys.Count; i++){
                setStat(self, name, keys[i].ToString(), preset[keys[i]].ToString()); // i think making them strings should be fine
                if (keys[i].Equals("Max HP") || keys[i].Equals("Max SP")){ // its been a while since ive touched a lot of this code _._
                    setStat(self, name, keys[i].ToString().Substring(2), preset[keys[i]].ToString());  // set the hp/sp to max on preset change
                }
            }
        }
    }
    // sets all the stats in a segment to defaults
    public void clearEntry(string name, bool removePersonal=false){
        copyPreset(name, "Blank");
        if (removePersonal){
            copyPreset(name, "Personal Blank");
        }
    }

    public bool changeNum(string uName, string numKey, int changeInNum, DataDictionary dictToChange, bool cantGoUnder=false){
        string num = getStat(dictToChange, uName, numKey);
        bool result = int.TryParse(num, out int intNum);
        if (result){
            intNum += changeInNum;
            // prevent the number from going over the max if HP or SP
            if (numKey.Equals("HP") || numKey.Equals("SP")){
                string max = getStat(dictToChange, uName, "Max " + numKey);
                int.TryParse(max, out int maxNum);
                if (intNum > maxNum){
                    intNum = maxNum;
                }
            }
            if (intNum < 0){
                if (cantGoUnder){
                    return (false);
                }
                else{ // the numbers dont need to go into the negatives
                    intNum = 0;
                }
            }
            num = intNum.ToString(); // convert back to string :>
            setStat(dictToChange, uName, numKey, num); // change contents of dict
            Debug.Log("Changed " + numKey + "!");
            displayPlayers(); // update board
            return (true);
        }
        else{
            return (false);
        }
    }

    public static void removeEnemy(string name){
        // TODO: add this
        //clearEntry(name);
    }
    public static string determineSkillType(Dictionaries mainDict, string skill){
        DataDictionary skillInfo = Dictionaries.getSkillInfo(mainDict, skill);
        if (skillInfo["Element"].String.Equals("Slash") || skillInfo["Element"].String.Equals("Pierce") || skillInfo["Element"].String.Equals("Strike")){ 
            return ("Physical");
        }
        else{
           return ("Magic");
        }
    }
    public static string determineSkillType(DataDictionary skillInfo){
        if (skillInfo["Element"].String.Equals("Slash") || skillInfo["Element"].String.Equals("Pierce") || skillInfo["Element"].String.Equals("Strike")){ 
            return ("Physical");
        }
        else{
           return ("Magic");
        }
    }

    // returns all stat changes 
    public static string[] getStatChanges(DataDictionary entityStats){
        string statChangeStr = entityStats["Stat Changes"].String;
        if (statChangeStr.Length != 0){
            string[] statChanges = statChangeStr.Split(',');
            return (statChanges);
        }
        else{
            string[] arr = {}; // empty array
            return (arr);
        }
    }
    // returns specific stat changes as an array //
    public static string[] getStatChanges(DataDictionary entityStats, string stat){
        var statArr = getStatChanges(entityStats);
        string[] empty = new string[3];
        if (statArr.Length != 0){
            foreach(string statChange in statArr){
                var statChangeArr = parseStatChange(statChange);
                if (statChangeArr[0].Equals(stat)){
                    return (statChangeArr); // [stat, change]
                }
            }
            return (null);
        }
        else{
            return (null);
        }
    }


    /// <returns>Returns an array with the information about the stat change</returns>
    public static string[] parseStatChange(string statChange){
        string change = statChange[statChange.Length - 2].ToString();
        string time = statChange[statChange.Length - 1].ToString();
        string status = statChange.Substring(0, statChange.Length - 2); // saves the substring starting 2 positions off the end // basically [0:-2] :)
        string[] statChangeArr = {status, change, time};
        return statChangeArr;
    }
    // change - false + true
    // 
    public static string packStatChange(Dictionaries dict, string stat, bool change, int timer=3){
        string packStat = "";
        string shortStat = dict.shortStatChanges[Array.IndexOf(dict.statChanges, stat)]; // get abbreviated form of the stat change
        packStat += shortStat;
        if (change){packStat += "+";}
        else {packStat += "-";}
        packStat += timer + "";
        return (packStat);
    }

    public static void applyStatChange(Dictionaries dict, string target, string stat, bool change){
        var shortStat = dict.shortStatChanges[Array.IndexOf(dict.statChanges, stat)];
        var statChanges = getStatChanges(getDict(dict.self, findID(dict.self, target)));
        string packet = packStatChange(dict, stat, change) + ",";
        string allStats = "";
        if (statChanges.Length > 0){
            foreach (var stats in statChanges){
                string[] checkStats = parseStatChange(stats);
                if (checkStats[0].Equals(shortStat)){
                    if (checkStats[1].Equals("+") == change){ // if the existing stat is the same as the new stat
                        allStats += stats;
                    }
                    else{ // replace it if the stat on there is opposite of what it will be
                        allStats += packet;
                    }
                }
                else{
                    allStats += stat;
                }
                allStats += ",";
            }
            setStat(dict.self, target, "Stat Changes", allStats);
        }
        else{
            setStat(dict.self, target, "Stat Changes", packet);
        }
    }
    // decreases all of the timers on one entries stat changes and repackages them //
    public static void decreaseStatTimer(Dictionaries mainDict, string uStr){
        string[] statChanges = Dictionaries.getArray(mainDict.self, uStr, "Stat Changes", "Name");
        string[] newStats = new string[4];
        int count = 0;
        for (int i = 0; i < statChanges.Length; i++){
            string timeStr = statChanges[i][statChanges[i].Length - 1].ToString(); // get the number
            int time = int.Parse(timeStr) - 1; // decrease the timer by one :)
            
            // only add if theres time left on the timer
            // stats are removed at the start of the turn
            if (time > 0){
                newStats[i] = (statChanges[i].Remove(statChanges[i].Length - 1) + time);
                count += 1; 
            }
        }
        string newStr = "";
        if (newStats.Length != 0){
            newStr = string.Join(",", newStats);
        }
        Dictionaries.setStat(mainDict.self, uStr, "Stat Changes", newStr);
    }

    public static Vector3 getLocation(string name){
        GameObject creature = GameObject.Find(name);
        if (creature != null){
            return creature.transform.position;
        }
        else{return new Vector3(0, 0, 0);}
        
    }

    public static void refreshMenu(){
        var evokerParent = GameObject.Find("evokerHolder"); // the parent go that holds all the evokers
        // put all the children in a box
        // GameObject[] evokers = GameObject[10];
        var numEvokers = evokerParent.transform.childCount;
        for (int i = 0; i < numEvokers; i++){
            var evoker = evokerParent.transform.GetChild(i).gameObject; // get the iterations funny
            var DPscript = (displaySkills)evoker.GetComponent(typeof(UdonBehaviour));
            DPscript.refreshMenu();
        }
    }

    // pass on to the damage calc script //
    // returns the value spent (hp/sp) //
    public static string calculateDamage(Dictionaries mainDict, string user, string target, string skill, VRCPlayerApi player){
        DataDictionary skillInfo = Dictionaries.getDict(mainDict.skillDict, 0)[skill].DataDictionary; // this will error if a bad spell is sent thru, i count check if the spell is in the list but that would be costly
        var damage = damageCalc.damageTurn(mainDict, user, target, skillInfo, mainDict.network, player);
        var skillType = Dictionaries.determineSkillType(skillInfo);
        var skillTarget = skillInfo["Targets"].String;
        bool canUse;
        string strReturn = "";
        // Cost the user HP/SP for the skill //
        if (skillType.Equals("Magic")){
            canUse = mainDict.changeNum(user, "SP", (int) (skillInfo["Cost"].Float * -1), mainDict.self, true);
            strReturn = "SP";
        }
        else{
            var maxHP = Dictionaries.getStat(mainDict.self, user, "Max HP");
            var cost = (int) (((float.Parse(maxHP)) * skillInfo["Cost"].Double) * -1); 
            canUse = mainDict.changeNum(user, "HP", cost, mainDict.self, true);
            strReturn = "HP";
        }
        // deal the damage to the target //
        if (canUse){ // can only use if the user has enough hp/sp
            string logMessage = user + " used " + skill + " on ";
            if (skillTarget.Equals("One")){
                // could make this into its own function but i dont want to do that yet -.-
                var damageDealt = damageCalc.damageTurn(mainDict, user, target, skillInfo, mainDict.network, player);
                if (damageDealt != -1){
                    mainDict.network.changeNumO(mainDict, target, "HP", damageDealt, true, player);
                    mainDict.changeNum(target, "HP", damageDealt * -1, mainDict.self);
                    updateText.changeEnemyText(target, "-" + damageDealt + "\n" + Dictionaries.getStat(mainDict.self, target, "HP") + "/" + Dictionaries.getStat(mainDict.self, target, "Max HP"));
                    logMessage += target + " dealing " + damageDealt + " damage.";
                }
                else{
                    updateText.changeEnemyText(target, "" + "\n" + Dictionaries.getStat(mainDict.self, target, "HP") + "/" + Dictionaries.getStat(mainDict.self, target, "Max HP"));
                    updateText.enemyHitText(target, "Miss");
                    logMessage += target + " and missed.";
                }
                mainDict.log.addToLog(logMessage);
                return (strReturn);
            }
            else if (skillTarget.Equals("Ally")){
                int amtHeal = skillInfo["Power"].Int;
                // Skills that heal //
                if (amtHeal != 0){
                    mainDict.changeNum(target, "HP", amtHeal, mainDict.self); // gonna have to change this one when adding syncing
                    updateText.changeEnemyText(target, "+" + skillInfo["Power"].Int + "\n" + Dictionaries.getStat(mainDict.self, target, "HP") + "/" + Dictionaries.getStat(mainDict.self, target, "Max HP"));
                    logMessage += target + " healing " + skillInfo["Power"].Int + " health.";
                }
                mainDict.log.addToLog(logMessage);
                return (strReturn);
            }
            else if (skillTarget.Equals("All")){
                string targetTeam;
                if (getStat(mainDict.self, user, "Tag", "Name").Equals("player") && skillTarget.Equals("All")){targetTeam = "enemy";}
                else{targetTeam = "player";} 
                var enemyCount = Dictionaries.countActive(mainDict, mainDict.self, targetTeam); // too lazy to change the name
                logMessage += "all " + targetTeam + "s: ";
                string[] targetNames = getActiveArray(mainDict, mainDict.self, targetTeam);
                for (int i = 0; i < enemyCount; i++){
                    // calculate damage for the enemy
                    var loopTarget = targetNames[i];
                    var damageDealt = damageCalc.damageTurn(mainDict, user, loopTarget, skillInfo, mainDict.network, player);
                    // this part could probably be its own function v
                    if (damageDealt != -1){
                        mainDict.changeNum(loopTarget, "HP", damageDealt * -1, mainDict.self);
                        updateText.changeEnemyText(loopTarget, "-" + damageDealt + "\n" + Dictionaries.getStat(mainDict.self, loopTarget, "HP") + "/" + Dictionaries.getStat(mainDict.self, loopTarget, "Max HP"));
                        logMessage += "[" + loopTarget + ", " + damageDealt + " damage], ";
                        }
                    else{
                        updateText.changeEnemyText(loopTarget, "" + "\n" + Dictionaries.getStat(mainDict.self, loopTarget, "HP") + "/" + Dictionaries.getStat(mainDict.self, loopTarget, "Max HP"));
                        updateText.enemyHitText(loopTarget, "Miss");
                        logMessage += "[" + loopTarget + ", Missed], ";
                    }   
                }
                mainDict.log.addToLog(logMessage + ".");
                return (strReturn);
            }
            else{ // if the skills target hasnt been added yet 
                return (null);
            }
        }
        else{
            return (null);
        }
    }
    public void networkTest(){
        Debug.Log("test sent");
        network.KSCNE("test", null, Networking.GetOwner(hi));
    }
}