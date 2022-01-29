using System;
using UnityEngine;

public class LandSlider : MonoBehaviour
{
    private Vector3 _normal;

    private Vector3 _lastDirection;

    public bool IsGrounded { get; private set; } 
    
    
    public Vector3 Project(Vector3 inputDirection)
    {
        // making direction parallel to the ground
        Vector3 direction = AlignToSurface(inputDirection, _normal);
        
        // rotating to the transform's rotation
        _lastDirection = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * direction;

        if (Physics.SphereCast(transform.position + Vector3.up * 1.3f 
                               - _lastDirection.normalized * 0.09f, 0.3f,
                _lastDirection, out var hit, 0.3f))
        {
            var aligned = AlignToSurface(_lastDirection, hit.normal).normalized;

            if (Physics.SphereCast(transform.position + Vector3.up * 1.3f
                                   - _lastDirection.normalized * 0.09f, 0.3f,
                    aligned, out _, 0.3f))
            {
                _lastDirection = Vector3.zero;
                return _lastDirection;
            }
            
            _lastDirection += aligned * 20;
        }

        _lastDirection = _lastDirection.normalized;
        
        return _lastDirection;
    }

    private static Vector3 AlignToSurface(Vector3 vector, Vector3 normal)
    {
        return vector - Vector3.Dot(vector, normal) * normal;
    }

    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        var position = transform.position;
        Gizmos.DrawLine(position, position + _normal * 5);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(position + Vector3.up, 
            position + Vector3.up + _lastDirection);
    }


    private void FixedUpdate()
    {
        
        // one slightly smaller, second slightly wider
        float[] radiuses = new[] {0.38f, 0.45f}; 
        
        IsGrounded = false;
        for (int i = 0; i < 2; i++)
        {
            if (Physics.SphereCast(transform.position + Vector3.up, radiuses[i],
                    Vector3.down, out RaycastHit hit, 0.63f))
            {
                _normal += hit.normal;
                IsGrounded = true;
            }
            else
            {
                _normal += Vector3.up;
            }
        }
        _normal = _normal.normalized;
    }
}
