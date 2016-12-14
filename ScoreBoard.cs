using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class ScoreBoard : MonoBehaviour
{

    string filePath;

    public int numberOfHighScores = 10;

    public GameObject scorePrefab;

    public InputField playerNameInput;

    public ScoringManager.ScoreTypes playerScores = new ScoringManager.ScoreTypes();

    private List<string> playerNames = new List<string>();
    private List<string[]> playerInformation = new List<string[]>();

    private List<ScoringManager.ScoreTypes> highScores = new List<ScoringManager.ScoreTypes>();

    // Use this for initialization
    void Start()
    {
        //Add\Remove score type here
        playerScores.score = ScoringManager.playerScore;
        playerScores.timeTaken = ScoringManager.playerTimeTaken;

        //Remember to update the score type count here
        ScoringManager.scoreTypeCount = 2;

        filePath = Application.dataPath + @"/Saved_data.csv";
        if (!File.Exists(filePath))
            File.Create(filePath);
    }

    public void SaveCSV()
    {
        playerNameInput.gameObject.SetActive(false);

        //Grabing the old values from the CSV file
        #region
        for (int scoreCounter = 0; scoreCounter < ReadCSV().Length; ++scoreCounter)
        {
            string[] data = ReadCSV()[scoreCounter].Split(',');
            playerNames.Add(data[0]);

            ScoringManager.ScoreTypes csvScore = new ScoringManager.ScoreTypes();

            //Add\Remove score type here
            csvScore.score = (float.Parse(data[1]));
            csvScore.timeTaken = (float.Parse(data[2]));

            highScores.Add(csvScore);
        }
        #endregion

        //Inserting the new score and order the scores correctly
        #region
        if (highScores.Count > 0)
            for (int scoreIndex = highScores.Count - 1; scoreIndex >= 0; --scoreIndex)
            {
                if (playerScores.score <= highScores[scoreIndex].score)
                {
                    playerNames.Insert(scoreIndex + 1, playerNameInput.text);
                    highScores.Insert(scoreIndex + 1, playerScores);
                    break;
                }
                else if (playerScores.score > highScores[0].score)
                {
                    playerNames.Insert(0, playerNameInput.text);
                    highScores.Insert(0, playerScores);
                    break;
                }
            }
        else
        {
            playerNames.Insert(0, playerNameInput.text);
            highScores.Insert(0, playerScores);
        }
        #endregion

        //Setting up the information for saving
        #region
        for (int scoreIndex = 0; scoreIndex < numberOfHighScores; ++scoreIndex)
        {
            if (highScores.Count > scoreIndex)
            {
                List<string> informationToSave = new List<string>();
                informationToSave.Add(playerNames[scoreIndex]);

                //Add\Remove Score types here
                informationToSave.Add(highScores[scoreIndex].score.ToString());
                informationToSave.Add(highScores[scoreIndex].timeTaken.ToString());

                playerInformation.Add(informationToSave.ToArray());
            }
            else
            {
                List<string> informationToSave = new List<string>();
                informationToSave.Add("NoName");

                for (int scoreTypeCounter = 0; scoreTypeCounter < ScoringManager.scoreTypeCount; ++scoreTypeCounter)
                {
                    informationToSave.Add("0");
                }

                playerInformation.Add(informationToSave.ToArray());
            }
        }
        #endregion

        string delimiter = ",";

        int playerDataLength = playerInformation.Count;
        StringBuilder sb = new StringBuilder();
        for (int index = 0; index < playerDataLength; index++)
            sb.AppendLine(string.Join(delimiter, playerInformation[index]));

        File.WriteAllText(filePath, sb.ToString());
    }

    string[] ReadCSV()
    {
        string[] saveData = File.ReadAllLines(filePath);
        return saveData;
    }

    public void SetScoreBoard()
    {
        string[] highScores = ReadCSV();
        for (int scoreIndex = 0; scoreIndex < numberOfHighScores; ++scoreIndex)
        {
            //Add\Remove score types here
            string[] data = highScores[scoreIndex].Split(',');
            string name = "Name: " + data[0].Trim();
            string score = " Score: " + data[1].Trim();
            string timeTaken = " Time Taken: " + data[2].Trim();

            GameObject scoreboardPlacements = Instantiate(scorePrefab) as GameObject;
            scoreboardPlacements.transform.SetParent(this.transform);
            scoreboardPlacements.transform.position = new Vector3(Screen.width / 2, Screen.height - scoreIndex * 42 - 30, 0);
            var txt = scoreboardPlacements.GetComponentInChildren<Text>();

            if (txt != null)
            {
                //Add\Remove score types here
                txt.text = name + score + timeTaken;
            }
        }
    }

}
