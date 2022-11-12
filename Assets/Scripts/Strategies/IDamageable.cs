namespace Strategies
{
    public interface IDamageable
    {
        float MaxHealth { get; }
        
        void TakeDamage(float damage, bool crit);
        
        void Die();
        
        void AddHealth(float health);
    }
}