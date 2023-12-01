using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class CarControllerUnit : MonoBehaviour
{
    public abstract Vector2 CarMovement();
}
