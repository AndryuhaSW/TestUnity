using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _forwardWayPoints;
    [SerializeField] private List<Transform> _backWayPoints;

    private EnemyFactory _enemyFactory;
    private CancellationTokenSource token;

    //Ќе awake потому что instrance не успевает установитьс€ в EnemyFactory.Awake
    private void Start()
    {
        _enemyFactory = EnemyFactory.instance;
    }

    public async UnityTask SpawnWave(List<Data_Enemy> waveList, float speed)
    {
        token = new CancellationTokenSource();

        foreach (Data_Enemy enemy in waveList)
        {
            string enemyType = enemy.enemyType;
            int countInLine = enemy.countInLine;
            float delayNextEnemy = enemy.delayNextEnemy;

            for (int i = 0; i < countInLine; i++)
            {
                Enemy enemy_gameobject = _enemyFactory.SpawnEnemy(enemyType, transform.position);
                enemy_gameobject.Initialize(_forwardWayPoints, _backWayPoints, speed);

                await UnityTask.Delay((int)(delayNextEnemy * 1000), token.Token);
            }

            await UnityTask.Delay((int)(delayNextEnemy * 1000), token.Token);
        }
    }


    public void StopSpawner()
    {
        token.Cancel();
    }

    private void OnEnable()
    {
        LevelManager.AllEnemiesDead += StopSpawner;
    }

    private void OnDisable()
    {
        LevelManager.AllEnemiesDead -= StopSpawner;
    }
}
