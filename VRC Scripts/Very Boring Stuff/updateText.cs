
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class updateText : UdonSharpBehaviour
{

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
