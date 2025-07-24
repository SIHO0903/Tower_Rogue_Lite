public abstract class BaseState<T> : IState<T>
{
    protected UnitDataSO unitData;
    public BaseState(UnitDataSO unitData)
    {
        this.unitData = unitData;      
    }
    public abstract void EnterState();
    public abstract void UpdateState(T owner);

}
