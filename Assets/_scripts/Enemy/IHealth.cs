public interface IHealth
{
    float currentHealth { get; set; }
    float maxHealth { get; set; }

    void PlusHealth(float health);
    void MinusHealth(float health);
    void Kill();
}