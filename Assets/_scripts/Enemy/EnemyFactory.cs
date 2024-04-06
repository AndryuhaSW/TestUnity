using UnityEngine;

//Fabric method
public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private Transform _parentTransform;

    [SerializeField] private GameObject _patrolEnemyPrefab; // Префаб врага для спавна
    [SerializeField] private GameObject _healingEnemyPrefab; // Префаб хилящего врага для спавна

    public static EnemyFactory instance {get; private set;}

    private void Awake()
    {
        instance = this;
    }

    public Enemy SpawnEnemy(string enemyType, Vector2 spawnPoint)
    {
        GameObject spawnedEnemy = null;

        switch (enemyType)
        {
            case "PatrolEnemy":
                spawnedEnemy = Instantiate(_patrolEnemyPrefab, spawnPoint, Quaternion.identity);
                break;
            case "HealingEnemy":
                spawnedEnemy = Instantiate(_healingEnemyPrefab, spawnPoint, Quaternion.identity);
                break;
        }
        
        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();

        spawnedEnemy.transform.parent = _parentTransform;

        spawnedEnemy.transform.localScale = Vector3.one;

        return enemy;
    }
}
