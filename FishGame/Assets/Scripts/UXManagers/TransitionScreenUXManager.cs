using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransitionScreenUXManager : MonoBehaviour
{
    [SerializeField] Button NextLevelButton;
    [SerializeField] TextMeshProUGUI _coinScore;
    [SerializeField] TextMeshProUGUI _bonusScore;
    [SerializeField] TextMeshProUGUI _levelScore;
    [SerializeField] TextMeshProUGUI _totalScore;
    [SerializeField] GameObject nameView;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] GameObject nameEdit;
    [SerializeField] TMP_InputField nameInput;
    private readonly float _waitTime = .5f;
    private readonly int _coinMultiplier = 100;
    private string _name;

    private void Start()
    {
        NextLevelButton.Select();
        _name = DataManager.Instance.GetName();
        if (_name == string.Empty)
        {
            ShowEditName(true);
            nameInput.ActivateInputField();
        }
        else
            nameText.text = _name;

        DataManager.Instance.CalculateScore();
        StartCoroutine(LoadScores());
    }

    public void NextLevel()
    {
        GameManager.Instance.LoadNextLevel();
    }

    public void ReplayLevel()
    {
        GameManager.Instance.ReplayLevel();
    }

    private IEnumerator LoadScores()
    {
        var levelData = DataManager.Instance.GetCurrentLevelData();
        yield return new WaitForSeconds(_waitTime);
        _coinScore.text = " " + levelData.Coins + " X " + _coinMultiplier;
        yield return new WaitForSeconds(_waitTime);
        _bonusScore.text = " " + levelData.TimeBonus;
        yield return new WaitForSeconds(_waitTime);
        _levelScore.text = " " + levelData.Score;
        yield return new WaitForSeconds(_waitTime);
        _totalScore.text = " " + DataManager.Instance.GetScore();
        if (_name != string.Empty)
            SubmitScore();
    }

    private void ShowEditName(bool show)
    {
        nameView.SetActive(!show);
        nameEdit.SetActive(show);
    }

    private void SubmitScore()
    {
        LeaderboardManager.Instance.SubmitLootLockerScore((int)DataManager.Instance.GetScore());
    }

    public void EditName()
    {
        ShowEditName(true);
        nameInput.ActivateInputField();
    }

    public void SubmitName()
    {
        _name = nameInput.text;
        LeaderboardManager.Instance.SetUserName(nameInput.text, (string name) => {
            DataManager.Instance.SetName(_name);
            nameText.text = _name;
            ShowEditName(false);
            SubmitScore();
            NextLevelButton.Select();
        });
    }
}
