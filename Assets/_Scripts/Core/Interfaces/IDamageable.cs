namespace _Scripts.Core.Interfaces
{
    public interface IDamageable
    {
        public float CurrentHealth { get; }
        public float MaxHealth { get; }
        public bool IsDead { get; }

        public void TakeDamage(float damage);
        public void SetHealth(float health);
        public void SetMaxHealth(float health);
    }
}
