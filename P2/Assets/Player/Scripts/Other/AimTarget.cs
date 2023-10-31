using UnityEngine;


public class AimTarget : MonoBehaviour
{
    [SerializeField]
    private float distFromTheCamera;
    
    void LateUpdate()
    {
        if(PlayerCameraController.Instance == null) return;
       
        transform.position = Camera.main.transform.position + (Camera.main.transform.forward * distFromTheCamera);

    }   
}
