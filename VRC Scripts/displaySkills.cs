
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;
public class displaySkills : UdonSharpBehaviour
{
    public Dictionaries dictionary;
    public TextMeshProUGUI txtBox;


    void Awake(){
        dictionary = GameObject.Find("Dictionary").GetComponent<Dictionaries>();
    }
    public override void OnPickup(){
        if (GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.isLocal){
            string playerName = GetComponent<VRC.SDKBase.VRC_Pickup>().currentPlayer.displayName;
            Debug.Log(playerName);
            var playerSkills = Dictionaries.getArray(dictionary.self, playerName, "Skills", "Name"); // get the array of skills the player has
            int i = 1;
            string leftText = "";
            foreach (var skill in playerSkills){
                // check if the skill is passive or not //
                var skillCost = Dictionaries.getSkillInfo(dictionary, skill)["Cost"].Int;
                if (skillCost != 0){
                    if (skillCost != -1){
                        leftText += skill + " \t" + skillCost + "\n";
                    }
                    else{
                        leftText += "null\n";
                    }
                    i++;
                }
            }
            txtBox.text = leftText;
        }
    }
}
