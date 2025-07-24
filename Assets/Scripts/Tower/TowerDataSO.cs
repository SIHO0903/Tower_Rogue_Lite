using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/TowerData", fileName = "TowerData")]
public class TowerDataSO : ScriptableObject
{
    [field: Header("�⺻ �ɷ�ġ")]
    [field: SerializeField] public string KorName { get; set; }
    [field: SerializeField] public int MaxLevel { get; set; }
    [field: SerializeField] public float Damage { get; set; }
    [field: SerializeField] public float AttackSpeed { get; set; }
    [field: SerializeField] public float Range { get; set; }
    [field: SerializeField] public float ProjectileSpeed { get; set; }
    [field: Header("������ �ɷ�ġ")]
    [field: SerializeField] public float Growth_Damage { get; set; }
    [field: SerializeField] public float Growth_AttackSpeed { get; set; }
    [field: SerializeField] public float Growth_Range { get; set; }

}
