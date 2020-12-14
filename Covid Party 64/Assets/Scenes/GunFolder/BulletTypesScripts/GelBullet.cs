using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelBullet : MonoBehaviour
{
    //Declaration of objects
    public Animator animator;    
    public Rigidbody2D rb;
    //Variables
    public float bulletSpeed;
    int damage;
    float range;
    float animDestroyDuration = .5f;
    //Check booleans
    bool collisionBool = false;


    void Start()
    {
        //Variables initialization
        damage = PlayerStat.BulletDamage;
        range = PlayerStat.ProjectileDistance;
        rb.velocity = transform.right * bulletSpeed;
        //Destroy "range" seconds after instanciation => Define the range of the bullet
        Invoke("DestroyBullet", range);
        //Physic gesture to ignore some Layers
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(0, 10);
        if (PlayerStat.WallBang)
        {
            //Ignore Walls if WallBang bonus is active
            Physics2D.IgnoreLayerCollision(8, 10);
        }
    }

    private void Update()
    {
        //Update bullet stats from PlayerStat
        damage = PlayerStat.BulletDamage;
        range = PlayerStat.ProjectileDistance;
        //Adapt destroy time if collision to play animation
        if (collisionBool)
        {
            CancelInvoke("DestroyBullet");
        }
    }

    //Gesture of collision event
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Update boolean checker to true
        collisionBool = true;
        //Play destruction animation
        animator.SetTrigger("Destruction");
        Destroy(gameObject, animDestroyDuration);
        //Regen Player if DrainAtTouch bonus is active
        if (Stats.PlayerStat.DrainAtTouch)
        {
            PlayerStatsHandler.instance.Drain();
        }
        //Chance to double damage if Critical bonus is active
        if (Stats.PlayerStat.Critical)
        {
            int rand = Random.Range(0, 5); // Generate a random number between 0 and 4

            if (rand == 1)
            {
                damage *= 2;
            }

        }
        if (collision.gameObject.tag == "EnemyS")
        {
            collision.gameObject.GetComponent<EnemySmallAI>().TakeDamage(damage);
            //Apply stun effect to enemy
            if (Stats.PlayerStat.Stun)
            {
                Debug.Log("Stun method call");
                collision.gameObject.GetComponent<EnemySmallAI>().StunFromPlayer();
            }

        }
        if (collision.gameObject.tag == "EnemyM")
        {
            collision.gameObject.GetComponent<EnemyMedAI>().TakeDamage(damage);
            //Apply stun effect to enemy
            if (Stats.PlayerStat.Stun)
            {
                collision.gameObject.GetComponent<EnemyMedAI>().StunFromPlayer();
            }
        }
        if (collision.gameObject.tag == "EnemyL")
        {
            collision.gameObject.GetComponent<EnemyLargeAI>().TakeDamage(damage);
            //Apply stun effect to enemy
            if (Stats.PlayerStat.Stun)
            {
                collision.gameObject.GetComponent<EnemyLargeAI>().StunFromPlayer();
            }
        }
        if (collision.gameObject.name == "BossPrefab(Clone)" || collision.gameObject.name == "BossSprite")
        {
            collision.gameObject.GetComponent<BossAI>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Boss")
        {
            //Apply increased damages to boss if bonus active
            if (Stats.PlayerStat.IncreasedBossDamage)
            {
                collision.gameObject.GetComponent<BossAI>().TakeDamage((int) (damage*1.5));
            }
            else
            {
                collision.gameObject.GetComponent<BossAI>().TakeDamage(damage);
            }
            
        }
    }
    //Method to destroy the GameObject bullet
    void DestroyBullet()
    {
        Destroy(gameObject);
    }



}
