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
    // Start is called before the first frame update
    void Start()
    {
        choice1.GetComponent<Button>().onClick.AddListener(() => handleClick(choice1.GetComponent<ButtonData>().LinkedCouple));
        choice2.GetComponent<Button>().onClick.AddListener(() => handleClick(choice2.GetComponent<ButtonData>().LinkedCouple));
        choice3.GetComponent<Button>().onClick.AddListener(() => handleClick(choice3.GetComponent<ButtonData>().LinkedCouple));
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
                        PlayerStat.Speed += modif;
                        break;
                    case "damage":
                        // TODO : Change projectile damage
                        break;
                    case "contaminationDist":
                        PlayerStat.ContaminationRate += modif;
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

        }
    }
}
