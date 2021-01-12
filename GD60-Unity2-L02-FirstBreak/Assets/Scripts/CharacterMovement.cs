using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody _rigidbody;

    [Header("Movement")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _acceleration = 10f;
    [SerializeField] private float _turnSpeed = 10f;

    // this is a property, similar to field (normal variable), but we have made it publicly readable, but only settable privately
    public Vector3 MoveInput {get; private set;}
    public Vector3 LookDirection {get; private set;}

    public void SetMoveInput(Vector3 input)
    {
        // flatten input
        input.y = 0f;
        // clamp and set input
        MoveInput = Vector3.ClampMagnitude(input, 1f);
    }

    public void SetLookDirection(Vector3 direction)
    {
        direction.y = 0f;
        LookDirection = direction.normalized;
    }

    private void FixedUpdate()
    {
        // calculate target velocity and difference from current
        Vector3 targetVelocity = MoveInput * _speed;
        Vector3 velocityDiff = targetVelocity - _rigidbody.velocity;
        velocityDiff.y = 0f;

        // get acceleration towards target velocity
        Vector3 acceleration = velocityDiff * _acceleration;
        _rigidbody.AddForce(acceleration);

        // rotate the character
        Quaternion targetRotation = Quaternion.LookRotation(LookDirection);     // creates a (quaternion) rotation based on an input direction
        Quaternion rotation = Quaternion.Slerp(transform.rotation, targetRotation, _turnSpeed * Time.fixedDeltaTime);
        _rigidbody.MoveRotation(rotation);
    }
}
