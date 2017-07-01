using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : Photon.MonoBehaviour {

    private Transform _ship;
    private GameManager _manager;

    void Start()
    {
        _ship = GameObject.FindGameObjectWithTag("Ship").transform;
        _manager = (GameManager)FindObjectOfType(typeof(GameManager));
        transform.parent.SetParent(_ship);
    }
}
