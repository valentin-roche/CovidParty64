public class SecondPeriodState : StateMachine.State
{
    //On enter give full healthbar to the boss
    public override void Enter()
    {
        base.Enter();
        Stats.BossStat.Life = Stats.BossStat.MaxHP;
    }

    public override void Update()
    {
        base.Update();
    }
    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}