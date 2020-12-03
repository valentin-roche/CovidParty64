using UnityEngine;

public class PickUpObjet : MonoBehaviour
{

    void Start()
    {
        Physics2D.IgnoreLayerCollision(11, 13);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Player")
        {
            PlayerStatsHandler.instance.addItem(gameObject);
            Destroy(gameObject);
        }
    }
}
