public interface IState
{
    abstract void Enter();
    abstract void Tick(float deltaTime);
    abstract void Exit();
}
