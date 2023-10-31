using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "EffectSO", menuName = "Effect")]
public class EffectSO : ScriptableObject
{
    public List<AudioClip> FootStepClips;
    public List<AudioClip> HitSoundClips;    
    public DecalProjector Decal;
    public List<VisualEffectAsset> HitEffectAsset;
    
}
