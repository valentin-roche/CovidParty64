using Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelBullet : MonoBehaviour
{

    public float bulletSpeed;
    public Rigidbody2D rb;
    int damage;

    

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * bulletSpeed;
        Destroy(gameObject, 1f);
        Physics2D.IgnoreLayerCollision(9, 10);
        Physics2D.IgnoreLayerCollision(10, 10);
        Physics2D.IgnoreLayerCollision(0, 10);
    }

    private void Update()
    {        
        damage = PlayerStat.BulletDamage;        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);

        if (collision.gameObject.tag == "Enemy")
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
        Destroy(gameObject);
    }


}
