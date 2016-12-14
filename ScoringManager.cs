using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ScoringManager {

    //Variable that need to be set
    #region
    //Add\Remove score types here
    public static float playerScore;
    public static float playerTimeTaken;
    #endregion

    public static float scoreTypeCount;

    public struct ScoreTypes
    {
        //Add\Remove score types here
        public float score;
        public float timeTaken;
    }
}
