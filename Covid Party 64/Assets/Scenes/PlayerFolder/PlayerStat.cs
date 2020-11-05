using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    // Start is called before the first frame update
    private static int
        contaminationRate = 0,
        speed = 5000,
        armor = 0;

    private static bool
       dodge = false,
       block = false,
       critical = false,
       slow = false,
       fly = false,
       regen = false;
    public static int ContaminationRate { get => contaminationRate; set => contaminationRate = value; }
    public static int Speed { get => speed; set => speed = value; }
    public static int Armor { get => armor; set => armor = value; }
    public static bool Dodge { get => dodge; set => dodge = value; }
    public static bool Block { get => block; set => block = value; }
    public static bool Critical { get => critical; set => critical = value; }
    public static bool Slow { get => slow; set => slow = value; }
    public static bool Fly { get => fly; set => fly = value; }
    public static bool Regen { get => regen; set => regen = value; }

    public static void ResetStat()
    {
        contaminationRate = 0;
        Speed = 5000;
        Armor = 0;
        Dodge = false;
        Block = false;
        Critical = false;
        Slow = false;
        Fly = false;
        Regen = false;
    }
}


