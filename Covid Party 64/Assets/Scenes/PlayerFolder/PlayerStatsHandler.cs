using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsHandler : MonoBehaviour
{

    public  HealthBar healthBar;
    private  int minContamination = 0;
    private  float readyForNextDamage;

    // Start is called before the first frame update
    void Start()
    {
        
        PlayerStat.ContaminationRate = minContamination;
        healthBar.SetContaminationInit(minContamination);
    }

    // Update is called once per frame
    void Update()
    {        

        if (Time.time > readyForNextDamage)
        {
            readyForNextDamage = Time.time + 1;
            PlayerStat.GetContaminate(PlayerStat.CalculateDamage());
            healthBar.SetContamination(PlayerStat.ContaminationRate);
        }

    } 

    
}
