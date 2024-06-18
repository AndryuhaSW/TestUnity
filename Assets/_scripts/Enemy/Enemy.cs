using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using Zenject;


public abstract class Enemy : MonoBehaviour, Sheepable
{
    [SerializeField] private int moneyAfterDeath;
    [SerializeField] private Animator animator;
    [SerializeField] private int healthPoints;

    private Health health;
    private Sheep sheep = null;
    private float speed;
    private WayPoints wayPoints;

    private CancellationTokenSource movingToken;

    private SheepManager sheepManager;
    private EnemyCounter enemyCounter;
    private Wallet wallet;

    [Inject]
    public void Injec(SheepManager sheepManager, 
        EnemyCounter enemyCounter, Wallet wallet)
    {
        this.sheepManager = sheepManager;
        this.enemyCounter = enemyCounter;
        this.wallet = wallet;
    }

    private void Awake()
    {
        health = GetComponent<Health>();
        health.Initialize(healthPoints);
    }

    public virtual void Initialize(WayPoints wayPoints, float speed)
    {
        movingToken = new CancellationTokenSource();

        this.wayPoints = wayPoints;
        this.speed = speed;

        Move();
    }

    private async Task Move()
    {
        for (int i = 0; i < wayPoints.forwardWayPoints.Count; i++)
            await GoToPoint(wayPoints.forwardWayPoints[i].position);


        for (int i = 0; i < wayPoints.backWayPoints.Count; i++)
            await GoToPoint(wayPoints.backWayPoints[i].position);

        ClearSheep();
        ResetEnemy();
        enemyCounter.Decrement();
    }

    private async Task GoToPoint(Vector2 direction)
    {
        while (Vector2.Distance(gameObject.transform.position, direction) > 0.01f)
        {
            Vector2 animationDirection = (direction - (Vector2)transform.position).normalized;
            animator.SetFloat("Vertical", animationDirection.x);
            animator.SetFloat("Horisontal", animationDirection.y);
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.fixedDeltaTime);
            await Task.Delay((int)(Time.fixedDeltaTime * 1000), movingToken.Token);
        }
    }

    private void ClearSheep()
    {
        if (sheep != null)
        {
            sheepManager.StealSheep();
            sheep = null;
        }
    }

    private void ResetEnemy()
    {
        StopMooving();
        PaintEnemy(Color.white);
        health.Reset();
        gameObject.SetActive(false);
    }

    private void OnKilled()
    {
        wallet.ChangeMoney(moneyAfterDeath);
        ResetEnemy();
        enemyCounter.Decrement();
    }

    private void PaintEnemy(Color color)
    {
        SpriteRenderer image = transform.GetChild(1).GetComponent<SpriteRenderer>();
        image.color = color;
    }

    private void OnEnable()
    {
        health.Killed += OnKilled;
        sheepManager.AllSheepStolen += ResetEnemy;
    }
    private void OnDisable()
    {
        health.Killed -= OnKilled;
        sheepManager.AllSheepStolen -= ResetEnemy;
    }

    public void TakeSheep(Sheep sheep)
    {
        if (this.sheep == null)
        {
            this.sheep = sheep;
            this.sheep.OnTake();
            PaintEnemy(Color.red);
        }
    }

    public void DropSheep()
    {
        sheep?.DropTo(transform.position);
        sheep = null;
    }

    protected void StopMooving()
    {
        movingToken.Cancel();
    }
}
