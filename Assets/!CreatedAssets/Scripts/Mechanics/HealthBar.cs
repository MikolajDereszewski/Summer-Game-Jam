using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {


    [SerializeField]
    private float _health = 1000;
    [SerializeField]
    private float _maxHealth = 1000;
    [SerializeField]
    private Image _healthBarImage = null;
    [SerializeField]
    private GameManager _manager;

    [SerializeField]
    private GameObject _normalExitButton = null;
    [SerializeField]
    private GameObject _saveScoreButton = null;
    [SerializeField]
    private GameObject _saveInputField = null;

    [SerializeField]
    private GameObject _gameOverCanvas = null;
    
    [PunRPC]
    public void UpdateHealthAmount(float delta)
    {
        _health += delta;
        _healthBarImage.fillAmount = (_health / _maxHealth);
        if (_health <= 0f)
            OnGameOverDetected();
    }
    
    public void OnGameOverDetected()
    {
        _manager.Dead = true;
        _gameOverCanvas.SetActive(true);
        if (PlayerPrefs.GetInt("LOCAL_PLAYER_TYPE") == 0)
        {
            _saveScoreButton.SetActive(true);
            _saveInputField.SetActive(true);
        }
        else
            _normalExitButton.SetActive(true);
    }

}
