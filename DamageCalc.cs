using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class DamageCalc
    {
        [SerializeField] private Dictionaries mainDict;
        [SerializeField] private Skills skills;
        static void Main(String[] args){
            // Void Giant
            //Console.WriteLine(CalcDamage(90, 99, 72, 98, 320, 1));
            determineAmp(0, false, 1);
            // amorous snake
            //Console.WriteLine(CalcDamage(90, 99, 49, 93, 320, 1));
        }
        static void userInput(){
            Console.Write("Magic/Strength: ");
            int power = int.Parse(Console.ReadLine());
            Console.Write("Enemy's Endurance: ");
            int endurance = int.Parse(Console.ReadLine());
            Console.Write("Player Level: ");
            int plv = int.Parse(Console.ReadLine());
            Console.Write("Enemy Level: ");
            int elv = int.Parse(Console.ReadLine());
            Console.Write("Move's Power: ");
            int moveDamage = int.Parse(Console.ReadLine());
            Console.Write("Amplifier: ");
            float amp = float.Parse(Console.ReadLine());
            Console.WriteLine("Damage: " + CalcDamage(power, plv, endurance, elv, moveDamage, amp));
        }

        
        // determines a number for the damage to be multiplied by
        public static double determineAmp(byte elementalDefense, bool isDown, double skillAmp){
            double amplifier = 1;
            switch(elementalDefense){
                case 0: //  nullify
                    amplifier *= 0;
                    break;
                case 1: // weak
                    amplifier *= 1.5;
                    break;
                case 2: // resist
                    amplifier *= 0.5;
                    break;
                default:
                    amplifier *= 1;
                    break;
            };
            Debug.Log(amplifier);
            if (isDown){amplifier *= 1;}
            amplifier *= skillAmp;
            return amplifier;
        }
        // The main formula for damage calculation //
        public static int calcDamage(double power, double PLV, double eEndurance, double ELV, double moveDamage, double amplifier){
            // power works as either magic or strength
            var rnd = new Random(); // create random object
            double rngSwing = (double) rnd.Next(-10, 10) / 100; // attacks are affected by a random 10% swing
            Console.WriteLine("Random: " + rngSwing);
            double damage = Math.Sqrt((power / eEndurance) * (PLV / ELV) * moveDamage) * 7.4 * amplifier;
            Console.WriteLine((damage + (damage * -.1)) + " - " + (damage + (damage * .1))) ;
            damage += damage * rngSwing; // apply the rng junk here
            //Console.WriteLine(damage);
            int roundDamage = (int) Math.Round(damage);
            return (roundDamage); 
        }
        /* next commit :3
        public static int damageTurn(string playerName, string enemyName, string skill){
            
        }
        */
    }
}