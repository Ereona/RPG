using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _content;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Text _resultText;

    void Start()
    {
        _content.SetActive(false);
        _restartButton.onClick.AddListener(Restart);
        EventBus.GameEnd += EventBus_GameEnd;
    }

    private void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void EventBus_GameEnd(Owner obj)
    {
        _content.SetActive(true);
        _resultText.text = obj == Owner.Player ? "VICTORY" : "DEFEAT";
    }

    private void OnDestroy()
    {
        EventBus.GameEnd -= EventBus_GameEnd;
    }
}
