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
    // Start is called before the first frame update
    void Start()
    {
        if (TransitionInfos.LevelTransitionInfo.IsNextLevelPlayerUpgrade)
        {
            choice1.GetComponent<Button>().onClick.AddListener(() => handleClick(choice1.GetComponent<ButtonData>().LinkedCouple));
            choice2.GetComponent<Button>().onClick.AddListener(() => handleClick(choice2.GetComponent<ButtonData>().LinkedCouple));
            choice3.GetComponent<Button>().onClick.AddListener(() => handleClick(choice3.GetComponent<ButtonData>().LinkedCouple));
        }
        else
        {
            weapon.GetComponent<Button>().onClick.AddListener(() => handleWeaponUpgrade());
            armor.GetComponent<Button>().onClick.AddListener(() => handleArmorUpgrade());
        }
    }

    private void handleArmorUpgrade()
    {
        // Increase player armor
        Stats.PlayerStat.Armor = (int)(Stats.PlayerStat.Armor * 1.25);
        // Increase armor level in level infos
        TransitionInfos.LevelTransitionInfo.ArmorLevel++;
    }

    private void handleWeaponUpgrade()
    {
        // Increase bullet damage (for now)
        Stats.PlayerStat.BulletDamage = (int)(Stats.PlayerStat.BulletDamage * 1.25);
        // Change Weapon level in level infos (usefull for upgrade generation)
        TransitionInfos.LevelTransitionInfo.GunLevel++;
        // TODO : Handle Weapon change
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void handleClick(Couple couple)
    {
        applyMod(couple.EnemyMod);
        applyMod(couple.PlayerMod);
    }

    public void applyMod(Power power)
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
                        break;
                    case "contaminationDist":
                        Stats.PlayerStat.ContaminationRate += modif;
                        break;
                    case "armor":
                        Stats.PlayerStat.Armor += modif;
                        break;
                    case "jump":
                        //TODO uncomment when jump modifier added
                        //Stats.PlayerStat.Jump += modif;
                        break;
                    case "projectileDistance":
                        //TODO uncomment when projectile distance modifier added
                        //Stats.PlayerStat.ProjetcileDistance += modif;
                        break;
                    case "dropRate":
                        //TODO uncomment when droprate increase procedure defined
                        break;
                    case "regen":
                        //TODO uncomment when regenrate implemented
                        //Stats.PlayerStat.RegenRate += modif;
                        break;
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
                        Stats.EnemyStatMedium.Speed += modif;
                        break;
                    case "health":
                        Stats.EnemyStatMedium.Life += modif;
                        break;
                    case "damage":
                        Stats.EnemyStatMedium.AtkSpeed += modif;
                        break;
                }
            }
            if (power.Target == "boss")
            {
                switch (power.AffectedStat)
                {
                    case "damage":
                        //TODO uncomment when boss stats handler is up
                        //Stats.BossStats.Damage += modif;
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
                    //TODO special powers handling
                    case "critical":
                        Stats.PlayerStat.Critical = true;
                        break;
                    case "dodge":
                        Stats.PlayerStat.Dodge = true;
                        break;
                    case "lifeSteal":
                        //TODO uncomment when lifesteal bullets implemented
                        //Stats.PlayerStat.LifeSteal = true;
                        break;
                    case "lifeStealZone":
                        //TODO uncomment when lifesteal zone implemented
                        //Stats.PlayerStat.LifeStealZone = true;
                        break;
                    case "stun":
                        //TODO uncomment when lifesteal zone implemented
                        //Stats.PlayerStat.Stun = true;
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
                        Stats.EnemyStatMedium.Spit = true;
                        break;
                    case "dodge":
                        Stats.EnemyStatMedium.Dodge = true;
                        break;
                    case "critical":
                        Stats.EnemyStatMedium.Critical = true;
                        break;
                    case "regen":
                        Stats.EnemyStatMedium.Regen = true;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
