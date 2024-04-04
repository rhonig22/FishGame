using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private float _volume = 1f;
    [SerializeField] private AudioSource _soundEffectsSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        AudioSource audioSource = Instantiate(_soundEffectsSource, position, Quaternion.identity);
        audioSource.clip = clip;
        audioSource.volume = _volume;
        audioSource.Play();
        float clipLength = clip.length;
        Destroy(audioSource.gameObject, clipLength);
    }

    public void ChangeMasterVolume(float volume)
    {
        _volume = volume;
    }
}
