using UnityEngine;

public class AudioSyncer : MonoBehaviour
{
    [SerializeField] private float bias; //what spectrum value will trigger our beat
    [SerializeField] private float timeStep; //min interval between beats
    public float timeToBeat; //how much time before visialization completes
    public float restSmoothTime; //how fast we return to resting position after a beat

    private float previousAudioValue;
    private float audioValue;
    private float timer;

    protected bool isBeat;

    void Update() => OnUpdate();

    public virtual void OnUpdate()
    {
        previousAudioValue = audioValue;
        audioValue = AudioSpectrum.spectrumValue;
        if (previousAudioValue > bias && audioValue <= bias)
            if (timer > timeToBeat)
                OnBeat();
        if (previousAudioValue <= bias && audioValue > bias)
            if (timer > timeToBeat)
                OnBeat();
        timer += Time.deltaTime;
        previousAudioValue = audioValue;
    }

    public virtual void OnBeat()
    {
        Debug.Log("Beat");
        timer = 0;
        isBeat = true;
    }
}