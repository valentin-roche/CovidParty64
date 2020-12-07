using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemySmallAI : MonoBehaviour
{
    public SoundManagerScript SoundManager;
    public Transform target;

    public int speed;
    public float nextWaypointDistance = 3f;
    public int life;
    public int armor;
    private int maxLife;

    private bool
       spit,
       dodge,
       block,
       critical,
       slow,
       fly,
       regen;

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

    // Initialisation des composants
    void Start()
    {
        target = GameObject.Find("Player").transform;
        enemyGFX = this.transform;
        armor = Stats.EnemyStatSmall.Armor;
        spit = Stats.EnemyStatSmall.Spit;
        dodge = Stats.EnemyStatSmall.Dodge;
        block = Stats.EnemyStatSmall.Block;
        critical = Stats.EnemyStatSmall.Critical;
        slow = Stats.EnemyStatSmall.Slow;
        fly = Stats.EnemyStatSmall.Fly;
        regen = Stats.EnemyStatSmall.Regen;
        maxLife = Stats.EnemyStatSmall.Life;
        life = maxLife;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        groundCheckRadius = 0.25f;
        collisionLayer = LayerMask.GetMask("Foundation");

        //Régénération
        if (regen == true)
        {
            InvokeRepeating("Regen", 1.0f, 1.0f);
        }
    }

    //Mise à jour du chemin
    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    //La fin du chemin est atteinte
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        speed = Stats.EnemyStatSmall.Speed;
        if (life <= 0)
        {
            death();
        }
    }


    void FixedUpdate()
    {
        //Vérification de collision avec le sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        if (isGrounded == true)
            animator.SetBool("isJumping", false);

        //Empecher l'accéleration
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -speed, speed), Mathf.Clamp(rb.velocity.y, -speed, speed));

        //Gestion de l'AI
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
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

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        //Changement d'orientation du sprite
        if (rb.velocity.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    //Fonction de dommages
    public void TakeDamage(int damage)
    {
        //L'ennemi à 1/4 chance de bloquer
        if (block == true)
        {
            int rand1 = Random.Range(0, 5);
            if (rand1 == 1)
            {
                damage = damage / 2;
            }
        }

        //L'ennemi à 1/6 chance d'esquiver
        if (dodge == true)
        {
            int rand2 = Random.Range(0, 7);
            if (rand2 == 1)
            {
                damage = 0;
            }
        }

        SoundManager.PlayHitSound();
        life = life - (damage * 100) / armor;
    }

    //Régénération
    private void Regen()
    {
        //L'ennemi régénère 10 PV par secondes
        if (life <= (maxLife - 10) && life > 0)
        {
            life += 10;

        }
    }

    //Gestion des différentes collisions
    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Jump":
                if (currentWaypoint + 1 <= path.vectorPath.Count)
                {
                    if (path.vectorPath[currentWaypoint].y < path.vectorPath[currentWaypoint + 1].y && isGrounded)
                    {
                        rb.AddForce(Vector2.up * 300f);
                        animator.SetBool("isJumping", true);
                    }
                }
                break;

            case "JumpHole":
                if (currentWaypoint + 1 <= path.vectorPath.Count)
                {
                    if (path.vectorPath[currentWaypoint].y == path.vectorPath[currentWaypoint + 1].y && isGrounded)
                    {
                        rb.AddForce(Vector2.up * 150f);
                        animator.SetBool("isJumping", true);
                    }
                }
                break;

            case "JumpHigh":
                if (currentWaypoint + 1 <= path.vectorPath.Count)
                {
                    if (path.vectorPath[currentWaypoint].y < path.vectorPath[currentWaypoint + 1].y && isGrounded)
                    {
                        rb.AddForce(Vector2.up * 500f);
                        animator.SetBool("isJumping", true);
                    }
                }
                break;

            case "EnemyS":
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), col.GetComponent<BoxCollider2D>());
                break;

            case "EnemyM":
                Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), col.GetComponent<BoxCollider2D>());
                break;

            case "EnemyL":
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

    //Fonction de mort
    public void death()
    {
        Destroy(gameObject);
    }
}
