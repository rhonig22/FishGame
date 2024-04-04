using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrackPlayer : MonoBehaviour
{
    public bool playOnStart = false;
    public bool playOnce = false;
    [SerializeField] private AudioClip _clip;

    // Start is called before the first frame update
    void Start()
    {
        if (playOnStart)
            PlayTrack();
    }

    public void PlayTrack()
    {
        MusicManager.Instance.PlayMusicClip(_clip, playOnce);
    }
}
