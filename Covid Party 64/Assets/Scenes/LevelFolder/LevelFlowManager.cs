using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFlowManager : MonoBehaviour
{
    public List<Wave> Waves = new List<Wave>();
    public GameObject Boss;
    private List<GameObject> liveEnemies;
    private EnemySpawner spawnerScript;
    private int currentWave = 0;
    private bool BossFight = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject spawner = GameObject.Find("Enemy Spawner");
        spawnerScript = spawner.GetComponent<EnemySpawner>();

    }

    // Update is called once per frame
    void Update()
    {
        if(spawnerScript.LiveEn.Count() <=0 && !BossFight)
        {
            spawnerScript.MakeWave(Waves[currentWave].Credit);
            currentWave++;
        }
        if(currentWave == Waves.Count() && !BossFight)
        {
            spawBoss();
            BossFight = true;
        }
        if (spawnerScript.LiveEn.Count() <= 0 && BossFight)
        {
            // End of the level as soon as the boss is defeated and load couple selection
            SceneManager.LoadScene("CoupleSelection");
        }
    }

    private void spawBoss()
    {
        spawnerScript.SpawnBoss();
    }
}
