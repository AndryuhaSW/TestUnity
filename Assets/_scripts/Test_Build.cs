using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Build : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            
            Health health = collision.GetComponent<Health>();
            health.MinusHealth(1);
        }
    }

}
