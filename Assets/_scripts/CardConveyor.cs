using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardConveyor : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject[] cardPrefabs;
    [SerializeField] private RectTransform spawnPoint;
    [SerializeField] private RectTransform destroyPoint;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float cardSpeed = 2f; 

    private float nextSpawnTime;

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnCard();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    private void SpawnCard()
    {
        GameObject randomCardPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Length)];

        GameObject newCard = Instantiate(randomCardPrefab, spawnPoint.position, Quaternion.identity, this.transform);
        newCard.GetComponent<TowerCard_DragAndDrop>().Initialize(_canvas, destroyPoint.anchoredPosition);

        Rigidbody2D cardRigidbody = newCard.GetComponent<Rigidbody2D>();
        cardRigidbody.velocity = transform.right * cardSpeed;
    }
}