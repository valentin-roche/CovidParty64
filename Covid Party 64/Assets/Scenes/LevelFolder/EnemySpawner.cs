using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject LeftSpawn { get; private set; }
    public GameObject RightSpawn { get; private set; }
    public GameObject BossSpawn { get; private set; }
    private int SpawnSide { get; set; }
    public GameObject prefabEnemySmall;
    public GameObject prefabEnemyMedium;
    public GameObject prefabEnemyLarge;
    public GameObject prefabBoss;
    public List<GameObject> LiveEn;

    // Start is called before the first frame update
    void Start()
    {
        LeftSpawn = transform.Find("Enemy Spawner Left").gameObject;
        RightSpawn = transform.Find("Enemy Spawner Right").gameObject;
        BossSpawn = transform.Find("Boss Spawner").gameObject;
        SpawnSide = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeWave(int cred)
    {

        Debug.Log("Spawning wave with " + cred + "cred");
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
            Debug.Log("New Enemy");
            GameObject enemy = Instantiate(prefabEnemyMedium, SpawnPoint.transform);
            
            LiveEn.Add(enemy);

            i++; //i+Valeur de cred de l'ennemi spawn
        }

        SpawnSide += 1;
    }

    public void SpawnBoss()
    {
        GameObject enemy = Instantiate(prefabBoss, BossSpawn.transform);
        LiveEn.Add(enemy);
    }
}
