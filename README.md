Unity-Scoreboard
Blog about the Scoreboard: https://tkbryant.wordpress.com/2016/12/12/keeping-score/ (Out of Date)

How To Use:

1. Import TyeKBryant-Scoreboard.unitypackage

2. Drag ScoreBoard prefab into the scene

3. Modify "Number Of High Scores" field to be a desired amount

4. Create method of setting the default scores in ScoringManager

Most basic setup = complete
Default setup supports 2 types of scores (an actual score and a Time, you can change these)

Adding\Removing score types:

1. The ScoringManager and ScoreBoard scripts have areas with comments saying "Add\Remove score types here"

2. a. Each comment is followed immediately after by an example of how to add your custom score
2. b. Remove the line with the score type you would like to remove

3. Check each to ensure each area marked has your custom score added\removed

Troubleshooting:
Remember to check that all fields are set in the inspector
Remember to check that each area with "Add\Remove score types here" is consistent
Check that the inputfield child object of the ScoreBoard has two actions for "On End Edit"
	- Top should be ScoreBoard.SaveCSV
	- Bottom should be ScoreBoard.SetScoreBoard
Check that the scene has an EventSystem (ScoreBoard prefab should have one by default)

If problems persist send me an email at tyebryant@hotmail.com
