using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class HighScore : MonoBehaviour
{
    string filePath;

    public int numberOfHighScores = 10;

    public GameObject scorePrefab;

    public InputField playerNameInput;

    public List<float> scores;
    public List<string> playerNames;

    private List<string[]> playerInformation = new List<string[]>();

    void Start ()
    {
        filePath = Application.dataPath + @"/Saved_data.csv";
        if (!File.Exists(filePath))
            File.Create(filePath);
    }

    public void SaveCSV ()
    {

        for (int scoreCounter = 0; scoreCounter < ReadCSV().Length; ++scoreCounter)
        {
            string[] data = ReadCSV()[scoreCounter].Split(',');
            playerNames.Add(data[0]);
            scores.Add(int.Parse(data[1]));
        }

        for (int highScoreIndex = scores.Count - 1; highScoreIndex >= 0; --highScoreIndex)
        {
            if (GameManager.score <= scores[highScoreIndex])
            {
                scores.Insert(highScoreIndex + 1, GameManager.score);
                playerNames.Insert(highScoreIndex + 1, playerNameInput.text);
                playerNameInput.gameObject.SetActive(false);
                break;
            }
            else if (GameManager.score > scores[0])
            {
                scores.Insert(0, GameManager.score);
                playerNames.Insert(0, playerNameInput.text);
                playerNameInput.gameObject.SetActive(false);
                break;
            }
        }

        for (int scoreIndex = 0; scoreIndex < numberOfHighScores; ++scoreIndex)
        {
            if (scores.Count > scoreIndex)
            {
                string[] informationToSave = new string[] { playerNames[scoreIndex], scores[scoreIndex].ToString() };
                playerInformation.Add(informationToSave);
            }
            else
            {
                string[] informationToSave = new string[] { "NoName", "0" };
                playerInformation.Add(informationToSave);
            }
        }

        string delimiter = ",";

        int playerDataLength = playerInformation.Count;
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < playerDataLength; index++)
            sb.AppendLine(string.Join(delimiter, playerInformation[index]));

        File.WriteAllText(filePath, sb.ToString());
    }

    string[] ReadCSV ()
    {
        string[] saveData = File.ReadAllLines(filePath);
        return saveData;
    }

    public void SetScoreBoard ()
    {
        string[] highScores = ReadCSV();
        for (int scoreIndex = 0; scoreIndex < numberOfHighScores; ++scoreIndex)
        {
            GameObject scoreboardPlacements = Instantiate(scorePrefab) as GameObject;
            scoreboardPlacements.transform.SetParent(this.transform);
            scoreboardPlacements.transform.position = new Vector3(Screen.width/2, Screen.height - scoreIndex * 42 - 30, 0);
            var txt = scoreboardPlacements.GetComponentInChildren<Text>();
            if (txt != null)
                txt.text = highScores[scoreIndex];
        }
    }
}