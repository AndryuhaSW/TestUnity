using System.Collections;
using System.Collections.Generic;
using UnityTask = System.Threading.Tasks.Task;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime;

public class Kernel : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();

    public async UnityTask Initialize(Vector2 target, int damage, float speed)
    {

        while (Vector2.Distance(transform.position, target) > 0.2f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target,
                speed * Time.fixedDeltaTime);
            await UnityTask.Delay((int)(Time.fixedDeltaTime * 1000));
        }


        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].MinusHealth(damage);
        }

        gameObject.SetActive(false);
    }

    
                            
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Add(collision.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Remove(collision.GetComponent<Enemy>());
        }
    }


}
