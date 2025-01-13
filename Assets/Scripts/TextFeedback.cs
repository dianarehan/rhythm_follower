using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFeedback : MonoBehaviour
{
    List<string> positiveWords = new List<string>
        {
            "Epic",
            "Lit",
            "Fire",
            "Winner",
            "Vibing",
            "Bet",
            "Hard",
            "Sick",
            "Hell yeah"
        };

    List<string> negativeWords = new List<string>
        {
            "Miss",
            "Mid",
            "Bruh",
            "Meh",
            "Womp",
            "Loser"
        };

    TextMeshProUGUI feedbackText;
    static int streak = 0;

    private void Awake()=>feedbackText = GetComponent<TextMeshProUGUI>();
    
    public void ShowGoodMsg()
    {
        if (streak > 3)
            feedbackText.text = "Combo x" + streak;
        
        else
        {
            feedbackText.text = positiveWords[Random.Range(0, positiveWords.Count)];
        }
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        streak++;
    }

    public void ShowBadMsg()
    {
        feedbackText.text = negativeWords[Random.Range(0, negativeWords.Count)];
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        streak = 0;
    }
}