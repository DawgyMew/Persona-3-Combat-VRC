﻿
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Data;
using System.Linq;

public class damageCalc : UdonSharpBehaviour
{
    public Dictionaries statDict;
    // determines a number for the damage to be multiplied by
    public static float determineAmp(DataDictionary enemyStats, string attackElement, bool isDown, double skillAmp, Dictionaries mainDict){
        string[] defences = {"Strengths", "Nullifies", "Absorb", "Reflect", "Weak"};
        int resistance = 0;
        for (int i = 0; i < defences.Length; i++){
            string eleStr = enemyStats[defences[i]].String;
            string[] elements = eleStr.Split(',');
            foreach (string element in elements){
                if (element.Equals(attackElement)){
                    resistance = i;
                    break;
                }
            } 
            if (resistance != 0){break;}
        }
        double amplifier = 1;
        switch(resistance){
            case 0: // strengths
                amplifier *= 0.5f;
                break;
            case 1: // Nulls
                amplifier *= 0;
                break;
            case 2: // Absorb
                amplifier *= -1;
                break;
            case 3: // Reflect
                amplifier *= 1;
                break;
            case 4: // weak
                amplifier *= 1.5f;
                string name = enemyStats["Name"].String;
                updateText.enemyHitText(name, "Weak");
                if (!isDown){
                    mainDict.setStat(mainDict.self, name, "isDown", true); // change the state of the enemy to downed if not already
                }
                else{
                    mainDict.setStat(mainDict.self, name, "Ailment", "Dizzy"); // make them dizzy if they were already downed

                }
                break;
            default:
                amplifier *= 1;
                break;
        };
        // i forgot that i dont know how much the damage was affected by the downed status
        if (isDown){amplifier *= 1;} // this will still be the old downed status which works for this
        amplifier *= skillAmp;
        return ((float) amplifier);
    }
    
    // The main formula for damage calculation //
    public static int calcDamage(float power, float PLV, float eEndurance, float ELV, float moveDamage, float amplifier){
        // power works as either magic or strength
        float rngSwing = (float) Random.Range(-10, 10) / 100; // attacks are affected by a random 10% swing
        //Debug.Log("Random: " + rngSwing);
        float damage = Mathf.Sqrt((power / eEndurance) * (PLV / ELV) * moveDamage) * 7.4f * amplifier;
        //Debug.Log((damage + (damage * -.1)) + " - " + (damage + (damage * .1))) ;
        damage += damage * rngSwing; // apply the rng junk here
        //Debug.Log(damage);
        int roundDamage = (int) Mathf.Round(damage);
        return (roundDamage); 
    }

    // true if hit, false if miss
    // accepts Accuracy, Critical, and Ailment Chance
    public static bool determineHit(DataDictionary skill, string stat){
        Debug.Log(stat);
        var accuracy = skill[stat].Number;
        int randNum = Random.Range(0, 100); // could add a luck thing here but thatd be some work
        return (randNum <= accuracy * 100); // this probably isnt the greatest way to do it but here we are
    }

    public static string determineAilment(Dictionaries dict, DataDictionary skill, string targetName){
        if (determineHit(skill, "Ailment Chance")){ // run if the ailment chance hits
            string ail;
            switch (skill["Element"].String){
                case "Ice":
                    ail = "Freeze";
                    break;
                case "Elec":
                    ail = "Shock";
                    break;
                case "Pierce": // need to make it so Poison arrow works
                    ail = "Charm";
                    break;
                default:
                    ail = skill["Element"].String;
                    break;
            }
            dict.setStat(dict.self, targetName, "Ailment", ail);
            return (ail);
        } 
        else{return ("");}
    }

    // calculates the damage dealt to one specific opponent //
    // loop call the function if hitting multiple //
    ///
    public static int damageTurn(Dictionaries mainDict, string userName, string targetName, DataDictionary skillInfo, networking network, VRCPlayerApi player){
        // determine if the skill is going to hit //
        bool hit = determineHit(skillInfo, "Accuracy");
        if (hit){
            // get the dictionaries for the user and enemies stats //
            int userId = Dictionaries.findID(mainDict.self, userName); // call the general for the static method and pull the nonstatic dictionary 
            DataDictionary userStats = Dictionaries.getDict(mainDict.self, userId);
            int targetId = Dictionaries.findID(mainDict.self, targetName); 
            DataDictionary targetStats = Dictionaries.getDict(mainDict.self, targetId);

            // Determine if the move will apply an ailment //
            string ailment = targetStats["Ailment"].String;
            if (ailment.Equals("")){ // cant get a different ailment
                var ail = determineAilment(mainDict, skillInfo, targetName);
                if (!ail.Equals("")){
                    network.statFromArrayIndexO(mainDict, targetName, "Ailment", 0, ail, player);
                }
            }
            // Determind if the move is physical or magical //
            float power = 0;
            if (Dictionaries.determineSkillType(skillInfo).Equals("Physical")){
                power = userStats["St"].Float; 
            }
            else{
                power = userStats["Mg"].Float; 
            }

            // apply stat changes //
            string change;
            // check user attack
            var statDif = Dictionaries.getStatChanges(userStats, "atk");
            if (statDif != null){
                change = statDif[1];
                if (change.Equals("+")){
                    power *= 1.50f;
                }
                else if (change.Equals("-")){
                    power *= .50f;
                }
            }
            // check target defence 
            float endurance = targetStats["En"].Float;
            statDif = Dictionaries.getStatChanges(targetStats, "df");
            if (statDif != null){
                change = statDif[1];
                if (change.Equals("+")){
                    endurance *= 1.66f;
                }
                else if (change.Equals("-")){
                    endurance *= .66f;
                }
            }
            int powerInt = (int) power;
            // calculate damage
            var amplifier = determineAmp(targetStats, skillInfo["Element"].String, false, 1, mainDict);
            // check if crit 
            if (determineHit(skillInfo, "Critical")){
                updateText.enemyHitText(targetName, "Crit");
                amplifier *= 1.50f;
            }
            var damage = calcDamage(power, userStats["LVL"].Float, endurance, targetStats["LVL"].Float, skillInfo["Power"].Float, amplifier);
            //Debug.Log(damage);
            return (damage);
        }
        else{
            return (-1); // miss
        }
    }
}
