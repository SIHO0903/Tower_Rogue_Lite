using UnityEngine;
public enum SynergyEnum
{
    Up_Left,
    Up,
    Up_Right,
    Left,
    Right,
    Down_Left,
    Down,
    Down_Right
}
public static class GridUtility
{

    public const float TileGap = 0.65f;
    public static Vector2 EnumToVector3(SynergyEnum pos)
    {
        switch (pos)
        {
            case SynergyEnum.Up_Left: return new Vector2(-1, 1);
            case SynergyEnum.Up: return new Vector2(0, 1);
            case SynergyEnum.Up_Right: return new Vector2(1, 1);
            case SynergyEnum.Left: return new Vector2(-1, 0);
            case SynergyEnum.Right: return new Vector2(1, 0);
            case SynergyEnum.Down_Left: return new Vector2(-1, -1);
            case SynergyEnum.Down: return new Vector2(0, -1);
            case SynergyEnum.Down_Right: return new Vector2(1, -1);
            default: return Vector2.zero;
        }
    }
    public static Vector2 SnapToGrid(Vector2 v)
    {
        float x = Mathf.Round(v.x * 100f) / 100f;
        float y = Mathf.Round(v.y * 100f) / 100f;
        return new Vector2(x, y);
    }
}
