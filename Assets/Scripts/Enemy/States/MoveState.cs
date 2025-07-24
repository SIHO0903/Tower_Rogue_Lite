using UnityEngine;

public class MoveState<T> : BaseState<T> where T : UnitState<T>
{
    
    public MoveState(UnitDataSO unitData) : base(unitData)
    {

    }

    public override void EnterState()
    {

    }

    public override void UpdateState(T owner)
    {
        owner.Rigid.velocity = owner.Dir() * unitData.MoveSpeed * Time.deltaTime;
        owner.Animator.SetFloat("Walk", owner.Rigid.velocity.magnitude);
        if (owner.Distance() < unitData.AttackRange)
        {
            owner.Rigid.velocity = Vector2.zero;
            owner.Animator.SetFloat("Walk", 0f);
            owner.TransitionToState(EUnit.Attack);
        }
    }
}