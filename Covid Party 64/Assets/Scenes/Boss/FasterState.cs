public class FasterState : StateMachine.State
{
    //Faster state so we increase the speed on enter
    public override void Enter()
    {
        base.Enter();
        Stats.BossStat.Speed = Stats.BossStat.Speed + (Stats.BossStat.Speed/4);
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