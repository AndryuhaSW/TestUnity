using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    private Vector2 spawnPosition;

    public void OnTake()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop(Vector2 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
    }

    public void Return()
    {
        gameObject.transform.position = spawnPosition;
        gameObject.SetActive(true);
    }

    private void Start()
    {
        spawnPosition = transform.position;
    }
}
