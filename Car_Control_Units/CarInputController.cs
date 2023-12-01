using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CarInputController : CarControllerUnit
{
    PlayerInput playerInput;
    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }
    public override Vector2 CarMovement()
    {
        Vector2 inputVector = playerInput.actions["Move"].ReadValue<Vector2>();
        return new Vector2(inputVector.x, inputVector.y);
    }

}
