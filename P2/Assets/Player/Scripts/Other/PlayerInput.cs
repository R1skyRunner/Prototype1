using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
   internal PlayerControls playerControls;
   private PlayerScriptFacade scrFacade;

   internal Vector2 mouseDelta;      

   private void Start()
   { 
      scrFacade = GetComponent<PlayerScriptFacade>();
 
      #region//Controls
      playerControls = new PlayerControls();
      playerControls.Enable();
      playerControls.Movement.Enable();
    
      #endregion

      playerControls.Movement.Reload.performed += scrFacade.playerCombat.Reload;

      playerControls.Movement.Aim.performed += SetAim;
      playerControls.Movement.Aim.canceled += ResetAim;
    
   }     

    private void LateUpdate()
    {      
       UpdateConstantly();
       Cursor.visible = false;
       Cursor.lockState = CursorLockMode.Locked;
       Fire();
       Run();
       Crouch();
        
       if(!playerControls.Movement.Aim.IsPressed())
       {
           scrFacade.playerCombat.HipFire(playerControls.Movement.Fire);
       }     
       
       if(Input.GetKeyDown(KeyCode.Escape)) {Application.Quit(); }

    }    

    private void UpdateConstantly()
    {
        #region //Input
        Vector3 input = new Vector3(
            playerControls.Movement.Walk.ReadValue<Vector2>().x,
            0,
            playerControls.Movement.Walk.ReadValue<Vector2>().y);

        Vector3 LookRotation = Quaternion.Euler(0, PlayerCameraController.Instance.playerCamera.transform.eulerAngles.y, 0) * new Vector3(input.x, 0, input.z);
       
        #endregion

        scrFacade.playerAnimation.WalkAnim(input.x, input.z);

        mouseDelta = playerControls.Movement.Look.ReadValue<Vector2>();

        if (scrFacade.playerState.isAiming == false)
        {
            scrFacade.playerMovement.Rotate(LookRotation);
        }
        else
        {
            scrFacade.playerCombat.AimRotation();
        }     
    }

    private void SetAim(InputAction.CallbackContext context)
    {
        scrFacade.playerCombat.SetAim();
    }

    private void ResetAim(InputAction.CallbackContext context)
    {
        scrFacade.playerCombat.ResetAim();
    }

    private void Fire()
    {        
      scrFacade.playerCombat.Shoot(playerControls.Movement.Fire);      
    }   

    private void Run()
    {       
       scrFacade.playerState.isRunning = playerControls.Movement.Run.IsPressed();               
    }

    private void Crouch()
    {
        scrFacade.playerCombat.Crouch(playerControls.Movement.Crouch);
    }
}