public interface IDamagable
{
    public void Reaction();
    public void ApplyDamage(int damage, IDoDamage.DamageType damageType);
    public void OnDamageTaken(IDamageSender damageSender);
}