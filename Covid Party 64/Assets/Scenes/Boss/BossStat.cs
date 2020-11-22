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
            level = 1,
            life = 100,
            maxHP = 100,
            speed = 5,
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

        public static void ResetStat()
        {
            Life = 100;
            MaxHP = 100;
            Speed = 5;
            Range = 0;
            Armor = 0;
            AtkSpeed = 1;
            Dodge = false;
        }
    }
}


