using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSO", menuName = "Weapon")]
public class WeaponSO : ScriptableObject
{
    public string AnimatorLayerName;

    public Vector3 PositionOffset;
    public Vector3 RotationOffset;

    #region // Stats
    [Header("Parameters for Burst FireMode"), Tooltip("Ignore if Firemode is not Burst type.")]
    public float timeBetweenShots;
    public int bulletsToSpawn;

    [Space, Header("Other Stats")]
    public int ShotsPerMinute;
    public int MagSize = 10;
    public float ProjectileForce = 100f;

    [Space, Header("Damage Stats")]
    public IDoDamage.DamageType DamageType;

    [Tooltip("If associated DamageType is not selected the int damage will not be passed.")]   
    public int FireDamage, ElectricDamage, PoisonDamage, PhysicalDamage, BleedDamage;

    #endregion

    #region // Visual Components
    [Space, Header("Visual Components")]
    public TrailRenderer trailRenderer;
    public AudioClip[] AudioClips;
    public ParticleSystem[] ParticleSystems;

    #endregion

    #region // ShootType
    [Space, Header("Fire Options")]
    public ShootType shootType;
    public FireType fireType;

    public LayerMask LayerMask;

    public enum FireType { SingleFire, BurstFire, AutoFire};
    public enum ShootType { Projectile, Raycast};

    #endregion
}
