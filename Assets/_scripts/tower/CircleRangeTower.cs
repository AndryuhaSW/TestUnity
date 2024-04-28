using System.Collections;
using System.Collections.Generic;
using UnityTask = System.Threading.Tasks.Task;
using UnityEngine;

public class CircleRangeTower : Tower
{
    [SerializeField] private GameObject _kernelPrefab;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private int _reload;
    [SerializeField] private Animator animator;
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
                Enemy enemy = enemies[0];

                GameObject kernel = Instantiate(_kernelPrefab, gameObject.transform.position, Quaternion.identity);

                Vector2 animationDirection = (enemy.gameObject.transform.position - transform.position).normalized;
                animator.SetFloat("Vertical", animationDirection.x);
                animator.SetFloat("Horisontal", animationDirection.y);
                Debug.Log((enemy.gameObject.transform.position - transform.position).normalized);

                kernel.GetComponent<Kernel>()?.Initialize(enemy.gameObject.transform.position, _damage, _speed);

                await UnityTask.Delay(_reload * 100);
                continue;
            }

            await UnityTask.Delay(100);
        }
    }
}
