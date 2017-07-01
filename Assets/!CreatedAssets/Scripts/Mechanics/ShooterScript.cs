using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterScript : Photon.MonoBehaviour {

    [SerializeField]
    private Transform _rotationCockpitObject = null;
    [SerializeField]
    private Vector2 _rotationClamp;
    [SerializeField]
    private GameObject _shotPrefab = null;
    [SerializeField]
    private Transform _shotSpawn1 = null;
    [SerializeField]
    private Transform _shotSpawn2 = null;

    private Transform _ship;
    private GameManager _manager;

    private Vector3 _currentRotation;

    private Coroutine _shootingCoroutine;

    void Start()
    {
        _ship = GameObject.FindGameObjectWithTag("Ship").transform;
        _manager = (GameManager)FindObjectOfType(typeof(GameManager));
        transform.parent.parent.SetParent(_ship);
    }

    void Update()
    {
        if (!_manager.Started)
            return;
        _currentRotation = _rotationCockpitObject.rotation.eulerAngles;
        _rotationCockpitObject.Rotate(ClampCockpitRotation(LoadJoystickInput()) - ReturnMinusVector(_rotationCockpitObject.rotation.eulerAngles));

        if (IsTriggerPressed() && _shootingCoroutine == null)
            _shootingCoroutine = StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(IsTriggerPressed())
        {
            CreateShots();
            yield return delay;
        }
        _shootingCoroutine = null;
    }

    private void CreateShots()
    {
        Instantiate(_shotPrefab, _shotSpawn1.position, _shotSpawn1.rotation);
        Instantiate(_shotPrefab, _shotSpawn2.position, _shotSpawn2.rotation);
    }

    private Vector3 ReturnMinusVector(Vector3 source)
    {
        float x = (source.x > 180) ? source.x - 360 : source.x;
        float y = (source.y > 180) ? source.y - 360 : source.y;
        float z = (source.z > 180) ? source.z - 360 : source.z;
        return new Vector3(x, y, z);
    }

    private Vector3 ClampCockpitRotation(Vector2 input)
    {
        float xrot = _currentRotation.x + input.y;
        float yrot = _currentRotation.y + input.x;
        float x = Mathf.Clamp((xrot > 180) ? xrot - 360 : xrot, _rotationClamp.x, _rotationClamp.y);
        float y = Mathf.Clamp((yrot > 180) ? yrot - 360 : yrot, _rotationClamp.x, _rotationClamp.y);
        return new Vector3(x, y, 0f);
    }

    private Vector2 LoadJoystickInput()
    {
        float y = Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Mouse X");
        return new Vector2(x, y);
    }

    private bool IsTriggerPressed()
    {
        return Input.GetAxis("Fire1") != 0;
    }
}
