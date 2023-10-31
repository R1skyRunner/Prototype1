using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    #region // Rigs
    [SerializeField]
    internal Rig AimRig;

    [SerializeField]
    internal Rig LeftHandRig;

    #endregion
   
    [SerializeField] internal Transform LeftHand;

    private PlayerScriptFacade playerScriptFacade;

    private void Start()
    {
        playerScriptFacade = GetComponent<PlayerScriptFacade>();

        SetRigActive(LeftHandRig, playerScriptFacade.weaponHolder.WeaponEquiped && !playerScriptFacade.playerState.isReloading);
        SetRigActive(AimRig, playerScriptFacade.playerState.isAiming);
    }

    private void LateUpdate()
    {
        if(playerScriptFacade.playerState.isReloading || !playerScriptFacade.weaponHolder.WeaponEquiped)
        {
            SetLeftHandRig(0);          
        }
        else 
        { 
            SetLeftHandRig(1); 
        }
    }

    public void SetRigActive(Rig rig, bool state)
    {
        if (state)
        {
            rig.weight = 1f;
        }
        else 
        {
            rig.weight = 0f;
        }            
    }
    public void SetAimRig(float weight)
    {
        AimRig.weight = weight;
    }
    public void SetLeftHandRig(float weight)
    {
       LeftHandRig.weight = weight;
    }

}
