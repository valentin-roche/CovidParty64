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
    float range = 1f;



    // Start is called before the first frame update
    void Start()
    {
        damage = PlayerStat.BulletDamage;
        range = PlayerStat.ProjectileDistance;
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 1f);
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(0, 10);
    }

    private void Update()
    {
        damage = PlayerStat.BulletDamage;
        range = PlayerStat.ProjectileDistance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        animator.SetTrigger("Destruction");

        if (collision.gameObject.tag == "EnemySmall" || collision.gameObject.tag == "EnemyMedium" || collision.gameObject.tag == "EnemyBig")
        {
            collision.gameObject.GetComponent<EnemyMedAI>().TakeDamage(damage);

        }
        if (collision.gameObject.name == "BossPrefab(Clone)" || collision.gameObject.name == "BossSprite")
        {
            collision.gameObject.GetComponent<BossAI>().TakeDamage(damage);
        }
        if (collision.gameObject.tag == "Boss")
        {
            collision.gameObject.GetComponent<BossAI>().TakeDamage(damage);
        }
       Destroy(gameObject, range);
    }


}
