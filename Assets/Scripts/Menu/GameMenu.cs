using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    private Button _endTurnButton;

    private void Start()
    {
        _endTurnButton.onClick.AddListener(EndTurn);
        EventBus.TurnChanged += EventBus_TurnChanged;
        EventBus.GameEnd += EventBus_GameEnd;
    }

    private void EventBus_TurnChanged(Owner obj)
    {
        _endTurnButton.interactable = obj == Owner.Player;
    }

    private void EventBus_GameEnd(Owner obj)
    {
        _endTurnButton.gameObject.SetActive(false);
    }

    private void EndTurn()
    {
        EventBus.RaiseEndTurn();
    }

    private void OnDestroy()
    {
        EventBus.TurnChanged -= EventBus_TurnChanged;
        EventBus.GameEnd -= EventBus_GameEnd;
    }
}
