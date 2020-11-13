using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    // Start is called before the first frame update
    public int nbrEnemy = 0;

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


    // Update is called once per frame
    void Update()
    {
        nbrEnemy = GameObject.FindGameObjectsWithTag("Enemy")
            .Count(enemyObject => Vector3.Distance(gameObject.transform.position, enemyObject.transform.position) < gameObject.GetComponent<CircleCollider2D>().radius);
    }

}
