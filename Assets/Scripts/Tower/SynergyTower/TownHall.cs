using UnityEngine;

public class TownHall : SynergyTower
{
    public static Vector2 Position;
    protected override void Awake()
    {
        Position = transform.position;
    }

}