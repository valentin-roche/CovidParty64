using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelBullet : MonoBehaviour
{

    public float bulletSpeed;
    public Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        if (Shoot.instance.isLeftClick)
        {
            rb.velocity = transform.right * bulletSpeed;
        }
        else if (!Shoot.instance.isLeftClick)
        {
            rb.velocity = transform.up * bulletSpeed;
        }
        
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision != GameObject.FindWithTag("Player").GetComponent("CapsuleCollider2D"))
        {
            Debug.Log(collision);        
            Destroy(gameObject);
        }        
    }

}
