using UnityEngine;

public class ChildObjectTakeDamage : MonoBehaviour, IDamagable
{
    [SerializeField]
    [Range(-1f, 2f)]
    private float DamageMultiplier;

    private IDamagable parent;

    private void Start()
    {
        parent = GetComponentInParent<TakeDamage>();
    }

    public void Reaction()
    {
        Debug.Log("Child Taken Damage " + this.gameObject.name);
    }

    public void ApplyDamage(int damage, IDoDamage.DamageType damageType)
    {
        damage = ReduceDamage(damage);

        Debug.Log("Child Taken Damage " + this.gameObject.name + " " + damage + " " + damageType);

        parent.ApplyDamage(damage, damageType);
    }
              
    public int ReduceDamage(int damage)
    {
        damage +=  Mathf.CeilToInt(DamageMultiplier * damage);

        return damage;
    }

    public void OnDamageTaken(IDamageSender damageSender)
    {
        parent.OnDamageTaken(damageSender);
    }
}
