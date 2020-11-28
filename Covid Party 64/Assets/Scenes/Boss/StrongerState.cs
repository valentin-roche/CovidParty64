public class StrongerState : StateMachine.State
{
    // Stronger state so we increase damage on enter
    public override void Enter()
    {
        base.Enter();
        Stats.BossStat.Damage = Stats.BossStat.Damage + (Stats.BossStat.Damage / 4);
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