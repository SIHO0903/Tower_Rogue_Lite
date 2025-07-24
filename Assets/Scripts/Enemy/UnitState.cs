using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.CanvasScaler;


public enum EUnit
{
    Idle,
    Move,
    Attack,
    GetHit,
    Die,
}



[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class UnitState<T> : MonoBehaviour
{

    protected IState<T> currentState;
    protected Dictionary<EUnit, IState<T>> states = new Dictionary<EUnit, IState<T>>();
  
    [field: SerializeField] 
    public UnitDataSO UnitData { get; set; }
    public float Health { get; set; }
    public float Damage { get; set; }
    public CircleCollider2D CircleCollider { get; set; }
    public Rigidbody2D Rigid { get; set; }
    public Animator Animator { get; set; }

    public Vector3 townHallPos;
    SpriteRenderer spriteRenderer;
    public Action<float> Die;
    public virtual void Awake()
    {
        Animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CircleCollider = GetComponent<CircleCollider2D>();
        Rigid = GetComponent<Rigidbody2D>();
        townHallPos = FindObjectOfType<TownHall>().transform.position;
        
    }
    void Start()
    {
        Init(1);
    }
    public virtual void OnEnable()
    {
        Debug.Log("UnitState : " + townHallPos);
    }
    public void Init(int currentLevel)
    {
        LevelChange(currentLevel);
        CircleCollider.enabled = true;
        gameObject.layer = LayerMask.NameToLayer("Enemy");

    }
    void LevelChange(int currentLevel)
    {
        Health = UnitData.Health + currentLevel * UnitData.HealthGrowth;
        Damage = UnitData.Damage + currentLevel * UnitData.AttackGrowth;
    }
    public void TransitionToState(EUnit estate)
    {
        currentState = states[estate];
        currentState.EnterState();
    }

    public Vector2 Dir()
    {
        Vector2 dir = Vector2.zero;
        dir = townHallPos - transform.position;
        dir.Normalize();
        spriteRenderer.flipX = dir.x > 0 ? false : true;
        return dir;
    }
    public float Distance()
    {
        return Vector3.Distance(transform.position, townHallPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TowerProjectile"))
        {
            GetHit(GetProjectileDamage(collision), Color.white);
            TransitionToState(EUnit.GetHit);
        }
    }
    public void GetHit(float damage,Color damageFontColor)
    {
        if (damage == 0) return;
        Health -= damage;
        DamageTxt.Create(transform.position, damage, damageFontColor);
    }
    float GetProjectileDamage(Collider2D projectile)
    {
        float damage = 0;
        if(projectile.TryGetComponent<FriendlyProjectile>(out FriendlyProjectile component))
        {
            damage = component.GetDamage();
        }

        return damage;
    }
}
