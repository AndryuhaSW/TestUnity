using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.U2D.IK;
using UnityTask = System.Threading.Tasks.Task;


public class TestTower : Tower
{
    [SerializeField] private int _damage;

    public override async UnityTask Initialize()
    {
        await base.Initialize();

        StartWork();
    }

    public async UnityTask StartWork()
    {
        while (true)
        {
            if (enemies.Count > 0)
            {
                Enemy enemy = enemies.Peek();
                enemy.MinusHealth(_damage);
                await UnityTask.Delay(400);
                continue;
            }

            await UnityTask.Delay(100);
        }
    }
}
