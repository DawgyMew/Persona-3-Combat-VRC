
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;
using VRC.SDK3.Data;
using System;
public class displaySkills : UdonSharpBehaviour
{
    public Dictionaries dictionary;
    // Skill Displaying //
    public TextMeshProUGUI txtBox; // the textbox that the skills are put onto
    public RectTransform skillBack; // scrolling
    public int currentStartY = 69; // the y value of the scrolling
    public string leftText; // the text that is displayed
    public TextMeshProUGUI description; // textbox where the skill description is held

    /// Status Displaying ///
    public Image ailmentBox; // the image that will have the ailment put onto it
    public TextMeshProUGUI statChangeTxt; // where the change of stats will be reflected

    // Materials //
    public Material charm;
    public Material distress;
    public Material dizzy;
    public Material fear;
    public Material freeze;
    public Material panic;
    public Material shock;
    public Material rage;
    public Material poison;

    public Material plink;

    public override void OnPickup(){
        if (GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.isLocal){
            string playerName = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.displayName;
            var dictionaryGO = GameObject.Find("Dictionary");
            dictionary = (Dictionaries)dictionaryGO.GetComponent(typeof(UdonBehaviour));
            
            // Display the List of Skills //
            var playerSkills = Dictionaries.getArray(dictionary.self, playerName, "Skills", "Name"); // get the array of skills the player has
            int i = 1;
            leftText = "|\n"; // the line indicates the top and bottom of the list of skills
            foreach (var skill in playerSkills){
                // check if the skill is passive or not //
                var skillInfo = Dictionaries.getSkillInfo(dictionary, skill);
                var cost = skillInfo["Cost"].Number; // this one checks if its 0 or -1
                var displayCost = determineCost(dictionary, skillInfo, playerName); // this is the amount of hp or sp will be taken
                    if (cost != -1){
                        leftText += skill + " \t" + displayCost + "\n";
                    }
                    else{
                        leftText += "null\n";
                    }
                    i++;
            }
            leftText += "|";
            txtBox.text = leftText;

            // Display the Status of the player //
            showAilment(playerName);
            showStatChanges(Dictionaries.getArray(dictionary.self, playerName, "Stat Changes", "Name"));
        }
    }
    public override void OnDrop(){
        //Debug.Log(leftText);
    }
    // changes the gui highlighting the skill the user is selecting //
    public void changeSel(int change){
        //direction.text = change + " DS";
        var newY = skillBack.anchoredPosition.y + (change * -70);
        if (newY < 70 && newY > -104){
            skillBack.anchoredPosition = new Vector2(0, newY);
        }
        else{
            string newOrder = "";
            string savedSkill = ""; // the skill that will be saved and moved 
            string[] skills = leftText.Split(new string[] {"\n"}, StringSplitOptions.None); // splits at each line break
            if (change == 1){ // move list down
                if (skills.Length >= 4){
                    if (!skills[4].Equals("|")){
                        //Debug.Log(skills.Length);
                        savedSkill = skills[0];
                        for (int i = 1; i < skills.Length; i++){
                            if (!skills[i].Equals("")){
                                if (i != skills.Length - 1){ // dont create a new line on the last one
                                    newOrder += skills[i] + "\n";
                                }
                                else{
                                    newOrder += skills[i];
                                }
                                
                            }
                        }
                        newOrder += "\n" + savedSkill;

                        leftText = newOrder;
                        txtBox.text = newOrder;
                    }
                }
            }
            else if (change == -1){ // move list up
                if (!skills[0].Equals("|")){
                    savedSkill = skills[skills.Length - 1];
                    //Debug.Log(savedSkill);
                    newOrder += savedSkill ;
                    for (int i = 0; i < skills.Length - 1; i++){
                        newOrder += "\n" + skills[i];
                    }
                    leftText = newOrder;
                    txtBox.text = newOrder;
                }
            }
            
        }
    } 

    // displays the information about the selected skill //
    public void displayDesc(Dictionaries dictionary, string skillName, string user){
        DataDictionary skillInfo = Dictionaries.getSkillInfo(dictionary, skillName);

        string skillText = skillName + " - "; // skill name
        skillText += determineCost(dictionary, skillInfo, user) + ": "; // Cost of the skill
        skillText += createDescMsg(dictionary, skillInfo); // description
        description.text = skillText;
    }

