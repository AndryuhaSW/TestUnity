using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static event Action AllEnemiesDead;
    public static event Action AllSheepsStealStolen;
    public static event Action AllWavesOver;

    [SerializeField] private List<Data_Level> _levels;

    private static int _enemyCount;
    private static int _sheepCount;
    private static int _currentWaveNumber;
    private static int _maxWaveNumber;

    public int CurrentLevel { get; set; } = 0;

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

    public static void KillEnemy()
    {
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

            AllEnemiesDead?.Invoke();
        }
    }

    public static void StealSheep()
    {
        _sheepCount--;

        if (_sheepCount <= 1)
        {
            AllSheepsStealStolen?.Invoke();
        }
    }


    public int GetMaxWaveNumber() => _maxWaveNumber;
    public int GetCurrentWaveNumber() => _currentWaveNumber;
}
