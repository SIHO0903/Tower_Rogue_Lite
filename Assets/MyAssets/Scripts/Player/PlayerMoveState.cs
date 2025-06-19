using UnityEngine;

public class PlayerMoveState<T> : BaseState<T> where T : UnitState<T>
{
    private float moveSpeed;

    public PlayerMoveState(UnitData unitData) : base(unitData)
    {
        moveSpeed = unitData.MoveSpeed;
    }

    public override void EnterState()
    {
        Debug.Log("Enter Move State");
    }

    public override void UpdateState(T owner)
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (input == Vector2.zero)
        {
            owner.TransitionToState(EUnit.Idle);
            return;
        }

        // 이동 처리
        owner.rigid.velocity = input * moveSpeed;

        // 애니메이션 방향 처리 (필요한 경우)
        if (owner.animator != null)
        {
            owner.animator.SetFloat("X", input.x);
            owner.animator.SetFloat("Y", input.y);
        }
    }
}