using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    [SerializeField]
    private string _levelToLoad;

    public void OnButtonClicked(int playerType)
    {
        PlayerPrefs.SetInt("LOCAL_PLAYER_TYPE", playerType);
        PlayerPrefs.Save();
        SceneManager.LoadScene(_levelToLoad);
    }
}
