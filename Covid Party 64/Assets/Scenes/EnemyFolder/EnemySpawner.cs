using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject LeftSpawn { get; private set; }
    public GameObject RightSpawn { get; private set; }
    private int SpawnSide { get; set; }
    public GameObject prefabEnemySmall;
    public GameObject prefabEnemyMedium;
    public GameObject prefabEnemyLarge;

    // Start is called before the first frame update
    void Start()
    {
        LeftSpawn = transform.Find("Enemy Spawner Left").gameObject;
        RightSpawn = transform.Find("Enemy Spawner Right").gameObject;
        SpawnSide = 0;
        MakeWave(1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void MakeWave(int cred)
    {
        GameObject SpawnPoint;

        if(SpawnSide%2==0)
        {
            SpawnPoint = LeftSpawn;
        }
        else
        {
            SpawnPoint = RightSpawn;
        }

        for (int i = 0; i < cred;)
        {
            Debug.Log("Passé ici");
            Instantiate(prefabEnemyMedium, SpawnPoint.transform);
            Debug.Log("Passé la");
            i++; //i+Valeur de cred de l'ennemi spawn
        }

        SpawnSide += 1;
    }
}
