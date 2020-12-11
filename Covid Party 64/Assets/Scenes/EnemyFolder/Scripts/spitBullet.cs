using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spitBullet : MonoBehaviour
{
    public float speed = 5.0f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D hitInfo)
    {

        //if(hitInfo.tag.Equals("EnemyS") == false || hitInfo.tag.Equals("EnemyM") == false || hitInfo.tag.Equals("EnemyL") == false || hitInfo.tag.Equals("SpitBullet") == false)
        if (hitInfo.tag.Equals("EnemyM") == false )
        {
            Debug.Log("touch : " + hitInfo.tag);
            Destroy(gameObject);
        }
    }
}
