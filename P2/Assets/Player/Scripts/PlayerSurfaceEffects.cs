using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerSurfaceEffects : MonoBehaviour
{
    AudioSource audioSource;
   
    void Start()
    {
        audioSource = GetComponentInParent<AudioSource>();
    }   

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent<Renderer>(out Renderer renderer);

        
        if(renderer != null)
        {
            Surface surface = SurfaceManager.Instance.GetSurface(renderer);

            SurfaceManager.DoFootStepImpact(surface, audioSource);
        }
    }
}
