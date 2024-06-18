using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using UnityTask = System.Threading.Tasks.Task;

public abstract class Tower : MonoBehaviour
{
    [SerializeField] protected int timeToBuild;
    [SerializeField] protected int salePrice;
    
    protected List<Enemy> enemies = new List<Enemy>();
    protected CancellationTokenSource token;

    private Wallet wallet;


    [Inject]
    public void Inject(Wallet wallet)
    {
        this.wallet = wallet;
    }

    public virtual async UnityTask Initialize()
    {
        token = new CancellationTokenSource();
        await Build();
    }

    
    protected virtual async UnityTask Build()
    {
        //Нужно переделать по нормальному. Полоса прогрессии мб
        for (int i = 0; i < timeToBuild; i++)
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
    
    //Если враг уничтожается, то он войдет в OnTriggerExit все равно. Поэтому все работает
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            enemies.Remove(enemy);
        }
    }


    public void SaleTower()
    {
        token.Cancel();
        wallet.ChangeMoney(salePrice);
        gameObject.SetActive(false);
    }
}
