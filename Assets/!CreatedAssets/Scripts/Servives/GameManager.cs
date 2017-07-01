using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public bool Started { get { return _started; } }

    private bool _started;

    private void Awake()
    {
        _started = false;
        StartCoroutine(CheckForTwoPlayers());
    }

    private IEnumerator CheckForTwoPlayers()
    {
        while(!(PhotonNetwork.playerList.Length == 2))
        {
            _started = false;
            Debug.Log(PhotonNetwork.playerList.Length);
            yield return null;
        }
        Debug.Log(PhotonNetwork.playerList.Length);
        _started = true;
    }
}
