public interface IState<T>
{
    void EnterState();
    void UpdateState(T owner);
}
