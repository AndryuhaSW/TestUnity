using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public class Spawner : MonoBehaviour
{
    private EnemyFactory enemyFactory;

    private CancellationTokenSource token;


    public async UnityTask SpawnWave(List<Data_Enemy> waveList, float speed, int level, WayPoints wayPoints)
    {
        token = new CancellationTokenSource();

        foreach (Data_Enemy enemyData in waveList)
        {
            EnemyType enemyType = enemyData.enemyType;
            int countInLine = enemyData.countInLine;
            float delayNextEnemy = enemyData.delayNextEnemy;

            for (int i = 0; i < countInLine; i++)
            {
                Enemy enemy = enemyFactory.SpawnEnemy(enemyType);

                enemy.transform.position = wayPoints.forwardWayPoints[0].position;
                enemy.Initialize(wayPoints.forwardWayPoints, wayPoints.backWayPoints, speed);

                await UnityTask.Delay((int)(delayNextEnemy * 1000), token.Token);
            }
        }
    }


    public void StopSpawner()
    {
        token.Cancel();
    }

    private void Start()
    {
        enemyFactory = EnemyFactory.instance;
        LevelManager.instance.EndWave += StopSpawner;
    }

    private void OnDestroy()
    {
        LevelManager.instance.EndWave -= StopSpawner;
    }
}
