using UnityEngine;

public class ShipInput : MonoBehaviour {

    [SerializeField]
    private Vector2 _positionSquareClamp = new Vector2();

    private Vector3 _targetPosition;

    private void Awake()
    {
        _targetPosition = Vector3.zero;
    }

    private void Update()
    {
        Vector3 moveVector = _targetPosition - transform.position;
        transform.Translate(new Vector3(moveVector.x, moveVector.y, 0f) * Time.deltaTime * (5f / moveVector.magnitude));
    }

    private Vector3 ClampTargetPosition(Vector2 input)
    {
        float x = Mathf.Clamp(_targetPosition.x + input.x, _positionSquareClamp.x, _positionSquareClamp.y);
        float y = Mathf.Clamp(_targetPosition.y + input.y, _positionSquareClamp.y, _positionSquareClamp.y);
        return new Vector3(x, y, 0);
    }

    [PunRPC]
    public void SetTargetPosition(Vector2 newTarget)
    {
        _targetPosition = ClampTargetPosition(newTarget);
    }

    void OnPhotonSerializeView()
    {

    }
}
