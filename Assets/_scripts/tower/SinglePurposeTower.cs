using System.Collections;
using System.Collections.Generic;
using UnityTask = System.Threading.Tasks.Task;
using UnityEngine;

public class SinglePurposeTower : Tower
{
    [SerializeField] private GameObject _buletPrefab;
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

                GameObject bullet = Instantiate(_buletPrefab, gameObject.transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().Initialize(enemy, _damage, _speed);

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
