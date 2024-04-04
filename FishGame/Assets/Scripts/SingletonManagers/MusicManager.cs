using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    private float _volume = 1f;
    [SerializeField] private AudioSource _musicSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _musicSource.Play();
    }

    public void ChangeMasterVolume(float volume)
    {
        _volume = volume;
    }

    public void PlayMusicClip(AudioClip clip, bool playOnce = false)
    {
        if (playOnce)
        {
            _musicSource.Stop();
            _musicSource.PlayOneShot(clip);
        }
        else
        {
            _musicSource.clip = clip;
            _musicSource.Play();
        }
    }

    public void StopMusicClip() {
        _musicSource.Stop();
    }
}
