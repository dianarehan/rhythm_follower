using UnityEngine;

public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumValue { get; private set; }
    private float[] audioSpcetrum;

    void Start()=> audioSpcetrum = new float[128];

    void Update()
    {
        // doesn't work on webGL
        //AudioListener.GetSpectrumData(audioSpcetrum, 0, FFTWindow.Hamming);
        
        audioSpcetrum[0] = Random.Range(0.0f, 1.0f);
        if (audioSpcetrum != null && audioSpcetrum.Length > 0)
            spectrumValue = audioSpcetrum[0] * 100;
    }
}