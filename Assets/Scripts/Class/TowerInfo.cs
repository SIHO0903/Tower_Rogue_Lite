using UnityEngine;

public class TowerInfo
{
    public Transform transform;
    public Sprite headSprite;
    public Transform headTransform;
    public Sprite bodySprite;
    public Transform bodyTransform;

    public TowerInfo(Transform transform, Sprite headSprite, Transform headTransform, Sprite bodySprite, Transform bodyTransform)
    {
        this.transform = transform;
        this.headSprite = headSprite;
        this.headTransform = headTransform;
        this.bodySprite = bodySprite;
        this.bodyTransform = bodyTransform;
    }

}