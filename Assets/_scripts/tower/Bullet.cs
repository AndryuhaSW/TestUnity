using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public class Bullet : MonoBehaviour
{
 
    public async UnityTask Initialize(Enemy target, int damage, float speed)
    {
        while (Vector2.Distance(transform.position, target.gameObject.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.gameObject.transform.position,
                speed * Time.fixedDeltaTime);
            await UnityTask.Delay((int)(Time.fixedDeltaTime * 1000));
        }

        target.MinusHealth(damage);
        Destroy(gameObject);
    }

}
