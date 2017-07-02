using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HighScore
{
    public string teamName;
    public float score;

    public HighScore(string name, float newscore)
    {
        teamName = name;
        score = newscore;
    }
}

[System.Serializable]
public class HighscoreContainer
{
    public List<HighScore> allHighScores;
}

public class SaveHighScore : MonoBehaviour
{

    const string file = "E:/GAMEJAM_LEADERBOARD/Scores.txt";

    [SerializeField]
    private GameManager _gameManager = null;
    [SerializeField]
    private Text _inputText = null;
    [SerializeField]
    private BackToMenu _backToMenuScript = null;

    public void SaveHighscoresToJSON()
    {
        HighscoreContainer loadedContainer = LoadHighscoresFromJSON();
        Debug.Log(loadedContainer.allHighScores.Count);
        if (loadedContainer == null)
        {
            loadedContainer = new HighscoreContainer();
            loadedContainer.allHighScores = new List<HighScore>();
        }

        HighScore addedHighscore = new HighScore(_inputText.text, _gameManager.Distance);

        loadedContainer.allHighScores.Add(addedHighscore);

        //DebugWholeHighscoreList(loadedContainer);
        SaveJSONToFile(JsonUtility.ToJson(loadedContainer, true));
    }

    private HighscoreContainer LoadHighscoresFromJSON()
    {
        if (LoadJSONFromFile() != "")
            return JsonUtility.FromJson<HighscoreContainer>(LoadJSONFromFile());
        return null;
    }

    private string LoadJSONFromFile()
    {
        return File.ReadAllText(file);
    }

    private void SaveJSONToFile(string inputJSON)
    {
        File.WriteAllText(file, inputJSON);
        OnSavingOver();
    }

    private void OnSavingOver()
    {
        _backToMenuScript.ClickBackToMenu();
    }

    private void DebugWholeHighscoreList(HighscoreContainer container)
    {
        for (int i = 0; i < container.allHighScores.Count; i++)
        {
            Debug.Log(container.allHighScores[i].teamName + ": " + container.allHighScores[i].score.ToString());
        }
    }
}
