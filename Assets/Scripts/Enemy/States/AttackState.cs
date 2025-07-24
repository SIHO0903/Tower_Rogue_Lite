using UnityEngine;

public class AttackState<T> : BaseState<T> where T : UnitState<T>
{
    float timer;
    bool hasAttacked;
    bool animationStarted;
    IAttack attackType;
    public AttackState(UnitDataSO unitData,IAttack attack) : base(unitData)
    {
        this.attackType = attack;
    }

    public override void EnterState()
    {
        timer = 0;
        hasAttacked = false;
        animationStarted = false;
    }

    public override void UpdateState(T owner)
    {
        timer += Time.deltaTime;

        if (!animationStarted)
        {
            owner.Rigid.velocity = Vector2.zero;
            owner.Animator.SetTrigger("Attack");
            SoundManager.instance.PlaySound(SoundType.EnemyAttack);
            animationStarted = true;
        }

        if (!hasAttacked && timer >= 0.8f)
        {
            //GameManager.Instance.DescreaseHealth(owner.Damage);
            attackType.Attack(owner.Damage,owner.transform.position,owner.Dir());
            hasAttacked = true;
            timer = 0;
            hasAttacked = false;
            animationStarted = false;
        }
    }
}
