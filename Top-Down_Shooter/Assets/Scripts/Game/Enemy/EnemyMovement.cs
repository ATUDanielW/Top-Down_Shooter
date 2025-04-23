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

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _screenBorder;

    [SerializeField] private float _obstacleCheckCircleRadius;

    [SerializeField] private float _obstacleCheckDistance;

    [SerializeField] private LayerMask _obstacleLayerMask;
    
    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _targetDirection;
    private float _changeDirectionCooldown;
    private Camera _camera;
    private RaycastHit2D[] _obstacleCollisions;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
        _targetDirection = transform.up;//this means that the firt direction they are moving is the direction they facing
        _camera = Camera.main;
        _obstacleCollisions = new RaycastHit2D[10];
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
        HandleEnemyOffScreen();
        HandleObstacles();
        
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

    private void HandleEnemyOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        if((screenPosition.x <_screenBorder && _targetDirection.x < 0) || 
        (screenPosition.x > _camera.pixelWidth - _screenBorder && _targetDirection.x > 0))
        {
            _targetDirection = new Vector2(-_targetDirection.x, _targetDirection.y);
        }
        //Top or bottom Y axis
        if((screenPosition.y < _screenBorder && _targetDirection.y < 0) || 
        (screenPosition.y > _camera.pixelHeight - _screenBorder && _targetDirection.y > 0))
        {
            _targetDirection = new Vector2(_targetDirection.x, -_targetDirection.y);
        }
    }

    private void HandleObstacles()
    {
        var contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(_obstacleLayerMask);

        int numberOfCollisions = Physics2D.CircleCast(
            transform.position,
            _obstacleCheckCircleRadius,
            transform.up,
            contactFilter,
            _obstacleCollisions,
            _obstacleCheckDistance
        );

        for (int index = 0; index < numberOfCollisions; index++)
        {
            var obstacleCollision = _obstacleCollisions[index];

            if (obstacleCollision.collider.gameObject == gameObject)
            {
                continue;
            }

            _targetDirection = obstacleCollision.normal;
            break;
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

