using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreenUXManager : MonoBehaviour
{
    [SerializeField] Button NextLevelButton;

    private void Start()
    {
        NextLevelButton.Select();
    }

    public void NextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void ReplayLevel()
    {
        GameManager.Instance.ReplayLevel();
    }
}
