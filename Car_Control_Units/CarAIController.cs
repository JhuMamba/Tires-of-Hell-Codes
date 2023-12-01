using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class CarAIController : CarControllerUnit
{
    [SerializeField] AIFollowTargetBehaviour followBehaviour;
    [SerializeField] AIAvoidanceBehaviour avoidanceBehaviour;

    [SerializeField] private string targetTag = "Player";
    [SerializeField] private float detectRange;
    [SerializeField] private float slowRange;
    [SerializeField] private Transform targetPos;
    public override Vector2 CarMovement()
    {
        DetectCars();
        return CalculateAIMovement();
    }
    private void DetectCars()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(gameObject.transform.position, detectRange);
        foreach(Collider2D collider in collider2Ds)
        {
            if (collider.CompareTag(targetTag)) targetPos = collider.transform;
        }
    }
    private Vector2 CalculateAIMovement()
    {
        if (targetPos == null) return Vector2.zero;
        //float targetDistance = Vector2.Distance(transform.position,targetPos.position);
        float forward, steer;

        Vector2 dirToMove = (Vector2)(targetPos.position - transform.position).normalized;
        float dot = Vector2.Dot(dirToMove, transform.up);

        float angleToDir = Vector2.SignedAngle(dirToMove, transform.up);
        forward = dot;
        steer = angleToDir / 100.0f;

        return new Vector2(steer, forward);

    }
}
