using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleFollower : MonoBehaviour {

    [SerializeField]
    private Transform _shipTransform;

    [SerializeField]
    private float _z;

    private void Update()
    {
        transform.position = new Vector3(_shipTransform.position.x, _shipTransform.position.y, _z);
    }
}
