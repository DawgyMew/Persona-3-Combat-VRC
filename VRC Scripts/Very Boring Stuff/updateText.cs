
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using TMPro;

public class updateText : UdonSharpBehaviour
{
    public static void changeEnemyText(string enemyName, string text){
        GameObject enemy = GameObject.Find(enemyName);
        var textBox = enemy.transform.GetChild(0).gameObject;
        if (textBox != null){
            textBox.GetComponent<TextMeshPro>().text = text;
        }
    }
}
