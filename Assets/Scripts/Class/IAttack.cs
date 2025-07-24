using UnityEngine;

public interface IAttack
{
    public void Attack(float damage, Vector2 currentPos,Vector2 dir);
}