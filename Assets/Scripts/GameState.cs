using UnityEngine;

public class GameState: MonoBehaviour
{
    public static GameStatus gameState { get;  set; }

    public static GameState Instance { get; private set; }

    private void Awake()
    {
        gameState = GameStatus.MainMenu;
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
    public void ChangeGameState(GameStatus newState)=>gameState = newState;
}
public enum GameStatus
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
