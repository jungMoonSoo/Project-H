using System;
using UnityEngine;

[Serializable]
public class AudioData
{
    public AudioClip clip;

    [Range(0.01f, 9.99f)] public float playTiming = 0.01f;
}
