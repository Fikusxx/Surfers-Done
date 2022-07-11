using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using TMPro;

public class GameStateDeath : GameState, IUnityAdsShowListener
{
    public GameObject deathUI;
    [SerializeField] private TextMeshProUGUI highscoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI fishTotalText;
    [SerializeField] private TextMeshProUGUI currentFishText;

    // Music
    [SerializeField] private AudioClip deathSound;


    // Completion circle fields
    [SerializeField] private Image completionCircle;
    [SerializeField] private float timeToDecision = 2.5f;
    private float deathTime;

    private void Start()
    {
        
    }


    public override void Construct()
    {
        GameManager.Instance.motor.PausePlayer();

        deathTime = Time.time;
        deathUI.SetActive(true);
        completionCircle.gameObject.SetActive(true);

        // Prior to saving, set the highscore if needed
        if (SaveManager.Instance.save.Highscore < (int)GameStats.Instance.score)
        {
            SaveManager.Instance.save.Highscore = (int)GameStats.Instance.score;
        }

        SaveManager.Instance.save.Fish += GameStats.Instance.fishCollectedThisSession;
        SaveManager.Instance.Save();

        highscoreText.text = "Highscore : " + SaveManager.Instance.save.Highscore;
        currentScoreText.text = GameStats.Instance.ScoreToText();
        fishTotalText.text = "Total : " + SaveManager.Instance.save.Fish;
        currentFishText.text = GameStats.Instance.FishToText();

        AudioManager.Instance.PlaySFX(deathSound);
    }


    public override void UpdateState()
    {
        float ratio = (Time.time - deathTime) / timeToDecision;
        completionCircle.color = Color.Lerp(Color.green, Color.red, ratio);
        completionCircle.fillAmount = 1 - ratio;

        if (ratio > 1)
        {
            completionCircle.gameObject.SetActive(false);
        }

    }

    public void ToMenu()
    {
        // Change state to Init (Menu)
        brain.ChangeState(GetComponent<GameStateInit>());

        // Reset the player and the world
        GameManager.Instance.motor.ResetPlayer();
        brain.worldGeneration.ResetWorld();
    }

    public void TryResumeGame()
    {
        AdManager.Instance.ShowRewardedAd();
    }

    public void ResumeGame()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameManager.Instance.motor.RespawnPlayer();

    }

    public override void Destruct()
    {
        deathUI.SetActive(false);
    }

    // Ads
    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        completionCircle.gameObject.SetActive(false);

        switch (showCompletionState)
        {
            case UnityAdsShowCompletionState.SKIPPED:
                break;
            case UnityAdsShowCompletionState.COMPLETED:
                ResumeGame();
                break;
            default:
                break;
        }
    }
}
