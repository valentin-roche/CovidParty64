using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StateMachine;



public class BossState : MonoBehaviour
{
    #region States
    public NormalState normal;
    public FasterState faster;
    public StrongerState stronger;
    public BerzerkState berzerk;
    public SecondPeriodState secondPeriod;
    #endregion

    private StateMachine.StateMachine stateMachine;
    private bool hasRevived = false;

    // Start is called before the first frame update
    void Start()
    {
        // As the boss stats are updated widely during the fight we reset them at the begining
        Stats.BossStat.ResetStat();
        // The first two bosses appear in their normal state
        if (Stats.BossStat.Level  < 2)
        {
            stateMachine.Initialize(normal);
        }
        // All the other bosses do more damage on start
        else
        {
            stateMachine.Initialize(stronger);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        // Call state special update
        stateMachine.CurrentState.Update();

        // If level 2 boss and up and half HP => speedup
        if (Stats.BossStat.Level >= 2 && Stats.BossStat.Life == Stats.BossStat.MaxHP/2 && !hasRevived)
        {
            stateMachine.ChangeState(faster);
        }

        // If level 4 boss and 1/4 HP => dmg up
        if (Stats.BossStat.Level >= 4 && Stats.BossStat.Life == Stats.BossStat.MaxHP / 4 && !hasRevived)
        {
            stateMachine.ChangeState(stronger);
        }

        // If level 8 boss and 1/8 HP => berzerk mode
        if (Stats.BossStat.Level >= 8 && Stats.BossStat.Life == Stats.BossStat.MaxHP / 8 && !hasRevived) 
        {
            stateMachine.ChangeState(berzerk);
        }

        // If level 10 boss and 1 hp =>  second period (revive) without applying the modifier twice
        if (Stats.BossStat.Level == 10 && Stats.BossStat.Life == 1)
        {
            stateMachine.ChangeState(secondPeriod);
        }

    }

    void FixedUpdate()
    {
        // Call state special physics update
        stateMachine.CurrentState.PhysicsUpdate();
    }
}
