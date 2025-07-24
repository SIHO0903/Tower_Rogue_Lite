using UnityEngine;


[CreateAssetMenu(menuName = "Data/UnitData", fileName = "UnitData")]
public class UnitDataSO : ScriptableObject
{
    [field: Header("기본정보")]
    [field: SerializeField] public int ID { get; set; }
    [field: SerializeField] public string UnitKorName { get; set; }
    [field: SerializeField] public string UnitName { get; set; }
    [field: Header("능력치")]
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }

    [field: Header("성장치")]
    [field: SerializeField] public float HealthGrowth { get; set; }
    [field: SerializeField] public float AttackGrowth { get; set; }

    [field: Header("경험치")]
    [field: SerializeField] public float EXP { get; set; }

}
