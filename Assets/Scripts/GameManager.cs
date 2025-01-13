using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrowsParent;
    [SerializeField] private float hitWindowSeconds = 0.15f;
    [SerializeField] private AudioSource musicSource;

    public event System.Action OnArrowHit;
    public event System.Action OnArrowMiss;

    private List<Arrow> activeArrows = new List<Arrow>();
    private int score = 0;

    private void Update()
    {
        GetAllArrows();
        CheckArrowTiming();
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
            else if (timeDifference < -hitWindowSeconds)
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
            points = 100;
        }
        else if (timing < 0.10f)
        {
            rating = "GREAT";
            points = 75;
        }
        else
        {
            rating = "GOOD";
            points = 50;
        }

        score += points;
        OnArrowHit?.Invoke();
        Debug.Log($"{rating}! Score: {score}");
        Destroy(arrow.gameObject);
    }

    private void HandleMiss(Arrow arrow)
    {
        OnArrowMiss?.Invoke();
        score -= 5;
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
}