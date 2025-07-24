using UnityEngine;

public class EnemyRangeAttack : IAttack
{
    public void Attack(float damage,Vector2 currentPos, Vector2 dir)
    {
        GameObject projectile = PoolManager.Instance.Get(PoolEnum.EnemyProjectile, 0, currentPos, Quaternion.identity);
        if(projectile.TryGetComponent<EnemyProjectile>(out EnemyProjectile component))
        {
            component.Init(damage, string.Empty, 1);
            component.Shoot(dir,3);
        }
    }
}
