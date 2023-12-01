using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Behaviours/Follow")]
public class AIFollowTargetBehaviour : Behaviour
{
    public override Vector2 CalculateMovement(Vector2 currentPosition, List<Transform> detected)
    {
        if (detected == null) return Vector2.zero;

        Vector2 targetDirection = Vector2.zero;
        foreach (Transform t in detected)
        {
            targetDirection += (Vector2)t.position - currentPosition;
        }
        return targetDirection.normalized;
    }
}
