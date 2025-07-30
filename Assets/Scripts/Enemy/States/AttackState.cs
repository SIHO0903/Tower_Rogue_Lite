using UnityEngine;

public class AttackState<T> : BaseState<T> where T : UnitState<T>
{
    float timer=0;
    bool hasAttacked;
    bool animationStarted;
    IAttack attackType;
    public AttackState(UnitDataSO unitData,IAttack attack) : base(unitData)
    {
        this.attackType = attack;
    }

    public override void EnterState()
    {
        hasAttacked = false;
        animationStarted = false;
    }

    public override void UpdateState(T owner)
    {
        timer += Time.deltaTime;

        if (!animationStarted)
        {
            owner.Rigid.velocity = Vector2.zero;

            animationStarted = true;
        }

        //공격속도
        if (!hasAttacked && timer >= 1f)
        {
            owner.Animator.SetTrigger("Attack");
            SoundManager.instance.PlaySound(SoundType.EnemyAttack);
            attackType.Attack(owner.Damage,owner.transform.position,owner.Dir());
            hasAttacked = true;
            timer = 0;
            hasAttacked = false;
            //animationStarted = false;
        }
    }
}
