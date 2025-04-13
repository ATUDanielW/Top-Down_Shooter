using System;
using System.Security.Cryptography;
using NUnit.Framework.Internal;
using Unity.Mathematics;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float _speed;

    [SerializeField]private float _rotationSpeed;
    
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;//this means that the firt direction they are moving is the direction they facing
    }

    void FixedUpdate()
    {
        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange(); // call this method before the spotting of player so that they wonder around rndomly
        HandlePlayerTargeting();// call over method that detects player
        
    }

    private void HandleRandomDirectionChange()
    {
        _changeDirectionCooldown -= Time.deltaTime;
        if (_changeDirectionCooldown <= 0)
        {
            	float angleChange = UnityEngine.Random.Range(-90f, 90f);
                Quaternion rotation = Quaternion.AngleAxis(angleChange, transform.forward);
                _targetDirection = rotation * _targetDirection;

                _changeDirectionCooldown = UnityEngine.Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            _targetDirection = _playerAwarenessController.DirectionToPlayer;
        }
    }


    private void RotateTowardsTarget()
    {
        

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, _targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        _rigidbody.SetRotation(rotation);

    }

    private void SetVelocity()
    {
        
            _rigidbody.linearVelocity = transform.up * _speed;
        
    }
}

