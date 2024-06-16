using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    private Vector2 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public void OnTake()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop(Vector2 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
    }

    public void Return(int level)
    {
        transform.position = startPosition;
        OnDrop(spawnPoints[level - 1].transform.position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Sheepable enemy = collision.GetComponent<Sheepable>();
            enemy.TakeSheep(this);
        }
    }
}
