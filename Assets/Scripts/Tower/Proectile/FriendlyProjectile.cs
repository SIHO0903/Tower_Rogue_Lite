using System.Collections;
using UnityEngine;

public class FriendlyProjectile : Projectile
{
    private void OnDisable()
    {
        GameObject Effect = PoolManager.Instance.Get(PoolEnum.Projectile_Effect, effectName, transform.position, transform.rotation);
        ProjectileEffect projectileEffect = Effect.GetComponent<ProjectileEffect>();
        if (HasEffectDamage)
        {
            projectileEffect.SetDamage(damage);
            projectileEffect.AOE(current_tier,effectName + "_Effect");
        }
    }
}
