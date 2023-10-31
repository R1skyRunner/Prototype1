using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static IDoDamage;

public class Bullet : MonoBehaviour
{
    private CancellationToken token;
    private CancellationTokenSource source;

    #region // Components

    public Rigidbody rb;
    public Collider capsuleCollider;

    public TrailRenderer trailRenderer;

    public IDamageSender DamageSender;

    [HideInInspector] public DamageType DamageType;
    public IDoDamage IDamage;
    #endregion

    public void ResetBullet()
    {
        trailRenderer.Clear();
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void AddForce(Vector3 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode.Impulse);
    }

    private void ReturnToPool()
    {
        BulletPool.bulletPool.Release(this);
    }

    private async void ReturnIfNotCollided()
    {
        await Task.Delay(10000, token);
        BulletPool.bulletPool.Release(this);
    }

    private void OnCollisionEnter(Collision collider)
    {
        ContactPoint[] points = new ContactPoint[1];

        points = collider.contacts;

        collider.collider.TryGetComponent<IDamagable>(out IDamagable damagable);
        collider.collider.TryGetComponent<Renderer>(out Renderer renderer);      
        
        if (damagable != null)
        {
            DoDamage.GiveDamage(IDamage, damagable, DamageType);

            damagable.OnDamageTaken(DamageSender);
        }

        if(renderer == null)
        {
            renderer = collider.collider.GetComponentInParent<Renderer>();                    
        }
     
        if (renderer != null)
        {
            Surface surface = SurfaceManager.Instance.GetSurface(renderer);

            SurfaceManager.DoImpact(surface, points[0].point, points[0].normal, SurfaceManager.ImpactType.Bullet, collider.transform);
        }
       
        ResetBullet();
        ReturnToPool();
    }

    private void OnEnable()
    {
        source = new CancellationTokenSource();
        token = source.Token;

        ReturnIfNotCollided();
    }

    private void OnDisable()
    {
        ResetBullet();

        source.Cancel();
        source.Dispose();
    }

}


