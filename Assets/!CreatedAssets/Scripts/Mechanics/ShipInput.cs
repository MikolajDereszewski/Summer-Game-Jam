using UnityEngine;

public class ShipInput : MonoBehaviour {

    [SerializeField]
    private HealthBar _healthBar = null;
    [SerializeField]
    private Vector2 _positionSquareClamp = new Vector2();

    private Vector3 _targetPosition;
    private Vector3 _targetRotation;

    private void Awake()
    {
        _targetPosition = Vector3.zero;
    }

    private void Update()
    {
        Vector3 moveVector = _targetPosition - transform.position;
        transform.Translate(new Vector3(moveVector.x, moveVector.y, 0f) * Time.deltaTime * (100f / moveVector.magnitude));
        transform.rotation = Quaternion.Euler(_targetRotation);
    }

    private Vector3 ClampTargetPosition(Vector2 input)
    {
        float x = Mathf.Clamp(_targetPosition.x + input.x, _positionSquareClamp.x, _positionSquareClamp.y);
        float y = Mathf.Clamp(_targetPosition.y + input.y, _positionSquareClamp.x, _positionSquareClamp.y);
        return new Vector3(x, y, 0);
    }

    private Vector3 ClampTargetRotation(Vector2 input)
    {
        return new Vector3(-input.y * 5f, 0f, -input.x * 15f);
    }

    [PunRPC]
    public void SetTargetPosition(Vector2 newTarget)
    {
        _targetPosition = ClampTargetPosition(newTarget);
        _targetRotation = ClampTargetRotation(newTarget);
    }

    void OnPhotonSerializeView()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Meteor")
        {
            _healthBar.UpdateHealthAmount(-100f);
            Destroy(other.gameObject);
        }
    }
}
