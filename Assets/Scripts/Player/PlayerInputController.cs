using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementComponent))]
public class PlayerInputController : MonoBehaviour
{
    private PlayerControls _playerControls;

    private MovementComponent _movementComponent;
    // Start is called before the first frame update

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _movementComponent = GetComponent<MovementComponent>();


    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Update()
    {
        _movementComponent.Move(_playerControls.Land.Move.ReadValue<Vector2>());
    }
}
