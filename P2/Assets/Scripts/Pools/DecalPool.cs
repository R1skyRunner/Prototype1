using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.HighDefinition;

public class DecalPool : MonoBehaviour, IPoolable
{
    private DecalProjector decal;    
    private CancellationTokenSource cancellationTokenSource;

    public static ObjectPool<DecalProjector> decalPool;
   
    private DecalProjector Create()
    {
        DecalProjector decal = Instantiate(this.decal);
        decal.gameObject.SetActive(false);
        
        return decal;
    }

    private void Return(DecalProjector decal)
    {
        decal.gameObject.SetActive(false);
    }

    private void Get(DecalProjector decal)
    {
        decal.gameObject.SetActive(true);
        FadeAway(decal);
    }

    private void Destroy(DecalProjector decal)
    {
        Destroy(decal);
    }
 
    public void Initialize(Component component, Transform parent = null)
    {                             
        cancellationTokenSource = new CancellationTokenSource();

        decal = component.GetComponent<DecalProjector>();
        decalPool = new ObjectPool<DecalProjector>(Create, Get, Return, Destroy, false, 10, 500);
    }

    public async void FadeAway(DecalProjector projector)
    {
        await Task.Delay(25000, cancellationTokenSource.Token);

        decalPool.Release(projector);
    }

    public static DecalProjector SetDecal(DecalProjector decal, DecalProjector TargetDecal)
    {
        decal.material = TargetDecal.material;
        decal.size = TargetDecal.size;
        decal.uvScale = TargetDecal.uvScale;    
        decal.fadeScale = TargetDecal.fadeScale;
        decal.affectsTransparency = TargetDecal.affectsTransparency;
        decal.decalLayerMask = TargetDecal.decalLayerMask;
        decal.fadeFactor = TargetDecal.fadeFactor;
        decal.drawDistance = TargetDecal.drawDistance;
        decal.size = TargetDecal.size;
        
        return decal;
    }

    private void OnDisable()
    {
        cancellationTokenSource.Cancel();
    }
}
