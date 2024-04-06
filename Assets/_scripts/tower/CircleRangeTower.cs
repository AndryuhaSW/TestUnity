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
                if (kernel.GetComponent<Kernel>() == null) Debug.Log(9999);
                kernel.GetComponent<Kernel>()?.Initialize(enemy.gameObject.transform.position, _damage, _speed);

                await UnityTask.Delay(_reload * 100);
                continue;
            }

            await UnityTask.Delay(100);
        }
    }

    //temp. delete next time
    private void Start()
    {
        Initialize();
    }
}
