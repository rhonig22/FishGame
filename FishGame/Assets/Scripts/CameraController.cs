using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    private PlayerController _playerController;
    private CinemachineBasicMultiChannelPerlin _noisePerlin;
    private readonly float _shakeAmplitude = 2f;
    private readonly float _shakeFrequency = 2f;
    private readonly float _shakeTime = .2f;
    private float _shakeTimeElapsed = 0, _startingZoom, _endingZoom, _zoomTime;
    private bool _isShaking = false;
    private bool _isZooming = false;

    // Start is called before the first frame update
    void Start()
    {
        _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _playerController.triggerScreenShake.AddListener(() => { ShakeCamera(); });
        _playerController.triggerFinish.AddListener((float zoom) => { ZoomIn(zoom); });
        _noisePerlin = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (_isShaking)
        {
            _shakeTimeElapsed += Time.deltaTime;
            if (_shakeTimeElapsed > _shakeTime)
            {
                StopShake();
            }
        }

        if (_isZooming)
        {
            _zoomTime += Time.deltaTime;
            _mainCamera.m_Lens.OrthographicSize = Mathf.Lerp(_startingZoom, _endingZoom, _zoomTime);
            if (_mainCamera.m_Lens.OrthographicSize == _endingZoom)
                _isZooming = false;
        }
    }

    private void ShakeCamera()
    {
        _noisePerlin.m_AmplitudeGain = _shakeAmplitude;
        _noisePerlin.m_FrequencyGain = _shakeFrequency;
        _shakeTimeElapsed = 0;
        _isShaking = true;
    }

    private void StopShake()
    {
        _noisePerlin.m_AmplitudeGain = 0;
        _noisePerlin.m_FrequencyGain = 0;
        _isShaking = false;
    }

    private void ZoomIn(float zoom)
    {
        _isZooming = true;
        _startingZoom = _mainCamera.m_Lens.OrthographicSize;
        _endingZoom = zoom;
        _zoomTime = 0;
    }
}