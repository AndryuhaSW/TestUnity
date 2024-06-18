using UnityEngine;

public class Sheep : MonoBehaviour
{
    private const string ENEMY_TAG = "Enemy";
    private Vector2 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    public void OnTake()
    {
        gameObject.SetActive(false);
    }

    public void DropTo(Vector2 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == ENEMY_TAG)
        {
            Sheepable enemy = collision.GetComponent<Sheepable>();
            enemy.TakeSheep(this);
        }
    }
}
