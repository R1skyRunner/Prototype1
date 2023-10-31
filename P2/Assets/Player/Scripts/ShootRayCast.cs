using UnityEngine;

public class ShootRayCast : MonoBehaviour
{  
    internal Vector3 hitPoint;
   
    [SerializeField] internal LayerMask LayerMask;

    void LateUpdate()
    {
        ShootRay();
        Debug.DrawLine(Camera.main.transform.position, hitPoint, Color.black);
        
    }

    private void ShootRay()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 1000f, LayerMask))
        {
            hitPoint = hit.point;
            
        }
        else
        {
            hitPoint = Camera.main.transform.position + (Camera.main.transform.forward * 100);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(hitPoint, 0.5f);
    }

}
