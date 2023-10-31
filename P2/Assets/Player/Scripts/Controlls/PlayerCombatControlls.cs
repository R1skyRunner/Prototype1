using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatControlls : MonoBehaviour
{    
    #region // Fields
    private PlayerScriptFacade playerScriptFacade;
    private WeaponHolder weaponHolder;

    #endregion
   
   [SerializeField] private Transform AimTarget; 
    
    private Vector3 TargetDir; 

    void Start()
    {        
        playerScriptFacade = GetComponent<PlayerScriptFacade>();
        weaponHolder = playerScriptFacade.weaponHolder;
    }
         
    internal void SetAim()
    {
        AimRotation();

        playerScriptFacade.rigController.SetAimRig(1);       
        PlayerCameraController.Instance.SetCamera(PlayerCameraController.Instance.AimCamera);
        playerScriptFacade.playerState.isAiming = true;
    }

    internal void ResetAim()
    {
        playerScriptFacade.rigController.SetAimRig(0);
        PlayerCameraController.Instance.SetCamera(PlayerCameraController.Instance.FreeLookCamera);
        playerScriptFacade.playerState.isAiming = false;
    }

    internal void Shoot(InputAction context)
    {
        if(weaponHolder == null ) return;   
        
         weaponHolder.Shoot(context); 
    }

    internal void Reload(InputAction.CallbackContext context)
    {
        if (playerScriptFacade.playerState.isReloading == true) return;
      
         playerScriptFacade.componentManager.PlayerAnimator.SetTrigger(playerScriptFacade.playerAnimation.reload);
    }

    internal void AimRotation()
    {       
       TargetDir = AimTarget.position - transform.position;
        
       Quaternion lookRotation = Quaternion.LookRotation(TargetDir, Vector3.up);         
       Quaternion affectedAxis = Quaternion.Euler(0 , lookRotation.eulerAngles.y, 0);
        
       playerScriptFacade.componentManager._rigidbody.MoveRotation(Quaternion.Lerp(playerScriptFacade.componentManager._rigidbody.rotation, affectedAxis, 8f * Time.deltaTime));
    }

    internal void Crouch(InputAction context)
    {
        if (context.IsPressed())
        {
            playerScriptFacade.playerState.isCrouching = true;
        }
        else
        {
            playerScriptFacade.playerState.isCrouching = false;
        }
    }

    internal void FeetAlignment()
    {

    }

    internal void HipFire(InputAction action)
    {
        if (action.IsPressed())
        {
            playerScriptFacade.rigController.SetAimRig(1);
        }
        else 
        {
            playerScriptFacade.rigController.SetAimRig(0);
        }
    }
}
