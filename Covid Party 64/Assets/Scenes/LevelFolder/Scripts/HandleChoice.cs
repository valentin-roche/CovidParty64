using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HandleChoice : MonoBehaviour
{
    public Button choice1;
    public Button choice2;
    public Button choice3;
    public Button weapon;
    public Button armor;
    public GameObject LevelFlowManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void applyMod(Power power)
    {
        if (power.EffectType == "modify")
        {
            var modif = power.Value;
            if (power.Target == "player")
            {
                switch (power.AffectedStat)
                {
                    case "speed":
                        Stats.PlayerStat.Speed += modif;
                        break;
                    case "damage":
                        Stats.PlayerStat.BulletDamage += modif;
                        Stats.PlayerStat.LaserDPS += modif;
                        break;
                    case "contaminationDist":
                        Stats.PlayerStat.ContaminationDist += modif/10;
                        break;
                    case "armor":
                        Stats.PlayerStat.Armor += modif;
                        Stats.PlayerStat.MaxContamination += Stats.PlayerStat.Armor;
                        break;
                    case "jump":
                        //TODO uncomment when jump modifier added
                        Stats.PlayerStat.Jump += modif * 100;
                        break;
                    case "projectileDistance":
                        //TODO uncomment when projectile distance modifier added
                        Stats.PlayerStat.ProjectileDistance *= modif;
                        break;
                    case "dropRate":
                        //TODO uncomment when droprate increase procedure defined
                        break;
                    //case "regen":
                    //    //TODO uncomment when regenrate implemented
                    //    //Stats.PlayerStat.RegenRate += modif;
                        //break;
                    //Not in CoupleData, equivalent to Armor boost ?
                    case "life":
                        Stats.PlayerStat.MaxContamination += modif;
                        break;
                }
            }
            if (power.Target == "enemy")
            {
                switch (power.AffectedStat)
                {
                    case "speed":
                        Stats.EnemyStatSmall.Speed += modif;
                        Stats.EnemyStatMedium.Speed += modif;
                        Stats.EnemyStatLarge.Speed += modif;
                        break;
                    case "health":
                        Stats.EnemyStatSmall.Life += modif;
                        Stats.EnemyStatMedium.Life += modif;
                        Stats.EnemyStatLarge.Life += modif;
                        break;
                    case "damage":
                        Stats.EnemyStatSmall.AtkSpeed += modif;
                        Stats.EnemyStatMedium.AtkSpeed += modif;
                        Stats.EnemyStatLarge.AtkSpeed += modif;
                        break;
                }
            }
            if (power.Target == "boss")
            {
                switch (power.AffectedStat)
                {
                    case "damage":
                        Stats.BossStat.BaseDamage += modif;
                        break;
                    case "speed":
                        Stats.BossStat.BaseSpeed+= modif;
                        break;
                }
            }
        }
        else
        {
            if (power.Target == "player")
            {
                switch (power.AffectedStat)
                {
                    case "regen":
                    //TODO uncomment when regenrate implemented
                        Stats.PlayerStat.Regen = true;
                        break;
                    //case "contaminationDist":
                    //    Stats.PlayerStat.LowerContaminationArea = true;
                    //    break;
                    //case "speed":
                    //    Stats.PlayerStat.LowerSpeed = true;
                    //    break;
                    //TODO special powers handling
                    case "critical":
                        Stats.PlayerStat.Critical = true;
                        break;
                    case "dodge":
                        Stats.PlayerStat.Dodge = true;
                        break;
                    case "lifeSteal":
                        //TODO uncomment when lifesteal bullets implemented
                        Stats.PlayerStat.DrainAtTouch = true;
                        break;
                    case "lifeStealZone":
                        //TODO uncomment when lifesteal zone implemented
                        //Stats.PlayerStat.LifeStealZone = true;
                        break;
                    case "stun":
                        //TODO uncomment when stun zone implemented
                        Stats.PlayerStat.Stun = true;
                        break;
                    case "IncreasedBossDamage":
                        Stats.PlayerStat.IncreasedBossDamage = true;
                        break;
                    default:
                        break;
                }
            }
            if (power.Target == "enemy")
            {
                switch (power.AffectedStat)
                {
                    //TODO special powers handling
                    case "spit":
                        Stats.EnemyStatSmall.Spit = true;
                        Stats.EnemyStatMedium.Spit = true;
                        Stats.EnemyStatLarge.Spit = true;
                        break;
                    case "dodge":
                        Stats.EnemyStatSmall.Dodge = true;
                        Stats.EnemyStatMedium.Dodge = true;
                        Stats.EnemyStatLarge.Dodge = true;
                        break;
                    case "critical":
                        Stats.EnemyStatSmall.Critical = true;
                        Stats.EnemyStatMedium.Critical = true;
                        Stats.EnemyStatLarge.Critical = true;
                        break;
                    case "regen":
                        Stats.EnemyStatSmall.Regen = true;
                        Stats.EnemyStatMedium.Regen = true;
                        Stats.EnemyStatLarge.Regen = true;
                        break;
                    default:
                        break;
                }
            }
            if (power.Target == "boss")
            {
                switch (power.AffectedStat)
                {
                    //TODO special powers handling
                    case "dodge":
                        Stats.BossStat.BaseDodge = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
