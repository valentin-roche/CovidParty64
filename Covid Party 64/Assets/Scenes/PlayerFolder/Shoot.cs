
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;

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
    }

    void shootVertical()
    {
        Debug.Log("Vertical shot");
        Quaternion verticalRotation = Quaternion.Euler(shootPoint.rotation.x, shootPoint.rotation.y, shootPoint.rotation.z + 90f);
        Instantiate(bullet, shootPoint.position, verticalRotation);
    }
    
}
