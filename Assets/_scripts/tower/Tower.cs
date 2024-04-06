using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected int _timeToBuild;

    protected Queue<Enemy> enemies = new Queue<Enemy>();
    protected CancellationTokenSource token;

    public virtual async UnityTask Initialize()
    {
        token = new CancellationTokenSource();
        await Build();
    }

    
    protected virtual async UnityTask Build()
    {
        //����� ���������� �� �����������. ������ ���������� ��
        for (int i = 0; i < _timeToBuild; i++)
        {
            Debug.Log("...");
            await UnityTask.Delay(1000, token.Token);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(1);
        if (collision.tag == "Enemy") 
        {
            Debug.Log(2);
            Enemy enemy = collision.GetComponent<Enemy>();
            enemies.Enqueue(enemy);
        }
    }
    
    //���� ���� ������������, �� �� ������ � OnTriggerExit ��� �����. ������� ��� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Dequeue();
        }
    }



    /*public void SaleTower()
    {
        token.Cancel();
        Destroy(gameObject);
    }*/
}
