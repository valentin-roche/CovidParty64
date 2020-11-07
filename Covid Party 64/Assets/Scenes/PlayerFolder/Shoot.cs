
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;

    public Transform shootPoint;

    public float fireRate;
    float readyForNextShot;
    public bool isLeftClick = false;

    public static Shoot instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de Shoot dans la scène.");
        }
        instance = this;
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
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);     
        GameObject BulletIns = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        BulletIns.GetComponent<Rigidbody2D>().AddForce(BulletIns.transform.right * bulletSpeed);
        BulletIns.GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        Destroy(BulletIns, 0.5f);
    }

    void shootVertical()
    {
        Debug.Log("Vertical shot");
        Quaternion verticalRotation = Quaternion.Euler(shootPoint.rotation.x, shootPoint.rotation.y, shootPoint.rotation.z + 90f);
        Instantiate(bullet, shootPoint.position, verticalRotation);
    }
}
