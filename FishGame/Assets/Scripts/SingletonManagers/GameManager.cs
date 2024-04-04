using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private readonly string _roomName = "Level_{0}";
    private readonly string _leaderboardSceneName = "Leaderboard";
    private readonly int _maxLevels = 1;
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
        _currentRoomId = 1;
        LoadNextBoss();
    }

    public void LevelFinished()
    {
        DataManager.Instance.PauseTimer();
        _currentRoomId++;
        LoadNextBoss();
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

    public void LoadNextBoss()
    {
        UnityAction loadNextBoss = () => { SceneManager.LoadScene(_roomName.Replace("{0}", _currentRoomId + "")); };
        StartCoroutine(WaitAndTransition(loadNextBoss, 0f));
    }

    private IEnumerator WaitAndTransition(UnityAction action, float transitionTime)
    {
        yield return new WaitForSeconds(transitionTime);
        _sceneTransition.RemoveAllListeners();
        _sceneTransition.AddListener(action);
        TransitionManager.Instance.FadeOut(() => { _sceneTransition.Invoke(); });
    }
}
