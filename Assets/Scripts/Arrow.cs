using UnityEngine;
using System.Collections;

[System.Serializable]
public class Arrow : MonoBehaviour
{
    [SerializeField] private ArrowType arrowType;
    private float targetTime;
    private float startY;
    private float targetY;

    public ArrowType ArrowType => arrowType;
    public float TargetTime { get => targetTime; set => targetTime = value; }

    public void Initialize(float startYPosition, float targetYPosition, float targetHitTime)
    {
        RectTransform rect = GetComponent<RectTransform>();
        startY = startYPosition;
        targetY = targetYPosition;
        targetTime = targetHitTime;

        float xPos = GetXPositionForArrowType(arrowType);
        rect.anchoredPosition = new Vector2(xPos, startY);
    }

    private float GetXPositionForArrowType(ArrowType type)
    {
        switch (type)
        {
            case ArrowType.Up: return -300f;
            case ArrowType.Down: return -100f;
            case ArrowType.Left: return 100f;
            case ArrowType.Right: return 300f;
            default: return 0f;
        }
    }

    void Update()
    {
        float progress = (float)(1 - ((targetTime - AudioSettings.dspTime) / ArrowsGenerator.SPAWN_AHEAD_TIME));
        progress = Mathf.Clamp01(progress);

        float currentY = Mathf.Lerp(startY, targetY, progress);

        RectTransform rect = GetComponent<RectTransform>();
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, currentY);
    }

    public IEnumerator AnimateAndDestroyArrow(Arrow arrow)
    {
        RectTransform rect = arrow.GetComponent<RectTransform>();
        float elapsed = 0f;
        float duration = 0.2f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float scale = Mathf.Lerp(1f, 1.5f, elapsed / duration);
            rect.localScale = new Vector3(scale, scale, 1f);
            yield return null;
        }
        Destroy(arrow.gameObject);
    }
}