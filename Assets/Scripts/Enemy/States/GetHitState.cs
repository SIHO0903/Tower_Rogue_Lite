using Unity.VisualScripting;
using UnityEngine;

public class GetHitState<T> : BaseState<T> where T : UnitState<T>
{
    int hitCount;
    public GetHitState(UnitDataSO unitData) : base(unitData)
    {

    }

    public override void EnterState()
    {
        hitCount = 1;
    }

    public override void UpdateState(T owner)
    {
        if (hitCount >= 1)
        {
            hitCount--;
            SoundManager.instance.PlaySound(SoundType.EnemyGetHit);
            if (owner.Health <= 0)
            {
                owner.TransitionToState(EUnit.Die);
            }
            else
                owner.TransitionToState(EUnit.Move);
        }

    }
}