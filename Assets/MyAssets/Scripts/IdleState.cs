using UnityEngine;

public class IdleState<T> : BaseState<T> where T : UnitState<T>
{
    public IdleState(UnitData unitData) : base(unitData)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState(T owner)
    {
        //이동, 공격, 피격

    }
}
