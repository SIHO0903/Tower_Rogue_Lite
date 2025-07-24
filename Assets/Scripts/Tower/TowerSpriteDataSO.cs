using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/TowerSpriteData", fileName = "TowerSpriteData")]
public class TowerSpriteDataSO : ScriptableObject
{
    [field: Header("Lv1 Head")]
    [field: SerializeField] public Sprite Head_Lv1 { get; set; }
    [field: SerializeField] public Vector2 HeadPos_Lv1 { get; set; }
    [field: SerializeField] public AnimatorOverrideController HeadAnimator_Lv1 { get; set; }

    [field: Header("Lv1 Body")]
    [field: SerializeField] public Sprite Body_Lv1 { get; set; }
    [field: SerializeField] public Vector2 BodyPos_Lv1 { get; set; }

    [field: Header("Lv2 Head")]
    [field: SerializeField] public Sprite Head_Lv2 { get; set; }
    [field: SerializeField] public Vector2 HeadPos_Lv2 { get; set; }
    [field: SerializeField] public AnimatorOverrideController HeadAnimator_Lv2 { get; set; }
    [field: Header("Lv2 Body")]
    [field: SerializeField] public Sprite Body_Lv2 { get; set; }
    [field: SerializeField] public Vector2 BodyPos_Lv2 { get; set; }
    [field: Header("Lv3 Head")]
    [field: SerializeField] public Sprite Head_Lv3 { get; set; }
    [field: SerializeField] public Vector2 HeadPos_Lv3 { get; set; }
    [field: SerializeField] public AnimatorOverrideController HeadAnimator_Lv3 { get; set; }
    [field: Header("Lv3 Body")]
    [field: SerializeField] public Sprite Body_Lv3 { get; set; }
    [field: SerializeField] public Vector2 BodyPos_Lv3 { get; set; }
}