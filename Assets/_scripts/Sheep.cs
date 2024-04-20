using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{

    public void OnTake()
    {
        gameObject.SetActive(false);
    }

    public void OnDrop(Vector2 position)
    {
        gameObject.SetActive(true);
        gameObject.transform.position = position;
    }
}
