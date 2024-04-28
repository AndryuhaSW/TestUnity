using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private RectTransform container;

    [SerializeField] private PatrolEnemy patrolEnemyPrefab;
    [SerializeField] private HealingEnemy healingEnemyPrefab;

    private PoolMono<PatrolEnemy> patrolEnemyPool;
    private PoolMono<HealingEnemy> healingEnemyPool;

    public static EnemyFactory instance {get; private set;}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        patrolEnemyPool = new PoolMono<PatrolEnemy>(patrolEnemyPrefab, 10, container);
        healingEnemyPool = new PoolMono<HealingEnemy>(healingEnemyPrefab, 10, container);
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
}
