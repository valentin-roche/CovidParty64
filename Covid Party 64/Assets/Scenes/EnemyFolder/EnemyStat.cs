using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class EnemyStat
{
    private static int 
        life = 100,
        damage = 1,
        speed = 10,
        range = 0,
        armor = 0, 
        atkSpeed = 1;
    private static bool 
        spit = false, 
        dodge = false, 
        block = false, 
        critical = false, 
        slow = false, 
        fly = false,
        regen = false;

    public static int Life { get => life; set => life = value; }
    public static int Damage { get => damage; set => damage = value; }
    public static int Speed { get => speed; set => speed = value; }
    public static int Range { get => range; set => range = value; }
    public static int Armor { get => armor; set => armor = value; }
    public static int AtkSpeed { get => atkSpeed; set => atkSpeed = value; }
    public static bool Spit { get => spit; set => spit = value; }
    public static bool Dodge { get => dodge; set => dodge = value; }
    public static bool Block { get => block; set => block = value; }
    public static bool Critical { get => critical; set => critical = value; }
    public static bool Slow { get => slow; set => slow = value; }
    public static bool Fly { get => fly; set => fly = value; }
    public static bool Regen { get => regen; set => regen = value; }

    public static void ResetStat()
    {
        Life = 100;
        Damage = 1;
        Speed = 10;
        Range = 0;
        Armor = 0;
        AtkSpeed = 1;
        Spit = false;
        Dodge = false;
        Block = false;
        Critical = false;
        Slow = false;
        Fly = false;
        Regen = false;
    }
}

