using UnityEngine;
public class DieState<T> : BaseState<T> where T : UnitState<T>
{
    float timer;
    public DieState(UnitDataSO unitData) : base(unitData)
    {

    }

    public override void EnterState()
    {
    }

    public override void UpdateState(T owner)
    {
  
        owner.CircleCollider.enabled = false;
        owner.gameObject.layer = LayerMask.NameToLayer("Default");

        owner.Animator.SetTrigger("Death");
        owner.Rigid.velocity = Vector2.zero;
        timer += Time.deltaTime;
        if (timer > 1f)
        {
            if (Random.value < 0.1f)
            {
                MinerManager.Gold += 10f;
            }
            owner.Die?.Invoke(owner.UnitData.EXP);
            owner.gameObject.SetActive(false);
        }
    }
}