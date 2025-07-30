using System.Collections;
using UnityEngine;

public class ProjectileEffect : MonoBehaviour
{
    float timer;
    float AOERange=0.5f;
    float damage;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {

        timer = 0.5f;
        StartCoroutine(CoroutineUtility.SetActiveFalse(gameObject, timer));
    }
    public void AOE(int current_tier,string effectName)
    {
        animator.SetTrigger("Lv" + current_tier);

        Collider2D[] target = Physics2D.OverlapCircleAll(transform.position, AOERange, LayerMask.GetMask("Enemy"));

        foreach (var item in target)
        {
            var enemy = item.GetComponent<BaseUnit>();
            enemy.GetHit(damage,Color.yellow);
            enemy.TransitionToState(EUnit.GetHit);
        }
    }
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

}