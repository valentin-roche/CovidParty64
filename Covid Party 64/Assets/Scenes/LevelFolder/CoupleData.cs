using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public readonly List<Couple> CoupleList = new List<Couple>() 
    {
        new Couple()
        {
            Name = "fastercont",
            DisplayName = "Ennemis plus rapide /\n Distance de contamination réduite",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "speed", EffectType = "modify", Value = 5 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "contaminationDist", EffectType = "modify", Value = -5 }
        },
        new Couple()
        {
            Name = "weakerslower",
            DisplayName = "Ennemis plus faibles /\n Joueur plus lent",
            EnemyMod = new Power() { Target = "enemy", AffectedStat = "health", EffectType = "modify", Value = -10 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "speed", EffectType = "modify", Value = -5 }
        },
        new Couple()
        {
            Name = "closerstronger",
            DisplayName = "Distance de contamination réduite /\n Dégâts augmentés",
            EnemyMod = new Power() { Target = "player", AffectedStat = "contaminationDist", EffectType = "modify", Value = -5 },
            PlayerMod = new Power() { Target = "player", AffectedStat = "damage", EffectType = "modify", Value = 5 }
        }
    };

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("CustomButton").GetComponent<ButtonData>().LinkedCouple = CoupleList[0];

        GameObject.Find("CustomButton (1)").GetComponent<ButtonData>().LinkedCouple = CoupleList[1];

        GameObject.Find("CustomButton (2)").GetComponent<ButtonData>().LinkedCouple = CoupleList[2];
        Debug.Log(GameObject.Find("CustomButton (2)").GetComponent<ButtonData>().LinkedCouple.DisplayName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
