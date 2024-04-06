using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;


public class PatrolEnemy : Enemy
{

    public override async UnityTask Initialize(List<Transform> forwardWayPoints,
        List<Transform> backWayPoints, float speed)
    {
        base.Initialize(forwardWayPoints, backWayPoints, speed);

        //Debug.Log("Spawn PatrolEnemy");
    }

    private void Start()
    {
        _healthComponent.Initialize(100f);
    }

}
