using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum PlayerType
{
    Captain = 0,
    Shooter = 1
}

public class NetworkManager : MonoBehaviour {

    [SerializeField]
    private string _gameVersion = "0.0.2";
    [SerializeField]
    private GameManager _gameManager = null;
    [SerializeField]
    private Transform _shipGameObject = null;

    [SerializeField]
    private GameObject _captainPrefab = null;
    [SerializeField]
    private GameObject _shooterPrefab = null;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
    }

    private void JoinRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
        PhotonNetwork.JoinOrCreateRoom("GAME_ROOM", roomOptions, TypedLobby.Default);
    }

    void OnJoinedLobby()
    {
        Debug.Log("Joined to lobby!");
        JoinRoom();
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined to the room!");
        AddPlayer((PlayerPrefs.GetInt("LOCAL_PLAYER_TYPE") == 0) ? PlayerType.Captain : PlayerType.Shooter);
    }

    private void AddPlayer(PlayerType type)
    {
        Debug.Log("Player added!");
        switch (type)
        {
            case PlayerType.Captain:
                PhotonNetwork.Instantiate("CaptainPlayer", new Vector3(0, 0, -10), Quaternion.identity, 0);
                break;
            case PlayerType.Shooter:
                PhotonNetwork.Instantiate("ShooterPlayer", new Vector3(0, 0, -20), Quaternion.identity, 0);
                break;
        }
    }
}
