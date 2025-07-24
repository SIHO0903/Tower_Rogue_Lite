using UnityEngine;

public abstract class BaseTower : MonoBehaviour
{
    public TowerDataSO TowerData;
    public TowerSpriteDataSO TowerSpriteData;

    protected SpriteRenderer headSprite;
    protected SpriteRenderer bodySprite;
    protected Animator animator;
    SpriteRenderer[] sprites;
    TowerInfo towerInfo;
    protected virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
        headSprite = sprites[0];
        bodySprite = sprites[1];
    }
    public TowerInfo TowerInfo()
    {
        headSprite = sprites[0];
        bodySprite = sprites[1];
        return new TowerInfo(transform, headSprite.sprite, headSprite.transform, bodySprite.sprite, bodySprite.transform);
    }
    // 이름은 공통으로 가져감
    public virtual string GetName()
    {
        return $"{TowerData.KorName}";
    }

    // 설명 문자열 (시너지 or 공격력 등)
    public abstract string GetDescription();
}
