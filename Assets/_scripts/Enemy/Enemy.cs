using System.Collections.Generic;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;
using System.Threading;


public abstract class Enemy : MonoBehaviour, Sheepable
{
    [SerializeField] private float _moneyAfterDeath;
    [SerializeField] private Animator animator;
    protected Sheep sheep = null;
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


        ClearSheep();
        ResetEnemy();
        LevelManager.instance.EnemyCountDecrement();
    }

    private void ClearSheep()
    {
        if (sheep != null)
        {
            LevelManager.instance.StealSheep();
            sheep.gameObject.SetActive(false);
            sheep = null;
        }
    }


    private void ResetEnemy()
    {
        StopMooving();
        PaintEnemy(Color.white);
        health.PlusHealth(health.maxHealth);
        gameObject.SetActive(false);
    }


    private async UnityTask GoToPoint(Vector2 direction)
    {
        while (Vector2.Distance(gameObject.transform.position, direction) > 0.01f)
        {
            Vector2 animationDirection = (direction - (Vector2)transform.position).normalized;
            animator.SetFloat("Vertical", animationDirection.x);
            animator.SetFloat("Horisontal", animationDirection.y);
            transform.position = Vector2.MoveTowards(transform.position, direction, speed * Time.fixedDeltaTime);
            await UnityTask.Delay((int)(Time.fixedDeltaTime * 1000), token.Token);
        }
    }

    protected void StopMooving()
    {
        token.Cancel();
    }

    protected virtual void OnKilled()
    {
        PaintEnemy(Color.white);
        Wallet.Instance.ChangeMoney(_moneyAfterDeath);
        ResetEnemy();
        LevelManager.instance.EnemyCountDecrement();
    }



    private void PaintEnemy(Color color)
    {
        SpriteRenderer image = transform.GetChild(1).GetComponent<SpriteRenderer>();
        image.color = color;
    }

    private void OnEnable()
    {
        health.Killed += OnKilled;
        if (LevelManager.instance != null)
            LevelManager.instance.LostGame += ResetEnemy;
    }
    private void OnDisable()
    {
        health.Killed -= OnKilled;
        if (LevelManager.instance != null)
            LevelManager.instance.LostGame -= ResetEnemy;
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
        sheep?.OnDrop(transform.position);
        sheep = null;
    }

}
