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

        if (Stats.PlayerStat.DrainAtTouch)
        {
            PlayerStatsHandler.instance.Drain();
        }

        if (Stats.PlayerStat.Critical)
        {
            int rand = Random.Range(0, 5); // Genere un nombre aleatoire entre 0 et 4

            if (rand == 1)
            {
                damage *= 2;
            }

        }

        if (collision.gameObject.tag == "EnemyS")
        {
            collision.gameObject.GetComponent<EnemySmallAI>().TakeDamage(damage);

        }
        if (collision.gameObject.tag == "EnemyM")
        {
            collision.gameObject.GetComponent<EnemyMedAI>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "EnemyL")
        {
            collision.gameObject.GetComponent<EnemyLargeAI>().TakeDamage(damage);
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
