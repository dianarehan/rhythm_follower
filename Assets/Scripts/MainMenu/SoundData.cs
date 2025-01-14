using UnityEngine;

[System.Serializable]
public class SoundData
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private float tempo;
    [SerializeField] private float timeInSec;

    public AudioClip Clip => clip;
    public float Tempo => tempo;
    public float TimeInSec => timeInSec;
}