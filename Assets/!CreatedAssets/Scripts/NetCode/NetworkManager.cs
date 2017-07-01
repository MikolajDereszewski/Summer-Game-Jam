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
    private string _gameVersion = "0.0.1";
    [SerializeField]
    private GameManager _gameManager = null;

    [SerializeField]
    private GameObject _captainPrefab = null;
    [SerializeField]
    private GameObject _shooterPrefab = null;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings(_gameVersion);
        //JoinRoom((PlayerPrefs.GetInt("LOCAL_PLAYER_TYPE") == 0) ? PlayerType.Captain : PlayerType.Shooter);
    }

    private void JoinRoom(PlayerType type)
    {
        switch(type)
        {
            case PlayerType.Captain:
                RoomOptions roomOptions = new RoomOptions() { isVisible = true, maxPlayers = 2 };
                PhotonNetwork.CreateRoom("GAME_ROOM", roomOptions, TypedLobby.Default);
                break;
            case PlayerType.Shooter:
                PhotonNetwork.JoinRoom("GAME_ROOM");
                break;
        }
        AddPlayer(type);
    }

    void OnJoinedLobby()
    {
        JoinRoom((PlayerPrefs.GetInt("LOCAL_PLAYER_TYPE") == 0) ? PlayerType.Captain : PlayerType.Shooter);
        Debug.Log("Joined to lobby!");
    }

    private void AddPlayer(PlayerType type)
    {
        _gameManager.StartGame();
    }
}
