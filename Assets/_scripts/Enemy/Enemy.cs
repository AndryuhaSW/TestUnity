using System.Collections.Generic;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;
using System.Threading;


public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _moneyAfterDeath;
    protected Health _healthComponent;
    protected CancellationTokenSource token;
    protected List<Transform> _forwardWayPoints;
    protected List<Transform> _backWayPoints;
    protected float _speed;

    private void Awake()
    {
        _healthComponent = gameObject.GetComponent<Health>();
    }

    public virtual async UnityTask Initialize(List<Transform> forwardWayPoints, 
        List<Transform> backWayPoints, float speed)
    {
        token = new CancellationTokenSource();
        _forwardWayPoints = forwardWayPoints;
        _backWayPoints = backWayPoints;
        _speed = speed;

        Move();
    }

    private async UnityTask Move()
    {
        for (int i = 0; i < _forwardWayPoints.Count; i++)
            await GoToPoint(_forwardWayPoints[i].position);


        for (int i = 0; i < _backWayPoints.Count; i++)
            await GoToPoint(_backWayPoints[i].position);

        _healthComponent.Kill();
    }

    private async UnityTask GoToPoint(Vector2 direction)
    {
        while (Vector2.Distance(gameObject.transform.position, direction) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction,
                _speed * Time.fixedDeltaTime);
            await UnityTask.Delay((int)(Time.fixedDeltaTime * 1000), token.Token);
        }
    }

    // Абстрактный метод для обработки смерти врага
    protected virtual void OnKilled()
    {
        token.Cancel();
        Wallet.Instance.ChangeMoney(_moneyAfterDeath);
        Destroy(gameObject);
        LevelManager.KillEnemy();
    }

    private void OnEnable()
    {
        _healthComponent.Killed += OnKilled;
    }
    private void OnDisable()
    {
        _healthComponent.Killed -= OnKilled;
    }

    public void PlusHealth(float health)
    {
        _healthComponent.PlusHealth(health);
    }

    public void MinusHealth(float health)
    {
        _healthComponent.MinusHealth(health);
        
    }
}
