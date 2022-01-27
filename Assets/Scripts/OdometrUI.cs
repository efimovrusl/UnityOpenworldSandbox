using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class OdometrUI : MonoBehaviour
{
    [SerializeField] private GameObject observable;
    private MovementComponent _observableMovementComponent;
    
    private TextMeshProUGUI _odometrUI;
    
    private void Awake()
    {
        _odometrUI = GetComponent<TextMeshProUGUI>();
        _observableMovementComponent = observable.GetComponent<MovementComponent>();
    }

    private void FixedUpdate()
    {
        _odometrUI.text = 
            $"Speed: {_observableMovementComponent.Speed:0.00} m/s\n" +
            $"Acceleration: {_observableMovementComponent.Acceleration:0.00} m/s^2";
    }
}
