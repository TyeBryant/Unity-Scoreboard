using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class HighScore : MonoBehaviour
{

    [System.Serializable]
    public class ListWrapper
    {
        public List<float> scores;
    }

    string filePath;

    public int numberOfHighScores = 10;

    public GameObject scorePrefab;

    public InputField playerNameInput;

    public ListWrapper nList = new ListWrapper();
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
            for (int scoreTypeIndex = 1; scoreTypeIndex < ScoringManager.scores.Count - 1; ++scoreTypeIndex)
            {
                nList.scores.Add(float.Parse(data[scoreTypeIndex]));
                Debug.Log(scoreTypeIndex);
            }
        }

        for (int highScoreIndex = nList.scores.Count - 1; highScoreIndex >= 0; --highScoreIndex)
        {
            if (ScoringManager.scores[0] <= nList.scores[highScoreIndex])
            {
                for (int scoreTypeIndex = 0; scoreTypeIndex < ScoringManager.scores.Count; ++scoreTypeIndex)
                {
                    nList.scores.Insert(highScoreIndex + 1, ScoringManager.scores[scoreTypeIndex]);
                }

                playerNames.Insert(highScoreIndex + 1, playerNameInput.text);
                break;
            }
            else if (ScoringManager.scores[0] > nList.scores[0])
            {
                for (int scoreTypeIndex = 0; scoreTypeIndex < ScoringManager.scores.Count; ++scoreTypeIndex)
                {
                    nList.scores.Insert(0, ScoringManager.scores[scoreTypeIndex]);
                }

                playerNames.Insert(0, playerNameInput.text);
                break;
            }
        }

		playerNameInput.gameObject.SetActive(false);

        for (int scoreIndex = 0; scoreIndex < numberOfHighScores; ++scoreIndex)
        {
            if (nList.scores.Count > scoreIndex)
            {
                Debug.Log("test");
                List<string> informationToSave = new List<string>();
                informationToSave.Add(playerNames[scoreIndex]);

                for (int scoreTypeIndex = 0; scoreTypeIndex < ScoringManager.scores.Count; ++scoreTypeIndex)
                {
                    informationToSave.Add(nList.scores[scoreTypeIndex].ToString());
                }

                playerInformation.Add(informationToSave.ToArray());
                Debug.Log(playerInformation[scoreIndex]);
            }
            else
            {
                List<string> informationToSave = new List<string>();
                informationToSave.Add("NoName");

                for (int scoreTypeIndex = 0; scoreTypeIndex < ScoringManager.scores.Count; ++scoreTypeIndex)
                {
                    informationToSave.Add("0");
                }

                playerInformation.Add(informationToSave.ToArray());
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