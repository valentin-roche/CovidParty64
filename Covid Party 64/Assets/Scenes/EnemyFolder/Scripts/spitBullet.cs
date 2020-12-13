using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitBullet : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D rb;
    float animDestroyDuration = .5f;
    public Animator animator;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        Invoke("DestroyBullet", 1);
        damage = Stats.EnemyStatMedium.Damage;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        switch (hitInfo.tag)
        {
            case "EnemyS":
                break;

            case "EnemyM":
                break;

            case "EnemyL":
                break;

            case "Player":
                //Physics2D.IgnoreCollision(hitInfo.GetComponent<CircleCollider2D>(), GetComponent<BoxCollider2D>());
                if(hitInfo == GameObject.Find("Player").GetComponent<CapsuleCollider2D>())
                {
                    GameObject.Find("Player").GetComponent<PlayerStatsHandler>().GetContaminate(damage);

                    animator.SetTrigger("Destruction");

                    Destroy(gameObject);
                }
                break;
            case "Wall":
                Destroy(gameObject);
                break;
        }           
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
