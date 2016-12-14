using UnityEngine;
using System.Collections;

public class SetScores : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ScoringManager.scores.Add(20f);
        ScoringManager.scores.Add(204323f);
        ScoringManager.scores.Add(2453420f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
