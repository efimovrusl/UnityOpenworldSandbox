using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(MovementComponent))]
public class PlayerInputController : MonoBehaviour
{
    private float xSensitivity = 1 / 10f;
    private float ySensitivity = 1 / 12f;
    
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
        _movementComponent.Look(new Vector2(
            _playerControls.Land.MouseX.ReadValue<float>() * xSensitivity,
            _playerControls.Land.MouseY.ReadValue<float>() * ySensitivity));
        _movementComponent.Move(_playerControls.Land.Move.ReadValue<Vector2>());
        // _movementComponent.Jump();
    }
}
