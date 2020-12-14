using Stats;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerStatsHandler : MonoBehaviour
{
    //Declaration of Objects
    public HealthBar healthBar;
    public Text maskCountText;
    public Text radioCountText;
    public Text bottleCountText;
    public GameObject Particles;
    //Declaration of variables used in this class
    private  int minContamination = 0;
    private  float readyForNextDamage, readyForNextRegenTick;
    //Declaration of the instance to allow other class to call this 
    //instance and so access the class content
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

    void Start()
    {
        GetComponent<CircleCollider2D>().radius = PlayerStat.ContaminationDist;     //Put the radius value from PlayerStat of the circle that detect enemies
        if(PlayerStat.PlayerInventory.Count == 0)
        {
            initInventory();                                                        //Initialize inventory if needed
        }
        PlayerStat.ContaminationRate = minContamination;                            //Reset contamination of the player
        healthBar.SetContaminationInit(minContamination);                           //Update Health Bar with the right value of the current contamination
        upArmor();                                                                  //Update armor state if needed
    }

    void FixedUpdate()
    {
        //Test if regen mode is active
        if (PlayerStat.Regen)
        {
            //Apply regen effects every seconds
            if (Time.time > readyForNextRegenTick)
            {
                readyForNextRegenTick = Time.time + 1;
                Regen(PlayerStat.RegenTick);                                        //Reduce contamination by "PlayerStat.RegenTick"
                healthBar.SetContamination(PlayerStat.ContaminationRate);           //Update Health Bar
            }
        }

        //Damages from enemies
        if (Time.time > readyForNextDamage)
        {
            readyForNextDamage = Time.time + 1;
            GetContaminate(CalculateDamage());                                      //Apply damages calculated to the player
            healthBar.SetContamination(PlayerStat.ContaminationRate);               //Update Health Bar
        }
        upArmor();                                                                  //Update armor state if needed
        if(!maskCountText.text.Equals(PlayerStat.PlayerInventory["Mask"].ToString())//Update inventory quantities if not to date
            || !bottleCountText.text.Equals(PlayerStat.PlayerInventory["BottleGel"].ToString())
            || !radioCountText.text.Equals(PlayerStat.PlayerInventory["Radio"].ToString()))
        {
            updateInventoryDisplay();
        }
    }

    //dégats donnés au joeur
    public void GetContaminate(int _contamination)
    {
        //Update the player's contamination if damages are taken
        PlayerStat.ContaminationRate += _contamination;

        //Handle death case (contaminationMax reached)
        if (PlayerStat.ContaminationRate >= PlayerStat.MaxContamination)
        {
            Die();
            return;
        }
    }
    
    public static void Regen(int _regenTick)
    {
        //Reduce contamination rate by _regenTick
        if (PlayerStat.ContaminationRate > 0)
        {
            PlayerStat.ContaminationRate -= _regenTick;
        }
        //
        if (PlayerStat.DrainAtProximity && EnemyDetection.instance.nbrEnemySmall + EnemyDetection.instance.nbrEnemyMedium + EnemyDetection.instance.nbrEnemyBig >= 3)
        {        
            PlayerStat.ContaminationRate -= 3;
            for(int i = 0; i < EnemyDetection.instance.enemiesSmallAtProximity.Length; i++)
            {
                EnemyDetection.instance.enemiesSmallAtProximity[i].GetComponent<EnemySmallAI>().TakeDamage(2);
            }
            for(int i = 0; i < EnemyDetection.instance.enemiesMediumAtProximity.Length; i++)
            {
                EnemyDetection.instance.enemiesMediumAtProximity[i].GetComponent<EnemyMedAI>().TakeDamage(2);
            }
            for(int i = 0; i < EnemyDetection.instance.enemiesLargeAtProximity.Length; i++)
            {
                EnemyDetection.instance.enemiesLargeAtProximity[i].GetComponent<EnemyLargeAI>().TakeDamage(2);
            }
        }
    }

    //Calculate damages taken regarding the number of ennemies in the detection area
    public static int CalculateDamage()
    {
        int damage = 0;
        int _nbrEnemySmall = 0;
        int _nbrEnemyMedium = 0;
        int _nbrEnemyBig = 0;
        int _nbrBoss = 0;
        int dmgSmall = Stats.EnemyStatSmall.Damage;
        int dmgMed = Stats.EnemyStatMedium.Damage;
        int dmgLarge = Stats.EnemyStatLarge.Damage;
        int dmgBoss = Stats.BossStat.Damage;
        int rand;

        //Apply Critical Enemy effect if bonus active
        if (Stats.EnemyStatSmall.Critical)
        {
            rand = Random.Range(1, 5);

            if (rand == 1)
            {
                dmgSmall = dmgSmall * 2;
            }
        }

        if (Stats.EnemyStatMedium.Critical)
        {
            rand = Random.Range(1, 5);

            if (rand == 1)
            {
                dmgMed = dmgMed * 2;
            }
        }

        if (Stats.EnemyStatLarge.Critical)
        {
            rand = Random.Range(1, 5);

            if (rand == 1)
            {
                dmgLarge = dmgLarge * 2;
            }
        }

        //Retrieve numbers of enemy by types detected by EnemeyDetection script
        if (EnemyDetection.instance)
        {
            _nbrEnemySmall = EnemyDetection.instance.nbrEnemySmall;
            _nbrEnemyMedium = EnemyDetection.instance.nbrEnemyMedium;
            _nbrEnemyBig = EnemyDetection.instance.nbrEnemyBig;
            _nbrBoss = EnemyDetection.instance.nbrBoss;
        }
        //Calculate damages 
        if (Stats.PlayerStat.Dodge)                                                     //Check if Dodge bonus is active to apply its effect
        {
            int rand2 = Random.Range(0, 7);
            if (rand2 == 1)
            {
                damage = 0;
            }
            else
            {
                damage = _nbrBoss * dmgBoss + _nbrEnemySmall * dmgSmall + _nbrEnemyMedium * dmgMed + _nbrEnemyBig* dmgLarge;
            }
        }
        else
        {
            damage = _nbrBoss * dmgBoss + _nbrEnemySmall * dmgSmall + _nbrEnemyMedium * dmgMed + _nbrEnemyBig * dmgLarge;
        }
        
        

        //Saturation of damages
        if (damage >= 25)
        {
            damage = 25;
        }

        return damage;
    }

    //Method called if the player reaches the max contamination rate
    public void Die()
    {
        //Check if the player can revive
        if (PlayerStat.PlayerInventory["Mask"]>0)
        {
            //Display menu to use the mask or not
            GameOverManager.instance.OnPlayerRespawnActive();
        }
        else
        {
            //Kill the player if mask is not used or possessed
            Kill();
        }


    }

    //Method called to kill the player
    public void Kill()
    {
        //Block the player
        PlayerMovement.instance.PlayerMovementStop();
        PlayerMovement.instance.enabled = false;
        GameObject.Find("WeaponHolder").SetActive(false);
        Shoot.instance.enabled = false;        

        GameOverManager.instance.OnPlayerRespawnNoActive();
        GameOverManager.instance.OnPlayerDeath();
    }

    //Make the player respawn
    public static void Respawn()
    {
        //Display th respawn UI
        GameOverManager.instance.OnPlayerRespawnNoActive();
        //Use the item Mask
        PlayerStat.PlayerInventory["Mask"]--;
        instance.maskCountText.text = PlayerStat.PlayerInventory["Mask"].ToString();
        //Update the contamination of the player
        PlayerStat.ContaminationRate = PlayerStat.MaxContamination / 2;
    }

    //Steal life when enemy is touch by bullet/laser
    public void Drain()
    {
        if(PlayerStat.ContaminationRate >= 1)
        {
            //Update player's contamination if enough contamination to be decreased
            PlayerStat.ContaminationRate -= 1;
            healthBar.SetContamination(PlayerStat.ContaminationRate);//Update Health Bar
        }
        else
        {
            //Leave contamination to zero if the player has no contamination
            PlayerStat.ContaminationRate = 0;
            healthBar.SetContamination(PlayerStat.ContaminationRate);//Update Health Bar
        }
        
    }

    //Make the aura of the player correspond to its DefenseLevel
    public void upArmor()
    { 
        if(PlayerStat.DefenseLevel == 1)
        {
            //Update display
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 0);
            //Update stats
            PlayerStat.Armor += 10;
            PlayerStat.MaxContamination += PlayerStat.Armor;
        }

        if(PlayerStat.DefenseLevel == 2)
        {
            //Update display
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 0.68f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Width", 0.008f);
            gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(43/255f, 184/255f, 195/255f, 1f));
            //Update stats
            PlayerStat.Armor += 10;
            PlayerStat.MaxContamination += PlayerStat.Armor;


        }

        if (PlayerStat.DefenseLevel == 3)
        {
            //Update display
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 2.03f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Width", 0.008f);
            gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(43/255f, 184/255f, 195/255f, 1f));
            //Update stats
            PlayerStat.Armor += 10;
            PlayerStat.MaxContamination += PlayerStat.Armor;

        }

        if (PlayerStat.DefenseLevel== 4)
        {
            //Update display
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 4.15f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Width", 0.0185f);
            gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(43/255f, 184/255f, 195/255f, 1f));
            //Update stats
            PlayerStat.Armor += 10;
            PlayerStat.MaxContamination += PlayerStat.Armor;
        }
        
    }




    //Initialize the inventory with the objects names
    public static void initInventory()
    {
        PlayerStat.PlayerInventory["Mask"] = 0;
        PlayerStat.PlayerInventory["Radio"] = 0;
        PlayerStat.PlayerInventory["BottleGel"] = 0;
    }

    //Update inventory display
    public void updateInventoryDisplay()
    {
        maskCountText.text = PlayerStat.PlayerInventory["Mask"].ToString();
        radioCountText.text = PlayerStat.PlayerInventory["Radio"].ToString();
        bottleCountText.text = PlayerStat.PlayerInventory["BottleGel"].ToString();
    }

    //Update inventory when item looted
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

    //Update inventory when item used
    public void useItem(string tagObject)
    {
        switch (tagObject)
        {
            case "BottleGel":
                //Apply Bottle effect : -60% contamination
                PlayerStat.PlayerInventory["BottleGel"]--;
                if(PlayerStat.ContaminationRate - (int)(PlayerStat.MaxContamination * 0.6) < 0)
                {
                    PlayerStat.ContaminationRate = 0;
                }
                else
                {
                    PlayerStat.ContaminationRate -= (int) (PlayerStat.MaxContamination * 0.6);
                }  
                //Update inventory
                bottleCountText.text = PlayerStat.PlayerInventory["BottleGel"].ToString();
                Particles.SetActive(true);
                LifeParticule.Instance.Life(gameObject.transform.position);
                break;

            case "Radio":
                //Update inventory
                PlayerStat.PlayerInventory["Radio"]--;
                radioCountText.text = PlayerStat.PlayerInventory["Radio"].ToString();
                //Apply flash effect
                PlayFlashEffect.instance.FlashEffect();
                break;
        }
    }

}
