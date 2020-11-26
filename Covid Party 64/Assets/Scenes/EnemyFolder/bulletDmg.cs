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
        if (enemy.gameObject.tag == "Enemy")
        {
            enemy.gameObject.GetComponent<EnemyMedAI>().TakeDamage(damage);
        }
        Destroy(gameObject); 
    }
}
