using UnityEngine;

public class RangeEnemy : UnitState<RangeEnemy>
{
    public override void Awake()
    {
        base.Awake();
        states.Add(EUnit.Idle, new IdleState<RangeEnemy>(UnitData));
        states.Add(EUnit.Move, new MoveState<RangeEnemy>(UnitData));
        states.Add(EUnit.GetHit, new GetHitState<RangeEnemy>(UnitData));
        states.Add(EUnit.Attack, new AttackState<RangeEnemy>(UnitData, new EnemyRangeAttack()));
        states.Add(EUnit.Die, new DieState<RangeEnemy>(UnitData));
    }
    public override void OnEnable()
    {
        base.OnEnable();
        TransitionToState(EUnit.Idle);
    }
    private void Update()
    {
        Debug.Log("currentState : " + currentState.GetType().Name);
        currentState.UpdateState(this);
    }
}