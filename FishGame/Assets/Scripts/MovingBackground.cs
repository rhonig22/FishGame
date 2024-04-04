using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    [SerializeField] private GameObject _camera;
    [SerializeField] private float _movementPercent;
    private Vector3 _previousPosition;

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = _camera.transform.position;
        Vector3 diff = currentPosition - _previousPosition;
        transform.localPosition += diff * -1 * _movementPercent;
        _previousPosition = currentPosition;
    }
}
