using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenuUXManager : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    public bool IsPaused { get; private set; } = false;
    public bool DoNotStartTimeOnUnPause = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            Pause();
        }
    }

    public void Pause()
    {
        IsPaused = !IsPaused;
        if (!DoNotStartTimeOnUnPause)
        {
            TimeManager.Instance.Pause(IsPaused);
        }
        else
        {
            DoNotStartTimeOnUnPause = false;
        }

        _pauseMenu.SetActive(IsPaused);
    }
}
