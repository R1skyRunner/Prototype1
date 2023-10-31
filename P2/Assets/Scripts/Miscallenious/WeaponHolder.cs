using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponHolder : MonoBehaviour
{
    #region //Objects
    [SerializeField]
    internal Transform leftHandGrip;

    [SerializeField]
    internal Transform leftHandTarget;
    
    [SerializeField]
    internal GameObject[] weapons;

    internal GameObject WeaponMagazine;

    #endregion
   
    #region //Fields
   [SerializeField] internal Weapon weaponClass;
    private GameObject currentObj;
    private PlayerScriptFacade scriptFacade;
    private CameraRecoil cameraRecoil;

    #endregion
   
    private int weaponIndex;


    [SerializeField]
    internal bool WeaponEquiped;
       
    private void Start()
    {
        scriptFacade = GetComponentInParent<PlayerScriptFacade>();   

        cameraRecoil = scriptFacade.cameraRecoil;
        weaponIndex = 0;
    }

    private void LateUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Q)) 
        {
            SpawnWeapon();            
        }

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            CleanHands();
        }
       
        if (weaponClass != null)
        {
            leftHandTarget.position = leftHandGrip.position;
            leftHandTarget.rotation = leftHandGrip.rotation;
          
            weaponClass.shootDirection = scriptFacade.ShootRayCast.hitPoint - weaponClass.Barrel.position;

            Debug.DrawRay(weaponClass.Barrel.position, weaponClass.shootDirection * 100, Color.blue);
        }       
    }

    internal void Reload()
    {
        if (weaponClass == null) return;
         
         weaponClass.Reload();     
    }

    private void SpawnWeapon(GameObject gameObject = null)
    {
        if (currentObj != null)
        {            
            Destroy(currentObj);
            Destroy(WeaponMagazine);
        }

        if(weaponIndex > weapons.Length - 1)
        {
            weaponIndex = 0;
        }

        currentObj = Instantiate(weapons[weaponIndex], transform.position, Quaternion.identity, this.transform);
              
        currentObj.TryGetComponent(out Weapon weapon);

        SetWeapon(weapon);

        weaponIndex += 1;            
    }

    private void SetWeapon(Weapon weapon)
    {
        if (weapon == null) return;
        
        weaponClass = weapon; 
        leftHandGrip = weapon.LeftHandTarget;
        cameraRecoil.weapon = weapon;
        WeaponEquiped = true;

        weapon.DamageSender = GetComponentInParent<IDamageSender>();

        scriptFacade.playerAnimation.SetWeaponLayer(scriptFacade.playerAnimation.GetWeaponLayerIndex(weapon.WeaponSO.AnimatorLayerName));

        weapon.AlignTransform();
        weapon.SetDamageStats(scriptFacade.playerStats);

        GetMagReference(weapon.Magazine);                   
    }

    private void GetMagReference(GameObject weaponMag)
    {
        WeaponMagazine = Instantiate(weaponMag, this.transform);     
        WeaponMagazine.SetActive(false);
    }

    internal void CleanHands()
    {
        Destroy(currentObj);
        scriptFacade.playerAnimation.ResetCurentWeaponLayer();   
        
        if(weaponClass != null) 
        {
            Destroy(weaponClass);
            Destroy(WeaponMagazine);
            WeaponEquiped = false;
        }

    }
    
    internal void Shoot(InputAction input = null)
    {
        if (weaponClass == null || weaponClass.WeaponInput == null) return; 
              
          weaponClass.WeaponInput.Shoot(input);
        
        if (weaponClass.FireTriggered)
        {
            cameraRecoil.SetVariables();  
       
            scriptFacade.componentManager.PlayerAnimator.SetTrigger(scriptFacade.playerAnimation.shoot);           
            weaponClass.FireTriggered = false;
        }                      
    }   
}