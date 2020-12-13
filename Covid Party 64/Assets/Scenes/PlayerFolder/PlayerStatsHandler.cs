using Stats;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerStatsHandler : MonoBehaviour
{

    public  HealthBar healthBar;
    private  int minContamination = 0;
    private  float readyForNextDamage, readyForNextRegenTick;

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
        GetComponent<CircleCollider2D>().radius = PlayerStat.ContaminationDist;
        initInventory(); //initialisation de l'inventaire à zéro
        PlayerStat.ContaminationRate = minContamination; //contamination du joueur nulle
        healthBar.SetContaminationInit(minContamination); //barre de vie initialisée à zéro
        upArmor();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerStat.addBonusEffect();

        //Test si regen active
        if (PlayerStat.Regen)
        {
            //Applique les effets de regen toutes les secondes
            if (Time.time > readyForNextRegenTick)
            {
                readyForNextRegenTick = Time.time + 1;
                Regen(PlayerStat.RegenTick);//Soigne la contamination du Player de "PlayerStat.RegenTick"
                healthBar.SetContamination(PlayerStat.ContaminationRate);//Met a jour la barre de vie
            }
        }

        //prise de dégat du joueur par seconde
        if (Time.time > readyForNextDamage)
        {
            readyForNextDamage = Time.time + 1;
            GetContaminate(CalculateDamage());//calcul les dégats subits
            healthBar.SetContamination(PlayerStat.ContaminationRate);//augmente la barre de vie en fonction des dégats subits
        }


        upArmor();

    }

    //dégats donnés au joeur
    public static void GetContaminate(int _contamination)
    {
        //ContaminationRate = vie actuelle du joueur
        //on l'ajoute au dégat que subit le jouer
        PlayerStat.ContaminationRate += _contamination;

        //si la contamination du joueur dépasse 100, il meurt
        if (PlayerStat.ContaminationRate >= PlayerStat.MaxContamination)
        {
            Die();
            return;
        }
    }
    
    public static void Regen(int _regenTick)
    {
        //ContaminationRate = vie actuelle du joueur
        //on l'ajoute au dégat que subit le jouer
        if (PlayerStat.ContaminationRate > 0)
        {
            PlayerStat.ContaminationRate -= _regenTick;
        }
    }

    //calcul des dégats en fonction des ennemis 
    //et du nombre présent dans le circle collider du joueur
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
        int rand = Random.Range(0, 5);


        

        //???????????????
        //applique des coups critiques
        if (Stats.EnemyStatSmall.Critical)
        {
            if(rand == 1)
            {
                dmgSmall = dmgSmall * 2;
            }
        }

        if (Stats.EnemyStatMedium.Critical)
        {
            if (rand == 1)
            {
                dmgMed = dmgMed * 2;
            }
        }

        if (Stats.EnemyStatLarge.Critical)
        {
            if (rand == 1)
            {
                dmgLarge = dmgLarge * 2;
            }
        }

        //calcul le nombre d'ennemi dans le circle collider du joueur
        if (EnemyDetection.instance)
        {
            _nbrEnemySmall = EnemyDetection.instance.nbrEnemySmall;
            _nbrEnemyMedium = EnemyDetection.instance.nbrEnemyMedium;
            _nbrEnemyBig = EnemyDetection.instance.nbrEnemyBig;
            _nbrBoss = EnemyDetection.instance.nbrBoss;
        }
        //application des dégats 
        if (Stats.PlayerStat.Dodge)
        {
            int rand2 = Random.Range(0, 7);
            if (rand2 == 1)
            {
                damage = 0;
            }
            else
            {
                damage = _nbrBoss * dmgBoss + _nbrEnemySmall * dmgSmall + _nbrEnemyMedium * dmgMed + _nbrEnemyBig* dmgLarge; //Formule à modifier
            }
        }
        else
        {
            damage = _nbrBoss * dmgBoss + _nbrEnemySmall * dmgSmall + _nbrEnemyMedium * dmgMed + _nbrEnemyBig * dmgLarge; //Formule à modifier
        }
        
        

        //Saturation du nbr de degats pris
        if (damage >= 25)
        {
            damage = 25;
        }

        return damage;
    }

    //Test possession masque + mort
    public static void Die()
    {
        //si le joueur possède un masque on affiche le menu de respawn 
        //permettant de recommencer le niveau
        //sinon il meurt

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

    //mort du joueur
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


    //affichages du menu de respawn
    //diminution du nombre de masque dans l'inventaire
    //division par 2 de la contamination du joueur
    public static void Respawn()
    {
        GameOverManager.instance.OnPlayerRespawnNoActive();
        PlayerStat.PlayerInventory["Mask"]--;
        instance.maskCountText.text = PlayerStat.PlayerInventory["Mask"].ToString();
        PlayerStat.ContaminationRate = PlayerStat.MaxContamination / 2;
    }


    public void Drain()
    {
        if(PlayerStat.ContaminationRate >= 2)
        {
            PlayerStat.ContaminationRate += 2;
            healthBar.SetContamination(PlayerStat.ContaminationRate);//Met a jour la barre de vie
        }
        else
        {
            PlayerStat.ContaminationRate = 0;
            healthBar.SetContamination(PlayerStat.ContaminationRate);//Met a jour la barre de vie
        }
        
    }

    public void upArmor()
    { 
        if(PlayerStat.DefenseLevel == 1)
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 0);
        }

        if(PlayerStat.DefenseLevel == 2)
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 0.68f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Width", 0.008f);
            gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(43/255f, 184/255f, 195/255f, 1f));
            

        }

        if (PlayerStat.DefenseLevel == 3)
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 2.03f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Width", 0.008f);
            gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(43 / 255f, 184 / 255f, 195 / 255f, 1f));

        }

        if (PlayerStat.DefenseLevel== 4)
        {
            gameObject.GetComponent<Renderer>().material.SetFloat("_Brightness", 4.15f);
            gameObject.GetComponent<Renderer>().material.SetFloat("_Width", 0.0185f);
            gameObject.GetComponent<Renderer>().material.SetColor("_OutlineColor", new Color(43 / 255f, 184 / 255f, 195 / 255f, 1f));
        }
        
    }




    //initialisation de l'inventaire
    public static void initInventory()
    {
        PlayerStat.PlayerInventory["Mask"] = 0;
        PlayerStat.PlayerInventory["Radio"] = 0;
        PlayerStat.PlayerInventory["BottleGel"] = 0;
    }
    
    //ajout à l'inventaire de l'objet récupéré
    //mise a jour du canvas
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

    //diminution de l'objet utilisé 
    //mise à jour du canvas
    public void useItem(string tagObject)
    {
        switch (tagObject)
        {
            case "BottleGel":
                PlayerStat.PlayerInventory["BottleGel"]--;

                //on diminue de 60% la contamination du joueur
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
                PlayFlashEffect.instance.FlashEffect();
                break;
        }
    }

}
