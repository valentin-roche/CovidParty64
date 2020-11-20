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
                        // TODO : Change projectile damage
                        break;
                    case "contaminationDist":
                        Stats.PlayerStat.ContaminationRate += modif;
                        break;
                }  
            }
            if (power.Target == "enemy")
            {
                switch (power.AffectedStat)
                {
                    case "speed":
                        EnemyStatMedium.Speed += modif;
                        break;
                    case "health":
                        EnemyStatMedium.Life += modif;
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
                    default:
                        break;
                }
            }
            if (power.Target == "enemy")
            {
                switch (power.AffectedStat)
                {
                    //TODO special powers handling
                    default:
                        break;
                }
            }
        }
    }
}
