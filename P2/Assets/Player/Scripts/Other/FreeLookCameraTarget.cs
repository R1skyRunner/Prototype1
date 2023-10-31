using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeLookCameraTarget : MonoBehaviour
{
    public Transform cameraTarget;
    public PlayerInput input;

    public float Sensitivity;

    private Vector2 inputSum;
    private Vector2 InputValue;

    private Quaternion rotationWithInput;
  
    private bool ReadInput;
              
    private void LateUpdate()
    {
        if (input == null) return;
        
        InputValue = input.playerControls.Movement.Look.ReadValue<Vector2>();
        
        RotateCameraTarget();       
    }       

    public bool ToggleReadInput(bool state)
    {
        ReadInput = state;
        return ReadInput;
    } 

    private void RotateCameraTarget()
    {
        if (ReadInput == true)
        {
            RotateWhenActive();           
        }
        else if(ReadInput == false)
        {
            RotateWhenInActive();
        }

        inputSum.y = Mathf.Clamp(inputSum.y, -75, 75);

        rotationWithInput = Quaternion.Euler(-inputSum.y, inputSum.x, 0);

        cameraTarget.rotation = Quaternion.Lerp(cameraTarget.rotation, rotationWithInput, 50f * Time.deltaTime);
    }

    private void RotateWhenActive()
    {
        if ( InputValue != Vector2.zero)
        {                      
            inputSum += InputValue.normalized * (Sensitivity * Time.deltaTime);
            inputSum.y = Mathf.Clamp(inputSum.y, -75, 75);         
        }          
    }

    private void RotateWhenInActive()
    {
       inputSum.x = PlayerCameraController.Instance.AimPov.m_HorizontalAxis.Value;
       inputSum.y = -PlayerCameraController.Instance.AimPov.m_VerticalAxis.Value;           
    }
}
