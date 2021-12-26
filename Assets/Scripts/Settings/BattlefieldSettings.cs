using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/BattleFieldSettings")]
public class BattleFieldSettings : ScriptableObject
{
    public int N;
    public int M;

    public List<BattleFieldUnitData> PlayerUnits = new List<BattleFieldUnitData>();
    public List<BattleFieldUnitData> EnemyUnits = new List<BattleFieldUnitData>();
}
