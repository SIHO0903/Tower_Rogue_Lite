using UnityEngine;


[CreateAssetMenu(menuName = "Unit/UnitData", fileName = "UnitData")]
public class UnitData : ScriptableObject
{
    [field: SerializeField] public string UnitKorName { get; set; }
    [field: SerializeField] public string UnitName { get; set; }
    [field: SerializeField] public int Level { get; set; }
    [field: SerializeField] public float Health { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float MoveSpeed { get; set; }
    [field: SerializeField] public float AttackRange { get; set; }

}
