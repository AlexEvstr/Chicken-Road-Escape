using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float scoreCount;
    public float highScoreCount;

    public Text scoreText;
    public Text highScore;

    public float pointsPerSecond;

    public bool scoreIncreasing;

    public bool shouldDouble;

    void Start()
    {
        highScoreCount = PlayerPrefs.GetFloat("Highscore", 0);
    }

    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("Highscore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round (scoreCount);
        highScore.text = "High Score: " + Mathf.Round(highScoreCount);
    }

    public void addScore(int pointsToAdd)
    {
        if(shouldDouble)
        {
            pointsToAdd = pointsToAdd * 2;
        }
        scoreCount += pointsToAdd;
    }
}