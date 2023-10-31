using UnityEngine;
using UnityEngine.Pool;
using System.Threading.Tasks;
using UnityEngine.VFX;
using System.Threading;

public class AudioSourcePool : MonoBehaviour, IPoolable
{
    public AudioSource AudioSource;
    private Transform parent;
    private CancellationTokenSource cancellationTokenSource;

    public static ObjectPool<AudioSource> audioSourcePool;

    public void Initialize(Component component, Transform parent = null)
    {
        if (parent != null)
        { 
            this.parent = parent; 
        }

        AudioSource = component.GetComponent<AudioSource>();
        audioSourcePool = new ObjectPool<AudioSource>(Create, Get, Return, Destroy, false, 10, 200);

        cancellationTokenSource = new CancellationTokenSource();
    }

    private AudioSource Create()
    {
        AudioSource source = Instantiate(this.AudioSource);
        source.gameObject.SetActive(false);

        source.transform.SetParent(parent, true);

        return source;
    }

    private void Get(AudioSource source)
    {
        source.gameObject.SetActive(true);
        FadeAway(source);
    }

    private void Return(AudioSource source)
    {
        source.gameObject.SetActive(false);
    }

    private void Destroy(AudioSource source)
    {
        Destroy(source);
    }

    public async void FadeAway(AudioSource source)
    {
        await Task.Delay(5000, cancellationTokenSource.Token);
        audioSourcePool.Release(source);
    }

    public static AudioSource SetAudioSource(AudioSource source, AudioSource TargetSource)
    {
        source.spread = TargetSource.spread;
        source.bypassEffects = TargetSource.bypassEffects;
        source.bypassListenerEffects = TargetSource.bypassListenerEffects;
        source.bypassReverbZones = TargetSource.bypassReverbZones;
        source.dopplerLevel = TargetSource.dopplerLevel;
        source.pitch = TargetSource.pitch;
        source.volume = TargetSource.volume;
        source.time = TargetSource.time;
        source.loop = TargetSource.loop;
        source.maxDistance = TargetSource.maxDistance;
        source.minDistance = TargetSource.minDistance;        
        source.panStereo = TargetSource.panStereo;

        return source;
    }

    private void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }
}

public class VisualEffectPool : MonoBehaviour, IPoolable
{
    public VisualEffect visualEffect;
    private Transform parnet;
    private CancellationTokenSource cancellationTokenSource;

    public static ObjectPool<VisualEffect> visualEffectPool;

    public void Initialize(Component component, Transform parent = null)
    {
        if(parent != null)
        {
          this.parnet = parent;
        }

        cancellationTokenSource = new CancellationTokenSource();

        visualEffect = component.GetComponent<VisualEffect>();
        visualEffectPool = new ObjectPool<VisualEffect>(Create, Get, Return, Destroy, false, 100, 1200);
    }

    private VisualEffect Create()
    {
        VisualEffect visualEffect = Instantiate(this.visualEffect);
        visualEffect.gameObject.SetActive(false);

        visualEffect.transform.SetParent(parnet, true);

        return visualEffect;
    }

    private void Get(VisualEffect effect)
    {
        
        FadeAway(effect);
    }

    private void Return(VisualEffect effect)
    {        
        effect.gameObject.SetActive(false);
    }

    private void Destroy(VisualEffect effect)
    {
        Destroy(effect);
    }

    private async void FadeAway(VisualEffect effect)
    {
        await Task.Delay( 2000, cancellationTokenSource.Token);

        visualEffectPool.Release(effect);
    }
  
    private void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }
}


