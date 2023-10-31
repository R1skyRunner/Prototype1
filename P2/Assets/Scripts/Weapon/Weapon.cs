using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

[RequireComponent(typeof(WeaponInput))] 
public class Weapon : MonoBehaviour, IDoDamage
{      
    #region // Classes
    public WeaponInput WeaponInput;
    public WeaponSO WeaponSO;
    public WeaponRecoilSO RecoilSO;

    public IDamageSender DamageSender;

    #endregion

    #region // Objects
    public Transform LeftHandTarget;
    public Transform Barrel;
    public GameObject Magazine;

    internal Animator animator;

    [SerializeField] private VisualEffect muzzleEffect;

    [SerializeField] private VisualEffectAsset muzzleEffectAsset;
 
    [SerializeField] private AudioSource AudioSource;
   
    [SerializeField] private AudioClip clip;
   
    #endregion

    #region // Variables   
    [HideInInspector] public int currentMagSize;
    [HideInInspector] public bool canShoot;
    [HideInInspector] public bool FireTriggered = false;
    private float lastShot;
    private bool loading = false;

    [HideInInspector] public Vector3 shootDirection;

    private int availableAmmo = 400, reloadAmount, maxReload, BulletsUsed;
    public float TimeFireIsPressed;

    #endregion

    #region// RecoilVaiables   
    [HideInInspector] public float recoilToAddY, recoilToAddX, lastShotValueY, lastShotValueX, recoilValueY, recoilValueX;
    [HideInInspector] public float RecoilDuration;

   
    public delegate void ApplyRecoil();
    private ApplyRecoil applyRecoil;

    #endregion

    #region //Damage

    private int Fire, Electric, Poison, Physical, Bleed;

    public int FireDamage
    {     
        get { return Fire;}
        set { Fire += value;}
    }
    public int ElectricalDamage
    {
        get { return Electric; }
        set { Electric += value; }
    }
    public int PoisonDamage
    {
        get { return Poison; }
        set { Poison += value; }
    }
    public int PhysicalDamage
    {
        get { return Physical; }
        set { Physical += value; }
    }
    public int BleedDamage
    {
        get { return Bleed; }
        set { Bleed += value; }
    }



    public void SetDamageStats(IDoDamage stats = null)
    {
        if(stats == null)
        {
            FireDamage = WeaponSO.FireDamage;
            ElectricalDamage = WeaponSO.ElectricDamage;
            PoisonDamage = WeaponSO.PoisonDamage;
            PhysicalDamage = WeaponSO.PhysicalDamage;
            BleedDamage = WeaponSO.BleedDamage;
            return;
        }

        FireDamage += stats.FireDamage;
        ElectricalDamage += stats.ElectricalDamage;
        PoisonDamage += stats.PoisonDamage;
        PhysicalDamage += stats.PhysicalDamage;
        BleedDamage += stats.BleedDamage;

    }

    #endregion  

    public delegate void ShootType();
    public ShootType shootType;
   
    private void Start()
    {       
        animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
        currentMagSize = WeaponSO.MagSize;

        muzzleEffect.visualEffectAsset = muzzleEffectAsset;

        SetDamageStats();
               
        SetFireMode();
        SetRecoilMode();
        ResetRecoil();  
    }
     
    #region // ShootSystem
    public void CallShoot()
    {
        canShoot =
            (UnityEngine.Time.time > lastShot + (60f / WeaponSO.ShotsPerMinute))
           && currentMagSize > 0; 
          // && loading == false;

        if (!canShoot) return;

        applyRecoil();       
        shootType();
        lastShot = UnityEngine.Time.time;
        FireTriggered = true;   
    }

    private void Shoot()
    {
        currentMagSize -= 1;

        animator.SetTrigger("Shoot");

        PlayShotEffects();

        if(WeaponSO.shootType == WeaponSO.ShootType.Projectile) { ShootProjectile(Barrel, WeaponSO.ProjectileForce);}

        if(WeaponSO.shootType == WeaponSO.ShootType.Raycast) { ShootRayCast(Barrel); }                                                          
    }

    private void ShootProjectile(Transform barrelPos, float force)
    {
        Bullet instance = BulletPool.bulletPool.Get();
               
        SetBullet(instance, Barrel.position);               
       
        instance.gameObject.SetActive(true);

        instance.AddForce(shootDirection, force);              
    }

    private void ShootRayCast(Transform barrelPos)
    {
        if (Physics.Raycast(barrelPos.position, barrelPos.forward, 1000f, WeaponSO.LayerMask))
        {                                 
             Debug.Log("Hiit");                       
        }
    }

    #endregion
    
    #region // Recoil  
    public void ReduceRecoilRecoilOverTime()
    {
        recoilToAddY = Mathf.Lerp(recoilToAddY, RecoilSO.minY, RecoilSO.RecoilChangeSmoothness * Time.deltaTime);
        recoilToAddX = Mathf.Lerp(recoilToAddX, RecoilSO.minX, RecoilSO.RecoilChangeSmoothness * Time.deltaTime);

        TimeFireIsPressed = Mathf.Lerp(TimeFireIsPressed, 0, RecoilSO.RecoilChangeSmoothness * Time.deltaTime);
    }

