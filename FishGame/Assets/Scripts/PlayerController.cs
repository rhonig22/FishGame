using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static System.TimeZoneInfo;

public class PlayerController : MonoBehaviour
{
    private float _horizontalInput, _currentVelocity = 0, _currentRotation = 0, _currentJump = 0, _jumpBuffer = 0;
    private bool _jumpPressed = false, _isStopped = false, _isJumping = false;
    private const float _maxVelocity = 35f, _jumpForce = 25f, _ringForce = 10f, _jumpTime = .7f, _gravityForce = .5f, _antiGravityForce = 8f, _jumpBufferTime = .1f,
            _angleThreshhold = 140f, _finishZoom = 12f, _slowFactor = .05f, _slowTime = 2f, _transitionTime = 1f;
    private const float _rotationAngle = -5f, _dragForce = -.25f;
    private const int _pointDecrease = 5;
    [SerializeField] private Rigidbody2D _playerRB;
    [SerializeField] private AudioClip _jumpSound;
    [SerializeField] private AudioClip _wallSound;
    [SerializeField] private GameObject _ringPrefab;
    [SerializeField] private GameObject _ringContainer;
    [SerializeField] private bool _devMode;
    public UnityEvent triggerScreenShake = new UnityEvent();
    public UnityEvent<float> triggerFinish = new UnityEvent<float>();
    private float _ringStep = .4f;
    private float _currentStep = 0;

    public void CollectRing()
    {
        _currentJump += _ringForce;
    }
    public void FinishLevel()
    {
        // TODO
        DataManager.Instance.PauseTimer();
        triggerFinish.Invoke(_finishZoom);
        TimeManager.Instance.DoSlowmotion(_slowFactor, _slowTime);
        StartCoroutine(TriggerNextLevel());
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _jumpPressed |= Input.GetButtonDown("Jump");

        if (_devMode)
        {
            _currentStep += Time.deltaTime;
            if (_currentStep > _ringStep)
            {
                var newRing = Instantiate(_ringPrefab, _ringContainer.transform);
                newRing.transform.position = transform.position;
                _currentStep -= _ringStep;
            }
        }
    }

    private void FixedUpdate()
    {
        Rotate(_horizontalInput * _rotationAngle);

        if (_jumpPressed || (!_isJumping && _jumpBuffer > 0))
        {
            Jump();
            _jumpPressed = false;
        }

        UpdateJump();
        UpdateForces();
    }

    private void UpdateJump()
    {
        _jumpBuffer -= Time.fixedDeltaTime;
        if (_currentJump > 0)
        {
            _currentJump -= _jumpForce / _jumpTime * Time.fixedDeltaTime;

            _currentJump = Mathf.Clamp(_currentJump, 0f, _currentJump); 
            if (_currentJump == 0f)
                _isJumping = false;
        }
    }


    private void UpdateForces()
    {
        Vector3 dragForce = Vector3.zero;
        if (_isStopped)
        {
            dragForce = transform.up * _currentVelocity * _dragForce;
        }

        if (_currentRotation >= 0 && _currentRotation <= 180)
        {
            _currentVelocity -= _antiGravityForce * Time.fixedDeltaTime;
        }
        else if (_currentRotation > 180 && _currentRotation <= 270)
        {
            _currentVelocity += (_currentRotation % 180) / 90 * _gravityForce;
        }
        else if (_currentRotation > 270 && _currentRotation < 360)
        {
            _currentVelocity += (180 - (_currentRotation % 180)) / 90 * _gravityForce;
        }

        _currentVelocity = Mathf.Clamp(_currentVelocity, 0f, _maxVelocity);
        _playerRB.velocity = transform.up * ( _currentVelocity + _currentJump ) + dragForce;
    }

    private void Rotate(float angle)
    {
        if (angle == 0)
            return;

        _currentRotation = (_currentRotation + angle + 360f) % 360f;
        transform.Rotate(Vector3.forward, angle);
    }

    private void Jump()
    {
        if (_isJumping)
        {
            _jumpBuffer = _jumpBufferTime;
            return;
        }

        SoundManager.Instance.PlaySound(_jumpSound, transform.position);
        _currentJump = _jumpForce;
        _isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            SoundManager.Instance.PlaySound(_wallSound, transform.position);
            DataManager.Instance.DecreaseBonus(_pointDecrease);
            _isStopped = true;
            var normal = collision.GetContact(0).normal;
            var angle = Vector2.Angle(transform.up, normal);
            if (angle > _angleThreshhold)
            {
                _currentVelocity = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            _isStopped = false;
        }
    }

    private IEnumerator TriggerNextLevel()
    {
        yield return new WaitForSeconds(_transitionTime);
        GameManager.Instance.LoadTransition();
    }
}
