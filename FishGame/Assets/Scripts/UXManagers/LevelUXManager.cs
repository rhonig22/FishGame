using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static System.TimeZoneInfo;

public class LevelUXManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _coinCount;
    [SerializeField] TextMeshProUGUI _bonusScore;
    [SerializeField] AudioClip _waitSound;
    [SerializeField] AudioClip _goSound;
    [SerializeField] ControlsUXManager _controls;
    private LevelData _levelData;
    private bool _isLevelStarted = false;
    private const float _waitTime = .5f;
    private const int _numWaits = 3;
    private int _waitCount = 0;


    private void Start()
    {
        TimeManager.Instance.Pause(true);
        if (DataManager.Instance.ShouldPauseAtStart && _controls != null)
        {
            _controls.gameObject.SetActive(true);
            _controls._controlsFinished.AddListener(() => { _controls.gameObject.SetActive(false); StartTimer(); });
        }
        else
        {
            StartTimer();
        }
    }

    private void StartTimer()
    {
        StartCoroutine(WaitSound());
    }

    private IEnumerator WaitSound()
    {
        yield return new WaitForSecondsRealtime(_waitTime);
        SoundManager.Instance.PlaySound(_waitSound, transform.position);
        _waitCount++;
        if (_waitCount >= _numWaits)
            StartCoroutine(StartLevel());
        else
            StartCoroutine(WaitSound());

    }

    private IEnumerator StartLevel()
    {
        yield return new WaitForSecondsRealtime(_waitTime);
        SoundManager.Instance.PlaySound(_goSound, transform.position);
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
