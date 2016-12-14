using UnityEngine;
using System.Collections;

public class SetScores : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ScoringManager.scores.Add(20f);
        ScoringManager.scores.Add(204323f);
        ScoringManager.scores.Add(24520f);
        ScoringManager.scores.Add(235f);
        ScoringManager.scores.Add(7653f);
        ScoringManager.scores.Add(78327840f);
        ScoringManager.scores.Add(3420f);
        ScoringManager.scores.Add(23420f);
        ScoringManager.scores.Add(240f);
        ScoringManager.scores.Add(175489f);
        Debug.Log(ScoringManager.scores.Count);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
