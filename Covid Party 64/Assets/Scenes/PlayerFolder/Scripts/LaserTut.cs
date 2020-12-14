using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTut : MonoBehaviour
{
    //Declaration of objects
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject startVFX;
    public GameObject endVFX;
    //Variables
    private int laserDPS = Stats.PlayerStat.LaserDPS;
    private float readyForNextDamage;
    private Quaternion rotation;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    void Start()
    {
        GameObject.Find("items").layer = LayerMask.NameToLayer("Ignore Raycast");
        FillLists();
        DisableLaser();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableLaser();

        }
        
        if (Input.GetMouseButton(0))
        {
            UpdateLaser();

        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();

        }
    }

    //Laser and particles activation 
    void EnableLaser()
    {
        lineRenderer.enabled = true;

        for(int i = 0; i<particles.Count; i++)
        {
            particles[i].Play();
        }
    }


    void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);                //start position of the laser
        startVFX.transform.position = firePoint.position;               //blue light at the exit of the barrel 

        lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 10);  //end position of the laser

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);  //object detection


        //test if there is an enemy between the start and the end  laser position 
        if (hitInfo && Mathf.Abs(firePoint.position.x - hitInfo.point.x) < Mathf.Abs(firePoint.right.x * 10))
        {
            if(hitInfo.transform.tag == "EnemyS" || hitInfo.transform.tag == "EnemyM" || hitInfo.transform.tag == "EnemyL" || hitInfo.transform.tag == "Boss")
            {
                lineRenderer.SetPosition(1, hitInfo.point);
                DamageEnemy(laserDPS, hitInfo);
            }
            if(!Stats.PlayerStat.WallBang && hitInfo.transform.tag == "Wall") 
            {
                lineRenderer.SetPosition(1, hitInfo.point);
            }

            
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);  //blue light at the end of the laser position
    }


    //Destroy laser
    void DisableLaser()
    {
        lineRenderer.enabled = false;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }


    //Instantiate particles coming out of the blue lights
    void FillLists()
    {
        for(int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }
        
        for(int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }
    }

    //Laser dammage on enemies
    void DamageEnemy(int DPS, RaycastHit2D target)
    {
        if (Time.time > readyForNextDamage)
        {
            //apply critical dammage
            if (Stats.PlayerStat.Critical)
            {
                int rand = Random.Range(0, 5); // Genere un nombre aleatoire entre 0 et 4

                if(rand == 1)
                {
                    DPS *= 2; //Dammage per seconds
                }
                
            }
            //apply DrainAtTouch effect
            if (Stats.PlayerStat.DrainAtTouch)
            {
                PlayerStatsHandler.instance.Drain();
            }

            readyForNextDamage = Time.time + 0.1f;
            Debug.Log("Damage to : " + target.transform.tag +", DPS : "+DPS);

            //test the enemy type
            if (target.transform.tag == "EnemyS")
            {
                target.transform.GetComponent<EnemySmallAI>().TakeDamage(DPS);
                //Apply stun effect to enemy
                if (Stats.PlayerStat.Stun)
                {
                    target.transform.GetComponent<EnemySmallAI>().StunFromPlayer();
                }

            }
            if (target.transform.tag == "EnemyM")
            {
                target.transform.GetComponent<EnemyMedAI>().TakeDamage(DPS);
                //Apply stun effect to enemy
                if (Stats.PlayerStat.Stun)
                {
                    target.transform.GetComponent<EnemyMedAI>().StunFromPlayer();
                }
            }
            if (target.transform.tag == "EnemyL")
            {
                target.transform.GetComponent<EnemyLargeAI>().TakeDamage(DPS);
                //Apply stun effect to enemy
                if (Stats.PlayerStat.Stun)
                {
                    target.transform.GetComponent<EnemyLargeAI>().StunFromPlayer();
                }
            }
            if (target.transform.name == "BossPrefab(Clone)" || target.transform.name == "BossSprite")
            {
                target.transform.GetComponent<BossAI>().TakeDamage(DPS);
            }
            if (target.transform.tag == "Boss")
            {
                if (Stats.PlayerStat.IncreasedBossDamage)
                {
                    target.transform.GetComponent<BossAI>().TakeDamage((int)(DPS * 1.5));
                }
                else
                {
                    target.transform.GetComponent<BossAI>().TakeDamage(DPS);
                }
            }
        }
    }
}
