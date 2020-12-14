using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelBullet : MonoBehaviour
{

    public Animator animator;
    public float bulletSpeed;
    public Rigidbody2D rb;
    int damage;
    float range;
    float animDestroyDuration = .5f;
    bool collisionBool = false;



    // Start is called before the first frame update
    void Start()
    {
        damage = PlayerStat.BulletDamage;
        range = PlayerStat.ProjectileDistance;
        rb.velocity = transform.right * bulletSpeed;
        Invoke("DestroyBullet", 1);
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(0, 10);
        if (PlayerStat.WallBang)
        {
            Physics2D.IgnoreLayerCollision(8, 10);
        }
    }

    private void Update()
    {
        damage = PlayerStat.BulletDamage;
        range = PlayerStat.ProjectileDistance;
        if (collisionBool)
        {
            CancelInvoke("DestroyBullet");
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisionBool = true;

        Debug.Log(collision);

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

    void DestroyBullet()
    {
        Destroy(gameObject);
    }



}
