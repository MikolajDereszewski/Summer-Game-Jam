using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour {

    private GameManager _gameManager;

    private Vector3 _movementVector;
    private float _speed;

    public void Init(GameManager manager)
    {
        _gameManager = manager;
        _movementVector = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), -1f);
        transform.localScale *= Random.Range(0.5f, 3f);
    }

    private void Update()
    {
        if(_gameManager != null)
            _speed = _gameManager.GameSpeed * 50f;
        transform.Translate(_movementVector * _speed * Time.deltaTime);
        if (transform.position.z <= -15)
            Destroy(gameObject);
    }
}
