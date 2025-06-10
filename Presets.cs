using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.SDK3.Data;

public class Presets : UdonSharpBehaviour
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
            {"Skills", "Bufu,Pulinpa,Re Patra,Sonic Punch,Ice Boost,Mabufu,Guard"},
            {"Passive", ""},
            {"Preset Base", "P02"}, // tag should make it easier to track and transfer data | maybe a * after to denote if its been edited somehow?
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
            {"Skills", "Mudo,Agilao,Bufula,Marakunda,Re Patra,Ice Boost,Rakukaja,Trafuri,Guard"},
            {"Passive", ""},
            {"Preset Base", "P01"},
        }},
        {"Blank", new DataDictionary(){ // use to clear stats
            // persona stats //
            {"pName", ""},
            {"St", 0},
            {"Mg", 0},
            {"En", 0},
            {"Ag", 0},
            {"Lu", 0},
            
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", "Wait"},
            {"Passive", ""},
            {"Preset Base", "P00"},
        }},
        {"Personal Blank", new DataDictionary(){
            {"Max HP", 1},
            {"Max SP", 1},
            {"LVL", 1},
        }}
    };
    // Preset of Enemies //
    public DataDictionary enemies = new DataDictionary(){
        {"Shouting Tiara", new DataDictionary(){
            {"Max HP", 242},
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
            {"Passive", ""},
            {"Preset Base", "E01"},
        }},
        {"Amorous Snake", new DataDictionary(){
            {"Max HP", 470},
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
            {"Passive", ""},
            {"Preset Base", "E02"},
        }},
        {"Blank", new DataDictionary(){ // use to remove enemy
            {"Name", "_"},
            { "Max HP", 1},
            {"Max SP", 1},
            {"LVL", 1},
            // persona stats //
            {"pName", ""},
            {"St", 0},
            {"Mg", 0},
            {"En", 0},
            {"Ag", 0},
            {"Lu", 0},
            
            // persona type affinities //
            {"Strengths", ""},
            {"Nullifies", ""},
            {"Absorb", ""},
            {"Reflect", ""},
            {"Weak", ""},
            // skills //
            {"Skills", "Wait"},
            {"Passive", ""},
            {"Preset Base", "E00"},
        }},
        
    };

    public static DataDictionary getDict(DataDictionary dict, string presetName){
        if (dict.TryGetValue(presetName, TokenType.DataDictionary, out DataToken value)){
            return (value.DataDictionary);
        }
        else{return null;}
    }
    // returns the names of the presets from the specified dictionary //
    public static DataList getPresetList(DataDictionary dict){
        DataList keys = dict.GetKeys();
        keys.Sort();
        return (keys);
    }
}