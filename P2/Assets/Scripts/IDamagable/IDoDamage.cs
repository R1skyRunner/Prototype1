using System;

public interface IDoDamage 
{
    [Flags]
    public enum DamageType
    {
        None = 0,
       
        Fire = 1,
        Poison = 2,
        Bleeding = 4,
        Electrical = 8,
        Physical = 16,
    }

    public void DoDamage(IDamagable damagable, DamageType damageType) { }
    
    public int FireDamage { get;  set; }
    public int ElectricalDamage { get; set; }
    public int PoisonDamage { get; set; }
    public int PhysicalDamage { get; set; }
    public int BleedDamage { get; set; }    
}
