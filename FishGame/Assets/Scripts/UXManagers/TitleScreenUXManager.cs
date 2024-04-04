using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleScreenUXManager : MonoBehaviour
{
    [SerializeField] Button PlayButton;

    private void Start()
    {
        PlayButton.Select();
    }

    public void StartGame()
    {
        GameManager.Instance.StartGame();
    }

    public void ViewLeaderboard()
    {
        GameManager.Instance.LoadLeaderboard();
    }
}
