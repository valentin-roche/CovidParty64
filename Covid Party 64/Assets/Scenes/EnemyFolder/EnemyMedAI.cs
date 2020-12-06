﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using System;

public class EnemyMedAI : MonoBehaviour
{
    public SoundManagerScript SoundManager;
    public Transform target;

    public int speed;
    public float nextWaypointDistance = 3f;
    public int life;

    private bool isGrounded;
    private float groundCheckRadius;
    private LayerMask collisionLayer;
    public Transform groundCheck;
    Transform enemyGFX;

    public Animator animator;
   
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //speed = EnemyStatMedium.Speed;
        target = GameObject.Find("Player").transform;
        enemyGFX = this.transform;
        life = Stats.EnemyStatMedium.Life;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        groundCheckRadius = 0.25f;
        collisionLayer = LayerMask.GetMask("Foundation");
    }

    void UpdatePath()
    {
        if(seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path p)
    {
        if(!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        speed = Stats.EnemyStatMedium.Speed;

        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        if(isGrounded == true)
            animator.SetBool("isJumping", false);

        if (path == null)
            return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;

        try
        {
            SoundManager.PlayHitSound();
        } 
        catch(Exception e)
        {
            Debug.LogError(e.Message);

        }
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Jump":
                if (path.vectorPath[currentWaypoint].y < path.vectorPath[currentWaypoint + 1].y && isGrounded)
                {
                    rb.AddForce(Vector2.up * 300f);
                    animator.SetBool("isJumping", true);
                }                
                break;

            case "JumpHole":
                if(path.vectorPath[currentWaypoint].y == path.vectorPath[currentWaypoint + 1].y && isGrounded)
                {
                    rb.AddForce(Vector2.up * 150f);
                    animator.SetBool("isJumping", true);
                }             
                break;

            case "Enemy":
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), col.GetComponent<BoxCollider2D>());
                break;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), collision.gameObject.GetComponentInChildren<CapsuleCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), collision.gameObject.GetComponent<CircleCollider2D>());
        }
    }
}