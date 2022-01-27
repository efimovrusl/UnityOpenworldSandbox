using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandSlider : MonoBehaviour
{
    private Vector3 _normal;
    private int _collisions = 0;
    
    public Vector3 Project(Vector3 forward)
    {
        return forward - Vector3.Dot(forward, _normal) * _normal;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Hey fuck");
        _normal = collision.contacts[0].normal;
        Debug.Log($"{Project(Vector3.forward)}, {_normal}");

    }

    private void OnCollisionExit(Collision other)
    {
        _normal = Vector3.up;
    }

}
