using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour
{

    #region States
    public NormalState normal = new NormalState();
    public FasterState faster = new FasterState();
    public StrongerState stronger = new StrongerState();
    public BerzerkState berzerk = new BerzerkState();
    public SecondPeriodState secondPeriod = new SecondPeriodState();
    #endregion

    // States variables
    private StateMachine.StateMachine stateMachine;
    private bool hasRevived = false;
    
    // Stats variables
    public int life;
    public int speed;

    // Audio variable
    public BossSoundManager SoundManager;

    // AI variables
    public Transform target;
    public float nextWaypointDistance = 3f;
    public Transform enemyGFX;
    private bool isGrounded;
    private float groundCheckRadius;
    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    private LayerMask collisionLayer;
    Pathfinding.Seeker seeker;
    Rigidbody2D rb;
    public Transform groundCheck;

    // Animator variable
    //public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        // States init
        normal = new NormalState();
        faster = new FasterState();
        stronger = new StrongerState();
        berzerk = new BerzerkState();
        secondPeriod = new SecondPeriodState();

        // Stats init
        speed = Stats.BossStat.Speed;
        life = Stats.BossStat.MaxHP;

        // AI init
        target = GameObject.Find("Player").transform;
        seeker = GetComponent<Pathfinding.Seeker>();
        rb = GetComponent<Rigidbody2D>(); 
        groundCheckRadius = 0.25f;
        collisionLayer = LayerMask.GetMask("Foundation");
        enemyGFX = this.transform;

        InvokeRepeating("UpdatePath", 0f, 0.5f);

        // As the boss stats are updated widely during the fight we reset them at the begining
        //Stats.BossStat.ResetStat();
        // The first two bosses appear in their normal state
        if (Stats.BossStat.Level < 2)
        {
            stateMachine.Initialize(normal);
        }
        // All the other bosses do more damage on start
        else
        {
            stateMachine.Initialize(stronger);
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

    // Update is called once per frame
    void Update()
    {

        speed = Stats.BossStat.Speed;

        if (life <= 0)
        {
            Debug.Log("Boss died");

            // TODO Play death animation

            Destroy(gameObject);
        }

        // Call state special update
        //stateMachine.CurrentState.Update();

        // If level 2 boss and up and half HP => speedup
        if (Stats.BossStat.Level >= 2 && life == Stats.BossStat.MaxHP / 2 && !hasRevived)
        {
            stateMachine.ChangeState(faster);
        }

        // If level 4 boss and 1/4 HP => dmg up
        if (Stats.BossStat.Level >= 4 && life == Stats.BossStat.MaxHP / 4 && !hasRevived)
        {
            stateMachine.ChangeState(stronger);
        }

        // If level 8 boss and 1/8 HP => berzerk mode
        if (Stats.BossStat.Level >= 8 && life == Stats.BossStat.MaxHP / 8 && !hasRevived)
        {
            stateMachine.ChangeState(berzerk);
        }

        // If level 10 boss and 1 hp =>  second period (revive) without applying the modifier twice
        if (Stats.BossStat.Level == 10 && life == 1 && !hasRevived)
        {
            stateMachine.ChangeState(secondPeriod);
        }
    }

    void FixedUpdate()
    {
        //Vérification de collision avec le sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, collisionLayer);

        /*if (isGrounded == true)
            animator.SetBool("isJumping", false);
        */
        
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

    public void TakeDamage(int damage)
    {

        Debug.Log("Remaining HP : " + life);
        life -= damage;
    }

    //Gestion des différentes collisions
    // TODO unomment when boss animator is done
    public void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Jump":
                if (path.vectorPath[currentWaypoint].y < path.vectorPath[currentWaypoint + 1].y && isGrounded)
                {
                    rb.AddForce(Vector2.up * 300f);
                    // animator.SetBool("isJumping", true);
                }
                break;

            case "JumpHole":
                if (path.vectorPath[currentWaypoint].y == path.vectorPath[currentWaypoint + 1].y && isGrounded)
                {
                    rb.AddForce(Vector2.up * 150f);
                    // animator.SetBool("isJumping", true);
                }
                break;
        }
    }
}
