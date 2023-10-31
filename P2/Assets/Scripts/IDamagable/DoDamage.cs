using static IDoDamage;

public static class DoDamage
{   
    public static void GiveDamage(IDoDamage doDamage, IDamagable damagable, DamageType damageType)
    {
        if (HasDamageType(damageType, DamageType.Fire) == true)
        {            
            damagable.ApplyDamage(doDamage.FireDamage, damageType);
        }

        if (HasDamageType(damageType, DamageType.Bleeding) == true)
        {
            damagable.ApplyDamage(doDamage.BleedDamage, damageType);
        }

        if (HasDamageType(damageType, DamageType.Poison) == true)
        {
            damagable.ApplyDamage(doDamage.PoisonDamage, damageType);
        }

        if (HasDamageType(damageType, DamageType.Electrical) == true)
        {
            damagable.ApplyDamage(doDamage.ElectricalDamage, damageType);
        }

        if (HasDamageType(damageType, DamageType.Physical) == true)
        {
            damagable.ApplyDamage(doDamage.PhysicalDamage, damageType);
        }
    }

    public static int ApplyRessistence(int damage, DamageType damageType, IUnitStats unitStats)
    {
        if(HasDamageType(damageType, DamageType.Fire))
        {
            damage -= unitStats.FireResistence;
        }

        if (HasDamageType(damageType, DamageType.Bleeding))
        {
            damage -= unitStats.BleedResistence;
        }

        if (HasDamageType(damageType, DamageType.Electrical))
        {
            damage -= unitStats.ElectricityResistence;
        }

        if (HasDamageType(damageType, DamageType.Poison))
        {
            damage -= unitStats.PoisonResistence;
        }

        if (HasDamageType(damageType, DamageType.Physical))
        {
            damage -= unitStats.PhysicalResistence;
        }

        return damage;
    }

    public static bool HasDamageType(DamageType damageEnum, DamageType WantedType)
    {
        if((damageEnum & WantedType) != 0)
        {
            return true;
        }       

        return false;
    }

    /*
     * damageType {}
     * retunDamageType
     */
}
