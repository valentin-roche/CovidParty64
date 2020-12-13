﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTut : MonoBehaviour
{

    public Camera cam;
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public GameObject startVFX;
    public GameObject endVFX;
    private int laserDPS = Stats.PlayerStat.LaserDPS;
    private float readyForNextDamage;

    private Quaternion rotation;
    private List<ParticleSystem> particles = new List<ParticleSystem>();

    void Start()
    {

        FillLists();
        DisableLaser();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            EnableLaser();

        }
        
        if (Input.GetMouseButton(0))
        {
            UpdateLaser();

        }

        if (Input.GetMouseButtonUp(0))
        {
            DisableLaser();

        }
    }

    void EnableLaser()
    {
        lineRenderer.enabled = true;

        for(int i = 0; i<particles.Count; i++)
        {
            particles[i].Play();
        }
    }

    void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);
        startVFX.transform.position = firePoint.position;  
         
        lineRenderer.SetPosition(1, firePoint.position + firePoint.right * 100);

        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.right);

        if (hitInfo)
        {
            lineRenderer.SetPosition(1, hitInfo.point);

            DamageEnemy(laserDPS, hitInfo);
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    void DisableLaser()
    {
        lineRenderer.enabled = false;

        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Stop();
        }
    }

    void FillLists()
    {
        for(int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }
        
        for(int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
            {
                particles.Add(ps);
            }
        }
    }

    void DamageEnemy(int DPS, RaycastHit2D target)
    {
        if (Time.time > readyForNextDamage)
        {

            if (Stats.PlayerStat.Critical)
            {
                int rand = Random.Range(0, 5); // Genere un nombre aleatoire entre 0 et 4

                if(rand == 1)
                {
                    DPS *= 2;
                }
                
            }

            if (Stats.PlayerStat.DrainAtTouch)
            {
                PlayerStatsHandler.instance.Drain();
            }

            readyForNextDamage = Time.time + 0.1f;
            Debug.Log("Damage to : " + target.transform.tag +", DPS : "+DPS);
            if (target.transform.tag == "EnemyS")
            {
                target.transform.GetComponent<EnemySmallAI>().TakeDamage(DPS);

            }
            if (target.transform.tag == "EnemyM")
            {
                target.transform.GetComponent<EnemyMedAI>().TakeDamage(DPS);
            }
            if (target.transform.tag == "EnemyL")
            {
                target.transform.GetComponent<EnemyLargeAI>().TakeDamage(DPS);
            }
            if (target.transform.name == "BossPrefab(Clone)" || target.transform.name == "BossSprite")
            {
                target.transform.GetComponent<BossAI>().TakeDamage(DPS);
            }
            if (target.transform.tag == "Boss")
            {
                if (Stats.PlayerStat.IncreasedBossDamage)
                {
                    target.transform.GetComponent<BossAI>().TakeDamage((int)(DPS * 1.5));
                }
                else
                {
                    target.transform.GetComponent<BossAI>().TakeDamage(DPS);
                }
            }
        }
    }
}
