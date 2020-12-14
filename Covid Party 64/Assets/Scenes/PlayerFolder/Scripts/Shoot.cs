
using UnityEngine;

public class Shoot : MonoBehaviour
{
    //Declaration of objects
    public GameObject bullet;
    public Animator animator;
    public Transform shootPoint;
    //Variables
    public float fireRate;
    float readyForNextShot;
    //Instance
    public static Shoot instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Il y a plus d'une instance Shoot dans la scène.");
        }
        instance = this;
    }

    void Update()
    {

        //Horizontal shot
        if (Input.GetMouseButton(0))
        {
            if(Time.time > readyForNextShot)
            {
                //Define the fire rate of the weapon
                readyForNextShot = Time.time + fireRate;
                //Calling method to make weapon shoot
                shootHorizontal();
            }

            
        }
        //Vertical shot
        else if (Input.GetMouseButton(1))
        {
            if (Time.time > readyForNextShot)
            {
                //Define the fire rate of the weapon
                readyForNextShot = Time.time + fireRate;
                //Calling method to make weapon shoot
                shootVertical();            
            }
        }

    }

    
    //Horizontal shot
    void shootHorizontal()
    {
        //Set animator in shoot mode
        animator.SetTrigger("Shoot");
        //Instantiate a bullet prefab
        Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    void shootVertical()
    {
        //Set animator in shoot mode
        animator.SetTrigger("Shoot");
        //Reorient in the right position the bullet to shoot
        Quaternion verticalRotation = Quaternion.Euler(shootPoint.rotation.x, shootPoint.rotation.y, shootPoint.rotation.z + 90f);
        //Instantiate a bullet prefab
        Instantiate(bullet, shootPoint.position, verticalRotation);
    }
}
