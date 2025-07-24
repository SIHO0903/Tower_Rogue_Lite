using UnityEngine;

public class PenetrationTower : AttackTower
{

    public override void AttackMethod(FriendlyProjectile component, GameObject target)
    {
        Vector2 dir = target.transform.position - transform.position;
        component.Shoot(dir, TowerData.ProjectileSpeed,2);
    }
}