using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Stats
{ 
    public static class BossStat
    {
        private static int
            baseLife = 1000,
            baseMaxHP = 100,
            baseSpeed = 300,
            baseRange = 0,
            baseArmor = 0,
            baseDamage = 2,
            baseAtkSpeed = 1;
        private static bool
            baseDodge = false;

        private static int
            level = 1,
            life = 1000,
            maxHP = 100,
            speed = 300,
            range = 0,
            armor = 0,
            damage = 2,
            atkSpeed = 1;
        private static bool
            dodge = false;

        public static int Life { get => life; set => life = value; }
        public static int Speed { get => speed; set => speed = value; }
        public static int Range { get => range; set => range = value; }
        public static int Armor { get => armor; set => armor = value; }
        public static int AtkSpeed { get => atkSpeed; set => atkSpeed = value; }
        public static bool Dodge { get => dodge; set => dodge = value; }
        public static int Level { get => level; set => level = value; }
        public static int Damage { get => damage; set => damage = value; }
        public static int MaxHP { get => maxHP; set => maxHP = value; }
        public static int BaseLife { get => baseLife; set => baseLife = value; }
        public static int BaseMaxHP { get => baseMaxHP; set => baseMaxHP = value; }
        public static int BaseSpeed { get => baseSpeed; set => baseSpeed = value; }
        public static int BaseRange { get => baseRange; set => baseRange = value; }
        public static int BaseArmor { get => baseArmor; set => baseArmor = value; }
        public static int BaseDamage { get => baseDamage; set => baseDamage = value; }
        public static int BaseAtkSpeed { get => baseAtkSpeed; set => baseAtkSpeed = value; }
        public static bool BaseDodge { get => baseDodge; set => baseDodge = value; }

        public static void ResetStat()
        {
            Life = BaseLife;
            MaxHP = BaseMaxHP;
            Speed = BaseSpeed;
            Range =BaseRange;
            Armor =BaseArmor;
            AtkSpeed = BaseAtkSpeed;
            Dodge = false;
        }
    }
}


