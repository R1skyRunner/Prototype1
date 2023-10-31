using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class WeaponInput : MonoBehaviour
{  
    public UnityEvent FireTriggered;
    public UnityEvent FireIsPressed;
    public UnityEvent FireReleased;
   
    public void Shoot(InputAction FireInput = null)
    {
        if (FireInput == null) 
        {
            Debug.LogWarning("No InputAction is detected for WeaponInput in " + this.GetType());
            return;
        }

        FireInput.started += OnInputDown;

       if (FireInput.IsPressed())
       {
           FireIsPressed.Invoke();
       }
        
        FireInput.canceled += OnInputUp;              
    }   
    
    private void OnInputUp(InputAction.CallbackContext context)
    {
       FireReleased.Invoke();
    }

    private void OnInputDown(InputAction.CallbackContext context)
    {
       FireTriggered.Invoke();
    }
}
