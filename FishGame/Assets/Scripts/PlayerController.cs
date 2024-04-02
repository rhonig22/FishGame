using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _horizontalInput, _currentVelocity = 0;
    private bool _jumpPressed;
    private const float _jumpForce = 5f;
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
    }

    private void FixedUpdate()
    {
        _currentVelocity = _playerRB.velocity.magnitude;
        Rotate(_horizontalInput * _rotationAngle);

        if (_jumpPressed)
        {
            Jump();
            _jumpPressed = false;
        }
    }

    private void Rotate(float angle)
    {
        transform.Rotate(Vector3.forward, angle);
        _playerRB.velocity = transform.up * _currentVelocity;
    }

    private void Jump()
    {
        _playerRB.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void EnterWater()
    {
        _playerRB.gravityScale = -1;
    }

    public void LeaveWater()
    {
        _playerRB.gravityScale = 1;
    }
}
