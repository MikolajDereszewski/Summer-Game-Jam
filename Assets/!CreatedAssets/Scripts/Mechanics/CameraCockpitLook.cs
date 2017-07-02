using UnityEngine;

public class CameraCockpitLook : MonoBehaviour {
    
    [SerializeField]
    private Vector2 _rotationClamp;

    private Vector3 _currentRotation;

    void Update()
    {
        _currentRotation = transform.rotation.eulerAngles;
        transform.Rotate(ClampCockpitRotation(LoadJoystickInput()) - ReturnMinusVector(transform.rotation.eulerAngles));
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
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        return new Vector2(x, -y);
    }
}