    // creates a description to display //
    private static string createDescMsg(Dictionaries dictionary, DataDictionary skillInfo){
        string description = "";
        // general skill info // 
        string skillElement = skillInfo["Element"].String;
        string targets = skillInfo["Targets"].String;
        var power = skillInfo["Power"].Number;
        // quickly check the funny ones //
        switch (skillElement){
            case "Pass":
                if (skillInfo["Power"].Int == 0){
                    return ("Pass your turn.");
                }
                else{
                    return ("Take defensive action.");
                }
            case "Light":
            case "Darkness":
                description += skillElement + ": instant kill, " + targets + " foe";
                if (skillInfo["Accuracy"].Double > .40){ // for samsara and die for me
                    description += " (very high)";
                }
                else if (skillInfo["Accuracy"].Double > .30){ 
                    description += " (high odds)";
                }
                description += ".";
                return (description);
            case "Patra":
                description += "Dispels ";
                foreach (string ailment in dictionary.PATRA){
                    description += ailment + ", ";
                }
                description += "(" + targets + ")";
                return (description);
            case "Recovery":
                if (power <= 50){
                    description += "Slightly ";
                }
                else if (power <= 160){
                    description += "Moderately ";
                }
                else{
                    description += "Fully ";
                }
                description += "restores " + targets + "'s HP.";
                return (description);
            default:
                break;
        }
        // check if its a general offensive element //
        foreach (var element in dictionary.offensiveElements){
            if (skillElement.Equals(element)){
                description += "Deals ";
                // determine the vague damage word to use //
                if (power <= 110){ description += "light ";}// kill rush is considered light at 110 while zanei is considered medium at 120
                // the lines are very blurry on these vague messages
                else if(power < 300){description += "medium ";}
                else if(power < 400){description += "heavy ";}
                else if(power < 800){description += "severe ";}
                else{description += "massive ";}
                description += skillInfo["Element"].ToString() + " damage to ";
                description += skillInfo["Targets"].ToString() + " foe.";
                return description;
            }
        }

        // check if its a stat change skill //
        foreach (var element in dictionary.statChanges){
            if (skillElement.Equals(element)){
                
                switch (targets){
                    case "Ally":
                    case "Party":
                    case "Everyone":
                        description += "Increases " + targets; 
                        break;
                    case "One":
                    case "All":
                        description += "Decreases " + targets + " foe";
                        break;
                    default:
                        break;
                }
                description += "'s " + element + ".";
                return description;
            }
        }

        return (skillElement + " skill."); // text for if it doesnt fall under any of these elements.
    }

    private static string determineCost(Dictionaries dictionary, DataDictionary skillInfo, string userName){
        string returnText = "";
        var type = Dictionaries.determineSkillType(skillInfo);
        if (type.Equals("Magic")){
            returnText = skillInfo["Cost"].Int + " SP";
        }
        else{
            int maxHP = int.Parse(Dictionaries.getStat(dictionary.self, userName, "Max HP", "Name"));
            double cost = skillInfo["Cost"].Double;
            returnText = Math.Round((double)maxHP * cost) + " HP";
        }
        return returnText;
    }
    // Display an icon for what ailment the user is inflicted with
    public void showAilment(string playerName){
        var ailment = Dictionaries.getStat(dictionary.self, playerName, "Ailment");
        if (!ailment.Equals("")){
            Material newIcon; // material to change into
            switch (ailment){
                case "Charm":
                    newIcon = charm;
                    break;
                case "Distress":
                    newIcon = distress;
                    break;
                case "Dizzy":
                    newIcon = dizzy;
                    break;
                case "Fear":
                    newIcon = fear;
                    break;
                case "Freeze":
                    newIcon = freeze;
                    break;
                case "Panic":
                    newIcon = panic;
                    break;
                case "Shock":
                    newIcon = shock;
                    break;
                /*
                case "Rage":
                    newIcon = rage;
                    break;
                */
                case "Poison":
                    newIcon = poison;
                    break;
                default:
                    newIcon = plink;
                    break;
            }
            ailmentBox.material = newIcon;
            ailmentBox.enabled = true; 
        }
        else{
            ailmentBox.enabled = false;
        }
    }

    public void showStatChanges(string[] statChanges){
        string text = "";
        foreach (string statChange in statChanges){
            string[] statInfo = Dictionaries.parseStatChange(statChange);
            if (!statInfo[0].Equals("crit")){
                text += statInfo[0];
                if (statInfo[1].Equals("+")){
                    text += " ↑";
                }
                else{
                    text += " ↓";
                }
                text += "\n";
            }
        }
        statChangeTxt.text = text;
    }
}
