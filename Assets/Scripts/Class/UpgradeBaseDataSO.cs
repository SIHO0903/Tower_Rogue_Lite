using UnityEngine;

[CreateAssetMenu(menuName = "Data/UpgradeBaseData", fileName = "UpgradeBaseData")]
public class UpgradeBaseDataSO : ScriptableObject
{
    [field: Header("기본 능력치")]
    [field: SerializeField] public int key { get; set; }
    [field: SerializeField] public string UPName { get; set; }
    [field: SerializeField] public int MaxLvl { get; set; }
    [field: SerializeField] public int Cost { get; set; }
    [field: SerializeField] public float CostMultiplier { get; set; }
}
