using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _horizontalInput, _currentVelocity = 0, _currentRotation = 0, _currentJump = 0, _jumpBuffer = 0;
    private bool _jumpPressed = false, _isStopped = false, _isJumping = false;
    private const float _maxVelocity = 30f, _jumpForce = 25f, _jumpTime = .7f, _gravityForce = .35f, _antiGravityForce = 12.5f, _jumpBufferTime = .1f,
            _angleThreshhold = 140f;
    private const float _rotationAngle = -6f;
    [SerializeField] private Rigidbody2D _playerRB;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _jumpPressed |= Input.GetButtonDown("Jump");
        UpdateJump();
        UpdateForces();
    }

    private void FixedUpdate()
    {
        Rotate(_horizontalInput * _rotationAngle);

        if (_jumpPressed || (!_isJumping && _jumpBuffer > 0))
        {
            Jump();
            _jumpPressed = false;
        }
    }

    private void UpdateJump()
    {
        _jumpBuffer -= Time.deltaTime;
        if (_currentJump > 0)
        {
            _currentJump -= _jumpForce / _jumpTime * Time.deltaTime;

            _currentJump = Mathf.Clamp(_currentJump, 0f, _currentJump); 
            if (_currentJump == 0f)
                _isJumping = false;
        }
    }

    private void UpdateForces()
    {
        if (!_isStopped)
        {
            if (_currentRotation >= 0 && _currentRotation <= 180)
            {
                _currentVelocity -= _antiGravityForce * Time.deltaTime;
            }
            else if (_currentRotation > 180 && _currentRotation <= 270)
            {
                _currentVelocity += (_currentRotation % 180) / 90 * _gravityForce;
            }
            else if (_currentRotation > 270 && _currentRotation < 360)
            {
                _currentVelocity += (180 - (_currentRotation % 180)) / 90 * _gravityForce;
            }
        }

        _currentVelocity = Mathf.Clamp(_currentVelocity, 0f, _maxVelocity);
        _playerRB.velocity = transform.up * ( _currentVelocity + _currentJump );
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

        _currentJump = _jumpForce;
        _isJumping = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            var normal = collision.GetContact(0).normal;
            var angle = Vector2.Angle(transform.up, normal);
            if (angle > _angleThreshhold)
            {
                _currentVelocity = 0;
                _isStopped = true;
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
}
