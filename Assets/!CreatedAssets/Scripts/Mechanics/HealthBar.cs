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
    
    [PunRPC]
    public void UpdateHealthAmount(float delta)
    {
        _health += delta;
        _healthBarImage.fillAmount = (_health / _maxHealth);
    }
}
