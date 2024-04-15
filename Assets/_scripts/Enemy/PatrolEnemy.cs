using System.Collections.Generic;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;


public class PatrolEnemy : Enemy
{
    [SerializeField] private float healthPoints;

    public override async UnityTask Initialize(List<Transform> forwardWayPoints,
        List<Transform> backWayPoints, float speed)
    {
        await base.Initialize(forwardWayPoints, backWayPoints, speed);

        health.Initialize(healthPoints);
    }
}
