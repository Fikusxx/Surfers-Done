using UnityEngine;
using TMPro;


public class GameStateInit : GameState
{
    public GameObject menuUI;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI fishText;
    [SerializeField] private AudioClip menuLoopMusic;



    public override void Construct()
    {
        GameManager.Instance.ChangeCamera(GameCameras.Init);

        scoreText.text = "Highscore: " + SaveManager.Instance.save.Highscore.ToString("000000");
        fishText.text = "Fish : " + SaveManager.Instance.save.Fish.ToString("000");

        menuUI.SetActive(true);

        AudioManager.Instance.PlayMusicWithXFade(menuLoopMusic, 0f);
    }

    public void OnPlayClick()
    {
        brain.ChangeState(GetComponent<GameStateGame>());
        GameStats.Instance.ResetSession();
    }

    public void OnShopClick()
    {
        brain.ChangeState(GetComponent<GameStateShop>());
    }

    public override void Destruct()
    {
        menuUI.SetActive(false);
    }
}
