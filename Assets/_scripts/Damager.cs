using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int _bulletDamage;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health health = other.GetComponent<Health>();
            health.MinusHealth(_bulletDamage);
        }
    }

}

