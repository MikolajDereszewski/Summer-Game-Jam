using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LoadLeaderboard : MonoBehaviour {

    const string file = "C:/Users/admin/OneDrive/Scores.txt";

    [SerializeField]
    private Text _showText = null;

    private HighscoreContainer _container;

    private void Awake()
    {
        _container = new HighscoreContainer();
        _container.allHighScores = new List<HighScore>();
    }

    private void Update()
    {
        UpdateContainer();
        UpdateVisibleText();
    }

    private void UpdateVisibleText()
    {
        string output = "";
        for(int i = 0; i < _container.allHighScores.Count; i++)
        {
            output += (i+1).ToString() + ". " + _container.allHighScores[i].teamName + ": " + ((int)_container.allHighScores[i].score).ToString() + "\n";
            if (i == 20)
                break;
        }
        _showText.text = output;
    }

    private void UpdateContainer()
    {
        _container = LoadHighscoresFromJSON();
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
}
