using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour, IPoolable
{
    public Bullet bulletPrefab;
    private Transform parent;
   
    public static ObjectPool<Bullet> bulletPool;
   
    public Bullet OnCreate()
    {
        Bullet bullet = Instantiate(bulletPrefab);
        bullet.gameObject.SetActive(false);
        bullet.transform.SetParent(parent, true);
        
        return bullet;
    }

    public void OnGet(Bullet bullet)
    {          
       
    }
    
    public void OnRelease(Bullet bullet)
    {       
        bullet.gameObject.SetActive(false);              
    }

    public void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet);
    }

    public void Initialize(Component component, Transform parent = null)
    {
        if(parent != null) 
        {
            this.parent = parent;
        }
             
        bulletPrefab = component.GetComponent<Bullet>();

        bulletPool = new ObjectPool<Bullet>(OnCreate, OnGet, OnRelease, OnDestroyBullet, false, 200, 10_000);       
    }    
}
