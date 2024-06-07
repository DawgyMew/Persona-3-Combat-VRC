using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VRC.Udon;

namespace ConsoleApp1

    public class skills
    {
        // https://megamitensei.fandom.com/wiki/List_of_Persona_3_Skills //
        // store the skills within a dictionary //

        /*
            Element: The Type of Skill
                Slash, Strike, Pierce - Physical
                Fire, Ice, Elec, Wind - Magic
                Almighty - Magic but Funnier
                Light, Darkness - Instant Kills
                Recovery - Healing
                Revive - Revive from death
                Attack, Defense, Evasion, Crit Rate, All Stats - Stat Changes
                Reflect Phys, Reflect Magic - Barriers that reflect physical/magic skills once
                Fear, Panic, Distress, Poison, Charm, Rage, Ailments - Cause/Heal Ailments
                HP Drain, SP Drain - Transfers HP/SP from the opponent to the user.
            Power: How much damage the attack does. (Healing power for recovery moves)
            Accuracy: How likely the move is to hit. (1.00 is guarenteed to hit(maybe not if evasion lowered))
            Cost: The amount of SP/Percentage of HP used when the move is used.
            Targets: Who the move will hit.
                One/All: Opposing Team (Enemies)
                Ally/Party: Team Using the ability (Party)
                Everyone: Everyone alive on the field
                Self: The user of the skill
            Critical: Chance to knock down the opponent even if they are not weak to the element
            Ailment Chance: Chance to apply an ailment such as shock or poison.
        */
        public DataDictionary skillDict = new DataDictionary(){
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
                {"Accuracy", .97,
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

            // TODO: add these ones
            /*
            Patra
            Re Patra
            Me Patra
            */
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

            /*
            TODO:  
                Foul Breath
                Stagnant Air
            */

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
                {"Power", 65o},
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

            // passive skills are gonna be so much fun -.-
            // make the attack menu not show skills with a cost of 0?
        };
    }
}