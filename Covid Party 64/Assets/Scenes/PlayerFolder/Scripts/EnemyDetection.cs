using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDetection : MonoBehaviour
{
    //Arrays of enemies by type
    public GameObject[] enemiesSmallAtProximity = new GameObject[0];
    public GameObject[] enemiesMediumAtProximity = new GameObject[0];
    public GameObject[] enemiesLargeAtProximity = new GameObject[0];
    //Variables (number of enemies by type, boss included)
    public int nbrEnemySmall = 0;
    public int nbrEnemyMedium = 0;
    public int nbrEnemyBig = 0;
    public int nbrBoss = 0;
    //Instance
    public static EnemyDetection instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de EnemyDetection dans la scène.");
            return;
        }
        instance = this;
    }

    void FixedUpdate()
    {
        //Count enemies in the circle collider of the player
        //4 tests => Get the number of each type of enemy
        nbrEnemySmall = GameObject.FindGameObjectsWithTag("EnemyS")
            .Count(enemyObject => Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y))
        < gameObject.GetComponent<CircleCollider2D>().radius);

        nbrEnemyMedium = GameObject.FindGameObjectsWithTag("EnemyM")
            .Count(enemyObject => Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y))
        < gameObject.GetComponent<CircleCollider2D>().radius);

        nbrEnemyBig = GameObject.FindGameObjectsWithTag("EnemyL")
            .Count(enemyObject => Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y))
        < gameObject.GetComponent<CircleCollider2D>().radius);

        nbrBoss = GameObject.FindGameObjectsWithTag("Boss")
           .Count(enemyObject => Vector2.Distance(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), new Vector2(enemyObject.transform.position.x, enemyObject.transform.position.y))
       < gameObject.GetComponent<CircleCollider2D>().radius);

        //Fill arrays with the enmies detected in the player's detection area
        enemiesSmallAtProximity = GameObject.FindGameObjectsWithTag("EnemyS");
        enemiesMediumAtProximity = GameObject.FindGameObjectsWithTag("EnemyM");
        enemiesLargeAtProximity = GameObject.FindGameObjectsWithTag("EnemyL");

    }

}
