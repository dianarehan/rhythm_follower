using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrowsParent;
    [SerializeField] private float hitWindowSeconds = 0.15f;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Slider scoreSlider;
    [SerializeField] private CanvasGroup endGameCanvasGroup;
    [SerializeField] private TextMeshProUGUI endGameScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private string[] winSceneText;
    [SerializeField] private string[] loseSceneText;
    public UnityEvent OnArrowHit;
    public UnityEvent OnArrowMiss;

    private List<Arrow> activeArrows = new List<Arrow>();
    private int score = 0;

    private void Start()
    {
        ArrowsGenerator arrowsGenerator = FindAnyObjectByType<ArrowsGenerator>();
        arrowsGenerator.OnSongEnd +=HandleSoundEnd;
    }

    private void Update()
    {
        GetAllArrows();
        CheckArrowTiming();

        score = Mathf.Clamp(score, 0, 10000);
        scoreSlider.value = score;
    }

    private void GetAllArrows()
    {
        activeArrows.Clear();
        for (int i = 0; i < arrowsParent.childCount; i++)
        {
            if (arrowsParent.GetChild(i).TryGetComponent(out Arrow arrow))
                activeArrows.Add(arrow);
        }
    }

    private void HandleSoundEnd()
    {
        arrowsParent.gameObject.SetActive(false);
        scoreSlider.gameObject.SetActive(false);
        //play some random animatons tany gher ely fel list
        endGameCanvasGroup.alpha = 1;
        if(score > 5000)
            endGameScoreText.text= winSceneText[Random.Range(0, winSceneText.Length)];
        else
            endGameScoreText.text = loseSceneText[Random.Range(0, loseSceneText.Length)];
        musicSource.Play();
    }

    private void CheckArrowTiming()
    {
        double currentTime = AudioSettings.dspTime;

        foreach (Arrow arrow in activeArrows)
        {
            double timeDifference = arrow.TargetTime - currentTime;

            if (Mathf.Abs((float)timeDifference) < hitWindowSeconds)
            {
                if (IsCorrectKeyPressed(arrow.ArrowType))
                {
                    HandleHit(arrow, Mathf.Abs((float)timeDifference));
                    break;
                }
            }
            else if (timeDifference < -0.5*hitWindowSeconds)
            {
                HandleMiss(arrow);
            }
        }
    }

    private void HandleHit(Arrow arrow, float timing)
    {
        string rating;
        int points;

        if (timing < 0.05f)
        {
            rating = "PERFECT";
            points = 50;
        }
        else if (timing < 0.10f)
        {
            rating = "GREAT";
            points = 35;
        }
        else
        {
            rating = "GOOD";
            points = 20;
        }

        score += points;
        OnArrowHit?.Invoke();
        Debug.Log($"{rating}! Score: {score}");
        if(arrow != null)
            arrow?.StartCoroutine(arrow?.AnimateAndDestroyArrow(arrow));
    }

    private void HandleMiss(Arrow arrow)
    {
        OnArrowMiss?.Invoke();
        score -= 30;
        Debug.Log($"MISS! Score: {score}");
        Destroy(arrow.gameObject);
    }

    private bool IsCorrectKeyPressed(ArrowType arrowType)
    {
        switch (arrowType)
        {
            case ArrowType.Up:
                return Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
            case ArrowType.Down:
                return Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
            case ArrowType.Left:
                return Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow);
            case ArrowType.Right:
                return Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow);
            default:
                return false;
        }
    }

    private void OnDestroy()
    {
        OnArrowHit.RemoveAllListeners();
        OnArrowMiss.RemoveAllListeners();
    }

    private void OnEnable()=>restartButton.onClick.AddListener(OnRestartClick);
    
    private void OnDisable()=>
        restartButton.onClick.RemoveListener(OnRestartClick); 

    private void OnRestartClick()=>
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
}