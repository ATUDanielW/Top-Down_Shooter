using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

 [SerializeField] private float _speed;
[SerializeField] private float _rotationSpeed;
[SerializeField] private float _screenBorder;

private Rigidbody2D _rigidBody;
private Vector2 _movementInput;
private Vector2 _smoothedMovementInput;
private Vector2 _movementInputSmoothVelocity;
private Camera _camera;
private Animator _animator;

private void Awake()
{
    _rigidBody = GetComponent<Rigidbody2D>();
    _camera = Camera.main;
    _animator = GetComponent<Animator>();
}

private void FixedUpdate()
{
    SetPlayerVelocity();
    RotateInDirectionOfInput();
    SetAnimation();
}

private void SetAnimation()
{
    // Check if player is moving and update animator
    bool isMoving = _movementInput != Vector2.zero;
    _animator.SetBool("isMoving", isMoving);
}

private void SetPlayerVelocity()
{
    // Smooth out movement input
    _smoothedMovementInput = Vector2.SmoothDamp(
        _smoothedMovementInput,
        _movementInput,
        ref _movementInputSmoothVelocity,
        0.1f);

    // Apply velocity to Rigidbody
    _rigidBody.linearVelocity = _smoothedMovementInput * _speed;

    // Prevent player from going off-screen
    PreventPlayerGoingOffScreen();
}

private void PreventPlayerGoingOffScreen()
{
    Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);

    // Prevent horizontal exit
    if ((screenPosition.x < _screenBorder && _rigidBody.linearVelocity.x < 0) ||
        (screenPosition.x > _camera.pixelWidth - _screenBorder && _rigidBody.linearVelocity.x > 0))
    {
        _rigidBody.linearVelocity = new Vector2(0, _rigidBody.linearVelocity.y);
    }

    // Prevent vertical exit
    if ((screenPosition.y < _screenBorder && _rigidBody.linearVelocity.y < 0) ||
        (screenPosition.y > _camera.pixelHeight - _screenBorder && _rigidBody.linearVelocity.y > 0))
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
    // Called by Input System to update movement input
    _movementInput = inputValue.Get<Vector2>();
}
}
