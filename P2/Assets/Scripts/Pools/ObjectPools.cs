using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

public class ObjectPools : MonoBehaviour
{       
    public List<IPoolable> pools = new List<IPoolable>();

    [SerializeField] private Transform BulletParent;  
    [SerializeField] private Transform EffectParent;
    [SerializeField] private Transform AudioParent;

    [SerializeField] private DecalProjector projector;   
    [SerializeField] private Bullet bullet;   
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private VisualEffect effect;
   
    public DecalPool decalPool;
    public BulletPool bulletPool;    
    public AudioSourcePool audioSourcePool;
    public VisualEffectPool visualEffectPool;
  
    void Start()
    {       
        decalPool = this.AddComponent<DecalPool>();
        bulletPool = this.AddComponent<BulletPool>();      
        audioSourcePool = this.AddComponent<AudioSourcePool>();
        visualEffectPool = this.AddComponent<VisualEffectPool>();
        
        bulletPool.Initialize(bullet, BulletParent);      
        decalPool.Initialize(projector);
        audioSourcePool.Initialize(audioSource, AudioParent);
        visualEffectPool.Initialize(effect, EffectParent);                    
    }     
}


 