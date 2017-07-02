using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour {

	public void ClickBackToMenu()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Menu");
    }
}
