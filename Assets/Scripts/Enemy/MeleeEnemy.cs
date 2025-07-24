using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : UnitState<MeleeEnemy>
{
    public override void Awake()
    {
        base.Awake();
        states.Add(EUnit.Idle, new IdleState<MeleeEnemy>(UnitData));
        states.Add(EUnit.Move, new MoveState<MeleeEnemy>(UnitData));
        states.Add(EUnit.GetHit, new GetHitState<MeleeEnemy>(UnitData));
        states.Add(EUnit.Attack, new AttackState<MeleeEnemy>(UnitData,new EnemyMeleeAttack()));
        states.Add(EUnit.Die, new DieState<MeleeEnemy>(UnitData));
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
