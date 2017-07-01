using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public void StartGame()
    {
        if(!(PhotonNetwork.countOfPlayers == 2))
        {
            Debug.LogError("Not all players had connected to the game! Not starting");
            return;
        }
    }
}
