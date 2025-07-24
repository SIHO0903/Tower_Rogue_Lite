using UnityEngine;

public class EnemyMeleeAttack :IAttack
{
    public void Attack(float damage, Vector2 currentPos, Vector2 dir)
    {
        GameManager.Instance.DescreaseHealth(damage);
    }
}
