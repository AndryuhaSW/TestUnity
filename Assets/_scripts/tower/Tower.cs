using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected int _timeToBuild;

    //protected Queue<Enemy> enemies = new Queue<Enemy>();
    protected List<Enemy> enemies = new List<Enemy>();
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
            await UnityTask.Delay(1000, token.Token);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy") 
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemies.Add(enemy);
        }
    }
    
    //���� ���� ������������, �� �� ������ � OnTriggerExit ��� �����. ������� ��� ��������
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemies.Remove(enemy);
        }
    }



    /*public void SaleTower()
    {
        token.Cancel();
        Destroy(gameObject);
    }*/
}
