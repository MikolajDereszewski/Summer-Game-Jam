using UnityEngine;

public class CaptainScript : Photon.MonoBehaviour {
    
    private ShipInput _ship;
    private GameManager _manager;

	void Start ()
    {
        _ship = GameObject.FindGameObjectWithTag("Ship").transform.GetComponent<ShipInput>();
        _manager = (GameManager)FindObjectOfType(typeof(GameManager));
        transform.parent.SetParent(_ship.transform);
    }

    void Update ()
    {
        if (!_manager.Started)
            return;
        _ship.GetComponent<PhotonView>().RPC("SetTargetPosition", PhotonTargets.All, LoadJoystickInput());
	}

    private Vector2 LoadJoystickInput()
    {
        float y = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");
        return new Vector2(x, y);
    }
}
