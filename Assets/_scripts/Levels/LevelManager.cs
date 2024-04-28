using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public  event Action AllEnemiesDead;
    public  event Action AllSheepsStealStolen;
    public  event Action AllWavesOver;

    [SerializeField] private List<Data_Level> _levels;

    public static LevelManager instance { get; private set; }
    private  int _enemyCount;
    private  int _sheepCount = 3;
    private  int _currentWaveNumber;
    private  int _maxWaveNumber;
    private Mutex _mutex = new Mutex();
    public int CurrentLevel { get; set; } = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void LoadLevel(int level)
    {
        
        //-1 потому что индексы волн начинаются с 0
        _maxWaveNumber = _levels[level].waves.Count - 1;
        
        for (int spawnerIndex = 0; spawnerIndex < _levels[level].spawners.Count; spawnerIndex++)
        {
            List<Data_Enemy> enemies = _levels[level].waves[_currentWaveNumber].enemies;
            List<Data_Enemy> enemiesForThisSpawner = new List<Data_Enemy>();
            foreach (Data_Enemy enemy in enemies)
                if (enemy.spawnerIndex == spawnerIndex)
                {
                    enemiesForThisSpawner.Add(enemy);
                    _enemyCount += enemy.countInLine;
                }

            float speed = _levels[level].waves[_currentWaveNumber].speed;
            _levels[level].spawners[spawnerIndex].SpawnWave(enemiesForThisSpawner, speed);
        }
    }

    public void KillEnemy()
    {
        try
        {
            _mutex.WaitOne();

            _enemyCount--;
            if (_enemyCount <= 0)
            {
                _currentWaveNumber++;
                if (_currentWaveNumber > _maxWaveNumber)
                {
                    _currentWaveNumber = 0;
                    AllWavesOver?.Invoke();
                    return;
                }
                Debug.Log("AllEnemiesDead");
                AllEnemiesDead?.Invoke();
            }
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public void StealSheep()
    {
        _sheepCount--;

        if (_sheepCount <= 0)
        {
            Debug.Log(999);
            AllSheepsStealStolen?.Invoke();
        }
    }


    public int GetMaxWaveNumber() => _maxWaveNumber;
    public int GetCurrentWaveNumber() => _currentWaveNumber;
}
