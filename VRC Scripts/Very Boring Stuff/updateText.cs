
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class updateText : UdonSharpBehaviour
{
    // [SerializeField] private Dictionaries dict;
    // TODO: Set something up for when players are attacked

    // change the textbox above the enemy //
    public static void changeEnemyText(string enemyName, string text){
        GameObject enemy = GameObject.Find(enemyName);
        if (enemy != null){
            var textBox = enemy.transform.GetChild(0).gameObject;
            if (textBox != null){
                textBox.GetComponent<TextMeshPro>().text = text;
            }
        }
    }

    // change the text using the information from the network
    public static void changeEnemyText(Dictionaries dict){
        // preparation //
        byte[] data = dict.network.sharedInfo;
        if (data[6] == 1){ // check if the change has a display tag
            string enemyName = Dictionaries.getStat(dict.self, data[1], "Name");
            string stat = dict.syncStats[data[2]];
            var numChange = networking.convertBytes(new byte[] {data[3], data[4], data[5]}); // convert to n ubmer
            string sign = "";
            // show if a positive or negative change
            if (numChange > 0){sign = "+ ";}
            
            string displayText = stat + ": " + sign + numChange + ""; // build the string

            changeEnemyText(enemyName, displayText);
        }
    }

    // display the miss/weak/crit flavour text //
    // its close enough
    public static void enemyHitText(string enemyName, string text){
        GameObject enemy = GameObject.Find(enemyName);
        if (enemy != null){
            var particle = enemy.transform.GetChild(1).gameObject;
            if (particle != null){
                particle.GetComponent<hitText>().sendText(text);
            }
        }
    }
}
