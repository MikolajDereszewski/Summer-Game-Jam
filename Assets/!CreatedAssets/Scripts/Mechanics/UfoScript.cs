using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoScript : MonoBehaviour {

    private GameManager _gameManager;
    private Transform _ship;

    private Vector3 _movementVector;
    private float _speed;

    public void Init(GameManager manager)
    {
        _gameManager = manager;
        _ship = GameObject.FindGameObjectWithTag("Ship").transform;
        _movementVector = _ship.position - transform.position;
    }

    private void Update()
    {
        if (_gameManager != null && !_gameManager.Dead)
            _speed = _gameManager.GameSpeed * 50f;
        _movementVector = _ship.position - transform.position;
        transform.Translate((_movementVector * _speed * Time.deltaTime) / (_movementVector.magnitude * 2f));
        if (transform.position.z >= -15)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Shot")
            return;
        Destroy(gameObject);
    }
}
