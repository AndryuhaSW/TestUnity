using UnityEngine;
using System.Threading.Tasks;

public class HealingEnemy : Enemy
{
    [SerializeField] private float healRadius;
    [SerializeField] private float healAmount;

    public override void Initialize(WayPoints wayPoints, float speed)
    {
        base.Initialize(wayPoints, speed);

        HealEnemies();
    }

    private async Task HealEnemies()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, healRadius);

            foreach (Collider2D collider in colliders)
            {
                Enemy enemy = collider.GetComponent<Enemy>();
                if (enemy != null && enemy != this)
                {
                    //Какой то странный баг вылезает. Когда врач умирает из первой толпы, во второй здоровье ломается у последних трех. нихуя не поянл, но надо что-то делать
                    //enemy.PlusHealth(healAmount);
                }
            }
            
            await Task.Delay(1000);
        }
    }

}
