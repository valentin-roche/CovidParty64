using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Power
{
    string target; // The target can either be player or enemy
    string affectedStat; // If the modification affects a stat it is to be precised Here
    string effectType; // Enable or modify, enable is for special moves and modify is for numeric value modifications
    int value; // Value of the numeric modification

    public string Target { get => target; set => target = value; }
    public string AffectedStat { get => affectedStat; set => affectedStat = value; }
    public string EffectType { get => effectType; set => effectType = value; }
    public int Value { get => value; set => this.value = value; }
}

public struct Couple
{
    string name; // Name of the couple for the code
    string displayName; // Name to be displayed
    Power enemyMod; // Modification to be applied to the enemy
    Power playerMod; // Modification to be applied to the player

    public string Name { get => name; set => name = value; }
    public string DisplayName { get => displayName; set => displayName = value; }
    public Power EnemyMod { get => enemyMod; set => enemyMod = value; }
    public Power PlayerMod { get => playerMod; set => playerMod = value; }
}

public class CoupleData : MonoBehaviour
{
    public Button choice1;
    public Button choice2;
    public Button choice3;
    public Button weapon;
    public Button armor;
    public GameObject LevelsFlowHandler;
    public readonly List<Couple> CoupleList = new List<Couple>() 
    {
        new Couple()
        {
            Name = "fastercont",
            DisplayName = "Distance de contamination réduite /\n Ennemis plus  rapides",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "speed", EffectType = "modify", Value = 5 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "contaminationDist", EffectType = "modify", Value = -5 }
        },
        new Couple()
        {
            Name = "weakerslower",
            DisplayName = "Joueur plus lent /\n Ennemis plus faibles",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "health", EffectType = "modify", Value = -10 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "speed", EffectType = "enable", Value = -200 }
        },
        new Couple()
        {
            Name = "closerstronger",
            DisplayName = "Distance de contamination réduite /\n Dégâts augmentés",
            EnemyMod = new Power() { Target = "player", AffectedStat = "contaminationDist", EffectType = "modify", Value = -2 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "damage", EffectType = "modify", Value = 5 }
        },
        new Couple()
        {
            Name = "fastercloser",
            DisplayName = "Ennemis plus rapides /\n Distance de contamination plus faible",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "speed", EffectType = "modify", Value = -5 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "contaminationDist", EffectType = "modify", Value = -2 },
        },
        new Couple()
        {
            Name = "toughercloser",
            DisplayName = "Ennemis plus résistants aux dégâts /\n Contamination plus lente",
            EnemyMod = new Power() { Target = "player", AffectedStat = "damage", EffectType = "modify", Value = -1 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "armor", EffectType = "modify", Value = 2 },
        },
        new Couple()
        {
            Name = "spitjump",
            DisplayName = "Les ennemis peuvent cracher /\n Hauteur de saut plus élevée",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "spit", EffectType = "enable", Value = 1 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "jump", EffectType = "modify", Value = 2 },
        },
        new Couple()
        {
            Name = "dodgerange",
            DisplayName = "Les ennemis peuvent esquiver /\n Distance de tir plus élevée",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "dodge", EffectType = "enable", Value = 1 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "projectileDistance", EffectType = "modify", Value = 2 },
        },
        new Couple()
        {
            Name = "strongerdrop",
            DisplayName = "Les ennemis font plus de dégâts /\n Taux de chute d'objets plus élevé",
            // If no enemy upgrade for damage change to weaken player armor
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "damage", EffectType = "modify", Value = 2 }, 
            PlayerMod = new Power() { Target = "player", AffectedStat = "dropRate", EffectType = "modify", Value = 2 },
        },
        new Couple()
        {
            Name = "blockcrit",
            DisplayName = "Les ennemis peuvent bloquer des dégâts /\n Chances d'effectuer un coup critique",
            // Block = dodge so enable dodge
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "dodge", EffectType = "enable", Value = 1 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "critical", EffectType = "enable", Value = 2 },
        },
        new Couple()
        {
            Name = "critblock",
            DisplayName = "Les ennemis peuvent infliger des dégâts critiques /\n Chances d'éviter des attaques",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "critical", EffectType = "enable", Value = 1 },
            // Block = dodge so enable dodge
            PlayerMod = new Power() { Target = "player", AffectedStat = "dodge", EffectType = "enable", Value = 2 },
        },
        new Couple()
        {
            Name = "critblock",
            DisplayName = "Les ennemis peuvent infliger des dégâts critiques /\n Chances d'éviter des attaques",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "critical", EffectType = "enable", Value = 1 },
            // Block = dodge so enable dodge
            PlayerMod = new Power() { Target = "player", AffectedStat = "dodge", EffectType = "enable", Value = 2 },
        },
        new Couple()
        {
            Name = "slowregenvamp",
            DisplayName = "Vitesse de décontamination réduite /\n Vol de vie avec les projectiles",
            EnemyMod = new Power() { Target = "player", AffectedStat = "regen", EffectType = "modify", Value = -1 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "lifeSteal", EffectType = "enable", Value = 1 },
        },
        new Couple()
        {
            Name = "halflifedrain",
            DisplayName = "Votre vie est réduite de 50% /\n Vol de vie sur les ennemis à proximité",
            EnemyMod = new Power() { Target = "player", AffectedStat = "life", EffectType = "modify", Value = -Stats.PlayerStat.MaxContamination/2 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "lifestealzone", EffectType = "enable", Value = 1 },
        },
        new Couple()
        {
            Name = "slowerstronger",
            DisplayName = "Votre vitesse est réduite /\n Dégâts de projectiles considérablement augmentés",
            EnemyMod = new Power() { Target = "player", AffectedStat = "speed", EffectType = "modify", Value = -200 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "damage", EffectType = "modify", Value = 20},
        },
        new Couple()
        {
            Name = "regenstun",
            DisplayName = "Les ennemis se régénèrent /\n Les dégâts critiques étourdissent les ennemis ",
            EnemyMod = new Power() { Target = "player", AffectedStat = "speed", EffectType = "modify", Value = -200 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "stun", EffectType = "enable", Value = 1},
        },
        new Couple()
        {
            Name = "bossdmg",
            DisplayName = "Dégâts sur les boss augmentés/\n Dégâts des boss augmentés ",
            EnemyMod = new Power() { Target = "player", AffectedStat = "IncreasedBossDamage", EffectType = "enable", Value = 1 },
            PlayerMod = new Power() { Target = "boss", AffectedStat = "damage", EffectType ="modify", Value = 10},
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        // Only display player upgrade buttons
        if (TransitionInfos.LevelTransitionInfo.IsNextLevelPlayerUpgrade)
        {
            choice1.gameObject.SetActive(false);
            choice2.gameObject.SetActive(false);
            choice3.gameObject.SetActive(false);
            weapon.gameObject.SetActive(true);
            armor.gameObject.SetActive(true);
            weapon.GetComponent<Button>().onClick.AddListener(() => handleWeaponUpgrade());
            armor.GetComponent<Button>().onClick.AddListener(() => handleArmorUpgrade());

        }
        // Display random couple buttons and pick couples
        else
        {
            choice1.gameObject.SetActive(true);
            choice2.gameObject.SetActive(true);
            choice3.gameObject.SetActive(true);
            weapon.gameObject.SetActive(false);
            armor.gameObject.SetActive(false);
            choice1.GetComponent<ButtonData>().LinkedCouple = CoupleList[Random.Range(0, CoupleList.Count)];

            choice2.GetComponent<ButtonData>().LinkedCouple = CoupleList[Random.Range(0, CoupleList.Count)];

            choice3.GetComponent<ButtonData>().LinkedCouple = CoupleList[Random.Range(0, CoupleList.Count)];
            //Debug.Log(GameObject.Find("CustomButton (2)").GetComponent<ButtonData>().LinkedCouple.DisplayName);

            choice1.GetComponent<Button>().onClick.AddListener(() => handleClick(choice1.GetComponent<ButtonData>().LinkedCouple));
            choice2.GetComponent<Button>().onClick.AddListener(() => handleClick(choice2.GetComponent<ButtonData>().LinkedCouple));
            choice3.GetComponent<Button>().onClick.AddListener(() => handleClick(choice3.GetComponent<ButtonData>().LinkedCouple));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void handleArmorUpgrade()
    {
        // Increase player armor
        Stats.PlayerStat.DefenseLevel += 1;
        // Increase armor level in level infos
        TransitionInfos.LevelTransitionInfo.ArmorLevel++;

        Debug.Log("Load next level");
        LevelsData.LoadNextLevel();
    }

    public void handleWeaponUpgrade()
    {
        // Increase bullet damage (for now)
        Stats.PlayerStat.WeaponLevel += 1;
        // Change Weapon level in level infos (usefull for upgrade generation)
        TransitionInfos.LevelTransitionInfo.GunLevel++;
        // TODO : Handle Weapon change

        Debug.Log("Load next level");
        LevelsData.LoadNextLevel();
    }

    public void handleClick(Couple couple)
    {
        HandleChoice.applyMod(couple.EnemyMod);
        HandleChoice.applyMod(couple.PlayerMod);
        Stats.PlayerStat.ChosenCouples.Add(couple);

        Debug.Log("Load next level");
        LevelsData.LoadNextLevel();
    }
}