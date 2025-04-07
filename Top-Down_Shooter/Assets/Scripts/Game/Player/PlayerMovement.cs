using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class PlayerMpvement : MonoBehaviour
{

    [SerializeField] private float _speed;
    private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;
    private Vector2 _smoothedMovementInput;
    private Vector2 _movementInputSmoothVelocity;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _smoothedMovementInput = Vector2.SmoothDamp(
            _smoothedMovementInput,
            _movementInput,
            ref _movementInputSmoothVelocity,
        0.1f);

        _rigidBody.linearVelocity = _smoothedMovementInput * _speed;
    }

    private void OnMove(InputValue inputValue)
    {
       _movementInput = inputValue.Get<Vector2>();
    }
}
