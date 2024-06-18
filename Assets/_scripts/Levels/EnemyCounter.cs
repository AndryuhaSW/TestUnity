using System;
using System.Threading;
using UnityEngine;

public class EnemyCounter : MonoBehaviour
{
    public event Action AllEnemiesDead;

    private int enemyCountAlive;

    private Mutex mutex = new Mutex();


    public void Initialize()
    {
        enemyCountAlive = 0;
    }

    public void Decrement()
    {
        try
        {
            mutex.WaitOne();

            enemyCountAlive--;
            if (enemyCountAlive <= 0)
            {
                AllEnemiesDead?.Invoke();
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    public void Increment(int count)
    {
        try
        {
            mutex.WaitOne();

            enemyCountAlive += count;
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}
