using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }
    private float[] audioSpcetrum;

    void Start()=> audioSpcetrum = new float[128];

    void Update()
    {
        AudioListener.GetSpectrumData(audioSpcetrum, 0, FFTWindow.Hamming);
        if (audioSpcetrum != null && audioSpcetrum.Length > 0)
            spectrumValue = audioSpcetrum[0] * 100;
    }
}