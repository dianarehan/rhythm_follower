using UnityEngine;

public class GameSpawner : MonoBehaviour
{
    [SerializeField] private bool isMainMenu;
    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isPaused;
    [SerializeField] private bool isGameOver;

    private void Update()=>UpdateActiveState();
    
    private void UpdateActiveState()
    {
        if (isMainMenu && GameState.gameState == GameStatus.MainMenu)
            gameObject.SetActive(true);
        
        else if (isPlaying && GameState.gameState == GameStatus.Playing)
            gameObject.SetActive(true);
        
        else if (isPaused && GameState.gameState == GameStatus.Paused)
            gameObject.SetActive(true);
        
        else if (isGameOver && GameState.gameState == GameStatus.GameOver)
            gameObject.SetActive(true);
        
        else gameObject.SetActive(false);
        
    }
}