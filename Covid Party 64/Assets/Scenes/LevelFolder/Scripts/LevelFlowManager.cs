﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelFlowManager : MonoBehaviour
{
    public List<Wave> Waves = new List<Wave>();
    private List<GameObject> liveEnemies;
    private EnemySpawner spawnerScript;
    private int currentWave = 0;
    private bool BossFight = false;
    public bool NextUpgradeIsPlayerUpgrade = false;
    public LevelSoundManager SoundManager;


    // Start is called before the first frame update
    void Start()
    {
        GameObject spawner = GameObject.Find("EnemySpawner");
        spawnerScript = spawner.GetComponent<EnemySpawner>();
        SoundManager.PlayRegular();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1) && Stats.PlayerStat.PlayerInventory["BottleGel"] > 0)
        {
            PlayerStatsHandler.instance.useItem("BottleGel");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Stats.PlayerStat.PlayerInventory["Radio"] > 0)
        {
            PlayerStatsHandler.instance.useItem("Radio");
            applyRadioEffect();
        }

        // Debug.Log("remaining ennemies " + spawnerScript.LiveEn.Count());
        if (spawnerScript.LiveEn.Count() <= 0 && !BossFight && currentWave < Waves.Count())
        {
            Debug.Log("Spawning wave no " + currentWave);
            spawnerScript.MakeWave(Waves[currentWave].Credit);
            currentWave++;
        }
        if(spawnerScript.LiveEn.Count() <= 0 && currentWave <= Waves.Count() && !BossFight)
        {
            BossFight = true;
            Debug.Log("Spawning boss");
            spawBoss();
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

            Debug.Log("Loading couple selection");
            SceneManager.LoadScene("CoupleSelection");
        }
    }


    private void spawBoss()
    {
        spawnerScript.SpawnBoss();
        SoundManager.PlayBoss();
    }

    private void applyRadioEffect()
    {
        Debug.Log("Applying Flash Effect");
        PlayFlashEffect.instance.FlashEffect();

        foreach (GameObject enemy in spawnerScript.LiveEn)
        {
            if (enemy.tag == "EnemyS")
            {
                Debug.Log("Applying damage to S");
                enemy.GetComponent<EnemySmallAI>().TakeDamage(75);
            }
            else if (enemy.tag == "EnemyM")
            {
                Debug.Log("Applying damage to M");
                enemy.GetComponent<EnemyMedAI>().TakeDamage(75);
            }
            else if (enemy.tag == "EnemyL")
            {
                Debug.Log("Applying damage to L");
                enemy.GetComponent<EnemyLargeAI>().TakeDamage(75);
            }
            //Objet applicable sur Boss ?
            //else if (enemy.tag == "Boss")
            //{

            //}
        }
    }
}
