using System;
using UnityEngine;

[Serializable]
public class AudioData
{
    public AudioClip clip;

    [Range(0, 1f)] public float playTiming;
}
