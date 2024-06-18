using System;
using System.Threading;
using UnityEngine;
using Zenject;

public class WaveCounter : MonoBehaviour
{
    public event Action AllWavesOver;
    public event Action EndWave;

    private Mutex mutex = new Mutex();

    private int currentWaveNumber;
    private int maxWaveNumber;

    private EnemyCounter enemyCounter;


    [Inject]
    public void Inject(EnemyCounter enemyCounter)
    {
        this.enemyCounter = enemyCounter;
    }

    private void OnEnable()
    {
        enemyCounter.AllEnemiesDead += Decrement;
    }

    private void OnDisable()
    {
        enemyCounter.AllEnemiesDead -= Decrement;
    }

    public void Initialize(int maxWaveNumber)
    {
        this.maxWaveNumber = maxWaveNumber;
        currentWaveNumber = 0;
        enemyCounter.Initialize();
    }

    public void Decrement()
    {
        try
        {
            mutex.WaitOne();

            currentWaveNumber++;
            if (currentWaveNumber > maxWaveNumber)
            {
                AllWavesOver?.Invoke();
                return;
            }
            EndWave?.Invoke();
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    public int GetCurrentWave() => currentWaveNumber;
    public int GetMaxWaveNumber() => maxWaveNumber;
}
