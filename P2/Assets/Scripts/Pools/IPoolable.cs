using UnityEngine;

public interface IPoolable
{
    public void Initialize(Component component = null, Transform parent = null);    
}
