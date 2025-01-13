using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private MenuManager menuManager;
    private void Start()
    {
        menuManager = FindFirstObjectByType<MenuManager>();
        playButton.onClick.AddListener(menuManager.PlayGame);
        quitButton.onClick.AddListener(menuManager.QuitGame);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(menuManager.PlayGame);
        quitButton.onClick.RemoveListener(menuManager.QuitGame);
    }
}