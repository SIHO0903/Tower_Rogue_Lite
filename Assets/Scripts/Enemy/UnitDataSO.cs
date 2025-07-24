using UnityEngine;


[CreateAssetMenu(menuName = "Data/UnitData", fileName = "UnitData")]
public class UnitDataSO : ScriptableObject
{
    [field: Header("�⺻����")]
    [field: SerializeField] public int ID { get; set; }
    [field: SerializeField] public string UnitKorName { get; set; }
    [field: SerializeField] public string UnitName { get; set; }
    [field: Header("�ɷ�ġ")]
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }

    [field: Header("����ġ")]
    [field: SerializeField] public float HealthGrowth { get; set; }
    [field: SerializeField] public float AttackGrowth { get; set; }

    [field: Header("����ġ")]
    [field: SerializeField] public float EXP { get; set; }

}
