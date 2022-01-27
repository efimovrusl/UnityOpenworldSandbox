using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class MovementComponent : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 10;
    
    private float walkSpeed = 4.316f; // Steve walk speed
    private float runSpeed = 5.611f; // Steve run speed
    
    private Rigidbody _rigidbody;

    // Indexation: 0, 1, 2 (current position)
    private Vector3[] _previousPositions;

    public float Speed => 
        (_previousPositions[2] - _previousPositions[1])
        .magnitude / Time.fixedDeltaTime;

    public float Acceleration =>
        ((_previousPositions[2] - _previousPositions[1])
         - (_previousPositions[1] - _previousPositions[0]))
        .magnitude / Time.fixedDeltaTime / Time.fixedDeltaTime;

    public bool IsGrounded
    {
        get
        {
            return true;
        }
    }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        var pos = _rigidbody.position;
        _previousPositions = new[] {pos, pos, pos};
    }

    public void Move(Vector2 movement)
    {
        if (!IsGrounded) return;

        _rigidbody.AddRelativeForce(
        Mathf.Pow(Mathf.Abs(runSpeed * 1.2f - Speed), 0.2f) * Time.deltaTime * 10 *
        new Vector3(movement.x, 0, movement.y),
        ForceMode.VelocityChange);
    }

    public void Look(Vector2 delta)
    {
        
    }


    private void FixedUpdate()
    {
        _previousPositions[0] = _previousPositions[1];
        _previousPositions[1] = _previousPositions[2];
        _previousPositions[2] = _rigidbody.position;
        
        _rigidbody.AddForce(Time.fixedDeltaTime * Physics.gravity * 2, ForceMode.VelocityChange);
    }
}
