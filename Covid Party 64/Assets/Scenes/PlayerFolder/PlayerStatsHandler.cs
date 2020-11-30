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
        PlayerStat.addBonusEffect();

        if (Time.time > readyForNextDamage)
        {
            readyForNextDamage = Time.time + 1;
            GetContaminate(CalculateDamage());
            healthBar.SetContamination(PlayerStat.ContaminationRate);
        }

    }

    public static void GetContaminate(int _contamination)
    {
        PlayerStat.ContaminationRate += _contamination;

        if (PlayerStat.ContaminationRate >= 100)
        {
            Die();
            return;
        }
    }

    public static int CalculateDamage()
    {
        int damage;
        int _nbrEnemySmall = 0;
        int _nbrEnemyMedium = 0;
        int _nbrEnemyBig = 0;

        if (EnemyDetection.instance)
        {
            _nbrEnemySmall = EnemyDetection.instance.nbrEnemySmall;
            _nbrEnemyMedium = EnemyDetection.instance.nbrEnemyMedium;
            _nbrEnemyBig = EnemyDetection.instance.nbrEnemyBig;
        }

        damage = _nbrEnemySmall*1 + _nbrEnemyMedium*2 + _nbrEnemyBig*3; //Formule à modifier
        
        if (damage >= 25)//Saturation du nbr de degats pris
        {
            damage = 25;
        }

        return damage;
    }


    public static void Die()
    {
        if (false) //Test pour savoir si joueur peut revive
        {
            //Respawn();
        }
        else
        {
            Debug.Log("Le joueur a été contaminé ! GAME OVER");
            //Bloquer mouvements du personnages

            PlayerMovement.instance.enabled = false;
            Shoot.instance.enabled = false;
           
            //Jouer animation de mort

            PlayerMovement.instance.animator.SetTrigger("Death");

            //Empêcher l'interaction avec le reste de la scène.
        }


    }

}
