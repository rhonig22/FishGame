using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private PlayerData _playerData = new PlayerData();
    public float TimePassed { get; private set; } = 0f;
    public bool IsTimeStarted { get; private set; } = false;
    public bool ShouldPauseAtStart { get; private set; } = true;
    private string _userName;
    private string _userId;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        ResetData();
    }

    private void Update()
    {
        if (IsTimeStarted)
        {
            TimePassed += Time.deltaTime;
        }
    }
    public void StartTimer()
    {
        IsTimeStarted = true;
    }
    public void PauseTimer()
    {
        IsTimeStarted = false;
        SetTime(TimePassed);
    }

    public void ResetData()
    {
        _playerData = new PlayerData();
        TimePassed = 0f;
    }

    public void SetTime(float time)
    {
        _playerData.TimePassed = time;
    }

    public float GetTimeValue()
    {
        return _playerData.TimePassed;
    }

    public string GetTime()
    {
        return TimeSpan.FromSeconds((double)_playerData.TimePassed).ToString(@"mm\:ss");
    }

    public void SetName(string name)
    {
        _userName = name;
    }

    public string GetName() { return _userName; }

    public void SetId(string id)
    {
        _userId = id;
    }

    public string GetId() { return _userId; }

    public void InitialPauseComplete()
    {
        ShouldPauseAtStart = false;
    }
}
