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

    private StateMachine.StateMachine stateMachine;
    private bool hasRevived = false;
    public int life;

    public Transform target;

    public int speed;
    public float nextWaypointDistance = 3f;

    public Transform enemyGFX;

    Pathfinding.Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Pathfinding.Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        normal = new NormalState();
        faster = new FasterState();
        stronger = new StrongerState();
        berzerk = new BerzerkState();
        secondPeriod = new SecondPeriodState();
        speed = Stats.BossStat.Speed;
        target = GameObject.Find("Player").transform;
        life = Stats.BossStat.MaxHP;

        seeker = GetComponent<Pathfinding.Seeker>();
        rb = GetComponent<Rigidbody2D>();

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

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Pathfinding.Path p)
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
            Debug.Log("ded");
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
}
