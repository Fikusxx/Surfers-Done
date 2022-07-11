using UnityEngine;
using System;


public class GameStats : MonoBehaviour
{
    // Singleton
    public static GameStats Instance { get; private set; }

    // Score data
    public float score;
    public float highscore;
    public float distanceModifier = 1.5f;


    // Fish data
    public int totalFish;
    public int fishCollectedThisSession;
    public float pointsPerFish = 10f;


    // Internal cooldown
    private float lastScoreUpdate;
    private float scoreUpdateDelta = 0.2f;


    // Action
    public event Action OnCollectFish;
    public event Action OnScoreChange;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        UpdateScore();
    }


    public void CollectFish() 
    {
        fishCollectedThisSession++;
        OnCollectFish?.Invoke();
    }

    private void UpdateScore()
    {
        float s = GameManager.Instance.motor.transform.position.z * distanceModifier;
        s += fishCollectedThisSession * pointsPerFish;

        if (s > score)
        {
            score = s;

            if (Time.time - lastScoreUpdate > scoreUpdateDelta)
            {
                lastScoreUpdate = Time.time;
                OnScoreChange?.Invoke();
            }
        }
    }

    private void UpdateHighScore()
    {

    }

    private void UpdateTotalFishCount()
    {

    }

    public string ScoreToText()
    {
        return score.ToString("000000");
    }

    public string FishToText()
    {
        return fishCollectedThisSession.ToString("000");
    }

    public void ResetSession()
    {
        score = 0;
        fishCollectedThisSession = 0;

        OnScoreChange?.Invoke();
        OnCollectFish?.Invoke();
    }
}
