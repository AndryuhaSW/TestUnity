using System;
using System.Collections;
using UnityEngine;

public class CardConveyor : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private RectTransform spawnPoint;
    [SerializeField] private RectTransform destroyPoint;
    [SerializeField] private float spawnTimeInterval = 2f;
    [SerializeField] private float cardSpeed = 2f;

    private TowerCardFactory towerCardFactory;

    private void Start()
    {
        towerCardFactory = TowerCardFactory.instance;
        StartCoroutine(Initialize());
    }

    private IEnumerator Initialize()
    {
        while (true)
        {
            TowerCard towerCard = GetRandomTowerCard();
            towerCard.Initialize(canvas, spawnPoint.anchoredPosition, destroyPoint.anchoredPosition, cardSpeed);
            yield return new WaitForSeconds((int)spawnTimeInterval);
        }
    }

    private TowerCard GetRandomTowerCard()
    {
        switch (UnityEngine.Random.Range(0, /*Enum.GetNames(typeof(TowerType)).Length*/3))
        {
            case 0:
                return towerCardFactory.SpawnTowerCard(TowerType.SinglePurpose);
            case 1:
                return towerCardFactory.SpawnTowerCard(TowerType.CircleRange);
            case 2:
                return towerCardFactory.SpawnTowerCard(TowerType.Splash);
        }
        throw new NullReferenceException("type of tower is null");
    }
}