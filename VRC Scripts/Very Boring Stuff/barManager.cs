
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;
using TMPro;

public class barManager : UdonSharpBehaviour
{
    public Image healthBar;
    public Image spBar;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI spText;

    public void updateValue(string type, int value, int maxValue){
        float barValue = (float)value / (float)maxValue;
        if (type.Equals("HP")){
            healthBar.fillAmount = barValue;
            hpText.text = value + "/" + maxValue;
        }
        else if (type.Equals("SP")){
            spBar.fillAmount = barValue;
            spText.text = value + "/" + maxValue;
        }
    }
}
