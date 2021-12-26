using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    [SerializeField]
    private BattleFieldSettings _settings;
    [SerializeField]
    private BattleFieldSize _sizes;
    [SerializeField]
    private BattleFieldInitializer _fieldInitializer;
    [SerializeField]
    private BattleController _battleController;
    [SerializeField]
    private AIBrain _aiBrain;

    void Start()
    {
        _sizes.Init(_settings);
        BattleState battle = _fieldInitializer.CreateBattle(_settings, _sizes);
        _battleController.SetBattle(battle);
        _aiBrain.Init(battle);
    }
}
