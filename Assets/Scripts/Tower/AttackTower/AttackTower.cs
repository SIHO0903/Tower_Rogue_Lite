using System.Text;
using UnityEngine;


public abstract class AttackTower : BaseTower
{
    [Header("Tower Data")]
    protected int current_Level=1;
    protected int current_tier;
    protected float current_Damage;
    protected float current_AttackSpeed;
    protected float current_Range;
    string current_Projectile_Name;
    string current_Projectile_Effect_Name;
    float attackTimer = 0f;
    string baseName;
    protected string projectileName;
    protected string projectileEffectName;
    public GridTxtInfo gridTxtInfo;

    public bool IsHeadRotate;

    GameObject currentTarget = null;


    void Start()
    {
        UpdateVisuals();
    }
    public void LevelChanged(int level)
    {
        current_Level = level+1;
        UpdateStats();

        UpdateVisuals();

        baseName = gameObject.name.Replace(" Tower(Clone)", "");
        projectileName = baseName + "_Lv" + current_tier;
        projectileEffectName = baseName + "_Effect";
    }

    void UpdateStats()
    {
        current_Damage = TowerData.Damage + current_Level * TowerData.Growth_Damage;
        current_AttackSpeed = TowerData.AttackSpeed + current_Level * TowerData.Growth_AttackSpeed;
        current_Range = TowerData.Range + current_Level * TowerData.Growth_Range;
        gridTxtInfo = new GridTxtInfo(transform.position, current_Level, TowerData.MaxLevel);
    }

    public void UpgradeChanged()
    {
        current_Damage = TowerData.Damage + current_Level * TowerData.Growth_Damage + UpgradeData.TowerAttackDamage;
        current_AttackSpeed = TowerData.AttackSpeed + current_Level * TowerData.Growth_AttackSpeed + UpgradeData.TowerAttackSpeed;
    }
    void UpdateVisuals()
    {
        switch (current_Level)
        {
            case 1:
            case 2:
            case 3:
                headSprite.sprite = TowerSpriteData.Head_Lv1;
                headSprite.transform.localPosition = TowerSpriteData.HeadPos_Lv1;
                animator.runtimeAnimatorController = TowerSpriteData.HeadAnimator_Lv1;
                bodySprite.sprite = TowerSpriteData.Body_Lv1;
                current_tier = 1;
                break;
            case 4:
            case 5:
            case 6:
                headSprite.sprite = TowerSpriteData.Head_Lv2;
                headSprite.transform.localPosition = TowerSpriteData.HeadPos_Lv2;
                animator.runtimeAnimatorController = TowerSpriteData.HeadAnimator_Lv2;
                bodySprite.sprite = TowerSpriteData.Body_Lv2;
                current_tier = 2;
                break;
            case 7:
            case 8:
            case 9:
                headSprite.sprite = TowerSpriteData.Head_Lv3;
                headSprite.transform.localPosition = TowerSpriteData.HeadPos_Lv3;
                animator.runtimeAnimatorController = TowerSpriteData.HeadAnimator_Lv3;
                bodySprite.sprite = TowerSpriteData.Body_Lv3;
                current_tier = 3;
                break;
        }
    }

    void AttackTimer()
    {
        if (current_Level <= 0)
            return;

        if (attackTimer < current_AttackSpeed)
        {
            attackTimer += Time.deltaTime;
        }
        else
        {
            SearchNearestTarget();
        }
    }

    public void Attack(GameObject target)
    {
        animator.SetTrigger("Attack");
        SoundManager.instance.PlaySound(SoundType.Attack);
        attackTimer = 0;
        HeadLookAt(target);
        GameObject projectile = PoolManager.Instance.Get(PoolEnum.Projectile, projectileName, headSprite.transform.position, Quaternion.identity);
        if (projectile.TryGetComponent<FriendlyProjectile>(out FriendlyProjectile component))
        {
            component.Init(current_Damage, projectileEffectName, current_tier);
            AttackMethod(component, target);
        }
    }
    void SearchNearestTarget()
    {
        if (currentTarget != null && currentTarget.activeSelf)
        {
            float distance = Vector2.Distance(transform.position, currentTarget.transform.position);
            if (distance <= current_Range)
            {
                Attack(currentTarget);
                return;
            }
        }
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, current_Range, LayerMask.GetMask("Enemy"));
        if (targets.Length > 0)
        {
            float minDistance = float.MaxValue;
            GameObject nearest = null;

            foreach (var target in targets)
            {
                float dist = Vector2.Distance(transform.position, target.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    nearest = target.gameObject;
                }
            }
            if (nearest != null)
            {
                currentTarget = nearest;
                Attack(currentTarget);
            }
        }
    }
    void HeadLookAt(GameObject target)
    {
        if (IsHeadRotate)
        {
            Vector2 dir = target.transform.position - headSprite.transform.position;
            headSprite.transform.up = dir;
        }
    }
    public override string GetDescription()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"공격력 : {TowerData.Damage}");
        sb.AppendLine($"공격 속도 : {TowerData.AttackSpeed}");
        sb.AppendLine($"사거리 : {TowerData.Range}");
        return sb.ToString();
    }

    public abstract void AttackMethod(FriendlyProjectile component, GameObject target);
    void Update() => AttackTimer();
}
