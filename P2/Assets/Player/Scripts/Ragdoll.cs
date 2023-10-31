using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{     
    [SerializeField] internal Animator animator;
   
    public Rigidbody[] rigidBodies;  
    public Collider[]  Colliders;    
    public CharacterJoint[] joints;

    public bool IsActive;

    private void Start()
    {           
        SetRagdollActive(false);
    }   

    internal async void SetRagdollActive(bool state)
    {
        if (animator == null || rigidBodies.Length <= 0 || Colliders.Length <= 0 || joints.Length <= 0)
        {
            animator.enabled = true;
            return;
        }
        
        animator.enabled = !state;
              
        foreach (var joint in joints)
        {
            joint.enableCollision = state;
        }

        foreach (var rigidBody in rigidBodies)
        {
            rigidBody.isKinematic = !state;           
        }       

        IsActive = state;

        await Task.Yield();       
    }        
}


