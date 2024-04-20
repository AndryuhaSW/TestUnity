using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public class HealingEnemy : Enemy
{
    [SerializeField] private float healthPoints;
    [SerializeField] private float healRadius;
    [SerializeField] private float healAmount;



    public override async UnityTask Initialize(List<Transform> forwardWayPoints,
        List<Transform> backWayPoints, float speed)
    {

        await base.Initialize(forwardWayPoints, backWayPoints, speed);

        health.Initialize(healthPoints);

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
                    //Какой то странный баг вылезает. Когда врач умирает из первой толпы, во второй здоровье ломается у последних трех. нихуя не поянл, но надо что-то делать
                    //enemy.PlusHealth(healAmount);
                }
            }
            
            await UnityTask.Delay(1000);
        }
    }

}
