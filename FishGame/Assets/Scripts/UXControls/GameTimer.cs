using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using System;

public class GameTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimerText;

    private void Update()
    {
        if (DataManager.Instance.IsTimeStarted)
        {
            TimerText.text = TimeSpan.FromSeconds((double)DataManager.Instance.TimePassed).ToString(@"mm\:ss");
        }
    }
}
