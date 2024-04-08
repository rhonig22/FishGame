using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private PlayerData _playerData;
    public float TimePassed { get; private set; } = 0f;
    public int CurrentLevel { get; private set; } = 0;
    public bool IsTimeStarted { get; private set; } = false;
    public bool ShouldPauseAtStart { get; private set; } = true;
    private LevelData _currentLevelData;
    private float _currentScoreDecrement = 0;
    private readonly float _scoreDecrement = .05f;
    private readonly int _coinScore = 100;
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
    }

    private void Start()
    {
        ResetData();
        SetLevel(1);
    }

    private void Update()
    {
        if (IsTimeStarted)
        {
            TimePassed += Time.deltaTime;
            _currentScoreDecrement += Time.deltaTime;
            if (_currentScoreDecrement > _scoreDecrement)
            {
                _currentLevelData.TimeBonus--;
                _currentLevelData.TimeBonus = Mathf.Clamp(_currentLevelData.TimeBonus, 0, _currentLevelData.TimeBonus);
                _currentScoreDecrement -= _scoreDecrement;
            }
        }
    }

    public void StartTimer()
    {
        IsTimeStarted = true;
    }
    public void PauseTimer()
    {
        IsTimeStarted = false;
    }

    public void ResetData()
    {
        _playerData = new PlayerData(GameManager.Instance.MaxLevels);
        TimePassed = 0f;
    }

    public void SetLevel(int level)
    {
        CurrentLevel = level - 1;
        _currentLevelData = _playerData.Levels[CurrentLevel];
    }

    public void AddCoin()
    {
        _currentLevelData.Coins++;
    }

    public LevelData GetCurrentLevelData()
    {
        return GetLevelData(CurrentLevel);
    }

    public void ResetCurrentLevel()
    {
        _playerData.Levels[CurrentLevel] = new LevelData();
        _currentLevelData = _playerData.Levels[CurrentLevel];
    }

    public LevelData GetLevelData(int level)
    {
        return _playerData.Levels[level];
    }

    public void SetName(string name)
    {
        _userName = name;
    }

    public string GetName() { return _userName; }

    public void CalculateScore()
    {
        _currentLevelData.Score = _currentLevelData.Coins * _coinScore + _currentLevelData.TimeBonus;
        _playerData.CurrentHighScore = 0;
        for (int i = 0; i < _playerData.Levels.Length; i++)
        {
            _playerData.CurrentHighScore += _playerData.Levels[i].Score;
        }
    }

    public float GetScore()
    {
        return _playerData.CurrentHighScore;
    }

    public void DecreaseBonus(int points)
    {
        _currentLevelData.TimeBonus -= points;
        _currentLevelData.TimeBonus = Mathf.Clamp(_currentLevelData.TimeBonus, 0, _currentLevelData.TimeBonus);
    }

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
