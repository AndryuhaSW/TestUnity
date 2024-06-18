using System;
using System.Threading;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action MoneyChanged;

    [SerializeField] private int currentMoney;

    private Mutex _mutex = new Mutex();

    public bool ChangeMoney(int money)
    {
        try
        {
            _mutex.WaitOne();

            if (currentMoney + money < 0)
            {
                return false;
            }
            else
            {
                currentMoney += money;
                MoneyChanged?.Invoke();
                return true;
            }
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public int GetMoney() => currentMoney;
}