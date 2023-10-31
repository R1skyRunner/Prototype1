using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

public class SurfaceManager : MonoBehaviour
{    
    public List<Surface> Surfaces;
    
    public EffectSO defaultEffect;

    public static SurfaceManager Instance;  
    public enum ImpactType {Bullet, Melee};

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else DestroyImmediate(Instance);       
    }

    public Surface GetSurface(Renderer renderer)
    {
        if(Surfaces.Count <= 0) return null;

        Surface surface = Surfaces.Find(surface => surface.Material == renderer.sharedMaterial);

        if(surface != null) 
        {
            return surface;
        }
       
        Debug.LogWarning("Surface could not be found in " + renderer.name);
        return null;
    }

    public static void DoImpact(Surface surface, Vector3 pos, Vector3 rot, ImpactType impactType, Transform parent = null)
    {
        if(surface == null)
        {
            DefaultPhysicalEffect(pos, rot, parent);           
            return;
        }

        if(impactType == ImpactType.Bullet) 
        { 
            BulletImpact(surface, pos, rot, parent);
        }
        else if (impactType == ImpactType.Melee)
        {
            MeleeImpact();
        }
    }

    #region // Impacts
    private static void DefaultPhysicalEffect(Vector3 pos, Vector3 rot, Transform parent = null)
    {
        Quaternion rotation = Quaternion.LookRotation(rot);

        if (Instance.defaultEffect == null) return;

        if(Instance.defaultEffect.HitEffectAsset.Count > 0)
        {
            SpawnVisualEffect(pos, rotation, Instance.defaultEffect.HitEffectAsset[0]);
        }

        if (Instance.defaultEffect.HitSoundClips.Count > 0)
        {
            SpawnSurfaceSound(pos, rotation, Instance.defaultEffect.HitSoundClips[0]);
        }

        if (Instance.defaultEffect.Decal != null)
        {
            SpawnSurfaceDecal(pos, rotation, Instance.defaultEffect.Decal , parent);
        }        
    }

    public static void DoFootStepImpact(Surface surface, AudioSource audioSource)
    {
        if (surface == null)
        {
            audioSource.PlayOneShot(Instance.defaultEffect.FootStepClips[UnityEngine.Random.Range(0, Instance.defaultEffect.FootStepClips.Count)]);           
            return;
        }

        if (surface.Effect.FootStepClips.Count <= 0) return;

         audioSource.PlayOneShot(surface.Effect.FootStepClips[UnityEngine.Random.Range(0, surface.Effect.FootStepClips.Count)]);
        
        // no particle effect yet.
    }

    private static void BulletImpact(Surface surface, Vector3 pos, Vector3 rot, Transform parent)
    {
        Quaternion rotation = Quaternion.LookRotation(rot);

        if(surface.Effect.Decal != null)
        {
            SpawnSurfaceDecal(pos, rotation, surface.Effect.Decal, parent);
        }
        if (surface.Effect.HitEffectAsset.Count > 0)
        {
            SpawnVisualEffect(pos, rotation, surface.Effect.HitEffectAsset[0]);
        }
        if (surface.Effect.HitSoundClips.Count > 0)
        {
            SpawnSurfaceSound(pos, rotation, surface.Effect.HitSoundClips[0]);
        }       
    }

    private static void MeleeImpact()
    {
      // spawnParticle
      // spawn Decal
      // spawn AudioSource and PlayClip
    }

    #endregion

    #region // Spawn Objects
    private static void SpawnSurfaceSound(Vector3 pos, Quaternion rot, AudioClip clip)
    {
        AudioSource audio = AudioSourcePool.audioSourcePool.Get();

        audio.transform.position = pos;
        audio.transform.rotation = rot;
        audio.PlayOneShot(clip);
    }
  
    private static void SpawnSurfaceDecal(Vector3 pos, Quaternion rot, DecalProjector decal, Transform parent = null)
    {
        DecalProjector projector = DecalPool.decalPool.Get();
        
        if(parent != null)
        {
            projector.gameObject.transform.SetParent(parent.transform);
        }
       
        projector = DecalPool.SetDecal(projector, decal);

        projector.transform.position = pos;
        projector.transform.rotation = rot;
    }

    private static void SpawnVisualEffect(Vector3 pos, Quaternion rot, VisualEffectAsset effectAsset)
    {
        VisualEffect visualEffect = VisualEffectPool.visualEffectPool.Get();
        visualEffect.visualEffectAsset = effectAsset;

        visualEffect.gameObject.SetActive(true);

        visualEffect.transform.position = pos;
        visualEffect.transform.rotation = rot;
        visualEffect.Play();       
    }

    #endregion
}

[Serializable]
public class Surface
{
    public Material Material;
    public EffectSO Effect;
}
