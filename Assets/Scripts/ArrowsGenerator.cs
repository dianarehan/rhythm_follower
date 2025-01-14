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
    private float songDuration;

    public event System.Action OnSongEnd;

    private void Start()
    {
        tempoBPM= SoundSelector.Instance.CurrentSound.Tempo;
        songDuration = SoundSelector.Instance.CurrentSound.TimeInSec;

        beatsToSeconds = 60.0 / tempoBPM;
        double startTime = AudioSettings.dspTime + 2 + SPAWN_AHEAD_TIME;
        nextSpawnTime = startTime + firstBeatOffset;

        musicSource.PlayScheduled(startTime);
        StartCoroutine(GenerateArrowsRoutine(startTime));
    }

    private IEnumerator GenerateArrowsRoutine(double startTime)
    {
        while (true)
        {
            double currentTime = AudioSettings.dspTime;

            if (currentTime >= startTime && currentTime + SPAWN_AHEAD_TIME >= nextSpawnTime)
            {
                if (currentTime - startTime >= songDuration)
                {
                    OnSongEnd?.Invoke();
                    yield break;
                }
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