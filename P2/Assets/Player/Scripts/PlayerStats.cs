using UnityEngine;

public class PlayerStats : MonoBehaviour, IUnitStats, IDoDamage
{
    [Header("Resistence stats")]
    public int fireResistence;
    public int electricityResistence;
    public int poisonResistence;
    public int physicalResistence;
    public int bleedResistence;

    [Space(5f)]

    [Header("Damage stats")]
    public int fireDamage;
    public int electricalDamage;
    public int poisonDamage;
    public int physicalDamage;
    public int bleedDamage;

    #region //Resistence
    public int FireResistence
    {
        get { return fireResistence; }
        set { fireResistence = value; }
    }

    public int ElectricityResistence
    {
        get { return electricityResistence; }
        set { electricityResistence = value; }
    }

    public int PoisonResistence
    {
        get { return poisonResistence; }
        set { poisonResistence = value; }
    }

    public int PhysicalResistence
    {
        get { return physicalResistence; }
        set { physicalResistence = value; }
    }

    public int BleedResistence
    {
        get { return bleedResistence; }
        set { bleedResistence = value; }
    }

    #endregion

    #region //Damage
    public int FireDamage
    {
        get { return fireDamage; }
        set { fireDamage = value; }
    }

    public int ElectricalDamage
    {
        get { return electricalDamage; }
        set { electricalDamage = value; }
    }

    public int PoisonDamage
    {
        get { return poisonDamage; }
        set { poisonDamage = value; }
    }

    public int PhysicalDamage
    {
        get { return physicalDamage; }
        set { physicalDamage = value; }
    }

    public int BleedDamage
    {
        get { return bleedDamage; }
        set { bleedDamage = value; }
    }

    #endregion

}
