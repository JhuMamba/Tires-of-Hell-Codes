using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Behaviours/Avoid")]
public class AIAvoidanceBehaviour : Behaviour
{
    public override Vector2 CalculateMovement(Vector2 currentPosition, List<Transform> detected)
    {
        Vector2 avoidanceDirection = Vector2.zero;

        foreach (Transform car in detected)
        {
            Vector2 carPosition = car.position;
            Vector2 direction = carPosition - currentPosition;
            float distance = direction.magnitude;

            avoidanceDirection += direction.normalized / distance;
        }

        return avoidanceDirection.normalized;
    }
}
