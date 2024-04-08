using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.TimeZoneInfo;

public class LevelUXManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coinCount;
    [SerializeField] TextMeshProUGUI _bonusScore;
    private LevelData _levelData;
    private bool _isLevelStarted = false;
    private readonly float _startTime = 3f;


    private void Start()
    {
        StartCoroutine(StartLevel());
        TimeManager.Instance.Pause(true);
    }

    private IEnumerator StartLevel()
    {
        yield return new WaitForSecondsRealtime(_startTime);
        _isLevelStarted = true;
        TimeManager.Instance.Pause(false);
        DataManager.Instance.StartTimer();
    }

    private void Update()
    {
        if (!_isLevelStarted)
        {
            return;
        }

        _levelData = DataManager.Instance.GetCurrentLevelData();
        if (DataManager.Instance.IsTimeStarted)
        {
            _bonusScore.text = "Bonus: " + _levelData.TimeBonus;
        }

        _coinCount.text = "Coins: " + _levelData.Coins;
    }
}
