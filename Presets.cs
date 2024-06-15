using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Data;

public class Presets : MonoBehaviour
{
    // Presets of Personas players can use //
    public DataDictionary personas = new DataDictionary(){
        {"Jack Frost", new DataDictionary(){
            // persona stats //
            {"pName", "Jack Frost"},
            {"St", 5},
            {"Mg", 8},
            {"En", 8},
            {"Ag", 4},
            {"Lu", 6},
            
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", "Ice"},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", "Fire"},
            // skills //
            {"Skills", "Bufu,Pulinpa,Re Patra,Sonic Punch,Ice Boost,Mabufu"},
        }},
        {"Black Frost", new DataDictionary(){
            // persona stats //
            {"pName", "Black Frost"},
            {"St", 29},
            {"Mg", 31},
            {"En", 25},
            {"Ag", 27},
            {"Lu", 36},
            
            // persona type affinities //
            {"Strengths", "Fire,Darkness"},
            {"Nullifies", "Ice"},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", "Light"},
            // skills //
            {"Skills", "Mudo,Agilao,Bufula,Marakunda,Re Patra,Ice Boost,Rakukaja,Trafuri"},
        }},
    };
    // Preset of Enemies //
    public DataDictionary enemies = new DataDictionary(){
        {"Shouting Tiara", new DataDictionary(){
            {"Name", ""}, // unique identifier
            {"HP", 242},
            {"Max HP", 242},
            {"SP", 138},
            {"Max SP", 138},
            {"LVL", 35},
            // persona stats //
            {"pName", "Shouting Tiara"},
            {"St", 19},
            {"Mg", 31},
            {"En", 19},
            {"Ag", 22},
            {"Lu", 21},
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", "Light"},
            {"Absorb", "Fire"},
            {"Reflect", ""},
            {"Weak", "Ice,Dark"},
            // skills //
            {"Skills", "Maragi,Agilao,Maragion,Mahama,Media"},
        }},
        {"Amorous Snake", new DataDictionary(){
            {"Name", ""}, // unique identifier
            {"HP", 470},
            {"Max HP", 470},
            {"SP", 230},
            {"Max SP", 230},
            {"LVL", 93},
            // persona stats //
            {"pName", "Amorous Snake"},
            {"St", 60},
            {"Mg", 71},
            {"En", 49},
            {"Ag", 47},
            {"Lu", 59},
            // persona type affinities //
            {"Strengths", "Slash,Strike,Pierce"},
            {"Nullifies", ""},
            {"Absorb", "Fire"},
            {"Reflect", "Light"},
            {"Weak", "Ice,Dark"},
            // skills //
            {"Skills", "Agidyne,Maragidyne,Mahama,Mahamaon,Diarama,Mediarahan,Sexy Dance,Virus Breath,Re Patra"},
        }},
    };
}