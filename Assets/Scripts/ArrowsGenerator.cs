using AYellowpaper.SerializedCollections;
using UnityEngine;
using System.Collections;

public class ArrowsGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform arrowsParent;
    [SerializeField, SerializedDictionary] private SerializedDictionary<ArrowType, Arrow> arrowKeyValue;
    [SerializeField] private float tempoBPM = 130f;
    [SerializeField] private AudioSource song;
    [SerializeField] private RectTransform targetRect;
    [SerializeField] private int beatsToReachTarget = 2;

    private float interval;
    private float nextTimeToGenerate;

    private void Start()
    {
        interval = 60f / tempoBPM;
        nextTimeToGenerate = interval;
        song.Play();
    }

    private void Update()
    {
        if (song.isPlaying && song.time >= nextTimeToGenerate)
        {
            GenerateRandomArrow();
            nextTimeToGenerate += interval;
        }
    }

    public void GenerateArrow(ArrowType arrowType)
    {
        Arrow arrow = Instantiate(arrowKeyValue[arrowType], arrowsParent);
        RectTransform arrowRect = arrow.GetComponent<RectTransform>();
        float distance = Mathf.Abs(arrowRect.anchoredPosition.y - targetRect.anchoredPosition.y);
        float time = (60f / tempoBPM) * beatsToReachTarget;
        arrow.SetSpeed(distance, time);
    }

    private void GenerateRandomArrow()
    {
        ArrowType[] arrowTypes = (ArrowType[])System.Enum.GetValues(typeof(ArrowType));
        ArrowType randomArrowType = arrowTypes[Random.Range(0, arrowTypes.Length)];
        GenerateArrow(randomArrowType);
    }
}
