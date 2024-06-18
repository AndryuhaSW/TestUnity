using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

public class SheepManager : MonoBehaviour
{
    public event Action AllSheepStolen;

    private List<Sheep> sheeps;
    
    private List<Transform> spawnPoints;
    private int sheepCount;

    private Mutex mutex = new Mutex();


    [Inject]
    public void Inject(List<Sheep> sheeps)
    {
        this.sheeps = sheeps;
    }

    public void Initialize(List<Transform> spawnPoints)
    {
        this.spawnPoints = spawnPoints;
        sheepCount = sheeps.Count;
        ReturnSheepsToSpawnPoints();
    }

    public void StealSheep()
    {
        sheepCount--;
        if (sheepCount <= 0)
        {
            AllSheepStolen?.Invoke();
        }
        
    }

    public void ReturnSheepsToSpawnPoints()
    {
        for (int i = 0; i < sheeps.Count; i++)
        {
            sheeps[i].DropTo(spawnPoints[i].position);
        }
    }
}
