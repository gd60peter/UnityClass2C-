using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMovement _characterMovement;
    [SerializeField] private Vector2 _moveInput;

    // callback from PlayerInput component
    public void OnMove(InputValue value)
    {
        // read vector2 value from InputValue
        _moveInput = value.Get<Vector2>();
    }

    private void Update()
    {
        // find move input based on camera direction
        Vector3 up = Vector3.up;
        Vector3 right = Camera.main.transform.right;
        Vector3 forward = Vector3.Cross(right, up);
        Debug.DrawRay(Camera.main.transform.position, forward * 3f, Color.green);

        Vector3 localMovement = right * _moveInput.x + forward * _moveInput.y;

        _characterMovement.SetMoveInput(localMovement);
        _characterMovement.SetLookDirection(forward);
    }
}
