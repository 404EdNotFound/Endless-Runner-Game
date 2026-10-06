using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Scoring_and_Timing : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;

    float startingTime;
    float runningTime;
    bool activeTime = false;
    float minutes;
    float seconds;
    int score;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        activeTime = true;
        startingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTime)
        {
            runningTime = Time.time - startingTime;
            minutes = Mathf.FloorToInt(runningTime / 60);
            seconds = Mathf.FloorToInt(runningTime % 60);
            score++;
        }

        scoreText.text = score.ToString();
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void EndTime()
    {
        activeTime = false;
    }
}
