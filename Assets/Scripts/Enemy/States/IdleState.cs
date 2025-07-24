using UnityEngine;

public class IdleState<T> : BaseState<T> where T : UnitState<T>
{
    public IdleState(UnitDataSO unitData) : base(unitData)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState(T owner)
    {

        owner.TransitionToState(EUnit.Move);
    }
}
