using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;
    private Camera _camera;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotateInDirectionOfInput();
    }

    private void SetPlayerVelocity()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
                    _smoothedMovementInput,
                    _movementInput,
                    ref _movementInputSmoothVelocity,
                0.1f);

        _rigidBody.linearVelocity = _smoothedMovementInput * _speed;

        PreventPlayerGoingOffScreen();
        
        
    }

    private void PreventPlayerGoingOffScreen()
    {
        //passing the position of the player to convert it to screen coordinates
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        //checking if the player is leaving the border and if so stopping their vlocity left and right X axis
        if((screenPosition.x <0 && _rigidBody.linearVelocity.x < 0) || (screenPosition.x > _camera.pixelWidth && _rigidBody.linearVelocity.x > 0))
        {
            _rigidBody.linearVelocity = new Vector2(0, _rigidBody.linearVelocity.y);
        }
        //Top or bottom Y axis
        if((screenPosition.y <0 && _rigidBody.linearVelocity.y < 0) || (screenPosition.y > _camera.pixelHeight && _rigidBody.linearVelocity.y > 0))
        {
            _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, 0);
        }
    }
    private void RotateInDirectionOfInput()
    {
        if (_movementInput != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _smoothedMovementInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

            _rigidBody.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
       _movementInput = inputValue.Get<Vector2>();
    }
}
