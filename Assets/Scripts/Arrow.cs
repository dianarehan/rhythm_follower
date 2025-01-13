using UnityEngine;

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
}