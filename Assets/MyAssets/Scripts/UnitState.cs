using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum EUnit
{
    Idle,
    Move,
    Attack,
    GetHit,
    Die,
    Test
}



[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class UnitState<T> : MonoBehaviour
{

    protected IState<T> currentState;
    protected Dictionary<EUnit, IState<T>> states = new Dictionary<EUnit, IState<T>>();
  
    [field: SerializeField] public UnitData UnitData { get; set; }
    [HideInInspector] public Rigidbody2D rigid;
    [HideInInspector] public Animator animator;
    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();

        rigid = GetComponent<Rigidbody2D>();
    }
    public virtual void OnEnable()
    {
        Init();
    }
    public void Init()
    {


    }
    public void TransitionToState(EUnit estate)
    {
        currentState = states[estate];
        currentState.EnterState();
    }


}
