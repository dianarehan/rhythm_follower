using UnityEngine;

public class GameState: MonoBehaviour
{
    public static GameStatus gameState { get;  set; }

    public static GameState Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
public enum GameStatus
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
