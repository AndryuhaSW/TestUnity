using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public class HealingEnemy : Enemy
{
    [SerializeField] private float healRadius;
    [SerializeField] private float healAmount; 
    public override async UnityTask Initialize(List<Transform> forwardWayPoints,
        List<Transform> backWayPoints, float speed)
    {
        base.Initialize(forwardWayPoints, backWayPoints, speed);
        //Debug.Log("Spawn HealingEnemy");

        HealEnemies();
    }

    private async UnityTask HealEnemies()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, healRadius);

            foreach (Collider2D collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null && enemy != this)
                {
                    enemy.PlusHealth(healAmount);
                }
            }
            
            await UnityTask.Delay(1000);
        }
    }

    private void Start()
    {
        _healthComponent.Initialize(150f);
    }


}
