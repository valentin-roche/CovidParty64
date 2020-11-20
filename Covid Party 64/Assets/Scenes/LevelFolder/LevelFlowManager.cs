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
    public bool NextUpgradeIsPlayerUpgrade = false;


    // Start is called before the first frame update
    void Start()
    {
        GameObject spawner = GameObject.Find("EnemySpawner");
        spawnerScript = spawner.GetComponent<EnemySpawner>();

    }

    // Update is called once per frame
    void Update()
    {
        //Killed ennemies will still appear as null in the list so we have to manuallu erase them
        RemoveNull();
        Debug.Log("remaining ennemies " + spawnerScript.LiveEn.Count());
        if(spawnerScript.LiveEn.Count() <= 0 && !BossFight)
        {
            Debug.Log("Spawning wave no " + currentWave);
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
            // End of the level as soon as the boss is defeated and load couple selection, set the param to decide wether 
            // we display the play enhancement UI or the random feat 
            if (NextUpgradeIsPlayerUpgrade)
            {
                TransitionInfos.LevelTransitionInfo.IsNextLevelPlayerUpgrade = true;
            }
            else
            {
                TransitionInfos.LevelTransitionInfo.IsNextLevelPlayerUpgrade = false;
            }
            SceneManager.LoadScene("CoupleSelection");
        }
    }

    private void RemoveNull()
    {
        for (int enIndex = 0; enIndex < spawnerScript.LiveEn.Count(); enIndex++)
        {
            if (spawnerScript.LiveEn[enIndex] == null)
            {
                spawnerScript.LiveEn.RemoveAt(enIndex);
            }
        }
    }

    private void spawBoss()
    {
        spawnerScript.SpawnBoss();
    }
}
