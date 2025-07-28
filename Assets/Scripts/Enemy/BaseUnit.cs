using System;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    public Action<float> Die;
    public abstract void Init(int currentLevel);
    public abstract void GetHit(float damage, Color damageFontColor);
    public abstract void TransitionToState(EUnit estate);
}
