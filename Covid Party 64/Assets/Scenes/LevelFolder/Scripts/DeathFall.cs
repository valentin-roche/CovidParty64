using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "EnemyS":
                col.gameObject.GetComponent<EnemySmallAI>().death();
                break;
            case "EnemyM":
                col.gameObject.GetComponent<EnemyMedAI>().death();
                break;
            case "EnemyL":
                col.gameObject.GetComponent<EnemyLargeAI>().death();
                break;
        }
    }
}