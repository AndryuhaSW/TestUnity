using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;
using System.Threading.Tasks;

public class Spawner : MonoBehaviour
{
    [SerializeField] private WayPoints wayPoints;

    private CancellationTokenSource spawnWaveToken;

    private WaveCounter waveCounter;
    private EnemyFactory enemyFactory;


    [Inject]
    public void Inject(WaveCounter waveCounter, EnemyFactory enemyFactory)
    {
        this.waveCounter = waveCounter;
        this.enemyFactory = enemyFactory;
    }

    public async Task SpawnWave(List<Data_Enemy> waveList, float speed)
    {
        spawnWaveToken = new CancellationTokenSource();

        foreach (Data_Enemy enemyData in waveList)
        {
            EnemyType enemyType = enemyData.enemyType;
            int countInLine = enemyData.countInLine;
            int delayNextEnemy = enemyData.delayNextEnemy_MS;

            for (int i = 0; i < countInLine; i++)
            {

                Enemy enemy = enemyFactory.SpawnEnemy(enemyType);

                enemy.transform.position = wayPoints.forwardWayPoints[0].position;
                enemy.Initialize(wayPoints, speed);

                await Task.Delay((delayNextEnemy * 100), spawnWaveToken.Token);
            }
        }
    }


    public void StopSpawner()
    {
        spawnWaveToken.Cancel();
    }

    private void Start()
    {
        waveCounter.EndWave += StopSpawner;
    }

    private void OnDestroy()
    {
        waveCounter.EndWave -= StopSpawner;
    }
}
