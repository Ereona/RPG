using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/UnitSettings")]
public class UnitSettings : ScriptableObject
{
    public int HealthPoints;
    public int DamageAttackPoints;
    public int AttackRange;
    public int MoveRange;
    public Unit Prefab;
}
