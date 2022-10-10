namespace Strategies
{
    public interface IDamageable
    {
        float MaxHealth { get; }
        
        void TakeDamage(float damage);
        
        void Die();
        
        void AddHealth(float health);
    }
}