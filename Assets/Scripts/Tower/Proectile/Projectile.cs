using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected float damage;
    public bool HasEffectDamage { get; set; }
    protected string effectName;
    protected int current_tier;
    Rigidbody2D rigid;
    protected int penetrationCount = 0;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        HasEffectDamage = false;
    }
    private void OnEnable()
    {
        StartCoroutine(CoroutineUtility.SetActiveFalse(gameObject, 3f));
    }
    public void Init(float damage, string effectName, int current_tier)
    {
        this.damage = damage;
        this.effectName = effectName;
        this.current_tier = current_tier;
    }

    public void Shoot(Vector2 direction, float projectileSpeed = 5, int penetrationCount = 0)
    {
        rigid.velocity = direction.normalized * projectileSpeed;
        rigid.transform.up = direction;
        this.penetrationCount = penetrationCount;
    }
    public void Shoot(Vector2 direction, float projectileSpeed)
    {
        rigid.velocity = direction.normalized * projectileSpeed;
        rigid.transform.right = direction;
    }
    public float GetDamage()
    {
        if (penetrationCount > 0)
        {
            penetrationCount--;

            if (penetrationCount <= 0)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameObject.SetActive(false);
        }

        return HasEffectDamage ? 0 : damage;
    }
}