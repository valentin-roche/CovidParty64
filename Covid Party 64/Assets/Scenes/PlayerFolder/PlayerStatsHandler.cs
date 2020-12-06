using Stats;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsHandler : MonoBehaviour
{

    public  HealthBar healthBar;
    private  int minContamination = 0;
    private  float readyForNextDamage;

    public Text maskCountText;
    public Text radioCountText;
    public Text bottleCountText;

    public GameObject Particles;

    public static PlayerStatsHandler instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de PlayerStatsHandler dans la scène");
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        initInventory();
        PlayerStat.ContaminationRate = minContamination;
        healthBar.SetContaminationInit(minContamination);
    }

    // Update is called once per frame
    void FixedUpdate()
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

        if (PlayerStat.ContaminationRate >= PlayerStat.MaxContamination)
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
        if (PlayerStat.PlayerInventory["Mask"]>0) //Test pour savoir si joueur peut revive
        {
            GameOverManager.instance.OnPlayerRespawnActive();
            //Stoper interaction Player / Niveau / Ennemis
        }
        else
        {
            Kill();
        }


    }

    public static void Kill()
    {
        Debug.Log("Kill method called");
        Debug.Log("Le joueur a été contaminé ! GAME OVER");
        //Bloquer mouvements du personnages
        PlayerMovement.instance.PlayerMovementStop();
        PlayerMovement.instance.enabled = false;
        GameObject.Find("WeaponHolder").SetActive(false);
        Shoot.instance.enabled = false;

        //Jouer animation de mort

        

        GameOverManager.instance.OnPlayerRespawnNoActive();
        GameOverManager.instance.OnPlayerDeath();


        
        
        

        //Empêcher l'interaction avec le reste de la scène.
    }

    public static void Respawn()
    {
        GameOverManager.instance.OnPlayerRespawnNoActive();
        PlayerStat.PlayerInventory["Mask"]--;
        instance.maskCountText.text = PlayerStat.PlayerInventory["Mask"].ToString();
        PlayerStat.ContaminationRate = PlayerStat.MaxContamination / 2;
    }

    public static void initInventory()
    {
        PlayerStat.PlayerInventory["Mask"] = 0;
        PlayerStat.PlayerInventory["Radio"] = 0;
        PlayerStat.PlayerInventory["BottleGel"] = 0;
    }
    
     public void addItem(GameObject objet)
    {
        switch (objet.tag)
        {
            case "Mask":
                PlayerStat.PlayerInventory["Mask"]++;
                maskCountText.text = PlayerStat.PlayerInventory["Mask"].ToString();
                break;
            case "Radio":
                PlayerStat.PlayerInventory["Radio"]++;
                radioCountText.text = PlayerStat.PlayerInventory["Radio"].ToString();
                break;
            case "BottleGel":
                PlayerStat.PlayerInventory["BottleGel"]++;
                bottleCountText.text = PlayerStat.PlayerInventory["BottleGel"].ToString();
                break;
        }
    }

    public void useItem(string tagObject)
    {
        switch (tagObject)
        {
            case "BottleGel":
                PlayerStat.PlayerInventory["BottleGel"]--;
                if(PlayerStat.ContaminationRate - (int)(PlayerStat.MaxContamination * 0.6) < 0)
                {
                    PlayerStat.ContaminationRate = 0;
                }
                else
                {
                    PlayerStat.ContaminationRate -= (int) (PlayerStat.MaxContamination * 0.6);
                }                
                bottleCountText.text = PlayerStat.PlayerInventory["BottleGel"].ToString();
                Particles.SetActive(true);
                LifeParticule.Instance.Life(gameObject.transform.position);

                break;
            case "Radio":
                PlayerStat.PlayerInventory["Radio"]--;
                radioCountText.text = PlayerStat.PlayerInventory["Radio"].ToString();
                break;
        }
    }

}
