public class NormalState:StateMachine.State
{
    //Standard state so we dont have to modify anything (more of a semantic state)
    public override void Enter()
    {
        base.Enter();
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