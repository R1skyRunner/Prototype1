using System.Collections.Generic;
using UnityEngine;

public class PlayerScriptFacade : MonoBehaviour
{   
    #region // Scripts
    internal PlayerComponentManager componentManager;
    internal PlayerAnimation playerAnimation;
    internal PlayerMovement playerMovement;
    internal PlayerState playerState;
    internal PlayerCombatControlls playerCombat;
    internal PlayerStats playerStats;
    internal RigController rigController;
    internal CameraRecoil cameraRecoil;
    internal ShootRayCast ShootRayCast;

    #endregion

    internal PlayerInput input;

    public WeaponHolder weaponHolder;
    
    public List<Component> componentList = new List<Component>();

    private void Awake()
    {     
        GetComponents();    
        AddCompsToList();
        CheckListForNull();

        input = GetComponent<PlayerInput>();

        if (weaponHolder == null)
        {
            weaponHolder = GetComponentInChildren<WeaponHolder>();
        }
    }
  
    private void GetComponents()
    {
        componentManager = GetComponent<PlayerComponentManager>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
        playerState = GetComponent<PlayerState>();  
        playerCombat = GetComponent<PlayerCombatControlls>();
        rigController = GetComponent<RigController>();
        playerStats = GetComponent<PlayerStats>();
        cameraRecoil = GetComponent<CameraRecoil>();
        ShootRayCast = GetComponent<ShootRayCast>();   
        
    }

    private void AddCompsToList()
    {
        AddToList(componentManager);
        AddToList(playerAnimation);
        AddToList(playerMovement);
        AddToList(playerState);
        AddToList(playerCombat);
        AddToList(rigController);
        AddToList(playerStats);
        AddToList(cameraRecoil);
        AddToList(ShootRayCast);
    }

    private void CheckListForNull()
    {
        foreach (var component in componentList) 
        {
            if(component == null)
            {
                Debug.LogError("Nullable component in the list. Check if all componnents are referenced in " + this.name + " " + this.GetType().Name);
            }
        }
    }

    private void AddToList(Component component)
    {
        componentList.Add(component);
    }
}
