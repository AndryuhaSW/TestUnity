using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    private List<LevelSettings> levelsSettings;
    private SheepManager sheepManager;
    private WaveCounter waveCounter;
    private EnemyCounter enemyCounter;
    private LevelBackground levelBackground;
    private int currentLevel;
    private FsmState_PrepareStage fsmState_PrepareStage;

    private Mutex mutex = new Mutex();


    [Inject]
    public void Inject(
        List<LevelSettings> levelsSettings, 
        SheepManager sheepManager, 
        WaveCounter waveCounter,
        EnemyCounter enemyCounter,
        LevelBackground levelBackground,
        FsmState_PrepareStage fsmState_PrepareStage)
    {
        this.levelsSettings = levelsSettings;
        this.sheepManager = sheepManager;
        this.waveCounter = waveCounter;
        this.enemyCounter = enemyCounter;
        this.levelBackground = levelBackground;
        this.fsmState_PrepareStage = fsmState_PrepareStage;
    }


    public void RestartLevel()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int level)
    {
        fsmState_PrepareStage.SetState();
        levelBackground.Change(levelsSettings[level - 1].Background);
        currentLevel = level;
        waveCounter.Initialize(levelsSettings[level - 1].LevelData.waves.Count);
        sheepManager.Initialize(levelsSettings[level - 1].LevelData.sheepSpawnPoints);
    }

    public void StartLevel()
    {
        List<Spawner> spwners = levelsSettings[currentLevel - 1].LevelData.spawners;
        List<Data_Wave> waves = levelsSettings[currentLevel - 1].LevelData.waves;
        int currentWave = waveCounter.GetCurrentWave();

        for (int spawnerIndex = 0; spawnerIndex < spwners.Count; spawnerIndex++)
        {
            

            List<Data_Enemy> enemies = waves[currentWave].enemies;
            List<Data_Enemy> enemiesForThisSpawner = new List<Data_Enemy>();
            foreach (Data_Enemy enemy in enemies)
                if (enemy.spawnerIndex == spawnerIndex)
                {
                    enemiesForThisSpawner.Add(enemy);
                    enemyCounter.Increment(enemy.countInLine);
                }

            float speed = waves[currentWave].speed;
            spwners[spawnerIndex].SpawnWave(enemiesForThisSpawner, speed);
        }
    }

}
