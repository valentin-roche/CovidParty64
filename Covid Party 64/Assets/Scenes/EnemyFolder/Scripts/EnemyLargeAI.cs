using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyLargeAI : MonoBehaviour
{
    public SoundManagerScript SoundManager;
    public Transform target;

    public int speed;
    public float nextWaypointDistance = 3f;
    public int life;
    public int armor;
    private int maxLife;
    private int dropChance;

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

    public GameObject gelBottle;
    public GameObject mask;
    public GameObject radio;

    // Initialisation des composants
    void Start()
    {
        target = GameObject.Find("Player").transform;
        enemyGFX = this.transform;
        armor = Stats.EnemyStatLarge.Armor;
        spit = Stats.EnemyStatLarge.Spit;
        dodge = Stats.EnemyStatLarge.Dodge;
        block = Stats.EnemyStatLarge.Block;
        critical = Stats.EnemyStatLarge.Critical;
        slow = Stats.EnemyStatLarge.Slow;
        fly = Stats.EnemyStatLarge.Fly;
        regen = Stats.EnemyStatLarge.Regen;
        maxLife = Stats.EnemyStatLarge.Life;
        dropChance = Stats.EnemyStatMedium.DropChance;
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
        speed = Stats.EnemyStatLarge.Speed;
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
        if (rb.velocity.x <= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (rb.velocity.x >= -0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }

        //Changement de couleur en fonction des hp
        changeColor();
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
        Debug.Log("vie : " + life);
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
        if (collision.gameObject.tag == "EnemyS" || collision.gameObject.tag == "EnemyM" || collision.gameObject.tag == "EnemyL")
        {
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), collision.gameObject.GetComponent<BoxCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), collision.gameObject.GetComponentInChildren<CapsuleCollider2D>());
            Physics2D.IgnoreCollision(GetComponent<CapsuleCollider2D>(), collision.gameObject.GetComponent<CircleCollider2D>());
        }
    }

    //Changement de couleur en fonction de la vie
    private void changeColor()
    {
        Renderer rend = GetComponent<Renderer>(); ;

        // Changer la couleur en fonction des hp
        if (life <= maxLife && life > (maxLife * 0.75))
        {
            rend.material.color = new Color((255f / 255f), (255f / 255f), (255f / 255f), (255f / 255f));
        }

        if (life <= (maxLife * 0.75) && life > (maxLife * 0.5))
        {
            rend.material.color = new Color((255f / 255f), (213f / 255f), (213f / 255f), (255f / 255f));
        }

        if (life <= (maxLife * 0.5) && life > (maxLife * 0.25))
        {
            rend.material.color = new Color((255f / 255f), (191f / 255f), (191f / 255f), (255f / 255f));
        }

        if (life <= (maxLife * 0.25) && life > 0)
        {
            rend.material.color = new Color((255f / 255f), (173f / 255f), (173f / 255f), (255f / 255f));
        }
    }

    //Fonction de mort
    public void death()
    {
        int chance = Random.Range(1, 101);
        int choice;

        Destroy(gameObject);
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        spawner.CleanList();

        if (chance <= dropChance)
        {
            choice = Random.Range(1, 4);
            switch (choice)
            {
                case 1:
                    Instantiate(gelBottle, transform.position, transform.rotation);
                    break;

                case 2:
                    Instantiate(mask, transform.position, transform.rotation);
                    break;

                case 3:
                    Instantiate(radio, transform.position, transform.rotation);
                    break;
            }
        }
    }
}
