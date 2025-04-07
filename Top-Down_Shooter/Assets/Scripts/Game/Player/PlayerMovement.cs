using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;


public class PlayerMpvement : MonoBehaviour
{

    [SerializeField] private float _speed;
    private Rigidbody2D _rigidBody;
    private Vector2 _movementInput;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = _movementInput * _speed;
    }

    private void OnMove(InputValue inputValue)
    {
       _movementInput = inputValue.Get<Vector2>();
    }
}
