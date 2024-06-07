using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class skills
    {
        // https://megamitensei.fandom.com/wiki/List_of_Persona_3_Skills //
        // store the skills within a dictionary 
        public DataDictionary skillDict = new DataDictionary(){
            // fire skills
            {"Agi", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 3},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Maragi", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 6},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Agilao", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Maragion", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Agidyne", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Maragidyne", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Ragnarok", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 30},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Maralagidyne", new DataDictionary(){
                {"Element", "Fire"},
                {"Power", 370},
                {"Accuracy", .95},
                {"Cost", 32},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},

            // Ice Skills //

            {"Bufu", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 4},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", .10}
            }},
            {"Mabufu", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 8},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", .08}
            }},
            {"Bufula", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 8},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", .10}
            }},
            {"Mabufula", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 16},
                {"Targets", "All"},
                {"Times Hit", 1},,
                {"Ailment Chance", .08}
            }},
            {"Bufudyne", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 16},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", .10}
            }},
            {"Mabufudyne", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 32},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", .08}
            }},
            {"Niflheim", new DataDictionary(){
                {"Element", "Ice"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 32},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 1.00}
            }},
            
            // Electricity Skills //

            {"Zio", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 4},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", .10}
            }},
            {"Mazio", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 8},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", .08}
            }},
            {"Zionga", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 8},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", .10}
            }},
            {"Mazionga", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 16},
                {"Targets", "All"},
                {"Times Hit", 1},,
                {"Ailment Chance", .08}
            }},
            {"Ziodyne", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 16},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", .10}
            }},
            {"Maziodyne", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 32},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", .08}
            }},
            {"Thunder Reign", new DataDictionary(){
                {"Element", "Elec"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 32},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 1.00}
            }},

            // Wind Skills //

            {"Garu", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 50},
                {"Accuracy", .95},
                {"Cost", 3},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Magaru", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 50},
                {"Accuracy", .90},
                {"Cost", 6},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Garula", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 100},
                {"Accuracy", .95},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Magarula", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 100},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Garudyne", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 320},
                {"Accuracy", .95},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Magarudyne", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 320},
                {"Accuracy", .90},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Panta Rhei", new DataDictionary(){
                {"Element", "Wind"},
                {"Power", 650},
                {"Accuracy", .99},
                {"Cost", 30},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            
            // Light Skills //

            {"Hama", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Mahama", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .25},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Hamaon", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .40},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Mahamaon", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .35},
                {"Cost", 24},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Samsara", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            
            // Darkness Skills //

            {"Mudo", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Mamudo", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .25},
                {"Cost", 12},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Mudoon", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .40},
                {"Cost", 12},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Mamudoon", new DataDictionary(){
                {"Element", "Darkness"},
                {"Power", 0},
                {"Accuracy", .35},
                {"Cost", 24},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Die For Me!", new DataDictionary(){
                {"Element", "Light"},
                {"Power", 0},
                {"Accuracy", .30},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},

            // Recovery Skills //
            // power is how much they restore
            
            // Health Restore //
            
            {"Dia", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 50}, 
                {"Accuracy", 1.00},
                {"Cost", 4},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Media", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 40},
                {"Accuracy", 1.00},
                {"Cost", 8},
                {"Targets", "Team"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Diarama", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 160},
                {"Accuracy", 1.00},
                {"Cost", 8},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Mediarama", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 50},
                {"Accuracy", 1.00},
                {"Cost", 16},
                {"Targets", "Team"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Diarahan", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 1000},
                {"Accuracy", 1.00},
                {"Cost", 20},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Mediarahan", new DataDictionary(){
                {"Element", "Recovery"},
                {"Power", 1000},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Team"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            
            // Revival Skills //

            {"Recarm", new DataDictionary(){
                {"Element", "Revive"},
                {"Power", .50},
                {"Accuracy", 1.00},
                {"Cost", 20},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Samarecarm", new DataDictionary(){
                {"Element", "Revive"},
                {"Power", 1.00},
                {"Accuracy", 1.00},
                {"Cost", 20},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},

            // Ailments Skills //

            // TODO: add ailments
            /*
            Patra
            Re Patra
            Me Patra
            Posumudi
            Charmdi
            Enradi
            Amrita
            Salvation
            */

            // Enhancing Skills //

            {"Tarukaja", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Tarunda", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Matarukaja", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Party"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Matarunda", new DataDictionary(){
                {"Element", "Attack"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "All"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Rakukaja", new DataDictionary(){
                {"Element", "Defense"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "Ally"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            {"Rakunda", new DataDictionary(){
                {"Element", "Defense"},
                {"Power", 0},
                {"Accuracy", 1.00},
                {"Cost", 6},
                {"Targets", "One"},
                {"Times Hit", 1},
                {"Ailment Chance", 0.00}
            }},
            
        };
    }
}