using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public  event Action EndWave;
    public  event Action LostGame;
    public  event Action WinGame;

    [SerializeField] private List<Sheep> sheeps;
    [SerializeField] private LevelBackground levelBackground;
    [SerializeField] private List<WayPoints> wayPoints;

    [SerializeField] private List<Data_Level> _levels;

    public static LevelManager instance { get; private set; }

    private int enemyCount;
    private  int sheepCount;
    private  int currentWaveNumber;
    private  int maxWaveNumber;
    private Mutex mutex = new Mutex();
    private int currentLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int level)
    {
        FsmState_PrepareStage.SetState();
        levelBackground.Change(level);
        currentLevel = level;
        currentWaveNumber = 0;
        sheepCount = 3;
        foreach (Sheep s in sheeps) s.Return(level);
    }

    public void StartLevel()
    {
        Data_Level currentLevelData = _levels[currentLevel-1];

        maxWaveNumber = currentLevelData.waves.Count-1;
        
        for (int spawnerIndex = 0; spawnerIndex < currentLevelData.spawners.Count; spawnerIndex++)
        {
            List<Data_Enemy> enemies = currentLevelData.waves[currentWaveNumber].enemies;
            List<Data_Enemy> enemiesForThisSpawner = new List<Data_Enemy>();
            foreach (Data_Enemy enemy in enemies)
                if (enemy.spawnerIndex == spawnerIndex)
                {
                    enemiesForThisSpawner.Add(enemy);
                    enemyCount += enemy.countInLine;
                }
            float speed = currentLevelData.waves[currentWaveNumber].speed;
            currentLevelData.spawners[spawnerIndex].SpawnWave(enemiesForThisSpawner, speed, 
                (currentLevel-1), wayPoints[currentLevelData.spawnerStartIndex + spawnerIndex]);
        }
    }

    public void EnemyCountDecrement()
    {
        try
        {
            mutex.WaitOne();

            enemyCount--;
            if (enemyCount <= 0)
            {  
                currentWaveNumber++;
                if (currentWaveNumber > maxWaveNumber)
                {
                    WinGame?.Invoke();
                    return;
                }
                EndWave?.Invoke();
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    public void StealSheep()
    {
        try
        {
            mutex.WaitOne();

            sheepCount--;

            if (sheepCount <= 0)
            {
                Debug.Log(999);
                LostGame?.Invoke();
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }


    public int GetMaxWaveNumber() => maxWaveNumber;
    public int GetCurrentWaveNumber() => currentWaveNumber;
}
