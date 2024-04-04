using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;
using System.Threading;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider Bar;
    private readonly float _lerpTime = .1f;
    private float _lerpSpeed = 8f;
    private float _maxVal, _currentVal, _targetVal = 0;

    private void Update()
    {
        if (_currentVal != _targetVal)
        {
            if(Mathf.Abs(_currentVal - _targetVal) < 1)
            {
                _currentVal = _targetVal;
            }
            else
            {
                _currentVal = Mathf.MoveTowards(_currentVal, _targetVal, _lerpSpeed * Time.deltaTime);
            }
        }

        Bar.value = _currentVal / _maxVal;
    }

    public void SetInitialVal(float max)
    {
        _maxVal = max;
        _currentVal = max;
        _targetVal = max;
    }

    public void SetStartingHealth(float start)
    {
        _currentVal = start;
        _targetVal = start;
    }

    public void SetNewVal(float newVal)
    {
        _targetVal = newVal;
        _lerpSpeed = Mathf.Abs(_currentVal - _targetVal) / _lerpTime;
    }
}
