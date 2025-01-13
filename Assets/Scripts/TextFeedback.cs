using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFeedback : MonoBehaviour
{
   [SerializeField] private List<string> positiveWords = new List<string>
        {
            "Epic",
            "Lit",
            "Fire",
            "Vibing",
            "Bet",
            "Hard",
            "Sick",
            "Hell yeah"
        };

    [SerializeField] private List<string> negativeWords = new List<string>
        {
            "Miss",
            "Mid",
            "Bruh",
            "Meh",
            "Loser"
        };

    private float displayDuration = 2f;
    private TextMeshProUGUI feedbackText;
    private static int streak = 0;
    private int counter = 0;
    private bool isCooldownActive = false;
    private float cooldownDuration = 1f;

    private void Awake()
    {
        cooldownDuration= 3f;
        feedbackText = GetComponent<TextMeshProUGUI>();
    }
    
    public void ShowGoodMsg()
    {
        if (isCooldownActive) return;

        if (counter %2==0)
        {
            if (streak > 3)
                feedbackText.text = "Combo x" + streak;
            cooldownDuration = 1f;
        }
        else
        {
            feedbackText.text = positiveWords[Random.Range(0, positiveWords.Count)];
            cooldownDuration = 2f;
        }
       counter++;
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        StartCoroutine(Cooldown());
        streak++;
    }

    public void ShowBadMsg()
    {
        streak = 0;
        if (isCooldownActive) return;
        
        feedbackText.text = negativeWords[Random.Range(0, negativeWords.Count)];
        gameObject.SetActive(false);
        gameObject.SetActive(true);
        cooldownDuration = 2f;
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        isCooldownActive = true;
        yield return new WaitForSeconds(cooldownDuration);
        isCooldownActive = false;
    }
}