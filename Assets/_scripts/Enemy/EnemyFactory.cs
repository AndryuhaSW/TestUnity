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

    public Enemy SpawnEnemy(string enemyType)
    {
        Enemy spawnedEnemy = null;

        switch (enemyType)
        {
            case "PatrolEnemy":
                spawnedEnemy = patrolEnemyPool.GetFreeElement();
                break;
            case "HealingEnemy":
                spawnedEnemy = healingEnemyPool.GetFreeElement();
                break;
        }
        
        return spawnedEnemy;
    }
}
