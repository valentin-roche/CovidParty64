public class BerzerkState : StateMachine.State
{
    // Berzerk state so we increase the damage and the speed
    public override void Enter()
    {
        base.Enter();
        Stats.BossStat.Speed = Stats.BossStat.Speed + (Stats.BossStat.Speed / 4);
        Stats.BossStat.Damage = Stats.BossStat.Damage + (Stats.BossStat.Damage / 4);
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