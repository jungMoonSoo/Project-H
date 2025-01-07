using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UnidadAudioHandle : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] sounds;

    private void Start() => TryGetComponent(out audioSource);

    public void OnPlay(int index) => audioSource.PlayOneShot(sounds[index]);
}
