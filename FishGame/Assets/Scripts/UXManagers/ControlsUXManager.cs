using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ControlsUXManager : MonoBehaviour
{
    public UnityEvent _controlsFinished = new UnityEvent();

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            DataManager.Instance.InitialPauseComplete();
            _controlsFinished.Invoke();
        }
    }
}
