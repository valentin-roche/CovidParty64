using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
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
        for (int enIndex = 0; enIndex < LiveEn.Count(); enIndex++)
        {
            if (LiveEn[enIndex] == null)
            {
                Debug.Log("rm ded enemy");
                LiveEn.RemoveAt(enIndex);
            }
        }
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

        int remainingCredit = cred;
        //Calculate the number of big enemies
        int nbBig = Random.Range(1, cred / 4); // A big enemy is worth 2 credits and we want at most cred/2 big enemies
        remainingCredit = remainingCredit - (nbBig * 2);

        //Calculate the number of small enemies
        int nbSmall = (Random.Range(1, (int)cred / 3)) * 2; // A small enemy is worth 0.5 credits and we want at most cred/3 small enemies (always spaw in pairs)
        remainingCredit = remainingCredit - (nbSmall / 2);

        //What is left is the credit alloted for medium enemies
        int nbMed = remainingCredit;

        //Hashtable counting the number of ennemies left to spawn for each category
        Hashtable remainingByCat = new Hashtable();
        remainingByCat.Add("small", nbSmall);
        remainingByCat.Add("medium", nbMed);
        remainingByCat.Add("big", nbBig);

        // Radom category's index in the remaining categories list
        int rCat;

        // Variable containing the valid category indexes
        List<int> remainingCategories = new List<int>(new int[] { 0, 1, 2 });

        while (remainingCategories.Count() > 0)
        {
            //Choose a random category based on the length of the hashtable
            rCat = Random.Range(0, remainingCategories.Count());

            switch (remainingCategories[rCat])
            {
                // Small enemy
                case 0 :
                    GameObject enemySmall = Instantiate(prefabEnemySmall, SpawnPoint.transform);
                    LiveEn.Add(enemySmall);
                    GameObject enemySmall2 = Instantiate(prefabEnemySmall, SpawnPoint.transform);
                    LiveEn.Add(enemySmall2);
                    remainingByCat["small"] = (int)remainingByCat["small"] - 2;
                    if ((int)remainingByCat["small"] == 0)
                    {
                        remainingCategories.Remove(0);
                    }
                    break;
                // Medium enemy
                case 1 :
                    GameObject enemyMed = Instantiate(prefabEnemyMedium, SpawnPoint.transform);
                    LiveEn.Add(enemyMed);
                    remainingByCat["medium"] = (int)remainingByCat["medium"] - 1;
                    if ((int)remainingByCat["medium"] == 0)
                    {
                        remainingCategories.Remove(1);
                    }
                    break;
                // Big enemy
                case 2 :
                    GameObject enemyBig = Instantiate(prefabEnemyLarge, SpawnPoint.transform);
                    LiveEn.Add(enemyBig);
                    remainingByCat["big"] = (int)remainingByCat["big"] - 1;
                    if ((int)remainingByCat["big"] == 0)
                    {
                        remainingCategories.Remove(2);
                    }
                    break;
            }
        }

        SpawnSide += 1;
    }

    public void SpawnBoss()
    {
        // If the boss prefab is set there is a boss in the level and we make it spawn
        if (prefabBoss)
        {
            LiveEn = new List<GameObject>();
            GameObject boss = Instantiate(prefabBoss, BossSpawn.transform);
            LiveEn.Add(boss);
        }
        // If there is no prefab set the boss will not spawn and we clear the LiveEn list to make sure the next step happens
        else
        {
            LiveEn.Clear();
        }
    }
}
