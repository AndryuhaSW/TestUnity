using System;
using System.Threading;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public event Action MoneyChanged;

    [SerializeField] private float _money;

    private Mutex _mutex = new Mutex();
    public static Wallet Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public bool ChangeMoney(float money)
    {
        try
        {
            _mutex.WaitOne();

            if (_money + money < 0)
            {
                return false;
            }
            else
            {
                _money += money;
                MoneyChanged?.Invoke();
                return true;
            }
        }
        finally
        {
            _mutex.ReleaseMutex();
        }
    }

    public float GetMoney()
    {
        return _money;
    }


}