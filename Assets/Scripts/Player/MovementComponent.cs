using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[
    RequireComponent(typeof(Rigidbody)),
    RequireComponent(typeof(LandSlider)),
]
public class MovementComponent : MonoBehaviour
{
    private float walkSpeed = 4.316f; // Steve walk speed
    private float runSpeed = 5.611f; // Steve run speed
    
    private Rigidbody _rigidbody;
    private LandSlider _landSlider;
    private GameObject _camera;
    
    // Numeration is ascending
    private Vector3[] _previousPositions;
    
    // 0.5s to slow
    private Vector2 _lastMovement;
    private float timePassed;
    
    public float Speed => 
        (_previousPositions[2] - _previousPositions[1])
        .magnitude / Time.fixedDeltaTime;

    public float Acceleration =>
        ((_previousPositions[2] - _previousPositions[1])
         - (_previousPositions[1] - _previousPositions[0]))
        .magnitude / Time.fixedDeltaTime / Time.fixedDeltaTime;

    public bool IsGrounded => _landSlider.IsGrounded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _landSlider = GetComponent<LandSlider>();
        _camera = transform.Find("MainCamera").gameObject;
        
        var pos = _rigidbody.position;
        _previousPositions = new[] {pos, pos, pos};
    }

    public void Move(Vector2 movement)
    {
        float multiplier;
        if (movement == Vector2.zero)
        {
            if (timePassed > 0.25f) return;
            multiplier = (0.25f - timePassed) * 4;
            timePassed += Time.deltaTime;
        }
        else
        {
            _lastMovement = movement;
            multiplier = 1;
            timePassed = 0;
        }
        Vector3 directionAlongSurface = _landSlider.Project(
            new Vector3(_lastMovement.x, 0, _lastMovement.y));
        Vector3 offset = multiplier * directionAlongSurface * (runSpeed * Time.deltaTime);

        _rigidbody.MovePosition(_rigidbody.position + offset);
    }

    public void Jump(float force)
    {
        if (!IsGrounded) return;
        _rigidbody.AddForce(Vector3.up * 1, ForceMode.VelocityChange);
    }

    public void Look(Vector2 lookDelta)
    {
        transform.Rotate(0, lookDelta.x, 0);
        _camera.transform.Rotate(-lookDelta.y, 0, 0);
    }


    private void FixedUpdate()
    {
        _previousPositions[0] = _previousPositions[1];
        _previousPositions[1] = _previousPositions[2];
        _previousPositions[2] = _rigidbody.position;

        
        _rigidbody.AddForce(
            Time.fixedDeltaTime * Physics.gravity
            * (IsGrounded ? 0.3f : 2.9f), ForceMode.VelocityChange);
    }
}
