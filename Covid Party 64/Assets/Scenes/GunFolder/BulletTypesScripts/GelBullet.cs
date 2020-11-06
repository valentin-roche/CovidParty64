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
        //this.transform.rotation = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z - 90f);
        rb.velocity = transform.right * bulletSpeed;
        
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
