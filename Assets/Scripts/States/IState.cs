using Assets.Scripts.States;
using Assets.Scripts.States.Contexts;

public interface IState
{
    public abstract IStateContext Context { get; }

    public abstract void OnEnter(IStateController sm);
    public abstract void OnUpdate();
    public abstract void OnExit();
    public abstract bool IsRunning();
}
