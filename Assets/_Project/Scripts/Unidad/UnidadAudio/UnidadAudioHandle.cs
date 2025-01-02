using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnidadAudioHandle : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioData[] audioDatas;

    private void Start() => TryGetComponent(out audioSource);

    public float GetPlayTiming(int index) => audioDatas[index].playTiming;

    public void OnPlay(int index) => audioSource.PlayOneShot(audioDatas[index].clip);
}
