using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour {

    private void Awake()
    {
        Invoke("DestroyOverTime", 1f);
    }

    private void Update()
    {
        transform.Translate(0f, 50f * Time.deltaTime, 0f);
    }

    private void DestroyOverTime()
    {
        Destroy(gameObject);
    }
}
