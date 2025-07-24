using UnityEngine;
public class FireTower : AttackTower
{
    public override void AttackMethod(FriendlyProjectile component, GameObject target)
    {
        component.Shoot(Vector2.up, TowerData.ProjectileSpeed);
        if (current_tier >= 2)
        {
            // 두 번째 발사체 (아래)
            GameObject downProjectile = PoolManager.Instance.Get(PoolEnum.Projectile, projectileName, headSprite.transform.position, Quaternion.identity);
            if (downProjectile.TryGetComponent<FriendlyProjectile>(out var downComp))
            {
                downComp.Init(current_Damage, projectileEffectName, current_tier);
                downComp.Shoot(Vector2.down, TowerData.ProjectileSpeed);
            }
        }
        if (current_tier == 3)
        {
            GameObject leftProjectile = PoolManager.Instance.Get(PoolEnum.Projectile, projectileName, headSprite.transform.position, Quaternion.identity);
            if (leftProjectile.TryGetComponent<FriendlyProjectile>(out var leftComp))
            {
                leftComp.Init(current_Damage, projectileEffectName, current_tier);
                leftComp.Shoot(Vector2.left, TowerData.ProjectileSpeed);
            }
            GameObject rightProjectile = PoolManager.Instance.Get(PoolEnum.Projectile, projectileName, headSprite.transform.position, Quaternion.identity);
            if (rightProjectile.TryGetComponent<FriendlyProjectile>(out var rightComp))
            {
                rightComp.Init(current_Damage, projectileEffectName, current_tier);
                rightComp.Shoot(Vector2.right, TowerData.ProjectileSpeed);
            }

        }
    }
}
