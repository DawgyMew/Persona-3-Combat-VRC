
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;
using VRC.SDK3.Data;
using System;
public class displaySkills : UdonSharpBehaviour
{
    public Dictionaries dictionary;
    public TextMeshProUGUI txtBox;

    public RectTransform skillBack;

    public int currentStartY = 69;
    public string leftText;

    public TextMeshProUGUI description;
    public override void OnPickup(){
        if (GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.isLocal){
            string playerName = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.displayName;
            var dictionaryGO = GameObject.Find("Dictionary");
            dictionary = (Dictionaries)dictionaryGO.GetComponent(typeof(UdonBehaviour));
            
            //Debug.Log(playerName);
            var playerSkills = Dictionaries.getArray(dictionary.self, playerName, "Skills", "Name"); // get the array of skills the player has
            int i = 1;
            leftText = "|\n"; // the line indicates the top and bottom of the list of skills
            foreach (var skill in playerSkills){
                // check if the skill is passive or not //
                var skillInfo = Dictionaries.getSkillInfo(dictionary, skill);
                var cost = skillInfo["Cost"].Number; // this one checks if its 0 or -1
                var displayCost = determineCost(dictionary, skillInfo, playerName); // this is the amount of hp or sp will be taken
                if (cost != 0){
                    if (cost != -1){
                        leftText += skill + " \t" + displayCost + "\n";
                    }
                    else{
                        leftText += "null\n";
                    }
                    i++;
                }
            }
            leftText += "|";
            txtBox.text = leftText;
        }
    }
    public override void OnDrop(){
        Debug.Log(leftText);
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
        foreach (var element in dictionary.offensiveElements){
            if (skillInfo["Element"].String.Equals(element)){
                var power = skillInfo["Power"].Int;
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
        return "...";
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
}
