using AYellowpaper.SerializedCollections;
using UnityEngine;
using System.Collections;

public class ArrowsGenerator : MonoBehaviour
{
    public const float SPAWN_AHEAD_TIME = 2f;

    [SerializeField] private Transform arrowsParent;
    [SerializeField, SerializedDictionary] private SerializedDictionary<ArrowType, Arrow> arrowKeyValue;
    [SerializeField] private RectTransform targetArrowsContainer;
    [SerializeField] private float tempoBPM = 120f;
    [SerializeField] private AudioSource musicSource;

    private double nextSpawnTime;
    private double beatsToSeconds;
    private double firstBeatOffset = 1;
    private float spawnY = 800f;
    private float targetY = 50f;

    private void Start()
    {
        beatsToSeconds = 60.0 / tempoBPM;
        nextSpawnTime = AudioSettings.dspTime + firstBeatOffset;

        musicSource.PlayScheduled(AudioSettings.dspTime+3 + SPAWN_AHEAD_TIME);
        StartCoroutine(GenerateArrowsRoutine());
    }

    private IEnumerator GenerateArrowsRoutine()
    {
        while (true)
        {
            double currentTime = AudioSettings.dspTime;

            if (currentTime + SPAWN_AHEAD_TIME >= nextSpawnTime)
            {
                GenerateRandomArrow(nextSpawnTime);
                nextSpawnTime += beatsToSeconds;
            }
            yield return null;
        }
    }

    private void GenerateRandomArrow(double targetHitTime)
    {
        ArrowType[] arrowTypes = (ArrowType[])System.Enum.GetValues(typeof(ArrowType));
        ArrowType randomArrowType = arrowTypes[Random.Range(0, arrowTypes.Length)];

        Arrow arrowPrefab = arrowKeyValue[randomArrowType];
        Arrow spawnedArrow = Instantiate(arrowPrefab, arrowsParent);

        spawnedArrow.Initialize(spawnY, targetY, (float)targetHitTime);
    }
}