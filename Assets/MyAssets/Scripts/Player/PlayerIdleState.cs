using UnityEngine;

public class PlayerIdleState<T> : BaseState<T> where T : UnitState<T>
{

    public PlayerIdleState(UnitData unitData) : base(unitData)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Enter Idle State");
    }

    public override void UpdateState(T owner)
    {
        //이동, 공격, 피격

        //이동감지
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (input != Vector2.zero)
        {
            owner.TransitionToState(EUnit.Move);
        }
    }
}
