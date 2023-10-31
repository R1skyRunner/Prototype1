using UnityEngine;

[RequireComponent(typeof(Health))]
public class TakeDamage : MonoBehaviour, IDamagable
{
    private Health Health; 
    private IUnitStats stats;

    Animator animator;

    private void Start()
    {
        Health = GetComponent<Health>();
        stats = GetComponent<IUnitStats>();       
        animator = GetComponent<Animator>();
    }
  
    public void Reaction()
    {
        animator.SetTrigger("Hit");

        Debug.Log("Parent Damaege apllied");
    }  
  
    public void ApplyDamage(int damage, IDoDamage.DamageType damageType)
    {
        Reaction();

        damage = DoDamage.ApplyRessistence(damage, damageType, stats);
        Debug.Log(damageType);

        if (damage > 0)
        {
            Health.currentHealth -= damage;
        }
        else 
        { 
            Debug.Log("Fully resisted");
        }
    }

    public void OnDamageTaken(IDamageSender damageSender)
    {
        damageSender.OnDamageGiven();
    }
}