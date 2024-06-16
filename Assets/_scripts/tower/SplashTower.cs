using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public class SplashTower : Tower
{
    [SerializeField] private int _damage;
    [SerializeField] private int _attackSpeed;
    [SerializeField] private float _splashRange;

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
                Enemy targetEnemy = enemies[0];
                Collider2D[] colliders = Physics2D.OverlapCircleAll(targetEnemy.transform.position, _splashRange);
                foreach (Collider2D col in colliders)
                {
                    if (col.CompareTag("Enemy"))
                    {
                        Enemy enemy = col.GetComponent<Enemy>();
                        enemy.GetComponent<Health>().MinusHealth(_damage);
                    }
                }

                await UnityTask.Delay(_attackSpeed);
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