using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private Color _playerColor;
    [SerializeField]
    private Color _enemyColor;
    [SerializeField]
    private Image _barImage;
    [SerializeField]
    private Text _healthText;

    private int _maxHealth;

    public void SetStartHealth(int value)
    {
        _maxHealth = value;
        SetHealth(value);
    }

    public void SetHealth(int value)
    {
        if (_maxHealth == 0)
        {
            return;
        }
        _barImage.fillAmount = (float)value / _maxHealth;
        _healthText.text = string.Format("{0}/{1}", value, _maxHealth);
    }

    public void SetOwner(Owner owner)
    {
        switch (owner)
        {
            case Owner.Player:
                _barImage.color = _playerColor;
                break;
            case Owner.Enemy:
                _barImage.color = _enemyColor;
                break;
        }
    }
}
