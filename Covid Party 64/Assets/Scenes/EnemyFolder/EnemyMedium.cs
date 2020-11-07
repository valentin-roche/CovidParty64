using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMedium : MonoBehaviour
{
    public int playerDamage;
    private Animator animator;
    public int speed = 5;
    public Transform target;
    private Rigidbody2D rigidbodyComponent;
    private Vector2 enemyDir;


    // Start is called before the first frame update
    void Start()
    {
       
        //target.position = new Vector2(0, 0); //GameObject.FindGameObjectWithTag ("Player").transform;
        enemyDir = new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        //If the difference in positions is approximately zero (Epsilon) do the following:
        if (Mathf.Abs(target.position.x - transform.position.x) > 1)

            //If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
            //enemyDir.y = target.position.y > transform.position.y ? 1 : -1;

        //If the difference in positions is not approximately zero (Epsilon) do the following:
        //else
            //Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
            enemyDir.x = target.position.x > transform.position.x ? 1 : -1;
        
    }

    void FixedUpdate()
    {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
        
        // Apply movement to the rigidbody
        rigidbodyComponent.velocity = speed * enemyDir;
    }
}
