using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public readonly int MaxLevels = 3;
    private readonly string _roomName = "Level_{0}";
    private readonly string _leaderboardSceneName = "Leaderboard";
    private readonly string _transitionSceneName = "Transition";
    private int _currentRoomId = 1;
    private UnityEvent _sceneTransition = new UnityEvent();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // called second
    private void OnLevelWasLoaded(int level)
    {
        TransitionManager.Instance.FadeIn(() => { });
    }

    public void StartGame()
    {
        DataManager.Instance.PauseTimer();
        DataManager.Instance.ResetData();
        _currentRoomId = 0;
        LoadNextLevel();
    }

    public void EndGame()
    {
        DataManager.Instance.PauseTimer();
    }

    public void LoadLeaderboard()
    {
        UnityAction loadEndScene = () => { SceneManager.LoadScene(_leaderboardSceneName); };
        StartCoroutine(WaitAndTransition(loadEndScene, 0f));
    }

    public void LoadTransition()
    {
        UnityAction loadTransition = () => { SceneManager.LoadScene(_transitionSceneName); };
        StartCoroutine(WaitAndTransition(loadTransition, 0f));
    }

    public void ReplayLevel()
    {
        DataManager.Instance.ResetCurrentLevel();
        UnityAction loadNext = () => { SceneManager.LoadScene(_roomName.Replace("{0}", _currentRoomId + "")); };
        StartCoroutine(WaitAndTransition(loadNext, 0f));
    }

    public void LoadNextLevel()
    {
        IncreaseLevel();
        UnityAction loadNext = () => { SceneManager.LoadScene(_roomName.Replace("{0}", _currentRoomId + "")); };
        StartCoroutine(WaitAndTransition(loadNext, 0f));
    }

    private void IncreaseLevel()
    {
        if (_currentRoomId < MaxLevels)
        {
            _currentRoomId++;
            DataManager.Instance.SetLevel(_currentRoomId);
        }
    }

    private IEnumerator WaitAndTransition(UnityAction action, float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);
        _sceneTransition.RemoveAllListeners();
        _sceneTransition.AddListener(action);
        TransitionManager.Instance.FadeOut(() => { _sceneTransition.Invoke(); });
    }
}
