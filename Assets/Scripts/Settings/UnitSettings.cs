using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/UnitSettings")]
public class UnitSettings : ScriptableObject
{
    [SerializeField]
    private int _healthPoints;
    [SerializeField]
    private int _damageAttackPoints;
    [SerializeField]
    private int _attackRange;
    [SerializeField]
    private int _moveRange;
    [SerializeField]
    private Unit _prefab;

    public int HealthPoints { get { return _healthPoints; } }
    public int DamageAttackPoints { get { return _damageAttackPoints; } }
    public int AttackRange { get { return _attackRange; } }
    public int MoveRange { get { return _moveRange; } }
    public Unit Prefab { get { return _prefab; } }
}
