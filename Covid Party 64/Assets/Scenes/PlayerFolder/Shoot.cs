
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public Transform gun;

    Vector2 direction;

    public GameObject bullet;
    public float bulletSpeed;

    public Transform shootPoint;

    public float fireRate;
    float readyForNextShot;
   
    void Start()
    {
        
    }

  
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + fireRate;
                shootHorizontal();
            }
            
        } else if (Input.GetMouseButton(1))
        {
            if (Time.time > readyForNextShot)
            {
                readyForNextShot = Time.time + fireRate;
                shootVertical();
            }
        }
    }

    void shootHorizontal()
    {
        Debug.Log("Horizontal shot");
        GameObject BulletIns = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletSpeed);
        Destroy(BulletIns, 2f);
    }

    void shootVertical()
    {
        Debug.Log("Vertical shot");
        GameObject BulletIns = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.up * bulletSpeed);
        Destroy(BulletIns, 2f);
    }
}
