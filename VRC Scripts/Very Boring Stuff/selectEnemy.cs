
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public class selectEnemy : UdonSharpBehaviour
{
    public Dictionaries dc;
    public GameObject selector;
    // moves the circle in front of the entity
    public void moveSelect(string uName){
        selector.SetActive(true);
        var newLocat = Dictionaries.getLocation(uName);
        selector.transform.position = newLocat; 
    }
    public void hide(){
        selector.SetActive(false);
    }
}
