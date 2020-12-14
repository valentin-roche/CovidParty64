using System;
using System.Collections.Generic;
using UnityEngine;



namespace Stats
{
    public static class PlayerStat
    {
        //Declaration of the Player's Stats
        private static List<Couple> chosenCouples = new List<Couple>();

        private static int
            contaminationRate = 0,
            maxContamination = 100,
            speed = 2500,
            armor = 0,
            bulletDamage = 40,
            laserDPS = 30,
            jump = 700,
            weaponLevel = 3,
            defenseLevel = 1,
            regenTick = 1;

        private static Dictionary<string, int> playerInventory = new Dictionary<string, int>();
        

        private static float
            projectileDistance = 1f,
            contaminationDist = 4f;

        private static bool
            wallBang = false,
            lowerSpeed = false,
            stun = false,
           increasedBossDamage = false,
           drainAtTouch = false,
           drainAtProximity = false,
           dodge = false,
           critical = false,
           regen = false;

        //Getters and Setters to intercat with the Stats of the Player
        public static int BulletDamage { get => bulletDamage; set => bulletDamage = value; }
        public static int LaserDPS { get => laserDPS; set => laserDPS = value; }
        public static int Jump { get => jump; set => jump = value; }
        public static int ContaminationRate { get => contaminationRate; set => contaminationRate = value; }
        public static int MaxContamination { get => maxContamination; set => maxContamination = value; }
        public static int Speed { get => speed; set => speed = value; }
        public static int Armor { get => armor; set => armor = value; }
        public static int WeaponLevel { get => weaponLevel; set => weaponLevel = value; }
        public static int DefenseLevel { get => defenseLevel; set => defenseLevel = value; }
        public static int RegenTick { get => regenTick; set => regenTick = value; }


        public static float ContaminationDist { get => contaminationDist; set => contaminationDist = value; }
        public static float ProjectileDistance { get => projectileDistance; set => projectileDistance = value; }


        public static bool Dodge { get => dodge; set => dodge = value; }
        public static bool Critical { get => critical; set => critical = value; }
        public static bool Regen { get => regen; set => regen = value; }
        public static bool IncreasedBossDamage { get => increasedBossDamage; set => increasedBossDamage = value; }
        public static bool DrainAtTouch { get => drainAtTouch; set => drainAtTouch = value; }
        public static bool Stun { get => stun; set => stun = value; }
        public static bool WallBang { get => wallBang; set => wallBang = value; }

        public static Dictionary<string, int> PlayerInventory { get => playerInventory; set => playerInventory = value; }
        
        public static List<Couple> ChosenCouples { get => chosenCouples; set => chosenCouples = value; }


        //Method that should be used to Reset all the Stats of the Player
        public static void ResetStat()
        {
            contaminationRate = 0;
            maxContamination = 100;
            speed = 2500;
            armor = 0;
            bulletDamage = 40;
            laserDPS = 30;
            jump = 700;
            weaponLevel = 3;
            defenseLevel = 1;
            regenTick = 1;

            playerInventory = new Dictionary<string, int>();
            chosenCouples = new List<Couple>();

            projectileDistance = 1f;
            contaminationDist = 4f;

            wallBang = false;
            lowerSpeed = false;
            stun = false;
            increasedBossDamage = false;
            drainAtTouch = false;
            drainAtProximity = false;
            dodge = false;
            critical = false;
            regen = false;
        }

    }
}


