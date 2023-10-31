using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] internal float currentHealth;
    [SerializeField] internal float maxHealth;

    private Ragdoll ragdoll;

    // Start is called before the first frame update
    void Start()
    {        
        currentHealth = maxHealth;

        ragdoll = GetComponentInChildren<Ragdoll>();
    }

    private void LateUpdate()
    {
      if (currentHealth <= 0)
      {
            Debug.Log("Dead");

            if (ragdoll != null && ragdoll.IsActive == false)
            {
                ragdoll.SetRagdollActive(true);
            }
      }
     
    }

}