    public void ApplyRecoilNumbers()
    {
        recoilValueY += recoilToAddY;
        recoilValueX += UnityEngine.Random.Range(-2, 2); //XrecoilAmount;
        RecoilDuration = RecoilSO.RecoilResetDuration;              
    }

    public void ApplyRecoilCurve()
    {
        TimeFireIsPressed += 10 * Time.deltaTime;
       
        recoilValueY += RecoilSO.RecoilY.Evaluate(TimeFireIsPressed);
        recoilValueX += Random.Range(RecoilSO.RecoilX.Evaluate(TimeFireIsPressed), -RecoilSO.RecoilX.Evaluate(TimeFireIsPressed));
        RecoilDuration = RecoilSO.RecoilResetDuration;
    }

    public void ResetRecoil()
    {
        TimeFireIsPressed = 0;

        recoilToAddY = RecoilSO.maxRecoilY;
        recoilToAddX = RecoilSO.maxRecoilX;
    }

    #endregion

    #region //FireModes
    private IEnumerator Burst()
    {
        for(int i = 0; i < WeaponSO.bulletsToSpawn; ++i) 
        {
           if (currentMagSize <= 0)
           {                             
               break; 
           }

            Shoot();
            currentMagSize -= 1;

            yield return new WaitForSeconds(WeaponSO.timeBetweenShots);           
        }        
    }
    private void BurstFire()
    {
        StartCoroutine(Burst());
    }

    private void SingleFire()
    {
        if (!loading)
        {
            Shoot();
           // loading = true;
           // use fo somthig like revolver or bolt rifle
        }
        else
        {
            Debug.Log("Loading Weapon");
            //load animation.  
        }

        Debug.Log("SingleFire");           
    }

    private void AutoFire()
    {
        Shoot();
        Debug.Log("Firre");
    }

    #endregion

    #region // Set Parameters
    private void SetBullet(Bullet instance, Vector3 pos)
    {
        instance.DamageType = WeaponSO.DamageType;
        instance.IDamage = this;

        instance.DamageSender = DamageSender;

        instance.transform.position = pos;
           
        SetTrailParameters(instance.trailRenderer);      
    }

    private void SetFireMode()
    {
        if(WeaponSO.fireType == WeaponSO.FireType.SingleFire)
        {
            shootType = SingleFire;
        }

        if (WeaponSO.fireType == WeaponSO.FireType.AutoFire)
        {
            shootType = AutoFire;
        }

        if (WeaponSO.fireType == WeaponSO.FireType.BurstFire)
        {
            shootType = BurstFire;
        }        
    }

    private void SetTrailParameters(TrailRenderer trail)
    {
        trail.time = WeaponSO.trailRenderer.time;
        trail.widthMultiplier = WeaponSO.trailRenderer.widthMultiplier;
        trail.widthCurve = WeaponSO.trailRenderer.widthCurve;
        trail.endColor = WeaponSO.trailRenderer.endColor;
        trail.startColor = WeaponSO.trailRenderer.startColor;
        trail.startWidth = WeaponSO.trailRenderer.startWidth;
        trail.endWidth = WeaponSO.trailRenderer.endWidth;
        trail.emitting = WeaponSO.trailRenderer.emitting;
        trail.autodestruct = WeaponSO.trailRenderer.autodestruct;
        trail.alignment = WeaponSO.trailRenderer.alignment;
        trail.colorGradient = WeaponSO.trailRenderer.colorGradient;
        trail.material = WeaponSO.trailRenderer.sharedMaterial;

    }

    private void SetRecoilMode()
    {
        if (RecoilSO.UseRecoilCurve == true)
        {
            applyRecoil = ApplyRecoilCurve;
        }
        else
        {
            applyRecoil = ApplyRecoilNumbers;
        }
    }

    #endregion
   
    public void Reload()
    {
        if (availableAmmo <= 0) return;

         BulletsUsed = WeaponSO.MagSize - currentMagSize;
         maxReload = Mathf.Min(WeaponSO.MagSize, BulletsUsed);

         reloadAmount = Mathf.Min(availableAmmo, maxReload);
                 
         currentMagSize += reloadAmount;
         availableAmmo -= reloadAmount;                         
    }

    public void AlignTransform()
    {
        transform.forward = transform.parent.up.normalized;

        if (WeaponSO.PositionOffset != Vector3.zero)
        {
            transform.localPosition = Vector3.zero;
            transform.localPosition += WeaponSO.PositionOffset;
        }

        if (WeaponSO.RotationOffset != Vector3.zero)
        {
            transform.localRotation = Quaternion.Euler(WeaponSO.RotationOffset);
        }     
    }
       
    private void PlayShotEffects()
    {
        if (muzzleEffectAsset != null) 
        {                               
            muzzleEffect.Play();
        }

        if(clip != null)
        { 
          AudioSource.PlayOneShot(clip);
        }                    
    } 
}

