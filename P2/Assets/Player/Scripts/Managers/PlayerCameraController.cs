using UnityEngine;
using Cinemachine;

public class PlayerCameraController : MonoBehaviour
{
    public static PlayerCameraController Instance;
      
    [SerializeField]
    internal FreeLookCameraTarget FreeLookTarget;

    #region // Cinemachine Properties
    [SerializeField]
    internal Camera playerCamera; 

    [SerializeField]
    internal CinemachineVirtualCamera AimCamera;

    [SerializeField]
    internal CinemachineVirtualCamera FreeLookCamera;

    internal CinemachinePOV AimPov;

    private ICinemachineCamera currentCamera;
    
    #endregion

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    void Start()
    {                           
        Initialize();        
    }

    private void LateUpdate()
    {
        ForceUpdateCamerasPos();
    }

    private void Initialize()
    {
        AimCamera.Priority = 0;
        FreeLookCamera.Priority = 1;

        currentCamera = null;

        SetCamera(FreeLookCamera);

        AimPov = AimCamera.GetCinemachineComponent<CinemachinePOV>();
    }

    internal void SetCamera(ICinemachineCamera camera)
    {
        if (currentCamera != null)
        {
            currentCamera.Priority = 0;           
            currentCamera = camera;
            currentCamera.Priority = 1;          
        }
        else
        {          
            currentCamera = camera;
            currentCamera.Priority = 1;
        }     
    }

    internal void ForceUpdateCamerasPos()
    {
       if (AimCamera.m_Priority < 1)
       {
            AimCamera.ForceCameraPosition(Camera.main.transform.position, Camera.main.transform.rotation);
       }

       if(FreeLookCamera.m_Priority > 0) 
       {
            FreeLookTarget.ToggleReadInput(true);            
       }
       else
       {
            FreeLookTarget.ToggleReadInput(false);                        
       }

    }

}
