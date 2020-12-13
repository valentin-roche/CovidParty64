using System;
using System.Collections.Generic;
using UnityEngine;



namespace Stats
{
    public static class PlayerStat
    {
        private static List<Couple> chosenCouples;

        private static int
            contaminationRate = 0,
            maxContamination = 100,
            speed = 2500,
            armor = 0,
            bulletDamage = 40,
            laserDPS = 30,
            jump = 700,
            weaponLevel = 1,
            defenseLevel = 1,
            regenTick = 1;

        private static Dictionary<string, int> playerInventory = new Dictionary<string, int>();

        private static float
            projectileDistance = 1f,
            contaminationDist = 4f;

        private static bool
            lowerSpeed = false,
            stun = false,
           lowerContaminationArea = false,
           slowerContamination = false,
           biggerJump = false,
           extendRange = false,
           increasedDropRate = false,
           increasedBossDamage = false,
           slowEnnemy = false,
           increasedSpeed = false,
           increasedAttackSpeed = false,
           explosionAtTouch = false,
           purifiantLaserPlus = false,
           drainAtTouch = false,
           drainAtProximity = false,
           getBackBonusChance = false,
           additionalVaccine = false,
           stunCrit = false,
           dodge = false,
           block = false,
           critical = false,
           slow = false,
           regen = false;

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
        public static bool Block { get => block; set => block = value; }
        public static bool Critical { get => critical; set => critical = value; }
        public static bool Slow { get => slow; set => slow = value; }
        public static bool Regen { get => regen; set => regen = value; }
        public static bool IncreasedBossDamage { get => increasedBossDamage; set => increasedBossDamage = value; }
        public static bool IncreasedSpeed { get => increasedSpeed; set => increasedSpeed = value; }
        public static bool LowerSpeed { get => lowerSpeed; set => lowerSpeed = value; }
        public static bool BiggerJump { get => biggerJump; set => biggerJump = value; }
        public static bool LowerContaminationArea { get => lowerContaminationArea; set => lowerContaminationArea = value; }
        public static bool DrainAtTouch { get => drainAtTouch; set => drainAtTouch = value; }
        public static bool Stun { get => stun; set => stun = value; }

        public static Dictionary<string, int> PlayerInventory { get => playerInventory; set => playerInventory = value; }
        
        public static List<Couple> ChosenCouples { get => chosenCouples; set => chosenCouples = value; }

        public static void ResetStat()
        {
            contaminationRate = 0;
            speed = 2500;
            jump = 500;
            Armor = 0;
            Dodge = false;
            Block = false;
            Critical = false;
            Slow = false;
            Fly = false;
            Regen = false;
        }

        public static void addBonusEffect()
        {
            //Reduce enemy detection area
            if (lowerContaminationArea)
            {
                ContaminationDist = 0.95f * ContaminationDist;
            }
            //Lower Player speed
            else if (lowerSpeed)
            {
                Speed = (int)Math.Truncate(Speed * 0.95);
            }         
        }
    }
}


