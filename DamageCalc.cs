
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using VRC.SDK3.Data;
using System.Linq;

// same as old damageCalc but now has an udon object with it in unity
public class damageCalc : UdonSharpBehaviour
{
    public Dictionaries statDict;
    public int test;
        
    // determines a number for the damage to be multiplied by
    public static float determineAmp(DataDictionary enemyStats, string attackElement, bool isDown, double skillAmp){
        string[] defences = {"Strengths", "Nullifies", "Absorb", "Reflect", "Weak"};
        int resistance = 5;
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
            case 0: //  nullify
                amplifier *= 0;
                break;
            case 1: // weak
                amplifier *= 1.5;
                break;
            case 2: // resist
                amplifier *= 0.5;
                break;
            default:
                amplifier *= 1;
                break;
        };
        //Debug.Log("Amp: " + amplifier);
        if (isDown){amplifier *= 1;}
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

    // calculates the damage dealt to an opponent //
    public static int damageTurn(Dictionaries mainDict, string userName, string targetName, DataDictionary skillInfo){
        // get the dictionaries for the user and enemies stats //
        int userId = Dictionaries.findID(mainDict.self, userName); // call the general for the static method and pull the nonstatic dictionary 
        DataDictionary userStats = Dictionaries.getDict(mainDict.self, userId);
        int targetId = Dictionaries.findID(mainDict.self, targetName); 
        DataDictionary targetStats = Dictionaries.getDict(mainDict.self, targetId);
        // Determind if the move is physical or magical //
        int power = 0;
        if (Dictionaries.determineSkillType(skillInfo).Equals("Physical")){ 
            power = userStats["St"].Int; 
        }
        else{
            power = userStats["Mg"].Int; 
        }
        var amplifier = determineAmp(targetStats, skillInfo["Element"].String, false, 1);
        var damage = calcDamage(power, userStats["LVL"].Float, targetStats["En"].Float, targetStats["LVL"].Float, skillInfo["Power"].Float, amplifier);
        //Debug.Log(damage);
        return (damage);
    }
}
