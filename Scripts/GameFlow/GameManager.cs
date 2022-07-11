using UnityEngine;



public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerMotor motor;
    public WorldGeneration worldGeneration;
    public GameObject[] cameras;

    private GameState state;


    private void Start()
    {
        Instance = this;
        state = GetComponent<GameStateInit>();
        state.Construct();
    }

    private void Update()
    {
        state.UpdateState();
    }

    public void ChangeState(GameState s)
    {
        state.Destruct();
        state = s;
        state.Construct();
    }

    public void ChangeCamera(GameCameras cam)
    {
        foreach (var camera in cameras)
        {
            camera.SetActive(false);
        }

        cameras[(int)cam].SetActive(true);
    }
}


public enum GameCameras
{
    Init,
    Game,
    Shop,
    Respawn
}