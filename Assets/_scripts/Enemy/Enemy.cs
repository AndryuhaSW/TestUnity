using System.Collections.Generic;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;
using System.Threading;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _moneyAfterDeath;

    protected Health health;
    protected List<Transform> forwardWayPoints;
    protected List<Transform> backWayPoints;
    protected float speed;

    protected CancellationTokenSource token;

    private void Awake()
    {
        health = GetComponent<Health>();
    }

    public virtual async UnityTask Initialize(List<Transform> forwardWayPoints, List<Transform> backWayPoints, float speed)
    {
        token = new CancellationTokenSource();
        this.forwardWayPoints = forwardWayPoints;
        this.backWayPoints = backWayPoints;
        this.speed = speed;

        Move();
    }

    private async UnityTask Move()
    {
        for (int i = 0; i < forwardWayPoints.Count; i++)
            await GoToPoint(forwardWayPoints[i].position);


        for (int i = 0; i < backWayPoints.Count; i++)
            await GoToPoint(backWayPoints[i].position);

        health.Kill();
    }

    private async UnityTask GoToPoint(Vector2 direction)
    {
        while (Vector2.Distance(gameObject.transform.position, direction) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.fixedDeltaTime);
            await UnityTask.Delay((int)(Time.fixedDeltaTime * 1000), token.Token);
        }
    }

    protected virtual void OnKilled()
    {
        token.Cancel();
        Wallet.Instance.ChangeMoney(_moneyAfterDeath);
        gameObject.SetActive(false);
        LevelManager.KillEnemy();
    }

    private void OnEnable()
    {
        health.Killed += OnKilled;
    }
    private void OnDisable()
    {
        health.Killed -= OnKilled;
    }


    //luchshe ne nado
    public void PlusHealth(float val)
    {
        health.PlusHealth(val);
    }

    public void MinusHealth(float val)
    {
        health.MinusHealth(val);
        
    }
}
