using UnityEngine;

public class PickUpObjet : MonoBehaviour
{

    void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 13);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Inventory.instance.addObjet(gameObject);
            Destroy(gameObject);
        }
    }
}
