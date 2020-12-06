using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDmg : MonoBehaviour
{

    int damage;

    // Start is called before the first frame update
    void Start()
    {
        damage = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        if (enemy.gameObject.tag == "EnemyS")
        {
            enemy.gameObject.GetComponent<EnemySmallAI>().TakeDamage(damage);
        }
        if (enemy.gameObject.tag == "EnemyM")
        {
            enemy.gameObject.GetComponent<EnemyMedAI>().TakeDamage(damage);
        }
        if (enemy.gameObject.tag == "EnemyL")
        {
            enemy.gameObject.GetComponent<EnemyLargeAI>().TakeDamage(damage);
        }
        Destroy(gameObject); 
    }
}
