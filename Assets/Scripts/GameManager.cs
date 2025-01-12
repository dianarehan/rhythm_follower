using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private RectTransform arrowsParent;
    [SerializeField] private RectTransform targetArrow;
    [SerializeField] private float allowedDistance = 30f;

    public event Action OnArrowHit;
    public event Action OnArrowMiss;

    private List<Arrow> arrows = new List<Arrow>();
    private int score = 0;

    private void Update()
    {
        GetAllArrows();
        CheckArrowsPosition();
    }

    private void GetAllArrows()
    {
        arrows.Clear();
        for (int i = 0; i < arrowsParent.childCount; i++)
        {
            Arrow arrow = arrowsParent.GetChild(i).GetComponent<Arrow>();
            if(arrow != null)
                arrows.Add(arrow);  
        }
    }

    private void CheckArrowsPosition()
    {
        foreach (Arrow arrow in arrows)
        {
            RectTransform arrowRectTransform = arrow.GetComponent<RectTransform>();
            float distance = Mathf.Abs(targetArrow.anchoredPosition.y - arrowRectTransform.anchoredPosition.y);
            if (distance < allowedDistance)
            {
                if (IsCorrectKeyPressed(arrow.ArrowType))
                {
                    OnArrowHit?.Invoke();
                    score += 10;
                    Debug.Log("Hit. Score: " + score);
                    Destroy(arrow.gameObject);
                }  
            }
            else if (arrowRectTransform.anchoredPosition.y < targetArrow.anchoredPosition.y - allowedDistance)
            {
                OnArrowMiss?.Invoke();
                score -= 5;
                Debug.Log("Miss. Score: " + score);
                Destroy(arrow.gameObject);
            }
        }
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