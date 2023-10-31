using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerScriptFacade scriptFacade;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector3 SpherePosition;
    [SerializeField] private float SphereRadius;
  
    private Vector3 animatorPosition;
    private Quaternion rotation;

    [SerializeField] private LayerMask stepOverMask;
    [SerializeField] Vector3 bottomRayPos;
    [SerializeField] Vector3 upperRayPos;
    [SerializeField] private float StepRayLenght;
    private Ray up, bottom;

    private void Start()
    {
        scriptFacade = GetComponent<PlayerScriptFacade>();       
    }

  
    private void FixedUpdate()
    {        
        MoveAnimator();
        StepOver();
        ApplyGravity();
    }

    private void MoveAnimator()
    {
        animatorPosition = scriptFacade.componentManager.PlayerAnimator.deltaPosition;
              
        scriptFacade.componentManager._rigidbody.MovePosition(scriptFacade.componentManager._rigidbody.position += animatorPosition * 20 * Time.fixedDeltaTime);      
    }

    internal void Rotate(Vector3 Direction)
    {
        if (Direction != Vector3.zero)
        {
            rotation = Quaternion.LookRotation(Direction, Direction);
            scriptFacade.componentManager._rigidbody.MoveRotation(Quaternion.Slerp(scriptFacade.componentManager._rigidbody.rotation, rotation, rotationSpeed * Time.fixedDeltaTime));          
        }                  
    }
    
    internal void ApplyGravity()
    {
        // Speed up y velocity
        if (!Physics.CheckSphere(transform.position + SpherePosition, SphereRadius, groundMask))
        {        
             scriptFacade.componentManager._rigidbody.velocity += Vector3.down * 100 * Time.fixedDeltaTime;            
        }
        
        // Enable falling Animation
        if(!Physics.Raycast(transform.position + Vector3.up * 3, Vector3.down, 30f, groundMask)) 
        {
            scriptFacade.playerState.isFalling = true;
        }
        else
        {  
            scriptFacade.playerState.isFalling = false;
        }

    }
  
    internal void StepOver()
    {        
        up = new Ray(transform.position + upperRayPos, transform.forward);
        bottom = new Ray(transform.position + bottomRayPos, transform.forward);
       
        if (Physics.Raycast(bottom, StepRayLenght, stepOverMask))
        {   

           if (!Physics.Raycast(up, StepRayLenght, stepOverMask))
           {                     
             scriptFacade.componentManager._rigidbody.position += new Vector3(0, 0.15f, 0);
           }

        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + SpherePosition, SphereRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(up.origin, up.direction * StepRayLenght);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(bottom.origin, bottom.direction * StepRayLenght);
    }
}
