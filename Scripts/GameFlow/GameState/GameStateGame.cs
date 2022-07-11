using UnityEngine;
using TMPro;


public class GameStateGame : GameState
{
    public GameObject gameUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI fishText;

    [SerializeField] private AudioClip gameMusic;


    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCameras.Game);
        GameManager.Instance.motor.ResumePlayer();

        GameStats.Instance.OnCollectFish += UpdateFishText;
        GameStats.Instance.OnScoreChange += UpdateScoreText;


        gameUI.SetActive(true);

        AudioManager.Instance.PlayMusicWithXFade(gameMusic, 2f);
    }

    public override void Destruct()
    {
        gameUI.SetActive(false);

        GameStats.Instance.OnCollectFish -= UpdateFishText;
        GameStats.Instance.OnScoreChange -= UpdateScoreText;
    }

    private void UpdateFishText()
    {
        fishText.text = GameStats.Instance.FishToText();
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = GameStats.Instance.ScoreToText();
    }
}
