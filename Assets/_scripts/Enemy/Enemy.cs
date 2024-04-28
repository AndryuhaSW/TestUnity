using System.Collections.Generic;
using UnityEngine;
using UnityTask = System.Threading.Tasks.Task;
using System.Threading;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;


public abstract class Enemy : MonoBehaviour
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

        LevelManager.instance.StealSheep();
        /*LevelManager.instance.KillEnemy();*/
        sheep?.Return();
        sheep = null;
        health.PlusHealth(health.maxHealth);
        SpriteRenderer image = transform.GetChild(1).GetComponent<SpriteRenderer>();
        image.color = Color.white;
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
        token.Cancel();
        Wallet.Instance.ChangeMoney(_moneyAfterDeath);
        LevelManager.instance.KillEnemy();
        sheep?.OnDrop(transform.position);
        sheep = null;
        SpriteRenderer image = transform.GetChild(1).GetComponent<SpriteRenderer>();
        image.color = Color.white;
        health.PlusHealth(health.maxHealth);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        health.Killed += OnKilled;
        if (LevelManager.instance != null) LevelManager.instance.AllSheepsStealStolen += StopMooving;
    }
    private void OnDisable()
    {
        health.Killed -= OnKilled;
        if (LevelManager.instance != null) LevelManager.instance.AllSheepsStealStolen -= StopMooving;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Sheep" && sheep == null)
        {
            sheep = collision.GetComponent<Sheep>();
            sheep.OnTake();
            SpriteRenderer image = transform.GetChild(1).GetComponent<SpriteRenderer>();
            image.color = Color.red;
        }
    }

}
