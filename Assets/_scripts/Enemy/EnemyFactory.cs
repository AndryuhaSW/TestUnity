using System;
using UnityEngine;
using Zenject;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private RectTransform container;

    [SerializeField] private PatrolEnemy patrolEnemyPrefab;
    [SerializeField] private HealingEnemy healingEnemyPrefab;

    private PoolMono<PatrolEnemy> patrolEnemyPool;
    private PoolMono<HealingEnemy> healingEnemyPool;

    private DiContainer diContainer;


    [Inject]
    private void Inject(DiContainer diContainer)
    {
        this.diContainer = diContainer;
    }


    private void Awake()
    {
        patrolEnemyPool = new PoolMono<PatrolEnemy>(CreatePatrolEnemy, 10);
        healingEnemyPool = new PoolMono<HealingEnemy>(CreateHealingEnemy, 10);
    }


    public Enemy SpawnEnemy(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Patrol:
                return patrolEnemyPool.GetFreeElement();
            case EnemyType.Healing:
                return healingEnemyPool.GetFreeElement();
        }

        throw new NullReferenceException("type of enemy is null");
    }

    private PatrolEnemy CreatePatrolEnemy(bool IsActiveByDefault)
    {
        GameObject obj = diContainer.InstantiatePrefab(patrolEnemyPrefab, container);
        obj.SetActive(IsActiveByDefault);
        return obj.GetComponent<PatrolEnemy>();
    }

    private HealingEnemy CreateHealingEnemy(bool IsActiveByDefault)
    {
        GameObject obj = diContainer.InstantiatePrefab(healingEnemyPrefab, container);
        obj.SetActive(IsActiveByDefault);
        return obj.GetComponent<HealingEnemy>();
    }
}
