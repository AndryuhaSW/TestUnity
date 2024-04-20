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

        foreach (Data_Enemy enemyData in waveList)
        {
            EnemyType enemyType = enemyData.enemyType;
            int countInLine = enemyData.countInLine;
            float delayNextEnemy = enemyData.delayNextEnemy;

            for (int i = 0; i < countInLine; i++)
            {
                Enemy enemy = _enemyFactory.SpawnEnemy(enemyType);

                enemy.transform.position = transform.position;
                enemy.Initialize(_forwardWayPoints, _backWayPoints, speed);

                await UnityTask.Delay((int)(delayNextEnemy * 1000), token.Token);
            }
        }
    }


    public void StopSpawner()
    {
        token.Cancel();
    }

    private void OnEnable()
    {
        LevelManager.instance.AllEnemiesDead += StopSpawner;
    }

    private void OnDisable()
    {
        LevelManager.instance.AllEnemiesDead -= StopSpawner;
    }
}
